namespace Paqueteria.Rastreo.Web.Backend.Models
{
    public class Paquete
    {
        public long PackageId { get; set; }
        public string? Codigo { get; set; }
        public decimal Dimentions { get; set; }
        public decimal Weigth { get; set; }
        public int PackagesTypeId { get; set; }
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
