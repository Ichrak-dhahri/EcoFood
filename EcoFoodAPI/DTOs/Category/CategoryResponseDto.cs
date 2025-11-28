namespace EcoFoodAPI.DTOs.Category
{
    public class CategoryResponseDto
    {
        public int IdCategorie { get; set; }
        public string NomCategorie { get; set; } = null!;
        public int NombreProduits { get; set; }
    }
}