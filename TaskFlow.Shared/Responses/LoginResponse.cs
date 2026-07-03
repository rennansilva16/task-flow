public class LoginResponse
{
    public required UserResponse Usuario { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}