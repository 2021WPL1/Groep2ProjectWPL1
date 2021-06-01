using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using System.Net.Mail;
//using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Barco.ModelViews.Settings;
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

        private static AppSettingsService<AppSettings> _appSettingsService = AppSettingsService<AppSettings>.Instance;



        private static SMTPMailCommunication smtpMailCommunication { get; set; }


        public SMTPMailCommunication(string userName, string SMTPPassword, string SMTPHost)
        {
           
        }


        //method to create an email 
        // Laurent ,Bianca

        public void CreateMail(string input)
        {

            var to = _appSettingsService.GetConfigurationSection<EmailAdresses>("EmailAdresses");
            var from = _appSettingsService.GetConfigurationSection<SMPTClientConfig>("SMPTClientConfig");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from.QueryResult.Username));
            message.To.Add(new MailboxAddress(to.QueryResult.Address2));
            message.Subject = "New Request";

            message.Body = new TextPart("plain")
            {
                Text = "there are " + input + " new requests!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, false);
                client.Authenticate(from.QueryResult.Username, from.QueryResult.SMTPPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }



    }
}
