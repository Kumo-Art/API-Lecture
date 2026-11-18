
using api.Data;
using api.Models;


namespace api.Repositories
{
    public class ActorsRepository : IActorsRepository
    {

        private readonly AppDbContext _db;

        public ActorsRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Actors> GetAll()
        {
            return _db.Actors.ToList();
        }

        public Actors? GetById(int id)
        {
            return _db.Actors.Find(id);
        }
        public Actors Add(Actors actor)
        {
            _db.Actors.Add(actor);

            _db.SaveChanges();

            return actor;
        }

        public void Delete(Actors actor)
        {
            _db.Actors.Remove(actor);

            _db.SaveChanges();
        }

        

        public void Update(Actors actor)
        {
            _db.Actors.Update(actor);

            _db.SaveChanges();
        }
    }
}