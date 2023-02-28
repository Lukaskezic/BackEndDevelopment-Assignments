using Microsoft.AspNetCore.Mvc;
using Assignment4_2.Services;

namespace Assignment4_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : Controller
    {
        private readonly SetsService _setsServices;
        private readonly ILogger<CardsController> _logger;

        public SetsController(SetsService sets, ILogger<CardsController> logger)
        {
            _setsServices = sets;
            _logger = logger;
        }

        [HttpGet("/sets")]
        public async Task<IActionResult> GetSets()
        {
            var result = await _setsServices.GetSets();

            _logger.LogInformation("Requested endpoint: /sets ");

            return Ok(result);
        }
    }
}