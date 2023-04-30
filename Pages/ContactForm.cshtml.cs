using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using GSF.Net.Smtp;

namespace BloodNetwork.Pages
{
    public class ContactModel : PageModel
    {
        public string isSend { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var name = Request.Form["name"];
            var email = Request.Form["emailaddress"];
            var message = Request.Form["message"];
            SendMail(name, email, message);
            try
            {
                SendMail(name, email, message);
                isSend = "send";
            }
            catch (Exception)
            {
                isSend = "failed";
            }

        }

        public bool SendMail(string name, string email, string message1)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("bloodnetwork@gmail.com");
            message.To.Add(new MailAddress("costea.mari23@gmail.com")); 
            string st = "<p>Name: " + name + "</p" + "<p>Email: " + email + "</p" + "<p>Message: " + message1 + "</p";
            message.IsBodyHtml = true;
            message.Body = st;

            smtpClient.Port = 587; 
            smtpClient.Host = "costea.mari23@gmail.com"; 
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("bloodnetwork@gmail.com","Password here");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //
            smtpClient.Send(message);
            return true;
        }
    }
}