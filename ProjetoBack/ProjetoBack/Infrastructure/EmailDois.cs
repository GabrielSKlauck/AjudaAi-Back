using Rest.DTO;
using Rest.Entity;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace ProjetoBack.Infrastructure
{
    public class EmailDois
    {
        public static void SendEmail(string origem, string destino, string assunto, string enviaMensagem)
        {
            try
            {
                // valida o email
                bool bValidaEmail = ValidateEmail(destino);

                // Se o email não é validao retorna uma mensagem
                if (bValidaEmail)
                {
                    
                    MailMessage mensagemEmail = new MailMessage(origem, destino, assunto, enviaMensagem);

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential("ajudaai.suporte@outlook.com", "GGJSNAjudaAi123");
                    client.Credentials = cred;

                    
                    client.UseDefaultCredentials = true;

                    
                    client.Send(mensagemEmail);
                    Console.WriteLine("Mensagem enviada para  " + origem + " às " + DateTime.Now.ToString() + ".");
                }              
            }
            catch (Exception ex)
            {
                
            }
        }

        private static bool ValidateEmail(string email)
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }    

    }
}
