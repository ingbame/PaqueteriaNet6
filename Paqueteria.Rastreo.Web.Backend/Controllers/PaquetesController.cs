using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Paqueteria.Rastreo.Web.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaquetesController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok(); // Devuelve status 200
        }
    }
}
