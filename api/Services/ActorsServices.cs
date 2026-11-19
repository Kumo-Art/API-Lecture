
using api.Dtos;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class ActorsServices : IActorsServices
    {

        private readonly IActorsRepository _repository;

        public ActorsServices(IActorsRepository repository)
        {
            _repository = repository;
        }
        public async Task<ActorsResponseDto> CreateAsync(CreateActorsDto dto)
        {
            Actors actor = new Actors();

            actor.Name = dto.Name;
            actor.Country = dto.Country;
            actor.BirthYear = dto.BirthYear;

            Actors saved = await _repository.AddAsync(actor);

            return ToDto(saved);
        }

        public async Task<List<ActorsResponseDto>> GetAllAsync()
        {
            List<Actors> actors =  await _repository.GetAllAsync();

            return actors.Select(ToDto).ToList();
        }

        public async Task<ActorsResponseDto?> GetByIdAsync(int id)
        {
           Actors? actor = await _repository.GetByIdAsync(id);

           if(actor is null)
            {
                return null;
            }

            return ToDto(actor);
        }

        public async Task<ActorsResponseDto?> UpdateAsync(int id, UpdateActorsDto dto)
        {
            Actors? actor = await _repository.GetByIdAsync(id);

            if(actor is null)
            {
                return null;
            }

            actor.Name = dto.Name;
            actor.Country = dto.Country;
            actor.BirthYear = dto.BirthYear;

            actor.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(actor);

            return ToDto(actor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Actors? actor = await _repository.GetByIdAsync(id);

            if(actor is null)
            {
                return false;
            }
             await _repository.DeleteAsync(actor);
             
            return true;
        }


        private static ActorsResponseDto ToDto(Actors actor)
        {
            return new ActorsResponseDto
            {
                Id = actor.Id,
                Name = actor.Name,
                Country = actor.Country,
                BirthYear = actor.BirthYear
            };
        }
    }
}