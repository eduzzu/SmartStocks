namespace SmartStocks.Models;

public class TokenResponse
{
    public required string AccessToken { get; set; } = string.Empty;
    public required string RefreshToken { get; set; } = string.Empty;
}