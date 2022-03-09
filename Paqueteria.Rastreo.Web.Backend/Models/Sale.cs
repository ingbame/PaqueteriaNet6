using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Rastreo.Web.Backend.Models
{
    public class Sale
    {
        [Key]
        public long SalesId { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Subtotal { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal TaxIva { get; set; }
        public long ClientId { get; set; }
        public List<Package>? Packages { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
