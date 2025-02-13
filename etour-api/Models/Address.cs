using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("addresses")]
[Index("UserId", Name = "index_addresses_user_id")]
public partial class Address
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("address_line")]
    [StringLength(255)]
    public string AddressLine { get; set; } = null!;

    [Column("city")]
    [StringLength(100)]
    public string City { get; set; } = null!;

    [Column("state")]
    [StringLength(100)]
    public string State { get; set; } = null!;

    [Column("country")]
    [StringLength(100)]
    public string Country { get; set; } = null!;

    [Column("zip_code")]
    [StringLength(10)]
    public string ZipCode { get; set; } = null!;

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("user_id")]
    public ulong UserId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Addresses")]
    public virtual User User { get; set; } = null!;
}
