using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Angular2Blank.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Angular2Blank.Common.Extensions;
using Angular2Blank.Services.Providers;

namespace Angular2Blank.Web.Authentication
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IUserService _userService;
        private readonly IPasswordProvider _passwordProvider;

        public TokenProviderMiddleware(
            RequestDelegate next,
            TokenProviderOptions options,
            IUserService userService,
            IPasswordProvider passwordProvider)
        {
            _next = next;
            _options = options;
            _userService = userService;
            _passwordProvider = passwordProvider;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var now = DateTime.UtcNow;

            var identity = await GetIdentity(username, password, now);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: identity.Claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password, DateTime dateTime)
        {
            var user = await _userService.FindByNameAsync(username);

            if (user == null)
                return null;

            if (!_passwordProvider.VerifyPassword(user.PasswordHash, password))
                return null;

            return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), 
                    new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, dateTime.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
                    });
        }
    }
}
