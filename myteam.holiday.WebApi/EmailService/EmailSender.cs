using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace myteam.holiday.WebApi.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendVerifyTokenAsync(string emailBody, string emailAddress)
        {
            var email = new MimeMessage();

            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.From.Add(MailboxAddress.Parse(_config["EmailConfig:EmailAddress"]));
            email.Subject = "Confirm your account";
            email.Body = new TextPart(TextFormat.Html) 
            {
                Text = "<p>Click to verify:</p>" +
                       $"<p><a href=\"{emailBody}\">bla bla verify</a></p>"               
            };
            
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailConfig:EmailHost"], 465, true);
            await smtp.AuthenticateAsync(_config["EmailConfig:EmailAddress"], _config["EmailConfig:EmailPassword"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
