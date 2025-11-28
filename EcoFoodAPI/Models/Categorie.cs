using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcoFoodAPI.Models
{

    [Table("categorie")]
    public partial class Categorie

    {
        [Key]
        public int IdCategorie { get; set; }

        public string NomCategorie { get; set; } = null!;

        public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
    }
}