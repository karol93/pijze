using Microsoft.AspNetCore.Authorization;

namespace Pijze.Api.Security.Requirements;

public class ScopeRequirement : IAuthorizationRequirement
{
    public string Scope { get;}

    public ScopeRequirement(string scope)
    {
        Scope = scope;
    }
}