using E_SportsAPP.Data;
using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_SportsAPP.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;
        
        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task CreateNewsAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(int id, News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
        }
    }
}
