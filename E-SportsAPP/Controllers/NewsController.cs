using AutoMapper;
using E_SportsAPP.Data;
using E_SportsAPP.DTOs.News;
using E_SportsAPP.Models;
using E_SportsAPP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_SportsAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        
        public NewsController(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsResponseDTO>>> GetAllNewsAsync()
        {
            var news = await _newsRepository.GetAllNewsAsync();
            return Ok(_mapper.Map<List<NewsResponseDTO>>(news));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsResponseDTO>> GetNewsByIdAsync(int id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<NewsResponseDTO>(news));
        }

        [HttpPost]
        public async Task<ActionResult<CreateNewsDTO>> CreateNewsAsync([FromBody] CreateNewsDTO createNews)
        {
            if (createNews == null)
            {
                return BadRequest("News não pode ser nulo.");
            }

            var news = _mapper.Map<News>(createNews);
            await _newsRepository.CreateNewsAsync(news);

            var newsResponse = _mapper.Map<NewsResponseDTO>(news);
            return CreatedAtAction(nameof(GetNewsByIdAsync), new { id = newsResponse.Id }, newsResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CreateNewsDTO>> UpdateNewsAsync(int id, [FromBody] CreateNewsDTO updateNewsDTO)
        {
            var existingNews = await _newsRepository.GetNewsByIdAsync(id);
            if (existingNews == null)
            {
                return NotFound();
            }
            _mapper.Map(updateNewsDTO, existingNews);
            await _newsRepository.UpdateNewsAsync(id, existingNews);
            return Ok(_mapper.Map<NewsResponseDTO>(existingNews));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsAsync(int id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            await _newsRepository.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
