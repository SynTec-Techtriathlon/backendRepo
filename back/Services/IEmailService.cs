namespace Back.Services
{
    public interface IEmailService
    {
        void RejectUserMail(string to, string fullName);
        void ApproveUserMail(string to, string fullName);
    }
}
