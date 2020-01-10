using BlazorWithSecutiry.Data;
using BlazorWithSecutiry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.DataAccess
{
    public class CommonDAL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void AddContactUs(ContactUsDetails model)
        {
            try
            {
                db.ContactUsDetails.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
