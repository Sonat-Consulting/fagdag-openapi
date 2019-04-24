using System;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreApi.Security
{
    public class HasPermissionRequirements : IAuthorizationRequirement
    {
        public string Issuer { get; set; }

        public string Scope { get; set; }

        public HasPermissionRequirements(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentException(nameof(issuer));
        }
    }
}