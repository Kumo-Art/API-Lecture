
using api.Models;

namespace api.Repositories
{
    public interface IActorsRepository
    {
        Task<List<Actors>> GetAllAsync();

        Task<Actors?> GetByIdAsync(int id);

        Task<Actors> AddAsync(Actors actor);

        Task UpdateAsync(Actors actor);

        Task DeleteAsync(Actors actor);
    }
}