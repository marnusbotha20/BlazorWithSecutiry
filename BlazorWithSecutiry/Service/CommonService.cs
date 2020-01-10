using BlazorWithSecutiry.DataAccess;
using BlazorWithSecutiry.Models;
using Microsoft.AspNetCore.Blazor.Components;
using System.Net.Mail;

namespace BlazorWithSecutiry.Service
{
    public class CommonService : BlazorComponent
    {
        CommonDAL common = new CommonDAL();
        public void Create(ContactUsDetails model)
        {
            common.AddContactUs(model);
            //SendContactUsEmail(model);
        }
        public void SendContactUsEmail(ContactUsDetails model)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(model.EmailAddress);
            mail.To.Add("planetthorndesigns@gmail.com");
            mail.Subject = model.Subject;
            mail.Body = model.Messgae;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("planetthorndesigns@gmail.com", "M@rnus2007");
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Send(mail);
        }
    }
}
