using SmartStocks.Models;

namespace SmartStocks.Services;

public interface IAuthService
{
    Task<User?> Register(Register request);
    Task<TokenResponse?> Login(Login request);
    Task<TokenResponse?> RefreshTokens(RefreshTokenRequest request);
    Task<bool> SendResetPasswordEmail(string email);

    Task<bool> ResetPassword(string token, string newPassword);
}