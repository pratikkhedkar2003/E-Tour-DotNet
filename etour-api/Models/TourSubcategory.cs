using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("tour_subcategories")]
[Index("SubCategoryName", Name = "index_tour_subcategories_sub_category_name")]
[Index("TourCategoryId", Name = "index_tour_subcategories_tour_category_id")]
public partial class TourSubcategory
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("sub_category_name")]
    [StringLength(100)]
    public string SubCategoryName { get; set; } = null!;

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_category_id")]
    public ulong TourCategoryId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("TourCategoryId")]
    [InverseProperty("TourSubcategories")]
    public virtual TourCategory TourCategory { get; set; } = null!;

    [InverseProperty("TourSubcategory")]
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
