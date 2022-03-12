using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paqueteria.Rastreo.Web.Backend.Data;
using Paqueteria.Rastreo.Web.Backend.Models;

namespace Paqueteria.Rastreo.Web.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaquetesController : ControllerBase
    {
        private readonly PaqueteriaContext _context;
        public PaquetesController(PaqueteriaContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(long id)
        {
            var _sale = await _context.Sales.FindAsync(id);
            if (_sale == null)
                return NotFound();
            return _sale;
        }
        [HttpGet("Seguimiento/{Track}")]
        public async Task<ActionResult> GetTrack(string Track)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-eu.dhl.com/track/shipments?trackingNumber={Track}"),
                Headers = {
                    { "DHL-API-Key", "LJhPQQ1e1M7r3hVAtwpfQZGn83ISiGVK" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);
                return Ok(body);
            }
        }
        [HttpGet]
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale _model)
        {
            if (_model == null)
                return BadRequest();
            await _context.Sales.AddAsync(_model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = _model.SaleId }, _model);
        }
        [HttpPut]
        public async Task<ActionResult<Sale>> PutSale(long id, Sale body)
        {
            if (id != body.SaleId)
                return BadRequest(); //400
            _context.Entry(body).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Sales.AnyAsync(e => e.SaleId == id))
                    return NotFound(); //404
                else
                    throw; //500
            }
            return NoContent(); //204 se usa regularmente con los puts, las ediciones
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sale>> DeleteSale(long id)
        {
            var _sale = await _context.Sales.FindAsync(id);
            if (_sale == null)
                return NotFound();
            _context.Sales.Remove(_sale);
            await _context.SaveChangesAsync();
            return _sale;
        }
    }
}
