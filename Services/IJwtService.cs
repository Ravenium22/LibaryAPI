namespace Kutuphane.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, string role);
        bool ValidateToken(string token);
    }
}