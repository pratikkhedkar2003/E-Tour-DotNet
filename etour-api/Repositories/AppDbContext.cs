using System;
using System.Collections.Generic;
using etour_api.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace etour_api.Repositories;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Confirmation> Confirmations { get; set; }

    public virtual DbSet<Departure> Departures { get; set; }

    public virtual DbSet<Itinerary> Itineraries { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourCategory> TourCategories { get; set; }

    public virtual DbSet<TourPrice> TourPrices { get; set; }

    public virtual DbSet<TourReview> TourReviews { get; set; }

    public virtual DbSet<TourSubcategory> TourSubcategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseMySql("server=localhost;database=etour_db;user=root;password=Pratik2003@", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.19-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses).HasConstraintName("fk_addresses_user_id");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.BookingDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.BookingStatus).HasDefaultValueSql("'PENDING'");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Departure).WithMany(p => p.Bookings).HasConstraintName("fk_bookings_departure_id");

            entity.HasOne(d => d.Tour).WithMany(p => p.Bookings).HasConstraintName("fk_bookings_tour_id");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings).HasConstraintName("fk_bookings_user_id");
        });

        modelBuilder.Entity<Confirmation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithOne(p => p.Confirmation).HasConstraintName("fk_confirmations_user_id");
        });

        modelBuilder.Entity<Departure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Tour).WithMany(p => p.Departures).HasConstraintName("fk_departures_tour_id");
        });

        modelBuilder.Entity<Itinerary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Tour).WithMany(p => p.Itineraries).HasConstraintName("fk_itineraries_tour_id");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Booking).WithMany(p => p.Passengers).HasConstraintName("fk_passengers_booking_id");

            entity.HasOne(d => d.Tour).WithMany(p => p.Passengers).HasConstraintName("fk_passengers_tour_id");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiekWCHIAty07z8GECv2GJAbdmv1p998meTA&s'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.TourSubcategory).WithMany(p => p.Tours).HasConstraintName("fk_tours_tour_subcategory_id");
        });

        modelBuilder.Entity<TourCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("'https://i.pinimg.com/736x/a7/3c/bf/a73cbfbcf18054bf31ee42e6453c5d94.jpg'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<TourPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourPrices).HasConstraintName("fk_tour_prices_tour_id");
        });

        modelBuilder.Entity<TourReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourReviews).HasConstraintName("fk_tour_reviews_tour_id");

            entity.HasOne(d => d.User).WithMany(p => p.TourReviews).HasConstraintName("fk_tour_reviews_user_id");
        });

        modelBuilder.Entity<TourSubcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQh0wf3YjxsHSxRSUxynBkbLrXM_dlOkLFMmg&s'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.TourCategory).WithMany(p => p.TourSubcategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tour_subcategories_tour_category_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AccountNonExpired).HasDefaultValueSql("'1'");
            entity.Property(e => e.AccountNonLocked).HasDefaultValueSql("'1'");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("'https://cdn-icons-png.flaticon.com/512/149/149071.png'");
            entity.Property(e => e.LastLogin).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.LoginAttempts).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
