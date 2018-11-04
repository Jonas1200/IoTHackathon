using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RaspBier.Helper
{
    public static class MailHelper
    {
        public static void SendMail(string subject, string body)
        {
            var settings = SettingsHelper.Settings;

            var client = new SmtpClient(settings.MailServer)
            {
                UseDefaultCredentials = false,
                Port = settings.MailPort,
                EnableSsl = true
            };

            client.Credentials = new NetworkCredential(settings.MailHost, settings.MailHostPassword);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(settings.MailHost)
            };

            mailMessage.To.Add(settings.MailReceiver);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            client.Send(mailMessage);
        }
    }
}
