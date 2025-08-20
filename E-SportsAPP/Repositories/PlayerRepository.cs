using E_SportsAPP.Data;
using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_SportsAPP.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }
        public async Task<Player?> GetPlayerByNameAsync(string name)
        { 
                return await _context.Players
                .FirstOrDefaultAsync(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }
        public async Task AddPlayerAsync(Player player)
        {
           await _context.Players.AddAsync(player);
           await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayerAsync(int id, Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Player>> GetHighlightedPlayersByAsync()
        {
            return await _context.Players
                .Where(p => p.IsHighlighted)
                .ToListAsync();
        }
    }
}
