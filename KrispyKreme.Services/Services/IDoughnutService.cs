using KrispyKreme.Application.DTO.DoughnutDTO;

namespace KrispyKreme.Application.Services
{
    public interface IDoughnutService
    {
        Task AddDoughnutAsync(DoughnutDto doughnutDto);
        Task DeleteDoughnutAsync(int id);
        Task<IEnumerable<GetDoughnutDto>> GetAllDoughnutsAsync();
        Task<DoughnutDto> GetDoughnutByIdAsync(int id);
        Task UpdateDoughnutAsync(DoughnutDto doughnutDto);
    }
}
