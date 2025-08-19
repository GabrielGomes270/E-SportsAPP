using E_SportsAPP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_SportsAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearController : ControllerBase
    {
        private readonly IGearRepository _gearRepository;

        public GearController(IGearRepository gearRepository)
        {
            _gearRepository = gearRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGearsByPlayerId(int playerId)
        {
            var gears = await _gearRepository.GetGearsByPlayerIdAsync(playerId);
            return Ok(gears);
        }
    }
}
