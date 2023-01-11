using Microsoft.Extensions.Options;
using MimeKit;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Models.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using MailKit.Net.Smtp;
using MimeKit;

namespace PauliTicket.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);

            sendGridMessage.SetClickTracking(false, false);
            sendGridMessage.SetOpenTracking(false);
            sendGridMessage.SetGoogleAnalytics(false);
            sendGridMessage.SetSubscriptionTracking(false);

            var response = await client.SendEmailAsync(sendGridMessage);

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public void SendRegisterEmail(string email, string ConfirmationLink)
        {
            MimeMessage msg = new MimeMessage();

            msg.From.Add(new MailboxAddress("PauliTicket", "gestionescolar1234@gmail.com"));

            msg.To.Add(MailboxAddress.Parse(email));

            msg.Subject = "Email Confirmation";

            msg.Body = new TextPart("plain")
            {
                Text = $@"Hi, follow this link to confirm your account:
                       {ConfirmationLink}
                If you don't confirm your account, you will not be able to log in into the web,
                Regards!,
                PauliTicket"
            };

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("gestionescolar1234@gmail.com", "gestionescolar1901");
                client.Send(msg);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
