using KrispyKreme.Data.Entities;
using KrispyKreme.Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KrispyKreme.Data.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly KrispyDbContext _context;

        public CustomerRepository(KrispyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity)
        {
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Customer with id {id} not found.");

            return entity;
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (entity == null)
                throw new KeyNotFoundException($"Customer with email {email} not found.");

            return entity;
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
