using KrispyKreme.Application.DTO;

namespace KrispyKreme.Application.Services
{
    public interface IDoughnutService
    {
        Task AddDoughnutAsync(DoughnutDto doughnutDto);
        Task DeleteDoughnutAsync(int id);
        Task<IEnumerable<DoughnutDto>> GetAllDoughnutsAsync();
        Task<DoughnutDto> GetDoughnutByIdAsync(int id);
        Task UpdateDoughnutAsync(DoughnutDto doughnutDto);
    }
}
