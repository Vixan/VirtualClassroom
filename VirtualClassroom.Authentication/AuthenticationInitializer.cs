using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VirtualClassroom.Authentication.Data;
using VirtualClassroom.CommonAbstractions;
using System.Security.Claims;

namespace VirtualClassroom.Authentication
{
    public class AuthenticationInitializer : IAuthentication
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

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

        #region AccesUserData
        private ApplicationUser GetUser(ClaimsPrincipal user)
        {
            Task<ApplicationUser> applicationUser = userManager.GetUserAsync(user);
            applicationUser.Wait();

            return applicationUser.Result;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            return userData.Result.Id;
        }

        public string GetUserName(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            return userData.Result.UserName;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            return userData.Result.Email;
        }

        public string GetUserPhoneNumber(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            return userData.Result.PhoneNumber;
        }

        public bool IsUserEmailConfirmed(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            return userData.Result.EmailConfirmed;
        }

        void SetUserId(ClaimsPrincipal user, string id)
        {
            ApplicationUser applicationUser = GetUser(user);

            applicationUser.Id = id;
        }

        void SetUserName(ClaimsPrincipal user, string userName)
        {
            ApplicationUser applicationUser = GetUser(user);

            applicationUser.UserName = userName;
        }

        void SetUserEmail(ClaimsPrincipal user, string email)
        {
            ApplicationUser applicationUser = GetUser(user);

            applicationUser.Email = email;
        }

        void SetUserPhoneNumber(ClaimsPrincipal user, string phoneNumber)
        {
            ApplicationUser applicationUser = GetUser(user);

            applicationUser.PhoneNumber = phoneNumber;
        }
        #endregion

        #region VerifyUserRoles
        public bool IsProfessor(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            Task<bool> isProfessor = userManager.IsInRoleAsync(userData.Result, "Professor");
            isProfessor.Wait();

            return isProfessor.Result;
        }

        public bool IsStudent(ClaimsPrincipal user)
        {
            Task<ApplicationUser> userData = userManager.GetUserAsync(user);
            userData.Wait();

            Task<bool> isStudent = userManager.IsInRoleAsync(userData.Result, "Student");
            isStudent.Wait();

            return isStudent.Result;
        }
        #endregion

        #region AuthenticationMethods
        public bool Login(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            Task<SignInResult> signInResult = signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);
            signInResult.Wait();

            return signInResult.Result.Succeeded;
        }

        public bool Register(string email, string password)
        {
            ApplicationUser newUser = new ApplicationUser { UserName = email, Email = email };
            Task<IdentityResult> identityResult = userManager.CreateAsync(newUser, password);
            identityResult.Wait();

            signInManager.SignInAsync(newUser, isPersistent: false).Wait();

            return identityResult.Result.Succeeded;
        }

        public void Logout()
        {
            signInManager.SignOutAsync().Wait();
        }


        public string GenerateEmailConfirmationToken(ClaimsPrincipal user)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<string> code = userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            code.Wait();

            return code.Result;
        }

        bool ConfirmEmail(ClaimsPrincipal user, string code)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> result = userManager.ConfirmEmailAsync(applicationUser, code);
            result.Wait();

            return result.Result.Succeeded;
        }

        string GeneratePasswordResetToken(ClaimsPrincipal user)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<string> passwordResetToken = userManager.GeneratePasswordResetTokenAsync(applicationUser);
            passwordResetToken.Wait();

            return passwordResetToken.Result;
        }

        bool ResetPassword(ClaimsPrincipal user, string code, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> result = userManager.ResetPasswordAsync(applicationUser, code, newPassword);
            result.Wait();

            return result.Result.Succeeded;
        }


        bool HasPassword(ClaimsPrincipal user)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<bool> result = userManager.HasPasswordAsync(applicationUser);
            result.Wait();

            return result.Result;
        }

        bool ChangedPassword(ClaimsPrincipal user, string oldPassword, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> result = userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
            result.Wait();

            signInManager.SignInAsync(applicationUser, isPersistent: false).Wait();

            return result.Result.Succeeded;
        }

        bool AddPassword(ClaimsPrincipal user, string newPassword)
        {
            ApplicationUser applicationUser = GetUser(user);

            Task<IdentityResult> result = userManager.AddPasswordAsync(applicationUser, newPassword);
            result.Wait();

            signInManager.SignInAsync(applicationUser, isPersistent: false).Wait();

            return result.Result.Succeeded;
        }
        #endregion

        #region AuthenticationInitializations
        private void InitializeManagers(IServiceProvider serviceProvider)
        {
            if (userManager == null)
                userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (signInManager == null)
                signInManager = serviceProvider.GetService<SignInManager<ApplicationUser>>();
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
    }
}
