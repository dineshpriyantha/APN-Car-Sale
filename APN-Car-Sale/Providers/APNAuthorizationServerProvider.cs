using APNCarSaleDataService.Interfaces;
using APNCarSaleDataService.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace APN_Car_Sale.Providers
{
    public class APNAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private string baseUrl = "http://localhost:1134/";
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = ValidateUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoles));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("Email", user.UserEmailID));

            context.Validated(identity);

        }

        public APN_UserMaster ValidateUser(string username, string password)
        {
            IEnumerable<APN_UserMaster> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Concat(baseUrl, "api/UserMaster"));

                var responseTask = client.GetAsync("UserMaster");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<APN_UserMaster>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
            }

            var validuser = users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && u.UserPassword == password);

            return validuser;
        }
    }

}