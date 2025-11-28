namespace EcoFoodAPI.DTOs.Profile
{
    public class ProfileResponseDto
    {
        public int IdUser { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Ville { get; set; }
        public string Role { get; set; } = null!;
        public decimal? MoyenneRating { get; set; }
        public int? TotalRatingsRecus { get; set; }
        public int? TotalRatingsDonnes { get; set; }
    }
}