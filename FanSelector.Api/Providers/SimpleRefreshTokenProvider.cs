using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FanSelector.Data.Auth;
using FanSelector.Models.Db;
using Microsoft.Owin.Security.Infrastructure;

namespace FanSelector.Api.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (var repo = new AuthRepository())
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken()
                {
                    Id = Helper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await repo.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {allowedOrigin});

            var hashedTokenId = Helper.GetHash(context.Token);

            using (var repo = new AuthRepository())
            {
                var refreshToken = await repo.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    await repo.RemoveRefreshToken(hashedTokenId);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHash(string input)
        {
            var hashAlgorithm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}