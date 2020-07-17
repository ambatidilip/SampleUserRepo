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
        public virtual DbSet<UserPreference> UserPreference { get; set; }
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
            modelBuilder.Entity<CountryPreference>(entity =>
            {
                entity.Property(e => e.CountryPreferenceId).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CultureCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateFormat)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SysCreated)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.SysModified)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.TimeFormat)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZoneId)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.Property(e => e.UserPreferenceId).ValueGeneratedNever();

                entity.Property(e => e.CultureCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateFormat)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SysCreated)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.SysModified)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.TimeFormat)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZoneId)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPreference)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPreference_Users");
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

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('SG')");

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

        internal void Rollback()
        {
           // throw new NotImplementedException();
        }

        internal void Commit()
        {
            // throw new NotImplementedException();
        }

        internal void BeginTransaction()
        {
            // throw new NotImplementedException();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
