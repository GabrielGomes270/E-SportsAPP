using E_SportsAPP.Models;

namespace E_SportsAPP.Repositories
{
    public interface IPlayerRepository 
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<IEnumerable<Player>> GetHighlightedPlayersByAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task <List<Player>> GetPlayerByNameAsync(string name);
        Task <IEnumerable<Player>> GetPlayersByRoleAsync(string role);
        Task AddPlayerAsync(Player player);
        Task UpdatePlayerAsync(int id, Player player);
        Task DeletePlayerAsync(int id);

        Task UpdatePlayerImageAsync(int id, string imageUrl);
    }
}
