// Services/IAuthService.cs
using EcoFoodAPI.DTOs.Auth;

namespace EcoFoodAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
