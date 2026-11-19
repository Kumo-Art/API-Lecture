
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repositories
{
    public class ActorsRepository : IActorsRepository
    {

        private readonly AppDbContext _db;

        public ActorsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Actors>> GetAllAsync()
        {
            return await _db.Actors.ToListAsync();
        }

        public async Task<Actors?> GetByIdAsync(int id)
        {
            return await _db.Actors.FindAsync(id);
        }
        public async Task<Actors> AddAsync(Actors actor)
        {
            _db.Actors.Add(actor);

            await _db.SaveChangesAsync();

            return actor;
        }

        public async Task DeleteAsync(Actors actor)
        {
            _db.Actors.Remove(actor);

            await _db.SaveChangesAsync();
        }

        

        public async Task UpdateAsync(Actors actor)
        {
            _db.Actors.Update(actor);

           await _db.SaveChangesAsync();
        }
    }
}