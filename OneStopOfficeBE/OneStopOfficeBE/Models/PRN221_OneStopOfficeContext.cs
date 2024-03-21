using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OneStopOfficeBE.Models
{
    public partial class PRN221_OneStopOfficeContext : DbContext
    {
        public PRN221_OneStopOfficeContext()
        {
        }

        public PRN221_OneStopOfficeContext(DbContextOptions<PRN221_OneStopOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("category_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("assigned_to");

                entity.Property(e => e.Attachment)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("attachment");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProcessNote)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("process_note");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.RequestAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("FK__Request__assigne__74444068");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Request__categor__753864A1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Request__user_id__73501C2F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.IsSuperAdmin).HasColumnName("is_super_admin");

                entity.Property(e => e.IsTokenValid).HasColumnName("is_token_valid");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Token)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "StaffCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Staff_Cat__categ__6D9742D9"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Staff_Cat__user___6E8B6712"),
                        j =>
                        {
                            j.HasKey("UserId", "CategoryId").HasName("PK__Staff_Ca__E4EAD994C37DEA2A");

                            j.ToTable("Staff_Category");

                            j.IndexerProperty<string>("UserId").HasMaxLength(50).IsUnicode(false).HasColumnName("user_id");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("category_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
