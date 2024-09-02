using KrispyKreme.Application.DTO;
using KrispyKreme.Data.Entities;
using KrispyKreme.Data.Repositories;

namespace KrispyKreme.Application.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> AddCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    Name = customerDto.Name,
                    Email = customerDto.Email,
                    Address = customerDto.Address,
                    Password = customerDto.Password
                };

                await _customerRepository.AddAsync(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Password = c.Password
            });
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }

            return new CustomerDto
            {
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                Password = customer.Password
            };
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Address = customerDto.Address,
                Password = customerDto.Password
            };

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task<CustomerDto> GetCustomerByEmail(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                Password = customer.Password
            };
        }
    }
}
