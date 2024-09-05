namespace Back.Services
{
    public interface IEmailService
    {
        void RejectUserMail(string to, string fname, string lname);
        void ApproveUserMail(string to, string fname, string lname);
    }
}
