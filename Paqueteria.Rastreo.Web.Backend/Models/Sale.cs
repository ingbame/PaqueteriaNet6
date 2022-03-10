using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Rastreo.Web.Backend.Models
{
    public class Sale
    {
        public long SaleId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public decimal TaxIva { get; set; }
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
