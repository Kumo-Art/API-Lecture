//Our Services layer is our midle man between our controller and repository
//It maps our DTOs to Entities (and back) And this is where our rules live
using api.Dtos;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class MovieServices : IMovieServices
    {

        private readonly IMovieRepository _repository;

        //DI:
        public MovieServices(IMovieRepository repository)
        {
            _repository = repository;
        }
        public async Task<MovieResponseDto?> CreateAsync(CreateMovieDto dto)
        {

          List<Movie> movies = await _repository.GetAllAsync();

        bool exist = movies.Any(m => m.Title.ToLower() == dto.Title.ToLower());

        if (exist)
            {
                return null;
            }

            Movie movie = new Movie();
            movie.Title = dto.Title;
            movie.Director = dto.Director;
            movie.ReleaseYear = dto.ReleaseYear;
            movie.Rating = dto.Rating;

            Movie saved = await _repository.AddAsync(movie);
            //Sending back the same movie but now with an Id
            return ToDto(saved);
        }

        //  public async Task<bool> Exists(CreateMovieDto dto)
        // {
        //     List<Movie> movies = await _repository.GetAllAsync();

        //     bool exist = movies.Any(m => m.Title.ToLower() == dto.Title.ToLower());

        //     return exist;
        // }

        //This method awaits the repository layer, so it HAS to be async and return a Task Too.
        //Once the bottom layer is async, every layer that calls it becomes async as well
        public async Task<List<MovieResponseDto>> GetAllAsync()
        {
            // await waits for the database without blocking the thread.
          List<Movie> movies = await  _repository.GetAllAsync();
            
           //turning every Movie entity into a MovieResponseDto
            return movies.Select(ToDto).ToList();
        }

        public async Task<MovieResponseDto?> GetByIdAsync(int id)
        {
            Movie? movie = await _repository.GetByIdAsync(id);
            //If there is no movie pass null up to the controller
            if(movie is null)
            {
                return null;
            }
            return ToDto(movie);
        }

       
        public async Task<MovieResponseDto?> UpdateAsync(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? entity = await _repository.GetByIdAsync(id);
            
            // if our id isnt in database we return null
            if(entity is null)
            {
                return null;
            }

            entity.Title = dto.Title;
            entity.Director = dto.Director;
            entity.Rating = dto.Rating;
            entity.ReleaseYear = dto.ReleaseYear;

            //Record when this entity was last changed
            entity.UpdatedAt = DateTime.UtcNow;
            
             await _repository.UpdateAsync(entity);

            return ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Movie? entity = await _repository.GetByIdAsync(id);
            if(entity is null)
            {
                //False tels our controller no movie was found -- send 404
                return false;
            }

             await _repository.DeleteAsync(entity);
            return true;
        }

         //We will create helper function that maps our Dtos
        //private because it doesn't need to be accessed outside of our class
        //static because it doesn't have access to instance data
        private static MovieResponseDto ToDto(Movie movie)
        {
            // long way Dto helper function
            // MovieResponseDto dto = new MovieResponseDto();
            // dto.Id = movie.Id;
            // dto.Director = movie.Director;
            // dto.Rating = movie.Rating;
            // dto.ReleaseYear = movie.ReleaseYear;
            // dto.Title = movie.Title;

            // return dto;

           // short way Dto helper function
            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Rating = movie.Rating,
                ReleaseYear = movie.ReleaseYear
            };
        }

    }
}