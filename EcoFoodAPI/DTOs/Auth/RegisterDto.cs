// DTOs/Auth/RegisterDto.cs
using System.ComponentModel.DataAnnotations;

namespace EcoFoodAPI.DTOs.Auth
{
    public class RegisterDto
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

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string MotDePasse { get; set; } = null!;

        [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmMotDePasse { get; set; } = null!;

        [StringLength(100, ErrorMessage = "La ville ne peut pas dépasser 100 caractères")]
        public string? Ville { get; set; }
    }
}

// DTOs/Auth/LoginDto.cs
