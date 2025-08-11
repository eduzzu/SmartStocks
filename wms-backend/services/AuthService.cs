using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartStocks.Data;
using SmartStocks.Models;

namespace SmartStocks.Services;

public class AuthService(AppDbContext appDbContext, TokenGenerator tokenGenerator, IEmailService emailService) : IAuthService
{
    public async Task<TokenResponse?> Login(Login request)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return null;
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }
        return await CreateTokenResponse(user);
    }

    private async Task<TokenResponse> CreateTokenResponse(User user)
    {
        return new TokenResponse
        {
            AccessToken = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role.ToString()),
            RefreshToken = await GenerateAndSaveRefreshToken(user)
        };
    }

    public async Task<User?> Register(Register request)
    {
        if (await appDbContext.Users.AnyAsync(u => u.Email == request.Email))
        {
            return null;
        }

        var newUser = new User();

        var hashedPassword = new PasswordHasher<User>()
            .HashPassword(newUser, request.Password);

        newUser.FirstName = request.FirstName;
        newUser.LastName = request.LastName;
        newUser.Email = request.Email;
        newUser.Password = hashedPassword;
        newUser.Role = request.Role;


        appDbContext.Users.Add(newUser);
        await appDbContext.SaveChangesAsync();
        return newUser;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshToken(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await appDbContext.SaveChangesAsync();
        return refreshToken;
    }

    private async Task<User?> ValidateRefreshToken(Guid userId, string refreshToken)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && u.RefreshToken == refreshToken);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    public async Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request)
    {
        var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);
        if (user is null) return null;
        return await CreateTokenResponse(user);
    }
    
    public async Task<bool> SendResetPasswordEmail(string email)
{
    var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null)
        return false;

    user.ResetPasswordToken = Guid.NewGuid().ToString();
    user.ResetPasswordTokenExpires = DateTime.UtcNow.AddHours(1);

    await appDbContext.SaveChangesAsync();

    var resetLink = $"http://localhost:5051/auth/reset-password?token={user.ResetPasswordToken}";
    var emailBody = $"Click <a href='{resetLink}'>here</a> to change your password. This link expires in an hour.";

    await emailService.SendEmail(user.Email, "Reset password", emailBody);

    return true;
}

public async Task<bool> ResetPassword(string token, string newPassword)
{
    var user = await appDbContext.Users.FirstOrDefaultAsync(u =>
        u.ResetPasswordToken == token && u.ResetPasswordTokenExpires > DateTime.UtcNow);

    if (user == null)
        return false;

    user.Password = new PasswordHasher<User>()
            .HashPassword(user, newPassword);
    user.ResetPasswordToken = null;
    user.ResetPasswordTokenExpires = null;

    await appDbContext.SaveChangesAsync();

    return true;
}
}