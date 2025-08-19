using E_SportsAPP.Models;

namespace E_SportsAPP.Repositories
{
    public interface IGearRepository
    {
        Task<IEnumerable<Gear>> GetAllGearsAsync();
        Task<Gear?> GetGearByIdAsync(int id);
        Task<IEnumerable<Gear>> GetGearsByPlayerIdAsync(int playerId);
        Task AddGearAsync(Gear gear);
        Task UpdateGearAsync(Gear gear);
        Task DeleteGearAsync(int id);
        Task<Gear?> GetGearByNameAsync(string name);
    }
}
