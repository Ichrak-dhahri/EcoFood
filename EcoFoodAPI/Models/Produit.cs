using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoFoodAPI.Models
{
    [Table("produit")]
    public partial class Produit
    {
        public int IdProduit { get; set; }

        public string Titre { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Prix { get; set; }

        public DateOnly? DateExpiration { get; set; }

        public string? ImageUrl { get; set; }

        public string? Statut { get; set; }

        public int IdUser { get; set; }

        public int IdCategorie { get; set; }

        public virtual Categorie IdCategorieNavigation { get; set; } = null!;

        public virtual User IdUserNavigation { get; set; } = null!;

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}