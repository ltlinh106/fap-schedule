using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FapSchedule.Models
{
    public partial class FapScheduleContext : DbContext
    {
        public FapScheduleContext()
        {
        }

        public FapScheduleContext(DbContextOptions<FapScheduleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Lecturer> Lecturers { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasIndex(e => e.ClassName, "UQ__Classes__F8BF561B63A6C769")
                    .IsUnique();

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LecturerId).HasColumnName("LecturerID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.FirstSlotNavigation)
                    .WithMany(p => p.ClassFirstSlotNavigations)
                    .HasForeignKey(d => d.FirstSlot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__FirstSl__4CA06362");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__Lecture__4AB81AF0");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__RoomID__4BAC3F29");

                entity.HasOne(d => d.SecondSlotNavigation)
                    .WithMany(p => p.ClassSecondSlotNavigations)
                    .HasForeignKey(d => d.SecondSlot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__SecondS__4D94879B");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classes__Subject__49C3F6B7");
            });

            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.HasIndex(e => e.LecturerName, "UQ__Lecturer__38525E1934D6D1B3")
                    .IsUnique();

                entity.HasIndex(e => e.LecturerCode, "UQ__Lecturer__BB9CDAB329BF61D2")
                    .IsUnique();

                entity.Property(e => e.LecturerId).HasColumnName("LecturerID");

                entity.Property(e => e.LecturerCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LecturerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.SubjectName, "UQ__Subjects__4C5A7D55FB047E4F")
                    .IsUnique();

                entity.HasIndex(e => e.SubjectCode, "UQ__Subjects__9F7CE1A91CCE54AF")
                    .IsUnique();

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

                entity.Property(e => e.WeekDay)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
