
using System.Runtime.InteropServices;
using api.Dtos;

namespace api.Services
{
    public interface IActorsServices
    {
        Task<List<ActorsResponseDto>> GetAllAsync();

        Task<ActorsResponseDto?> GetByIdAsync(int id);

        Task<ActorsResponseDto> CreateAsync(CreateActorsDto dto);

        Task<ActorsResponseDto?> UpdateAsync(int id, UpdateActorsDto dto);

        Task<bool> DeleteAsync(int id);
    }
}