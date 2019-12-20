using BlazorWithSecutiry.DataAccess;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Service
{
    public class AuthenticationService //: AuthenticationStateProvider
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();

        public async Task<AuthenticationState> GetAuthenticationStateAsync(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);
            var test = new AuthenticationState(user);

            return await Task.FromResult(test);
        }

        public async Task<bool> AuthenticateAsync(string username)
        {
            return true;
        }
    }
}
