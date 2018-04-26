using System.Security.Claims;

namespace VirtualClassroom.CommonAbstractions
{
    public interface IAuthentication : IInitializer
    {
        // GetUser
        UserData GetUserById(string userId);
        UserData GetUserByUserName(string userName);
        UserData GetUserByEmail(string email);

        // AccesUserData
        string GetUserId(ClaimsPrincipal user);
        string GetUserName(ClaimsPrincipal user);
        string GetUserEmail(ClaimsPrincipal user);
        string GetUserPhoneNumber(ClaimsPrincipal user);

        void SetUserId(ClaimsPrincipal user, string id);
        void SetUserName(ClaimsPrincipal user, string userName);
        void SetUserEmail(ClaimsPrincipal user, string email);
        void SetUserPhoneNumber(ClaimsPrincipal user, string phoneNumber);

        bool IsUserEmailConfirmed(ClaimsPrincipal user);
        bool IsUserEmailConfirmed(UserData user);

        // VerifyUserRoles
        bool IsProfessor(ClaimsPrincipal user);
        bool IsStudent(ClaimsPrincipal user);

        // AuthenticationMethods
        bool Login(string email, string password, bool rememberMe, bool lockoutOnFailure);
        bool Register(string email, string password);
        void Logout();

        // -> UserData user
        string GenerateEmailConfirmationToken(ClaimsPrincipal user);
        bool ConfirmEmail(ClaimsPrincipal user, string code);
        bool ConfirmEmail(string userId, string code);
        // -> UserData user
        string GeneratePasswordResetToken(ClaimsPrincipal user);
        bool ResetPassword(ClaimsPrincipal user, string code, string newPassword);

        bool HasPassword(ClaimsPrincipal user);
        bool ChangedPassword(ClaimsPrincipal user, string oldPassword, string newPassword);
        bool AddPassword(ClaimsPrincipal user, string newPassword);
    }
}
