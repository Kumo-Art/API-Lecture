
using api.Models;

namespace api.Repositories
{
    public interface IActorsRepository
    {
        List<Actors> GetAll();

        Actors? GetById(int id);

        Actors Add(Actors actor);

        void Update(Actors actor);

        void Delete(Actors actor);
    }
}