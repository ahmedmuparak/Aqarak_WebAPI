using Aqarak_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aqarak_WebAPI.Aqarak.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly APIContext _context;

        public GovernorateController(APIContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var All = _context.Governorates.ToList();
            return Ok(All);
        }
    }
}
