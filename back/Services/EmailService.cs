using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Back.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        

        public void RejectUserMail(string to, string fname, string lname)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "VISA Rejected";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<html><body><h3>{fname} {lname},</h3><p>We are sorry to inform that your VISA application has been denied.</p></body></html>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailConfig:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailConfig:EmailUsername").Value, _config.GetSection("EmailConfig:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void ApproveUserMail(string to, string fname, string lname)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "VISA Approved";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<html><body><h3>{fname} {lname},</h3><p>We are happy to inform that your VISA application has been approved.</p></body></html>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailConfig:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailConfig:EmailUsername").Value, _config.GetSection("EmailConfig:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}
