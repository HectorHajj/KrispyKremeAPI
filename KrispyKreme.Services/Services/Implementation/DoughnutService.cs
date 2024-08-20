using KrispyKreme.Application.DTO;
using KrispyKreme.Data.Entities;
using KrispyKreme.Data.Repositories;

namespace KrispyKreme.Application.Services.Implementation
{
    public class DoughnutService : IDoughnutService
    {
        private readonly IDoughnutRepository _doughnutRepository;

        public DoughnutService(IDoughnutRepository doughnutRepository)
        {
            _doughnutRepository = doughnutRepository;
        }

        public async Task AddDoughnutAsync(DoughnutDto doughnutDto)
        {
            var doughnut = new Doughnut
            {
                Name = doughnutDto.Name
            };

            await _doughnutRepository.AddAsync(doughnut);
        }

        public async Task DeleteDoughnutAsync(int id)
        {
            await _doughnutRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DoughnutDto>> GetAllDoughnutsAsync()
        {
            var doughnuts = await _doughnutRepository.GetAllAsync();
            return doughnuts.Select(d => new DoughnutDto
            {
                Name = d.Name
            });
        }

        public async Task<DoughnutDto> GetDoughnutByIdAsync(int id)
        {
            var doughnut = await _doughnutRepository.GetByIdAsync(id);
            if (doughnut == null)
            {
                throw new KeyNotFoundException($"Doughnut with ID {id} not found.");
            }

            return new DoughnutDto
            {
                Name = doughnut.Name
            };
        }

        public async Task UpdateDoughnutAsync(DoughnutDto doughnutDto)
        {
            var doughnut = new Doughnut
            {
                Name = doughnutDto.Name
            };

            await _doughnutRepository.UpdateAsync(doughnut);
        }
    }
}
