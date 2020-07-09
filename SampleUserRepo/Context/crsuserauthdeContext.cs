using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleUserRepo.Models;

namespace SampleUserRepo.Context
{
    public partial class crsuserauthdeContext : DbContext
    {
        private readonly string connectionString;
        public crsuserauthdeContext()
        {
        }

        public crsuserauthdeContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public crsuserauthdeContext(DbContextOptions<crsuserauthdeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CountryPreference> CountryPreference { get; set; }
        public virtual DbSet<UserPreferences> UserPreferences { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPreferences>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(8)
                    .HasDefaultValueSql("('MY')");

                entity.Property(e => e.DateFormat).HasMaxLength(20);

                entity.Property(e => e.Language).HasMaxLength(40);

                entity.Property(e => e.NumberFormat).HasMaxLength(50);

                entity.Property(e => e.TimeFormat).HasMaxLength(20);

                entity.Property(e => e.TimeZone).HasMaxLength(60);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .IsClustered(false);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.ApiKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ApiSecret)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MobileNumber).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SysCreated)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.SysModified)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
