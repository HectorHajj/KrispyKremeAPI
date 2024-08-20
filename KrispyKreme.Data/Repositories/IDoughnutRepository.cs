using KrispyKreme.Data.Entities;

namespace KrispyKreme.Data.Repositories
{
    public interface IDoughnutRepository
    {
        Task<IEnumerable<Doughnut>> GetAllAsync();
        Task<Doughnut> GetByIdAsync(int id);
        Task AddAsync(Doughnut doughnut);
        Task UpdateAsync(Doughnut doughnut);
        Task DeleteAsync(int id);
    }
}
