using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Barco.ModelViews.smtpConfig
{
    public class SMTPMailCommunication
    {
        // private NetworkCredential _networkCredentials { get; set; }
        private SmtpClient SmtpClient { get; set; }

        /// <summary>
        /// Helper for sending mail via SMTP
        /// </summary>
        /// <param name="userName">Userm, e.g. SendGrid 'apiKey', </param>
        /// <param name="SMTPPassword">Password, e.g. </param>
        /// <param name="SMTPHost">Password, e.g. </param>
        public SMTPMailCommunication(string userName, string SMTPPassword, string SMTPHost)
        {
            SmtpClient = new SmtpClient();

            //De sendgrid Relay server is een beveiligde server; dus er moet worden ingelogd
            //daarvoor zijn de credentials nodig om de gebruiker te authenticeren
            // deze credentials zijn de gebruikersnaam "apikey" en password de apikey
            SmtpClient.Credentials = new NetworkCredential(userName, SMTPPassword);
            // the SMTP Server we use to send our email
            SmtpClient.Host = SMTPHost;
            // TCP/IP port
            SmtpClient.Port = 587;
            // Use an SMTP server to send
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            // Send Grid best practice : use SSL
            SmtpClient.EnableSsl = true;
            // Use authentication
            SmtpClient.UseDefaultCredentials = false;
        }

   
       
        public MailMessage CreateMail(string toAdress, string htmlContent,
            string subject)
        {
            var from = new MailAddress("roy.decaestecker@vives.be", "R. Decaestecker - Vives");
            var to = new MailAddress(toAdress);

            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = htmlContent;
            message.IsBodyHtml = true;

            
            return message;
        }


        public MailResult SendMessage(MailMessage msg)
        {
            var result = new MailResult() { Status = MailSendingStatus.Ok };
            try
            {
                SmtpClient.Send(msg);
            }
            catch (Exception e)
            {
                result.Status = MailSendingStatus.HasError;
                result.Message = e.Message;
            }
            return result;
        }

     
    }
}
