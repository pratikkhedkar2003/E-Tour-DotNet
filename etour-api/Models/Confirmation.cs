using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("confirmations")]
[Index("Key", Name = "index_confirmations_key", IsUnique = true)]
[Index("UserId", Name = "index_confirmations_user_id", IsUnique = true)]
public partial class Confirmation
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("key")]
    public string Key { get; set; } = null!;

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
    [InverseProperty("Confirmation")]
    public virtual User User { get; set; } = null!;
}
