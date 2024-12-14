using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Models;

public partial class EventDbContext : DbContext
{
    public EventDbContext()
    {
    }

    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminTb> AdminTbs { get; set; }

    public virtual DbSet<BookingHistoryTb> BookingHistoryTbs { get; set; }

    public virtual DbSet<BookingRefundTb> BookingRefundTbs { get; set; }

    public virtual DbSet<BookingTb> BookingTbs { get; set; }

    public virtual DbSet<EventTb> EventTbs { get; set; }

    public virtual DbSet<OrganizerTb> OrganizerTbs { get; set; }

    public virtual DbSet<PaymentTb> PaymentTbs { get; set; }

    public virtual DbSet<TicketTb> TicketTbs { get; set; }

    public virtual DbSet<UserTb> UserTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5F1S63A;Initial Catalog=EventDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminTb>(entity =>
        {
            entity.HasKey(e => e.Adminid);

            entity.ToTable("AdminTB");

            entity.Property(e => e.Adminid).ValueGeneratedNever();
            entity.Property(e => e.AdminEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BookingHistoryTb>(entity =>
        {
            entity.HasKey(e => e.BookingHistoryId);

            entity.ToTable("BookingHistoryTb");

            entity.Property(e => e.BookingHistoryId).HasColumnName("BookingHistoryID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
        });

        modelBuilder.Entity<BookingRefundTb>(entity =>
        {
            entity.HasKey(e => e.RefundId).HasName("PK_BookingRefund");

            entity.ToTable("BookingRefundTb");

            entity.Property(e => e.RefundId).HasColumnName("RefundID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CancleDate).HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingRefundTbs)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_BookingRefundTb_BookingTB");

            entity.HasOne(d => d.Event).WithMany(p => p.BookingRefundTbs)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_BookingRefundTb_EventTB");
        });

        modelBuilder.Entity<BookingTb>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.ToTable("BookingTB");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.NumberOfTicket)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.BookingTbs)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_BookingTB_EventTB");

            entity.HasOne(d => d.User).WithMany(p => p.BookingTbs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_BookingTB_UserTB");
        });

        modelBuilder.Entity<EventTb>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("EventTB");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EventArtist)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventMoreDetails)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventThem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");

            entity.HasOne(d => d.Organizer).WithMany(p => p.EventTbs)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK_EventTB_OrganizerTB");
        });

        modelBuilder.Entity<OrganizerTb>(entity =>
        {
            entity.HasKey(e => e.OrganizerId);

            entity.ToTable("OrganizerTB");

            entity.Property(e => e.OrganizerId).HasColumnName("OrganizerID");
            entity.Property(e => e.OrganizerEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrganizerMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OrganizerMobileNO");
            entity.Property(e => e.OrganizerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrganizerPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentTb>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.ToTable("PaymentTB");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.PaymentTbs)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_PaymentTB_BookingTB");
        });

        modelBuilder.Entity<TicketTb>(entity =>
        {
            entity.HasKey(e => e.TicketId);

            entity.ToTable("TicketTb");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("TicketID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.TicketTbs)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_TicketTb_BookingTB");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketTbs)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_TicketTb_EventTB");

            entity.HasOne(d => d.Payment).WithMany(p => p.TicketTbs)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_TicketTb_PaymentTB");

            entity.HasOne(d => d.User).WithMany(p => p.TicketTbs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TicketTb_UserTB");
        });

        modelBuilder.Entity<UserTb>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserTB");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
