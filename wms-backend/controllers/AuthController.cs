using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartStocks.Models;
using SmartStocks.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(Register request)
    {
        var user = await authService.Register(request);
        if (user is null) return BadRequest("User already exists or registration failed.");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(Login request)
    {
        var response = await authService.Login(request);
        if (response is null) return Unauthorized("Invalid email or password.");
        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var result = await authService.RefreshTokens(request);
        if (result is null) return Unauthorized("Invalid refresh token.");
        return Ok(result); 
    }
}