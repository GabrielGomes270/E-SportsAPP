using E_SportsAPP.Models;

namespace E_SportsAPP.Repositories
{
    public interface INewsletterRepository
    {
        Task <IEnumerable<Newsletter>> GetAllAsync();
        Task AddAsync (Newsletter newsletter);
    }
}
