using Microsoft.EntityFrameworkCore.Sqlite;

namespace Coffe_site.Models

{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);

        Task<AuthResponse> LoginAsync(LoginRequest request);

        string GenerateJwtToken(User user);

    }
}
