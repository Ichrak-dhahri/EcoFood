using System.ComponentModel.DataAnnotations;

namespace EcoFoodAPI.DTOs.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string MotDePasse { get; set; } = null!;
    }
}
