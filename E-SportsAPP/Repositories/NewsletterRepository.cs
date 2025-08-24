using E_SportsAPP.Data;
using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_SportsAPP.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {

        private readonly AppDbContext _context;

        public NewsletterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Newsletter>> GetAllAsync()
        {
            return await _context.Newsletters.ToListAsync();
        }
        public async Task AddAsync(Newsletter newsletter)
        {
            await _context.Newsletters.AddAsync(newsletter);
            await _context.SaveChangesAsync();
        }

    }
}
