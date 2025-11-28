namespace EcoFoodAPI.DTOs.Profile
{
    public class UserReputationDto
    {
        public int IdUser { get; set; }
        public string NomComplet { get; set; } = null!;
        public decimal? MoyenneRating { get; set; }
        public int? TotalRatingsRecus { get; set; }
        public List<RatingDetailDto> Ratings { get; set; } = new List<RatingDetailDto>();
    }

    public class RatingDetailDto
    {
        public int IdRating { get; set; }
        public int? Note { get; set; }  // Changé de "Score" à "Note"
        public string? Commentaire { get; set; }
        public DateTime? DateRating { get; set; }
        public string NomDonneur { get; set; } = null!;
        public string PrenomDonneur { get; set; } = null!;
    }
}