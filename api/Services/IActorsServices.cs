
using System.Runtime.InteropServices;
using api.Dtos;

namespace api.Services
{
    public interface IActorsServices
    {
        List<ActorsResponseDto> GetAll();

        ActorsResponseDto? GetById(int id);

        ActorsResponseDto Create(CreateActorsDto dto);

        ActorsResponseDto? Update(int id, UpdateActorsDto dto);

        bool Delete(int id);
    }
}