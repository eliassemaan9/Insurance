using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InsuranceCore.Models
{
    public partial class InsuranceContext : DbContext
    {
        public InsuranceContext()
        {
        }

        public InsuranceContext(DbContextOptions<InsuranceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimCenter> ClaimCenters { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Insured> Insureds { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<TreatingPhysician> TreatingPhysicians { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BH-DEV26\\SQL2019;Initial Catalog=Insurance;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ClaimCenter>(entity =>
            {
                entity.ToTable("ClaimCenter");

                entity.Property(e => e.AdmissionDate).HasColumnType("datetime");

                entity.Property(e => e.EstimatedCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MedicalCase)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.ClaimCenters)
                    .HasForeignKey(d => d.HospitalId)
                    .HasConstraintName("FK_ClaimCenter_Hospitals");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.ClaimCenters)
                    .HasForeignKey(d => d.InsuredId)
                    .HasConstraintName("FK_ClaimCenter_Insured");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ClaimCenters)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Lookups_ClaimCenter");

                entity.HasOne(d => d.Treating)
                    .WithMany(p => p.ClaimCenters)
                    .HasForeignKey(d => d.TreatingId)
                    .HasConstraintName("FK_ClaimCenter_TreatingPhysician");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Other)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("other");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insured>(entity =>
            {
                entity.ToTable("Insured");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Insureds)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Insured_Lookups");
            });

            modelBuilder.Entity<Lookup>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LookupCode)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TreatingPhysician>(entity =>
            {
                entity.ToTable("TreatingPhysician");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_Id");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
