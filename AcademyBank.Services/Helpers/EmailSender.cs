using AcademyBank.BLL.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace AcademyBank.BLL.Helpers
{
    public class EmailSender
    {
        public void SendEmail(string email, string subject, string body)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = Constants.mailHost;
            client.Port = 587;

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Constants.webAddress, Constants.password);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(Constants.webAddress);
            msg.To.Add(new MailAddress(email));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            client.Send(msg);
        }
    }
}
