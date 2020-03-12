using Microsoft.AspNetCore.Authorization;

namespace AGTec.Microservice.Auth.Requirements
{
    class ClaimValueAuthorizationRequirement : IAuthorizationRequirement
    {
        public ClaimValueAuthorizationRequirement(string claimType, string[] claimValues)
        {
            ClaimType = claimType;
            ClaimValues = claimValues;
        }

        public string ClaimType { get; private set; }
        public string[] ClaimValues { get; private set; }
    }
}
