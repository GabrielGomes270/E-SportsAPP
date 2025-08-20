using E_SportsAPP.Data;
using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_SportsAPP.Repositories
{
    public class GearRepository : IGearRepository
    {

        private readonly AppDbContext _context;

        public GearRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gear>> GetAllGearsAsync()
        {
            return await _context.Gears.ToListAsync();
        }
        public async Task<IEnumerable<Gear>> GetGearsByPlayerIdAsync(int playerId)
        {
            return await _context.Gears
                   .Where(g => g.PlayerId == playerId)
                   .ToListAsync();
        }
        public async Task<Gear?> GetGearByIdAsync(int id)
        {
            return await _context.Gears.FindAsync(id);
        }
        public Task<Gear?> GetGearByNameAsync(string name)
        {
            return _context.Gears
                   .FirstOrDefaultAsync(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public async Task AddGearAsync(Gear gear)
        {
            await _context.Gears.AddAsync(gear);
        }

        public async Task UpdateGearAsync(Gear gear)
        {
            _context.Gears.Update(gear);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGearAsync(int id)
        {
            var gear = _context.Gears.Find(id);
            if (gear != null)
            {
                _context.Gears.Remove(gear);
               await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateGearImageAsync(int GearId, string imageUrl)
        {
            var gear = await _context.Gears.FindAsync(GearId);
            if (gear == null) return;

            gear.ImageUrl = imageUrl;
            await _context.SaveChangesAsync();
        }
    }
}
