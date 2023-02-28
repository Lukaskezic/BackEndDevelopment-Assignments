using Microsoft.AspNetCore.Mvc;
using Assignment4_2.Services;

namespace Assignment4_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardtypesController : Controller
    {
        private readonly CardtypesService _cardtypesServices;
        private readonly ILogger<CardsController> _logger;

        public CardtypesController(CardtypesService cardtypes, ILogger<CardsController> logger)
        {
            _cardtypesServices = cardtypes;
            _logger = logger;
        }

        [HttpGet("/types")]
        public async Task<IActionResult> GetCardTypes()
        {
            var result = await _cardtypesServices.GetCardTypeAll();

            _logger.LogInformation("Requested endpoint: /types ");

            return Ok(result);
        }
    }
}