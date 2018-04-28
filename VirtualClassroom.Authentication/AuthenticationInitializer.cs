using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VirtualClassroom.Authentication.Data;
using VirtualClassroom.CommonAbstractions;
using System.Security.Claims;
using System.Collections.Generic;

namespace VirtualClassroom.Authentication
{
    public class AuthenticationInitializer : IAuthentication
    {
        private static UserManager<ApplicationUser> userManager = null;
        private static SignInManager<ApplicationUser> signInManager = null;
        private static RoleManager<IdentityRole> roleManager = null;

        #region IInitializer
        public void InitializeContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AuthConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            InitializeManagers(services.BuildServiceProvider());
        }

        public void InitializeData(IServiceProvider serviceProvider)
        {
            CreateRoles(serviceProvider);
        }
        #endregion

        #region GetUser

        private ApplicationUser GetUser(ClaimsPrincipal user)
        {
            if (user == null)
                return null;

            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            return applicationUser.Result;
        }

        private ApplicationUser GetUser(UserData user)
        {
            Task<ApplicationUser> applicationUser = null;

            if (user.Id != null)
                applicationUser = userManager.FindByIdAsync(user.Id);
            else if (user.UserName != null)
                applicationUser = userManager.FindByNameAsync(user.UserName);
            else if (user.Email != null)
                applicationUser = userManager.FindByEmailAsync(user.Email);

            if (applicationUser == null)
                return null;

            applicationUser.Wait();
            return applicationUser.Result;
        }

        public UserData GetUserByAssociatedUser(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return MapApplicationUserToUserData(applicationUser.Result);
        }

        public UserData GetUserById(string userId)
        {
            Task<ApplicationUser> applicationUser = userManager.FindByIdAsync(userId);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return MapApplicationUserToUserData(applicationUser.Result);
        }

        public UserData GetUserByUserName(string userName)
        {
            Task<ApplicationUser> applicationUser = userManager.FindByNameAsync(userName);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return MapApplicationUserToUserData(applicationUser.Result);
        }

        public UserData GetUserByEmail(string email)
        {
            Task<ApplicationUser> applicationUser = userManager.FindByEmailAsync(email);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return MapApplicationUserToUserData(applicationUser.Result);
        }

        #endregion

        #region AccesUserData

        public string GetUserId(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return applicationUser.Result.Id;
        }

        public string GetUserName(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return applicationUser.Result.UserName;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return applicationUser.Result.Email;
        }

        public string GetUserPhoneNumber(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return null;

            return applicationUser.Result.PhoneNumber;
        }

        public AuthResult SetUserId(ClaimsPrincipal user, string id)
        {
            ApplicationUser applicationUser = GetUser(user);
            if (applicationUser != null)
                applicationUser.Id = id;

            Task<IdentityResult> updateUser = userManager.UpdateAsync(applicationUser);
            updateUser.Wait();

            return MapIdentityResultToAuth(updateUser.Result);
        }

        public AuthResult SetUserName(ClaimsPrincipal user, string userName)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> setUserNameResult = userManager.SetUserNameAsync(applicationUser, userName);
            setUserNameResult.Wait();

