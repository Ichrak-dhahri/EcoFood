using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace EcoFoodAPI.Models
{
    [Table("rating")]
    public partial class Rating
    {
        public int IdRating { get; set; }

        public int? Note { get; set; }

        public string? Commentaire { get; set; }

        public DateTime? DateRating { get; set; }

        public int IdFromUser { get; set; }

        public int IdToUser { get; set; }

        public virtual User IdFromUserNavigation { get; set; } = null!;

        public virtual User IdToUserNavigation { get; set; } = null!;
    }
}