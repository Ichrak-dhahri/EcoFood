using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoFoodAPI.Models;
[Table("reservation")]
public partial class Reservation
{
    public int IdReservation { get; set; }

    public string? Statut { get; set; }

    public DateTime? DateReservation { get; set; }

    public int IdProduit { get; set; }

    public int IdUser { get; set; }

    public virtual Produit IdProduitNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
