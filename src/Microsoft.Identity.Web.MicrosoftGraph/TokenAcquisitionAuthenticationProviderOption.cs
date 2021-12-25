﻿using Microsoft.Graph;

namespace Microsoft.Identity.Web
{
    internal class TokenAcquisitionAuthenticationProviderOption : IAuthenticationProviderOption
    {
        public string[]? Scopes { get; set; }
        public bool? AppOnly { get; set; }
        public string? Tenant { get; set; }

        public string? AuthenticationScheme { get; set; }
    }
}
