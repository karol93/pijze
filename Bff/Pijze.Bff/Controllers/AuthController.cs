using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pijze.Bff.Models;

namespace Pijze.Bff.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? returnUrl = "/")
    {
        return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
    }
    
    [HttpGet("get-user")]
    public IActionResult GetUser()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return Ok(new UserInfo(true));
        return Ok(new UserInfo(false));
    }
}
