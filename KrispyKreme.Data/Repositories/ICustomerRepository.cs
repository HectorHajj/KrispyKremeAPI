using KrispyKreme.Data.Entities;
using System.Linq.Expressions;

namespace KrispyKreme.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate);
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(int id);
        Task<Customer> GetByEmailAsync(string email);
    }
}
