using BookStore.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookStore.Services
{
    public class EmailService : IEmailService
    {
        private const string TemplatePath = @"EmailTemplates/{0}.html";
        private SMTPConfigModel _configModel;

        public EmailService(IOptions<SMTPConfigModel> configModel)
        {
            _configModel = configModel.Value;
        }

        public async Task SendEmailWelcome(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Welcome {{USERNAME}} from book store team.", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("welcome"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        public async Task SendMailConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{USERNAME}}, follow to confirm email.", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("SendEmailConfirmation"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        public async Task SendPasswordResetEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Welcome {{USERNAME}}, click on a limk to reset your password.", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("ForgotPassword"), userEmailOptions.Placeholders);

            await SendEmailAsync(userEmailOptions);
        }

        private async Task SendEmailAsync(UserEmailOptions userEmailOptions)
        {
            // create mail message object
            MailMessage mailMessage = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_configModel.SenderAddress, _configModel.SenderDisplayName),
                IsBodyHtml = _configModel.IsBodyHTML
            };
            // assign To emails to mail message
            foreach (var to in userEmailOptions.To)
            {
                mailMessage.To.Add(to);
            }
            NetworkCredential credential = new NetworkCredential(_configModel.Username, _configModel.Password);
            // create smtp client object
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _configModel.Host,
                Port = _configModel.Port,
                EnableSsl = _configModel.EnableSsl,
                UseDefaultCredentials = _configModel.UseDefaultCredentials,
                Credentials = credential
            };

            mailMessage.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mailMessage);
        }

        private string GetEmailBody(string templateName)
        {
            return File.ReadAllText(string.Format(TemplatePath, templateName));
        }

        private string UpdatePlaceholders(string text, List<KeyValuePair<string, string>> placeholders)
        {
            if (!string.IsNullOrEmpty(text) && placeholders != null)
            {
                foreach (var placeholder in placeholders)
                {
                    if(text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
