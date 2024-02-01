using Rest.DTO;
using Rest.Entity;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace ProjetoBack.Infrastructure
{
        public class Email
        {
            public string Provedor { get; private set; }
            public string Username { get; private set; }
            public string Password { get; private set; }

            public Email(string provedor, string username, string password)
            {
                Provedor = provedor;
                Username = username;
                Password = password;
            }

            public void SendEmail(List<string> emailsTo, string subject, string body)
            {
                var message = PrepareteMessage(emailsTo, subject, body);

                SendEmailBySmtp(message);
            }

            private MailMessage PrepareteMessage(List<string> emailsTo, string subject, string body)
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(Username);

                foreach (var email in emailsTo)
                {
                    if (ValidateEmail(email))
                    {
                        mail.To.Add(email);
                    }
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;              

                return mail;
            }

            private bool ValidateEmail(string email)
            {
                Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
                if (expression.IsMatch(email))
                    return true;

                return false;
            }

            private void SendEmailBySmtp(MailMessage message)
            {
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
                smtpClient.Host = Provedor;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;           
                smtpClient.Timeout = 50000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(Username, Password);
                smtpClient.Send(message);
                smtpClient.Dispose();
            }

    }
}
