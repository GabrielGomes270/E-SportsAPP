using AutoMapper;
using E_SportsAPP.DTOs.Player;
using E_SportsAPP.Models;
using E_SportsAPP.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_SportsAPP.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerResponseDTO>>> GetAllPlayers()
        {
            var players = await _playerRepository.GetAllPlayersAsync();
            return Ok(_mapper.Map<IEnumerable<PlayerResponseDTO>>(players));
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<PlayerDetailDTO>> GetPlayerById(int id)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlayerDetailDTO>(player));
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<PlayerResponseDTO>>> GetPlayersByName(string name)
        {
            var player = await _playerRepository.GetPlayersByNameAsync(name);
            if (player == null)
            {
                return NotFound();
            }

             return Ok(_mapper.Map<IEnumerable<PlayerResponseDTO>>(player));
        }


        [HttpPost]
        public async Task<ActionResult<PlayerResponseDTO>> CreatePlayer([FromBody] CreatePlayerDTO createPlayer)
        {
            if (createPlayer == null)
            {
                return BadRequest("Player não pode ser nulo.");
            }

            var player = _mapper.Map<Player>(createPlayer);
            await _playerRepository.AddPlayerAsync(player);

            var response = _mapper.Map<PlayerResponseDTO>(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerResponseDTO>> UpdatePlayer(int id, [FromBody] UpdatePlayerDTO updatePlayer)
        {
            var existingPlayer = await _playerRepository.GetPlayerByIdAsync(id);
            if (existingPlayer == null)
            {
                return NotFound();
            }

            _mapper.Map(updatePlayer, existingPlayer);

            await _playerRepository.UpdatePlayerAsync(id, existingPlayer);
            return Ok(_mapper.Map<PlayerResponseDTO>(existingPlayer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            await _playerRepository.DeletePlayerAsync(id);
            return NoContent();
        }

        [HttpGet("Highlights")]
        public async Task<ActionResult<IEnumerable<PlayerHighlightDTO>>> GetPlayerHighlights()
        {
            var players = await _playerRepository.GetAllPlayersAsync();

            var roleOrder = new Dictionary<string, int>
            {
                { "Top-Laner", 1 },
                { "Jungle", 2 },
                { "Mid-Laner", 3 },
                { "Bot-Laner", 4 },
                { "Support", 5 }
            };

            var highlights = players
                .Where(p => p.IsHighlighted)
                .OrderBy(p => roleOrder.ContainsKey(p.Role) ? roleOrder[p.Role] : int.MaxValue)
                .ToList();

            return Ok(_mapper.Map<IEnumerable<PlayerHighlightDTO>>(highlights));
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadPlayerImage(int id, IFormFile file)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound("Jogador não encontrado.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Arquivo inválido.");
            }

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", "players");
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

            var imageUrl = $"/images/players/{fileName}";

            await _playerRepository.UpdatePlayerImageAsync(id, imageUrl);
            return Ok(new { message = "Imagem salva com sucesso!", imageUrl });
        }

        [HttpPatch("{id}/highlight")]
        public async Task<IActionResult> ToggleHighlight(int id, [FromQuery] bool isHighlighted)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound("Jogador não encontrado.");
            }

            if (isHighlighted)
            {
                var playersWithSameRole = await _playerRepository.GetPlayersByRoleAsync(player.Role);

                foreach (var p in playersWithSameRole.Where(p => p.Id != id))
                {
                    if (p.IsHighlighted)
                    {
                        p.IsHighlighted = false;
                        await _playerRepository.UpdatePlayerAsync(p.Id, p);
                    }
                }
            }

            player.IsHighlighted = isHighlighted;
            await _playerRepository.UpdatePlayerAsync(id, player);

            return Ok(new { message = isHighlighted ? "Jogador destacado com sucesso!" : "Jogador não destacado." });
        }
    }
}
