﻿using System;
using System.IdentityModel.Tokens;

namespace Angular2Blank.Authentication
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
