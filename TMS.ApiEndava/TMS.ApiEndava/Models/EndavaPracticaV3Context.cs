using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TMS.ApiEndava.Models;

public partial class EndavaPracticaV3Context : DbContext
{
    public EndavaPracticaV3Context()
    {
    }

    public EndavaPracticaV3Context(DbContextOptions<EndavaPracticaV3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Event1> Event1s { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order1> Order1s { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<User1> User1s { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-85CE4MB\\SQLEXPRESS;Initial Catalog=endavaPracticaV3;Integrated Security=True;TrustServerCertificate=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event1>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event1__2DC7BD692CD061F2");

            entity.ToTable("Event1");

            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.DescriptionEvent)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descriptionEvent");
            entity.Property(e => e.EndDate)
                .HasPrecision(6)
                .HasColumnName("endDate");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("eventName");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.StartDate)
                .HasPrecision(6)
                .HasColumnName("startDate");
            entity.Property(e => e.VenueId).HasColumnName("venueID");

            entity.HasOne(d => d.EventType).WithMany(p => p.Event1s)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("FKk5tmq47v5l1bnt1gls5mwr70o");

            entity.HasOne(d => d.Venue).WithMany(p => p.Event1s)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK2c2mh8utgieprpucaa7v62jb");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__04ACC49DBF1FC520");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("eventTypeName");
        });

        modelBuilder.Entity<Order1>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order1__0809337D8CA27AC0");

            entity.ToTable("Order1");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.NumberOfTickets).HasColumnName("numberOfTickets");
            entity.Property(e => e.OrderedAt)
                .HasPrecision(6)
                .HasColumnName("orderedAT");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticketCategoryID");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.TicketCategoryId)
                .HasConstraintName("FK5gl1mrmbcjuhy7uv68s3adipw");

            entity.HasOne(d => d.User).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK51jxeqby1ffmd5otbaeo0pdlr");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__TicketCa__56F2E67A23DF356C");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticketCategoryID");
            entity.Property(e => e.DescriptionTicketCategory)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descriptionTicketCategory");
            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK1mu43sea6fje6ymr5i7th95h2");
        });

        modelBuilder.Entity<User1>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User1__CB9A1CDFE145F963");

            entity.ToTable("User1");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__4DDFB71F61D59E1E");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId).HasColumnName("venueID");
            entity.Property(e => e.VenueCapacity).HasColumnName("venueCapacity");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("venueLocation");
            entity.Property(e => e.VenueType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("venueType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
