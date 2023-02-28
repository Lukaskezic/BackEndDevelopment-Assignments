using Microsoft.AspNetCore.Mvc;
using Assignment4_2.Services;

namespace Assignment4_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : Controller
    {
        private readonly ClassesService _classesServices;
        private readonly ILogger<CardsController> _logger;

        public ClassesController(ClassesService classes, ILogger<CardsController> logger)
        {
            _classesServices = classes;
            _logger = logger;
        }

        [HttpGet("/classes")]
        public async Task<IActionResult> GetClasses()
        {
            var result = await _classesServices.GetClassAll();

            _logger.LogInformation("Requested endpoint: /classes ");

            return Ok(result);
        }
    }
}