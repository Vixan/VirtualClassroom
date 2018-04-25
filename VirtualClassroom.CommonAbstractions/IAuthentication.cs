namespace VirtualClassroom.CommonAbstractions
{
    public interface IAuthentication : IInitializer
    {
        int GetUserId();
        string GetUserName();
        string GetUserEmail();

        bool IsProfessor();
        bool IsStudent();

        void Login();
        void Logout();
        void Register();
    }
}
