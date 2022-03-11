using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Rastreo.Web.Backend.Data;
using Paqueteria.Rastreo.Web.Backend.Models;

namespace Paqueteria.Rastreo.Web.Backend.Areas.FirstContact
{
    [Area("FirstContact")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly PaqueteriaContext _context;
        public SalesController(PaqueteriaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }
    }
}
