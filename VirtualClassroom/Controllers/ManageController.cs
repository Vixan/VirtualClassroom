using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VirtualClassroom.Authentication.Services;
using VirtualClassroom.CommonAbstractions;
using VirtualClassroom.Models.ManageViewModels;
using VirtualClassroom.Services;

namespace VirtualClassroom.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          IAuthentication authentication,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _authentication = authentication;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = _authentication.IsUserEmailConfirmed(user),
                Roles = _authentication.GetUserRoles(User),
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                AuthResult setEmailResult = _authentication.SetUserEmail(User, model.Email);
                if (!setEmailResult.Succeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                AuthResult setPhoneResult = _authentication.SetUserPhoneNumber(User, model.PhoneNumber);
                if (!setPhoneResult.Succeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }
            
            string code = _authentication.GenerateEmailConfirmationToken(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }
            
            bool hasPassword = _authentication.HasPassword(User);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }
            
            AuthResult changePasswordResult = _authentication.ChangedPassword(User, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }
            
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public IActionResult SetPassword()
        {
            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }
            
            bool hasPassword = _authentication.HasPassword(User);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_authentication.GetUserId(User)}'.");
            }
            
            AuthResult addPasswordResult = _authentication.AddPassword(User, model.NewPassword);
            if (!addPasswordResult.Succeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }
            
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        #region Helpers
        
        private void AddErrors(AuthResult results)
        {
            foreach (var error in results.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenicatorUriFormat,
                _urlEncoder.Encode("VirtualClassroom"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        #endregion
    }
}
