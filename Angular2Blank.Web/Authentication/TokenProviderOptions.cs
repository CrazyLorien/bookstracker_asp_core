using System;
using Microsoft.IdentityModel.Tokens;

namespace Angular2Blank.Web.Authentication
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(14);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
