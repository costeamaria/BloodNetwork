using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;

namespace BloodNetwork.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string isSend { get; set; }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            var name = Request.Form["name"];
            var Email = Request.Form["emailaddress"];
            var message = Request.Form["message"];

            try
            {
                SendMail(name, Email, message);
                isSend = "send";
            }
            catch(Exception)
            {
                isSend = "failed";
            }
            
        }
        public bool SendMail(string name, string Email, string message1)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("mariacostea@gmail.com");
            message.To.Add("costea.mari23@gmail.com");
            message.Subject = "Test email";
            message.IsBodyHtml = true;
            message.Body = "<p>" + name + "</p>" + "<p>Email: " + Email + "</p>" + "<p>Message: " + message + "</p>";

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("costea.mari23@gmail.com", "Password here");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            return true;
        }
    }
}