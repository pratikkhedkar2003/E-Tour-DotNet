using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("passengers")]
[Index("BookingId", Name = "index_passengers_booking_id")]
[Index("TourId", Name = "index_passengers_tour_id")]
public partial class Passenger
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

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
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("gender", TypeName = "enum('MALE','FEMALE','OTHER')")]
    public string Gender { get; set; } = null!;

    [Column("passenger_type", TypeName = "enum('SINGLE_PERSON','EXTRA_PERSON','TWIN_SHARING','CHILD_WITH_BED','CHILD_WITHOUT_BED')")]
    public string PassengerType { get; set; } = null!;

    [Column("passenger_cost")]
    public double PassengerCost { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_id")]
    public ulong TourId { get; set; }

    [Column("booking_id")]
    public ulong BookingId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("Passengers")]
    public virtual Booking Booking { get; set; } = null!;

    [ForeignKey("TourId")]
    [InverseProperty("Passengers")]
    public virtual Tour Tour { get; set; } = null!;
}
