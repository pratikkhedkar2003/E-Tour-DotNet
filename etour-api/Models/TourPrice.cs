using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("tour_prices")]
[Index("TourId", Name = "index_tour_prices_tour_id")]
public partial class TourPrice
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("single_person_price")]
    public double SinglePersonPrice { get; set; }

    [Column("twin_sharing_price")]
    public double TwinSharingPrice { get; set; }

    [Column("extra_person_price")]
    public double ExtraPersonPrice { get; set; }

    [Column("child_with_bed_price")]
    public double ChildWithBedPrice { get; set; }

    [Column("child_without_bed_price")]
    public double ChildWithoutBedPrice { get; set; }

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
    [InverseProperty("TourPrices")]
    [JsonIgnore]
    public virtual Tour Tour { get; set; } = null!;
}
