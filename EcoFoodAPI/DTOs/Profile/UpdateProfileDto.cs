using System.ComponentModel.DataAnnotations;

namespace EcoFoodAPI.DTOs.Profile
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères")]
        public string Nom { get; set; } = null!;

        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères")]
        public string Prenom { get; set; } = null!;

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = null!;

        [StringLength(100, ErrorMessage = "La ville ne peut pas dépasser 100 caractères")]
        public string? Ville { get; set; }
    }
}