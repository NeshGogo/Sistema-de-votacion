using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sistema_de_votacion.Models
{
    public partial class ElectionDBContext : DbContext
    {
        //public ElectionDBContext()
        //{
        //}

        public ElectionDBContext(DbContextOptions<ElectionDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidate1 { get; set; }
        public virtual DbSet<Citizen> Citizen { get; set; }
        public virtual DbSet<Election> Election { get; set; }
        public virtual DbSet<ElectionCadidate> ElectionCadidate { get; set; }
        public virtual DbSet<ElectionCitizen> ElectionCitizen { get; set; }
        public virtual DbSet<ElectionPoliticParty> ElectionPoliticParty { get; set; }
        public virtual DbSet<ElectionPosition> ElectionPosition { get; set; }
        public virtual DbSet<PoliticParty> PoliticParty { get; set; }
        public virtual DbSet<Position> Position { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=ElectionDB;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProfilePhothoPath)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PoliticParty)
                    .WithMany(p => p.Candidate)
                    .HasForeignKey(d => d.PoliticPartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidate_PoliticParty");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Candidate)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidate_Position");
            });

            modelBuilder.Entity<Citizen>(entity =>
            {
                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("DNI")
                    .HasMaxLength(13);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Election>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ElectionCadidate>(entity =>
            {
                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.ElectionCadidate)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_Candidate");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionCadidate)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCandidate_Election");
            });

            modelBuilder.Entity<ElectionCitizen>(entity =>
            {
                entity.HasOne(d => d.Citizen)
                    .WithMany(p => p.ElectionCitizen)
                    .HasForeignKey(d => d.CitizenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCitizen_Citizen");

                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionCitizen)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionCitizen_Election");
            });

            modelBuilder.Entity<ElectionPoliticParty>(entity =>
            {
                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionPoliticParty)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionPoliticParty_Election");

                entity.HasOne(d => d.PoliticParty)
                    .WithMany(p => p.ElectionPoliticParty)
                    .HasForeignKey(d => d.PoliticPartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionPoliticParty_PoliticParty");
            });

            modelBuilder.Entity<ElectionPosition>(entity =>
            {
                entity.HasOne(d => d.Election)
                    .WithMany(p => p.ElectionPosition)
                    .HasForeignKey(d => d.ElectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionPosition_Election");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.ElectionPosition)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElectionPosition_Position");
            });

            modelBuilder.Entity<PoliticParty>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PartyLogoPath)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
