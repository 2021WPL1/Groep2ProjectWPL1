using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail;
//using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Barco.ModelViews.smtpConfig
{
    public class SMTPMailCommunication
    {
        private SmtpClient SmtpClient { get; set; }

      
        public SMTPMailCommunication(string userName, string SMTPPassword, string SMTPHost)
        {
           
        }

   
       
        public void CreateMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("laurentdev89@outlook.be"));
            message.To.Add(new MailboxAddress("bianca.capatina@student.vives.be"));
            message.Subject = "idk";

            message.Body = new TextPart("plain")
            {
                Text = "hey bianca how are you"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, false);
                client.Authenticate("laurentdev89@outlook.be", "laurentDev");
                client.Send(message);
                client.Disconnect(true);
            }
        }


        /*public MailResult SendMessage(MailMessage msg)
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
        }*/

     
    }
}
