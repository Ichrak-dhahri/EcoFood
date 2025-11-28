using System.ComponentModel.DataAnnotations;

namespace EcoFoodAPI.DTOs.Profile
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "L'ancien mot de passe est requis")]
        public string AncienMotDePasse { get; set; } = null!;

        [Required(ErrorMessage = "Le nouveau mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string NouveauMotDePasse { get; set; } = null!;

        [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
        [Compare("NouveauMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmNouveauMotDePasse { get; set; } = null!;
    }
}