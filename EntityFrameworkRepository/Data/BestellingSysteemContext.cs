using EntityFrameworkRepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EntityFrameworkRepository.Data
{
    public partial class BestellingSysteemContext : DbContext
    {
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Bestelling> Bestellingen { get; set; }
        public virtual DbSet<Product> Producten { get; set; }
        public virtual DbSet<Product_Bestelling> Product_Bestellingen { get; set; }
        
        public BestellingSysteemContext()
        {
        }

        public BestellingSysteemContext(DbContextOptions<BestellingSysteemContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["HPZBook"].ConnectionString);
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["HPEnvy"].ConnectionString);
                optionsBuilder.UseSqlServer("Server=CI00085249\\SQLEXPRESS;Database=Project_ProductKlantBestellingEF;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>(entity =>
            {
                entity.ToTable("Klant");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("Naam");

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasColumnName("Adres");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Bestelling>(entity =>
            {
                entity.ToTable("Bestelling");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.KlantId).HasColumnName("KlantId");

                entity.Property(e => e.Datum)
                    .IsRequired()
                    .HasColumnName("Datum");

                entity.Property(e => e.Betaald)
                    .HasColumnType("bit")
                    .IsRequired()
                    .HasColumnName("Betaald");

                entity.Property(e => e.Prijs)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Prijs");

                entity.HasOne(d => d.Klant)
                    .WithMany(p => p.Bestellingen)
                    .HasForeignKey(d => d.KlantId)
                    .HasConstraintName("FK_Bestelling_Klant");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");    
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Naam)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("Naam");

                entity.Property(e => e.Prijs)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Prijs");

                entity.Property(e => e.Valid)
                    .HasColumnName("VALID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");               
            });

            modelBuilder.Entity<Product_Bestelling>(entity =>
            {
                entity.ToTable("Product_Bestelling");

                entity.HasKey(e => new { e.ProductId, e.BestellingId });

                entity.Property(e => e.BestellingId).HasColumnName("BestellingId");

                entity.Property(e => e.ProductId).HasColumnName("ProductId");

                entity.Property(e => e.Aantal).HasColumnName("Aantal");

                entity.HasOne(d => d.Bestelling)
                    .WithMany(p => p.ProductBestellingen)
                    .HasForeignKey(d => d.BestellingId)
                    .HasConstraintName("FK_Product_Bestelling_Bestelling");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductBestellingen)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Product_Bestelling_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
