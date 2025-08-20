using AutoMapper;
using E_SportsAPP.DTOs.Gear;
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

        [HttpGet("GearsByPlayer")]
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

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadGearImage(int id, IFormFile file)
        {
            var gear= await _gearRepository.GetGearByIdAsync(id);
            if (gear == null)
            {
                return NotFound("Gear não encontrado.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "gears");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/images/gears/{fileName}";

            await _gearRepository.UpdateGearImageAsync(id, imageUrl);
            return Ok(new { message = "Imagem salva com sucesso!", imageUrl });
        }
    }
}
