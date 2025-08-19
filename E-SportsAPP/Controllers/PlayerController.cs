using AutoMapper;
using E_SportsAPP.DTOs;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDetailDTO>> GetPlayerById(int id)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlayerDetailDTO>(player));
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<PlayerResponseDTO>> GetPlayerByName(string name)
        {
            var player = await _playerRepository.GetPlayerByNameAsync(name);
            if (player == null)
            {
                return NotFound();
            }

             return Ok(_mapper.Map<PlayerResponseDTO>(player));
        }


        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer(Player player)
        {
            if (player == null)
            {
                return BadRequest("Player não pode ser nulo.");
            }
            await _playerRepository.AddPlayerAsync(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest("ID do jogador não corresponde.");
            }
            var existingPlayer = await _playerRepository.GetPlayerByIdAsync(id);
            if (existingPlayer == null)
            {
                return NotFound();
            }
            await _playerRepository.UpdatePlayerAsync(id, player);
            return NoContent();
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
            var highlights = _mapper.Map<IEnumerable<PlayerHighlightDTO>>(players);
            return Ok(highlights);
        }
    }
}
