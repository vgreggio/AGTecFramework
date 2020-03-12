﻿using AGTec.Microservice.Auth.Configuration;
using AGTec.Microservice.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AGTec.Microservice.Auth.Handlers
{
    class ScopeAuthorizationHandler : AuthorizationHandler<ScopeAuthorizationRequirement>
    {
        private const string ScopeClaim = "scope";
        private readonly string _authIssuer;

        public ScopeAuthorizationHandler(IAuthConfiguration authConfiguration)
        {
            _authIssuer = authConfiguration.AuthIssuer;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ScopeAuthorizationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Issuer == _authIssuer && c.Type == ScopeClaim && requirement.AllowedScopes.Contains(c.Value)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
