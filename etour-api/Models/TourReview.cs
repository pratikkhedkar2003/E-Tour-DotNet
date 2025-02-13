using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("tour_reviews")]
[Index("Rating", Name = "index_tour_reviews_rating")]
[Index("TourId", Name = "index_tour_reviews_tour_id")]
[Index("UserId", Name = "index_tour_reviews_user_id")]
public partial class TourReview
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [Column("review")]
    [StringLength(255)]
    public string Review { get; set; } = null!;

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_id")]
    public ulong TourId { get; set; }

    [Column("user_id")]
    public ulong UserId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("TourId")]
    [InverseProperty("TourReviews")]
    public virtual Tour Tour { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TourReviews")]
    public virtual User User { get; set; } = null!;
}
