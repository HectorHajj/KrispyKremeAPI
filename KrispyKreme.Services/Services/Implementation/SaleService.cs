using KrispyKreme.Application.DTO.SaleDTO;
using KrispyKreme.Data.Entities;
using KrispyKreme.Data.Repositories;

namespace KrispyKreme.Application.Services.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDoughnutRepository _doughnutRepository;

        public SaleService(ISaleRepository saleRepository, ICustomerRepository customerRepository, IDoughnutRepository doughnutRepository)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _doughnutRepository = doughnutRepository;
        }

        public async Task<SaleDto> AddSaleAsync(CreateSaleDto saleDto)
        {
            var customer = await _customerRepository.GetByIdAsync(saleDto.CustomerId);
            var doughnut = await _doughnutRepository.GetByIdAsync(saleDto.DoughnutId);

            if (customer == null || doughnut == null)
            {
                throw new ArgumentException("Invalid CustomerId or DoughnutId");
            }

            var sale = new Sale
            {
                CustomerId = saleDto.CustomerId,
                DoughnutId = saleDto.DoughnutId,
                SaleDate = saleDto.SaleDate,
                Quantity = saleDto.Quantity,
                Customer = customer,
                Doughnut = doughnut
            };

            var createdSale = await _saleRepository.AddAsync(sale);

            return new SaleDto
            {
                CustomerName = createdSale.Customer.Name,
                CustomerAddress = createdSale.Customer.Address,
                DoughnutName = createdSale.Doughnut.Name,
                Quantity = createdSale.Quantity,
                SaleDate = createdSale.SaleDate
            };
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GetSaleDto>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return sales.Select(s => new GetSaleDto
            {
                Id = s.Id,
                CustomerName = s.Customer.Name,
                CustomerAddress = s.Customer.Address,
                DoughnutName = s.Doughnut.Name,
                Quantity = s.Quantity,
                SaleDate = s.SaleDate
            });
        }

        public async Task<GetSaleDto> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {id} not found.");
            }

            return new GetSaleDto
            {
                CustomerName = sale.Customer.Name,
                CustomerAddress = sale.Customer.Address,
                DoughnutName = sale.Doughnut.Name,
                Quantity = sale.Quantity,
                SaleDate = sale.SaleDate
            };
        }

        public async Task UpdateSaleAsync(SaleDto saleDto)
        {
            var customer = await _customerRepository.GetByIdAsync(saleDto.CustomerId);
            var doughnut = await _doughnutRepository.GetByIdAsync(saleDto.DoughnutId);

            if (customer == null || doughnut == null)
            {
                throw new ArgumentException("Invalid CustomerId or DoughnutId");
            }

            var sale = new Sale
            {
                SaleDate = saleDto.SaleDate,
                Quantity = saleDto.Quantity,
                Customer = customer,
                Doughnut = doughnut
            };

            await _saleRepository.UpdateAsync(sale);
        }
    }
}
