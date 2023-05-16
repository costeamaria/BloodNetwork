using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;


namespace BloodNetwork.Pages
{
    public class ContactModel : PageModel
    {
        
        public void OnGet()
        {
         
        }

        public void OnPost()
        {
            var name = Request.Form["name"];
            var email = Request.Form["emailaddress"];
            var message = Request.Form["message"];
            SendMail(name, email, message);
               

        }

        public bool SendMail(string name, string email, string message1)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("costea.mari23@gmail.com");
            message.To.Add("costea.mari23@gmail.com"); 
            message.Subject = "Test email";
            message.IsBodyHtml = true;
            message.Body = "<p>Name: " + name + "</p><br>" + "<p>Email: " + email + "</p><br>" + "<p>Message: " + message1 + "</p>";

            smtpClient.Port = 587; 
            smtpClient.Host = "smtp.gmail.com"; 
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("costea.mari23@gmail.com","tqqxosmqoagxazrg");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            return true;
        }

    }
}