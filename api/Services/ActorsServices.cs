
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
        public ActorsResponseDto Create(CreateActorsDto dto)
        {
            Actors actor = new Actors();

            actor.Name = dto.Name;
            actor.Country = dto.Country;
            actor.BirthYear = dto.BirthYear;

            Actors saved = _repository.Add(actor);

            return ToDto(saved);
        }

        public List<ActorsResponseDto> GetAll()
        {
            List<Actors> actors = _repository.GetAll();

            return actors.Select(ToDto).ToList();
        }

        public ActorsResponseDto? GetById(int id)
        {
           Actors? actor = _repository.GetById(id);

           if(actor is null)
            {
                return null;
            }

            return ToDto(actor);
        }

        public ActorsResponseDto? Update(int id, UpdateActorsDto dto)
        {
            Actors? actor = _repository.GetById(id);

            if(actor is null)
            {
                return null;
            }

            actor.Name = dto.Name;
            actor.Country = dto.Country;
            actor.BirthYear = dto.BirthYear;

            actor.UpdatedAt = DateTime.UtcNow;

            _repository.Update(actor);

            return ToDto(actor);
        }

        public bool Delete(int id)
        {
            Actors? actor = _repository.GetById(id);

            if(actor is null)
            {
                return false;
            }
             _repository.Delete(actor);
             
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