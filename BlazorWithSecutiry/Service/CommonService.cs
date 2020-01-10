using BlazorWithSecutiry.DataAccess;
using BlazorWithSecutiry.Models;
using Microsoft.AspNetCore.Blazor.Components;


namespace BlazorWithSecutiry.Service
{
    public class CommonService : BlazorComponent
    {
        CommonDAL common = new CommonDAL();
        public void Create(ContactUsDetails model)
        {
            common.AddContactUs(model);
        }
    }
}
