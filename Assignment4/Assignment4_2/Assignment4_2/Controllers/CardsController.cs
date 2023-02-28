using Microsoft.AspNetCore.Mvc;
using Assignment4_2.Models;
using Assignment4_2.Services;
using AutoMapper;

namespace Assignment4_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardsService _cardsServices;
        private readonly CardtypesService _cardtypesServices;
        private readonly ClassesService _classesServices;
        private readonly RaritiesService _raritiesServices;
        private readonly SetsService _setsServices;
        private readonly ILogger<CardsController> _logger;
        private readonly IMapper _mapper;

        public CardsController(CardsService cards, CardtypesService cardtypes, ClassesService classes, RaritiesService rarities, SetsService sets, ILogger<CardsController> logger, IMapper mapper)
        {
            _cardsServices = cards;
            _cardtypesServices = cardtypes;
            _classesServices = classes;
            _raritiesServices = rarities;
            _setsServices = sets;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("/cards")]
        public async Task<IActionResult> GetCards(int? page = 1, int? setid = null, string? artistName = null, int? classid = null, int? rarityid = null)
        {
            var cards = await _cardsServices.GetCard(page, setid, artistName, classid, rarityid);
            int i = 0;
            var mapCardMap = _mapper.Map<IList<Card>, IList<MapCard>>(cards);
            while (i < mapCardMap.Count)
            {
                mapCardMap[i].Class = await _classesServices.GetClass(cards[i].ClassId);
                mapCardMap[i].Set = await _setsServices.GetSet(cards[i].cardSetId);
                mapCardMap[i].Cardtype = await _cardtypesServices.GetCardType(cards[i].cardTypeId);
                mapCardMap[i].Rarity = await _raritiesServices.GetRarity(cards[i].RarityId);
                i++;
            }
            _logger.LogInformation("Requested endpoint: /cards");

            return Ok(mapCardMap);
        }
    }
}