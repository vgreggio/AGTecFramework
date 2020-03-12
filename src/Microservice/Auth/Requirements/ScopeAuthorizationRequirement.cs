using Microsoft.AspNetCore.Authorization;

namespace AGTec.Microservice.Auth.Requirements
{
    class ScopeAuthorizationRequirement : IAuthorizationRequirement
    {
        public ScopeAuthorizationRequirement(params string[] allowedScopes)
        {
            AllowedScopes = allowedScopes;
        }

        public string[] AllowedScopes { get; private set; }
    }
}
