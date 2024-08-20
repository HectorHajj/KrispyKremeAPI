using KrispyKreme.Application.DTO.SaleDTO;

namespace KrispyKreme.Application.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<GetSaleDto>> GetAllSalesAsync();
        Task<GetSaleDto> GetSaleByIdAsync(int id);
        Task<SaleDto> AddSaleAsync(CreateSaleDto saleDto);
        Task UpdateSaleAsync(SaleDto saleDto);
        Task DeleteSaleAsync(int id);
    }
}
