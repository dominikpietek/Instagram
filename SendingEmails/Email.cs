using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.SendingEmails
{
    public class Email
    {
        private const string _MAILFROM = "instagramemailsender@op.pl";
        private const string _PASSWORD = "password";
        private MailMessage _mail;
        private SmtpClient _smtp;

        public void Create(string mailTo, string subject, string body)
        {
            _smtp = new SmtpClient() { 
                Host= "smtp.poczta.onet.pl", 
                Port = 25, 
                Credentials = new NetworkCredential(_MAILFROM, _PASSWORD), 
                EnableSsl = true 
            };
            _mail = new MailMessage() { 
                From = new MailAddress(_MAILFROM),  
                Subject = subject, 
                Body = body
            };
            _mail.To.Add(new MailAddress(mailTo));
        }

        public async void SendAsync()
        {
            _smtp.Send(_mail);
        }
    }
}
