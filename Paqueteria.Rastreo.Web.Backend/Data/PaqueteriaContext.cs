using Microsoft.EntityFrameworkCore;
using Paqueteria.Rastreo.Web.Backend.Models;

namespace Paqueteria.Rastreo.Web.Backend.Data
{
    public class PaqueteriaContext : DbContext
    {
        public PaqueteriaContext(DbContextOptions<PaqueteriaContext> options) : base(options) { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Package> Packages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aqui se puede personalizar la creación de las tablas y las relaciones entre ellas
            PackagesProperties(modelBuilder);
            SalesProperties(modelBuilder);

            base.OnModelCreating(modelBuilder); 
        }
        public ModelBuilder SalesProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>().HasKey(pk => pk.SaleId).HasName("PK_Sales_SaleId");
            modelBuilder.Entity<Sale>().Property(p => p.CreatedDate).HasColumnType("DATETIME");
            modelBuilder.Entity<Sale>().Property(p => p.TaxIva).HasPrecision(18, 2);
            modelBuilder.Entity<Sale>().Property(p => p.Subtotal).HasPrecision(18, 2);
            modelBuilder.Entity<Sale>().Property(p => p.Total).HasPrecision(18, 2);
            return modelBuilder;
        }
        public ModelBuilder PackagesProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>().HasKey(pk => pk.PackageId).HasName("PK_Packages_PackageId");
            modelBuilder.Entity<Package>().HasOne<Sale>().WithMany().HasForeignKey(fk => fk.SaleId).HasConstraintName("FK_Packages_SaleId");
            modelBuilder.Entity<Package>().Property(p => p.Dimentions).HasPrecision(18, 2);
            modelBuilder.Entity<Package>().Property(p => p.Weigth).HasPrecision(18, 2);
            return modelBuilder;
        }
    }
}
