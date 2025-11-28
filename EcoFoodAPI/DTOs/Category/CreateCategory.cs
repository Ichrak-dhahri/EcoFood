using System.ComponentModel.DataAnnotations;

namespace EcoFoodAPI.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Le nom de la catégorie est requis")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
        public string NomCategorie { get; set; } = null!;
    }
}