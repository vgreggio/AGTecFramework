using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGTec.Microservice.Auth.Requirements
{
    class ClaimOrScopeAuthorizationRequirement : IAuthorizationRequirement
    {
        public ClaimOrScopeAuthorizationRequirement(string claimType, params string[] allowedScopes)
        {
            ClaimType = claimType;
            AllowedScopes = allowedScopes;
        }

        public string ClaimType { get; private set; }
        public string[] AllowedScopes { get; private set; }
    }
}
