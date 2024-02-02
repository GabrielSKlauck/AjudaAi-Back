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
        //COMO USAR A CLASSE
        //Instancie um objeto
        //Email email = new Email("help.ajudaai.suporte@gmail.com", "GGJSNAjudaAi123");

        //chame o metodo
        //email.SendEmail(new List<string> { "email alvo" }, "titulo", "corpo");

            public string Provedor { get; private set; }
            public string Username { get; private set; }
            public string Password { get; private set; }

            public Email()
            {
                Provedor = "smtp.gmail.com";
                Username = "help.ajudaai.suporte@gmail.com";
                Password = "GGJSNAjudaAi123";
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
            {
                return true;
            }
            else
            {
                return false;
            }
                    

                
            }

            private void SendEmailBySmtp(MailMessage message)
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Host = Provedor;
                smtpClient.Port = 587;             
                smtpClient.EnableSsl = true;           
                smtpClient.Timeout = 50000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(Username, "crms vwpo igix bwmc");
                smtpClient.Send(message);
                smtpClient.Dispose();
            }

    }
}
