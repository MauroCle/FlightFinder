using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightFinder.Models
{
    public partial class FlightFinderContext : DbContext
    {
        public FlightFinderContext()
        {
        }

        public FlightFinderContext(DbContextOptions<FlightFinderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<ClassFeature> ClassFeatures { get; set; } = null!;
        public virtual DbSet<DemandBasis> DemandBases { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<FlightClass> FlightClasses { get; set; } = null!;
        public virtual DbSet<FlightFeature> FlightFeatures { get; set; } = null!;
        public virtual DbSet<FlightsStop> FlightsStops { get; set; } = null!;
        public virtual DbSet<OccupancyStatus> OccupancyStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9685F53\\SQLEXPRESS01; DataBase=FlightFinder;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClassFeature>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Class_Features");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Class)
                    .WithMany()
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Class_Features_Flight_Classes");
            });

            modelBuilder.Entity<DemandBasis>(entity =>
            {
                entity.ToTable("Demand_Bases");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DemandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate).HasColumnType("date");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.HasOne(d => d.DemandBased)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.DemandBasedId)
                    .HasConstraintName("FK_Flights_Demand_Bases");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.FlightDestinations)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Airports2");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.FlightOrigins)
                    .HasForeignKey(d => d.OriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Airports3");
            });

            modelBuilder.Entity<FlightClass>(entity =>
            {
                entity.ToTable("Flight_Classes");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FlightFeature>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Flight_Features");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Flight)
                    .WithMany()
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_Features_Flights");
            });

            modelBuilder.Entity<FlightsStop>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Flights_Stops");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Flight)
                    .WithMany()
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Stops_Flights");
            });

            modelBuilder.Entity<OccupancyStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Occupancy_Status");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Class)
                    .WithMany()
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupancy_Status_Flight_Classes");

                entity.HasOne(d => d.Flight)
                    .WithMany()
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Occupancy_Status_Flights");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
