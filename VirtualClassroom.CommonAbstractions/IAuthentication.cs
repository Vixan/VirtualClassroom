using System.Collections.Generic;
using System.Security.Claims;

namespace VirtualClassroom.CommonAbstractions
{
    public interface IAuthentication : IInitializer
    {
        // GetUser
        UserData GetUserByAssociatedUser(ClaimsPrincipal user);
        UserData GetUserById(string userId);
        UserData GetUserByUserName(string userName);
        UserData GetUserByEmail(string email);

        // AccesUserData
        string GetUserId(ClaimsPrincipal user);
        string GetUserName(ClaimsPrincipal user);
        string GetUserEmail(ClaimsPrincipal user);
        string GetUserPhoneNumber(ClaimsPrincipal user);

        AuthResult SetUserId(ClaimsPrincipal user, string id);
        AuthResult SetUserName(ClaimsPrincipal user, string userName);
        AuthResult SetUserEmail(ClaimsPrincipal user, string email);
        AuthResult SetUserPhoneNumber(ClaimsPrincipal user, string phoneNumber);

        bool IsUserEmailConfirmed(ClaimsPrincipal user);
        bool IsUserEmailConfirmed(UserData user);

        // VerifyUserRoles
        IEnumerable<string> GetRoles();
        IEnumerable<string> GetUserRoles(ClaimsPrincipal user);
        bool IsProfessor(ClaimsPrincipal user);
        bool IsStudent(ClaimsPrincipal user);

        // AuthenticationMethods
        bool Login(string username, string password, bool rememberMe, bool lockoutOnFailure);
        AuthResult Register(UserData userData, string password, string role);
        void Logout();

        string GenerateEmailConfirmationToken(UserData user);
        AuthResult ConfirmEmail(ClaimsPrincipal user, string code);
        AuthResult ConfirmEmail(string userId, string code);
        string GeneratePasswordResetToken(UserData user);
        AuthResult ResetPassword(UserData user, string code, string newPassword);

        bool HasPassword(ClaimsPrincipal user);
        AuthResult ChangedPassword(ClaimsPrincipal user, string oldPassword, string newPassword);
        AuthResult AddPassword(ClaimsPrincipal user, string newPassword);

        // Cookie
        string GetExternalScheme();
    }
}
