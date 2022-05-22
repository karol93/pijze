﻿using Microsoft.AspNetCore.Authorization;
using Pijze.Api.Security.Requirements;

namespace Pijze.Api.Security.AuthorizationHandlers;

public class ScopeHandler :
    AuthorizationHandler<ScopeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ScopeRequirement requirement)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var success = context.User.Claims.Any(c => c.Type == "scope" && 
                                                   c.Value.Contains(requirement.Scope));

        if (success)
            context.Succeed(requirement);
            
        return Task.CompletedTask;
    }
}