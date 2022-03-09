using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Rastreo.Web.Backend.Models
{
    public class Package
    {
        public long PackageId { get; set; }
        public string? CodigoRastreo { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Dimentions { get; set; }
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Weigth { get; set; }
        public int PackagesTypeId { get; set; }
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
