using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("users")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("UserId", Name = "index_users_user_id", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("user_id")]
    public string UserId { get; set; } = null!;

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("middle_name")]
    [StringLength(50)]
    public string MiddleName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    [JsonIgnore]
    public string Password { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("bio")]
    [StringLength(255)]
    public string? Bio { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("role", TypeName = "enum('ROLE_ADMIN','ROLE_USER')")]
    public string Role { get; set; } = null!;

    [Column("last_login", TypeName = "timestamp")]
    public DateTime? LastLogin { get; set; }

    [Column("login_attempts")]
    public int? LoginAttempts { get; set; }

    [Column("enabled")]
    public bool Enabled { get; set; }

    [Required]
    [Column("account_non_expired")]
    public bool? AccountNonExpired { get; set; }

    [Required]
    [Column("account_non_locked")]
    public bool? AccountNonLocked { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("User")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("User")]
    public virtual Confirmation? Confirmation { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TourReview> TourReviews { get; set; } = new List<TourReview>();
}
