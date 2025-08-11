using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartStocks.Models;
using SmartStocks.Services;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    
    public async Task<ActionResult<User>> Register(Register request)
    {
        var user = await _authService.Register(request);
        if (user is null) return BadRequest("User already exists or registration failed.");
        return Ok(user);
    }

    [HttpPost("login")]
    
    public async Task<ActionResult<TokenResponse>> Login(Login request)
    {
        var response = await _authService.Login(request);
        if (response is null) return Unauthorized("Invalid email or password.");
        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokens(request);
        if (result is null) return Unauthorized("Invalid refresh token.");
        return Ok(result);
    }

    [HttpPost("forgot-password")]
public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
{
    await _authService.SendResetPasswordEmail(request.Email);
   
    return Ok(new { message = "If a user with this email exist, you will receive a reset password link." });
}

[HttpPost("reset-password")]
public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
{
    var success = await _authService.ResetPassword(request.Token, request.NewPassword);
    if (!success)
        return BadRequest(new { message = "Invalid or expired token." });

    return Ok(new { message = "Password was changed successfully." });
}

}