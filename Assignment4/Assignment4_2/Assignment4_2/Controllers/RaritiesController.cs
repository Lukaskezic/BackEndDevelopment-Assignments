using Microsoft.AspNetCore.Mvc;
using Assignment4_2.Services;

namespace Assignment4_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaritiesController : Controller
    {
        private readonly RaritiesService _raritiesServices;
        private readonly ILogger<CardsController> _logger;

        public RaritiesController(RaritiesService rarities, ILogger<CardsController> logger)
        {
            _raritiesServices = rarities;
            _logger = logger;
        }

        [HttpGet("/rarities")]
        public async Task<IActionResult> GetRarities()
        {
            var result = await _raritiesServices.GetRaritiesAll();

            _logger.LogInformation("Requested endpoint: /rarites ");

            return Ok(result);
        }
    }
}