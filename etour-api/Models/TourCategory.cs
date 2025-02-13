using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("tour_categories")]
[Index("CategoryName", Name = "index_tour_categories_category_name")]
public partial class TourCategory
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("category_name", TypeName = "enum('INTERNATIONAL','DOMESTIC')")]
    public string CategoryName { get; set; } = null!;

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("TourCategory")]
    [JsonIgnore]
    public virtual ICollection<TourSubcategory> TourSubcategories { get; set; } = new List<TourSubcategory>();
}
