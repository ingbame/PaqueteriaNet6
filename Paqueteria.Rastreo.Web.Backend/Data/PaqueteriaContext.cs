using Microsoft.EntityFrameworkCore;
using Paqueteria.Rastreo.Web.Backend.Models;

namespace Paqueteria.Rastreo.Web.Backend.Data
{
    public class PaqueteriaContext : DbContext
    {
        public PaqueteriaContext(DbContextOptions<PaqueteriaContext> options) : base(options) { }

        public DbSet<Paquete> Paquetes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aqui se puede personalizar la creación de las tablas y las relaciones entre ellas
            base.OnModelCreating(modelBuilder); 
        }
    }
}
