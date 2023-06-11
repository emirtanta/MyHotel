using HotelProject.WebUI.Models.Mail;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HotelProject.WebUI.Controllers
{
    public class AdminMailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminMailVM model)
        {
            #region mail gönderme

            MimeMessage mimeMessage = new MimeMessage();

            //MailboxAddress mailboxAddressFrom = new MailboxAddress("HotelierAdmin", "mailAdresiniz");
            MailboxAddress mailboxAddressFrom = new MailboxAddress("HotelierAdmin", "emirtanta41@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", model.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder=new BodyBuilder();
            bodyBuilder.TextBody = model.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = model.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587,false); //false=>ssl'i kapatır
            //client.Authenticate("mailAdresiniz", "passwordKey");
            client.Authenticate("emirtanta41@gmail.com", "wkfwcfzqdhwfacer");
            client.Send(mimeMessage);
            client.Disconnect(true);
            
            #endregion

            return View();
        }
    }
}
