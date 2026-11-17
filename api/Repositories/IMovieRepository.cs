
using api.Models;

namespace api.Repositories
{


    //The repository interface: the list of database jobs for movies.
    //Repositories work with entities (Movie), NEVER DTOs
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie? GetById(int id);
        //Returns one movie, or null if no movie has that id (That's what the ? means).

        Movie Add(Movie movie);
    }
}