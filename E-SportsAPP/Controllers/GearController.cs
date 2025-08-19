using AutoMapper;
using E_SportsAPP.DTOs;
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
        private readonly IMapper _mapper;

        public GearController(IGearRepository gearRepository, IMapper mapper)
        {
            _gearRepository = gearRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GearResponseDTO>> GetGearsByPlayerId(int playerId)
        {
            var gears = await _gearRepository.GetGearsByPlayerIdAsync(playerId);
            return Ok(_mapper.Map<GearResponseDTO>(gears));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GearDetailDTO>> GetGearById(int id)
        {
            var gear = await _gearRepository.GetGearByIdAsync(id);
            if (gear == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GearDetailDTO>(gear));
        }
    }
}
