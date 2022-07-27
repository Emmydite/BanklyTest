using BanklyTest.Interface;
using BanklyTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BanklyTest.Services
{
    public class EmailService : IEmailSender
    {
        private readonly EmailConfig _config;
        public EmailService(EmailConfig emailConfig)
        {
            _config = emailConfig;
        }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                var credentials = new NetworkCredential(_config.Sender, _config.Password);

                var mail = new MailMessage()
                {
                    From = new MailAddress(_config.Sender),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Host = _config.MailServer,
                    Port = _config.MailPort,
                    Credentials = credentials,
                    EnableSsl = true
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string FormatEmailBody(User user)
        {
            try
            {
                string body = string.Empty;

                var getTemplatePath = GetTemplateType(user.Channel);

                var htmlContent = Nustache.Core.Render.FileToString(getTemplatePath, user);

                return htmlContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTemplateType(string template)
        {
            string path = string.Empty;

            if(template.ToLower() == "web")
            {
                path = Path.GetFullPath("~/EmailTemplates/WelcomeWeb.html").Replace("~\\", "");
            }
            else if (template.ToLower() == "mobile")
            {
                path = Path.GetFullPath("~/EmailTemplates/WelcomeMobile.html").Replace("~\\", "");
            }

            return path;
        }

    }
}
