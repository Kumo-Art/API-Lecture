//This Services interface is a list of jobs that our services must implament
// Our Services speak in DTO, So the controller never touches the Entity.
using api.Dtos;

namespace api.Services
{
    public interface IMovieServices
    {
        List<MovieResponseDto> GetAll();

        MovieResponseDto? GetById(int id);

        MovieResponseDto Create(CreateMovieDto dto);
    }
}