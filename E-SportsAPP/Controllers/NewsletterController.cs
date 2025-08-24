using AutoMapper;
using E_SportsAPP.DTOs.Newsletter;
using E_SportsAPP.Models;
using E_SportsAPP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_SportsAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterRepository _newsletterRepository;
        private readonly IMapper _mapper;

        public NewsletterController(INewsletterRepository newsletterRepository, IMapper mapper)
        {
            _newsletterRepository = newsletterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsletterResponseDTO>>> GetAll()
        {
            var newsletters = await _newsletterRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<NewsletterResponseDTO>>(newsletters));
        }

        [HttpPost("Subscribe")]
        public async Task<ActionResult> Subscribe(CreateNewsletterDTO createNewsletter)
        {
            if (createNewsletter == null)
            {
                return BadRequest("Newsletter não pode ser nulo.");
            }

            var newsletter = _mapper.Map<Newsletter>(createNewsletter);
            await _newsletterRepository.AddAsync(newsletter);

            var newsletterResponse = _mapper.Map<NewsletterResponseDTO>(newsletter);
            return CreatedAtAction(nameof(GetAll), new { id = newsletterResponse.Id }, newsletterResponse);
        }
    }
}
