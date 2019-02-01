using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRentalApi.Models.Entities
{
    public partial class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
        }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking", "cr");

                entity.HasIndex(e => e.BookingId)
                    .HasName("UQ__Booking__73951AEC6A4504DA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BookingId)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CarLicense)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__CarId__4222D4EF");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__Custome__412EB0B6");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car", "cr");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostFormula)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "cr");

                entity.HasIndex(e => e.Ssn)
                    .HasName("UQ__Customer__CA1E8E3CDBC74184")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(32);

                entity.Property(e => e.LastName).HasMaxLength(32);

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
