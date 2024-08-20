using KrispyKreme.Data.Entities;
using KrispyKreme.Data.Persistance;
using Microsoft.EntityFrameworkCore;

namespace KrispyKreme.Data.Repositories.Implementation
{
    public class DoughnutRepository : IDoughnutRepository
    {
        private readonly KrispyDbContext _context;

        public DoughnutRepository(KrispyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Doughnut doughnut)
        {
            await _context.Doughnuts.AddAsync(doughnut);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doughnut = await _context.Doughnuts.FindAsync(id);
            if (doughnut != null)
            {
                _context.Doughnuts.Remove(doughnut);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doughnut>> GetAllAsync()
        {
            return await _context.Doughnuts.ToListAsync();
        }

        public async Task<Doughnut> GetByIdAsync(int id)
        {
            return await _context.Doughnuts.FindAsync(id);
        }

        public async Task UpdateAsync(Doughnut doughnut)
        {
            _context.Doughnuts.Update(doughnut);
            await _context.SaveChangesAsync();
        }
    }
}
