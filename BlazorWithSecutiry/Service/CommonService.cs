using BlazorWithSecutiry.Data;
using BlazorWithSecutiry.DataAccess;
using BlazorWithSecutiry.Models;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.AspNetCore.Blazor.Components;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Service
{
    public class CommonService //: BlazorComponent
    {
        CommonDAL common = new CommonDAL();
        public void Create(ContactUsDetails model)
        {
            common.AddContactUs(model);
            SendContactUsEmail(model);
        }
        public void SendContactUsEmail(ContactUsDetails model)
        {
            var fromAddress = new MailAddress(model.EmailAddress, model.Name);
            var toAddress = new MailAddress("planetthorndesigns@gmail.com", "Planetthorn Designs");
            const string fromPassword = "M@rnus2007";
            const string subject = "Subject";
            const string body = "Body";

            MailMessage mail = new MailMessage();


            mail.From = new MailAddress(model.EmailAddress);
            mail.To.Add("planetthorndesigns@gmail.com");
            mail.Subject = model.Subject;
            mail.Body = model.Message;

            SmtpClient SmtpServer = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                SmtpServer.Send(message);
            }
        }
    }
}
