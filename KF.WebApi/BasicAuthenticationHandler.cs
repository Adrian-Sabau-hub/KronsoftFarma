using KF.CommonModel.Models;
using KF.Core.Data;
using KF.Core.DomainModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace KF.Web.API
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IRepository<User> usersRepository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IRepository<User> usersRepository) : base(options, logger, encoder, clock)
        {
            this.usersRepository = usersRepository;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No header found");
            }

            var autHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(autHeader.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if(!string.IsNullOrEmpty(credentials))
            {
                string[] array= credentials.Split(":");
                string username = array[0];
                string password = array[1];

                //database check
                var user = usersRepository.Table.FirstOrDefault(x => x.Username == username && x.Password == password);
                if(user is null)
                {
                    return AuthenticateResult.Fail("UnAutorized");

                }
                

                // Generate ticket
                var claim = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claim,Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal,Scheme.Name);

                return AuthenticateResult.Success(ticket);
            } else
            {
                return AuthenticateResult.Fail("No header found");
            }
        }
    }
}
