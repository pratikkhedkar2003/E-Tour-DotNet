using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace etour_api.Models;

[Table("bookings")]
[Index("DepartureId", Name = "index_bookings_departure_id")]
[Index("BookingStatus", Name = "index_bookings_status")]
[Index("TourId", Name = "index_bookings_tour_id")]
[Index("UserId", Name = "index_bookings_user_id")]
public partial class Booking
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("booking_date", TypeName = "timestamp")]
    public DateTime? BookingDate { get; set; }

    [Column("booking_status", TypeName = "enum('PENDING','CONFIRMED','CANCELLED')")]
    public string BookingStatus { get; set; } = null!;

    [Column("total_price")]
    public double TotalPrice { get; set; }

    [Column("reference_id")]
    [StringLength(255)]
    public string ReferenceId { get; set; } = null!;

    [Column("tour_id")]
    public ulong TourId { get; set; }

    [Column("departure_id")]
    public ulong DepartureId { get; set; }

    [Column("user_id")]
    public ulong UserId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("DepartureId")]
    [InverseProperty("Bookings")]
    public virtual Departure Departure { get; set; } = null!;

    [InverseProperty("Booking")]
    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    [ForeignKey("TourId")]
    [InverseProperty("Bookings")]
    public virtual Tour Tour { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Bookings")]
    public virtual User User { get; set; } = null!;
}
