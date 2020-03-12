using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGTec.Microservice.Auth.Requirements
{
    class ClaimAuthorizationRequirement : IAuthorizationRequirement
    {
        public ClaimAuthorizationRequirement(string[] claimTypes)
        {
            ClaimTypes = claimTypes;
        }

        public string[] ClaimTypes { get; private set; }
    }
}
