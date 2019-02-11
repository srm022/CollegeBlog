using Blog.Infrastructure;
using Blog.Models.Contact;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Blog.Services
{
    public interface IContactService
    {
        void SendMail(ContactFormModel model);
    }

    public class ContactService : IContactService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public ContactService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendMail(ContactFormModel model)
        {
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress("michal.sur96@gmail.com"));
            message.From.Add(new MailboxAddress(model.SenderName, model.SenderMail));

            message.Subject = model.Subject;
            message.Body = new TextPart(TextFormat.Text)
            {
                Text = model.Content
            };

            using (var emailClient = new SmtpClient())
            {
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                emailClient.Send(message);

                emailClient.Disconnect(true);
            }
        }
    }
}