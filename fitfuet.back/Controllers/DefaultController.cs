using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fitfuet.back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // GET: api/<DefaultController>
        [HttpGet]
        public string Get()
        {
            return "Aplicacion corriendo..";
        }
    }
}
