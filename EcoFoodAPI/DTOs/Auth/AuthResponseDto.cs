
namespace EcoFoodAPI.DTOs.Auth
{
    public class AuthResponseDto
    {
        public int IdUser { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Ville { get; set; }
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}