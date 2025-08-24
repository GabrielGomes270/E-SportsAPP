using E_SportsAPP.Models;

namespace E_SportsAPP.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<News?> GetNewsByIdAsync(int id);
        Task CreateNewsAsync(News news);
        Task UpdateNewsAsync(int id, News news);
        Task DeleteNewsAsync(int id);
    }
}
