﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Angular2Blank.Common.Extensions;
using Angular2Blank.Services.Interfaces;
using Angular2Blank.Services.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Angular2Blank.Web.Authentication
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IPasswordProvider _passwordProvider;

        public TokenProviderMiddleware(
            RequestDelegate next,
            TokenProviderOptions options,
            IPasswordProvider passwordProvider)
        {
            _next = next;
            _options = options;
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

            var identity = await GetIdentity(context, username, password, now);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

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

        private async Task<ClaimsIdentity> GetIdentity(HttpContext context, string username, string password, DateTime dateTime)
        {
            var userService = context.RequestServices.GetService<IUserService>();
            var roleService = context.RequestServices.GetService<IRoleService>();

            var passwordHash = await userService.GetPasswordHashAsync(username);

            if (passwordHash == null)
                return null;

            if (!_passwordProvider.VerifyPassword(passwordHash, password))
                return null;

            var user = await userService.FindByNameAsync(username);

            var roles = await userService.GetRolesAsync(user.Id);

            var claims = (new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, dateTime.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString(), ClaimValueTypes.Integer32)
            }).Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return new ClaimsIdentity(new System.Security.Principal.GenericIdentity(user.Id.ToString(), "Token"), claims);
        }
    }
}
