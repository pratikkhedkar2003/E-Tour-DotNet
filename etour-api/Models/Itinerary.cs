using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("itineraries")]
[Index("TourId", Name = "index_itineraries_tour_id")]
public partial class Itinerary
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("day")]
    public int Day { get; set; }

    [Column("itinerary_name")]
    [StringLength(255)]
    public string ItineraryName { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_id")]
    public ulong TourId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("TourId")]
    [InverseProperty("Itineraries")]
    public virtual Tour Tour { get; set; } = null!;
}