            return MapIdentityResultToAuth(setUserNameResult.Result);
        }

        public AuthResult SetUserEmail(ClaimsPrincipal user, string email)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> setUserEmailResult = userManager.SetEmailAsync(applicationUser, email);
            setUserEmailResult.Wait();

            return MapIdentityResultToAuth(setUserEmailResult.Result);
        }

        public AuthResult SetUserPhoneNumber(ClaimsPrincipal user, string phoneNumber)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> setUserPhoneResult = userManager.SetPhoneNumberAsync(applicationUser, phoneNumber);
            setUserPhoneResult.Wait();

            return MapIdentityResultToAuth(setUserPhoneResult.Result);
        }

        public bool IsUserEmailConfirmed(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return false;

            return applicationUser.Result.EmailConfirmed;
        }

        public bool IsUserEmailConfirmed(UserData user)
        {
            Task<ApplicationUser> applicationUser = userManager.FindByEmailAsync(user.Email);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return false;

            return applicationUser.Result.EmailConfirmed;
        }
        #endregion

        #region VerifyUserRoles

        public IEnumerable<string> GetRoles()
        {
            var roles = roleManager.Roles;
            List<string> roleList = new List<string>();

            foreach(var role in roles)
                roleList.Add(role.Name);

            return roleList;
        }

        public IEnumerable<string> GetUserRoles(ClaimsPrincipal user)
        {
            List<string> userRoles = new List<string>();

            if (user == null)
                return userRoles;

            var getRolesTask = userManager.GetRolesAsync(GetUser(user));
            getRolesTask.Wait();

            if(getRolesTask != null)
            {
                userRoles = (List<string>)getRolesTask.Result;
            }

            return userRoles;
        }

        public bool IsProfessor(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return false;

            Task<bool> isProfessor = userManager.IsInRoleAsync(applicationUser.Result, "Professor");
            isProfessor.Wait();

            return isProfessor.Result;
        }

        public bool IsStudent(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            if (applicationUser.Result == null)
                return false;

            Task<bool> isStudent = userManager.IsInRoleAsync(applicationUser.Result, "Student");
            isStudent.Wait();

            return isStudent.Result;
        }
        #endregion

        #region AuthenticationMethods
        public bool Login(string username, string password, bool rememberMe = false, bool lockoutOnFailure = false)
        {
            if (username == null || password == null)
                return false;

            Task<SignInResult> signInResult = signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure);
            signInResult.Wait();

            return signInResult.Result.Succeeded;
        }

        public AuthResult Register(string username, string email, string password, string role)
        {
            if (username == null || password == null)
                return new AuthResult { Succeded = false };

            ApplicationUser newUser = new ApplicationUser { UserName = username, Email = email };
            Task<IdentityResult> identityResult = userManager.CreateAsync(newUser, password);
            identityResult.Wait();

            userManager.AddToRoleAsync(newUser, role).Wait();
            signInManager.SignInAsync(newUser, isPersistent: false).Wait();

            return MapIdentityResultToAuth(identityResult.Result);
        }

        public void Logout()
        {
            signInManager.SignOutAsync().Wait();
        }


        public string GenerateEmailConfirmationToken(UserData user)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return null;

            Task<string> code = userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            code.Wait();

            return code.Result;
        }

        public AuthResult ConfirmEmail(ClaimsPrincipal user, string code)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return new AuthResult { Succeded = false };

            Task<IdentityResult> result = userManager.ConfirmEmailAsync(applicationUser, code);
            result.Wait();

            return MapIdentityResultToAuth(result.Result);
        }

        public AuthResult ConfirmEmail(string userId, string code)
        {
            Task<ApplicationUser> applicationUser = userManager.FindByIdAsync(userId);
            applicationUser.Wait();

            if (applicationUser == null)
                return new AuthResult { Succeded = false };

            Task<IdentityResult> result = userManager.ConfirmEmailAsync(applicationUser.Result, code);
            result.Wait();

            return MapIdentityResultToAuth(result.Result);
        }

        public string GeneratePasswordResetToken(UserData user)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return null;

            Task<string> passwordResetToken = userManager.GeneratePasswordResetTokenAsync(applicationUser);
            passwordResetToken.Wait();

            return passwordResetToken.Result;
        }

        public AuthResult ResetPassword(UserData user, string code, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return new AuthResult { Succeded = false };

            Task<IdentityResult> result = userManager.ResetPasswordAsync(applicationUser, code, newPassword);
            result.Wait();

            return MapIdentityResultToAuth(result.Result);
        }


        public bool HasPassword(ClaimsPrincipal user)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return false;

            Task<bool> result = userManager.HasPasswordAsync(applicationUser);
            result.Wait();

            return result.Result;
        }

        public AuthResult ChangedPassword(ClaimsPrincipal user, string oldPassword, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return new AuthResult { Succeded = false };

            Task<IdentityResult> result = userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
            result.Wait();

            signInManager.SignInAsync(applicationUser, isPersistent: false).Wait();

            return MapIdentityResultToAuth(result.Result);
        }

        public AuthResult AddPassword(ClaimsPrincipal user, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            if (applicationUser == null)
                return new AuthResult { Succeded = false };

            Task<IdentityResult> result = userManager.AddPasswordAsync(applicationUser, newPassword);
            result.Wait();

            signInManager.SignInAsync(applicationUser, isPersistent: false).Wait();

            return MapIdentityResultToAuth(result.Result);
        }
        #endregion

        #region AuthenticationInitializations
        private void InitializeManagers(IServiceProvider serviceProvider)
        {
            if (userManager == null)
                userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (signInManager == null)
                signInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();

            if (roleManager == null)
                roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            Task<IdentityResult> roleResult;

            Task<bool> hasProfessorRole = roleManager.RoleExistsAsync("Professor");
            hasProfessorRole.Wait();

            if (!hasProfessorRole.Result)
            {
                IdentityRole professorRole = new IdentityRole()
                {
                    Name = "Professor"
                };

                roleResult = roleManager.CreateAsync(professorRole);
                roleResult.Wait();
            }

            Task<bool> hasStudentsRole = roleManager.RoleExistsAsync("Student");
            hasStudentsRole.Wait();

            if (!hasStudentsRole.Result)
            {
                IdentityRole studentRole = new IdentityRole()
                {
                    Name = "Student"
                };

                roleResult = roleManager.CreateAsync(studentRole);
                roleResult.Wait();
            }
        }
        #endregion

        #region Helpers
        private UserData MapApplicationUserToUserData(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;

            return new UserData { Id = applicationUser.Id, UserName = applicationUser.UserName, Email = applicationUser.Email, PhoneNumber = applicationUser.PhoneNumber };
        }

        private AuthResult MapIdentityResultToAuth(IdentityResult identityResult)
        {
            if (identityResult == null)
                return null;

            List<string> errors = new List<string>();
            foreach(var error in identityResult.Errors)
            {
                errors.Add(error.Description);
            }

            return new AuthResult { Succeded = identityResult.Succeeded, Errors = errors };
        }
        #endregion

        #region Cookie
        public string GetExternalScheme()
        {
            return IdentityConstants.ExternalScheme;
        }
        #endregion
    }
}
