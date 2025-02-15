using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("tours")]
[Index("Duration", Name = "index_tours_duration")]
[Index("TourId", Name = "index_tours_tour_id", IsUnique = true)]
[Index("TourName", Name = "index_tours_tour_name")]
[Index("TourSubcategoryId", Name = "index_tours_tour_subcategory_id")]
public partial class Tour
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("tour_id")]
    public string TourId { get; set; } = null!;

    [Column("tour_name")]
    [StringLength(100)]
    public string TourName { get; set; } = null!;

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string Description { get; set; } = null!;

    [Column("duration")]
    [StringLength(50)]
    public string Duration { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_subcategory_id")]
    public ulong TourSubcategoryId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [Column("is_popular", TypeName = "bit(1)")]
    public ulong IsPopular { get; set; }

    [InverseProperty("Tour")]
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Tour")]
    public virtual ICollection<Departure> Departures { get; set; } = new List<Departure>();

    [InverseProperty("Tour")]
    public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();

    [InverseProperty("Tour")]
    [JsonIgnore]
    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    [InverseProperty("Tour")]
    public virtual ICollection<TourPrice> TourPrices { get; set; } = new List<TourPrice>();

    [InverseProperty("Tour")]
    public virtual ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();

    [ForeignKey("TourSubcategoryId")]
    [InverseProperty("Tours")]
    [JsonIgnore]
    public virtual TourSubcategory TourSubcategory { get; set; } = null!;
}
