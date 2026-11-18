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
        public MovieResponseDto Create(CreateMovieDto dto)
        {
            Movie movie = new Movie();
            movie.Title = dto.Title;
            movie.Director = dto.Director;
            movie.ReleaseYear = dto.ReleaseYear;
            movie.Rating = dto.Rating;

            Movie saved = _repository.Add(movie);
            //Sending back the same movie but now with an Id
            return ToDto(saved);
        }

        public List<MovieResponseDto> GetAll()
        {
          List<Movie> movies =  _repository.GetAll();
            
           //turning every element in this list into dto
            return movies.Select(ToDto).ToList();
        }

        public MovieResponseDto? GetById(int id)
        {
            Movie? movie = _repository.GetById(id);
            //If there is no movie pass null up to the controller
            if(movie is null)
            {
                return null;
            }
            return ToDto(movie);
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

        public MovieResponseDto? Update(int id, UpdateMovieDto dto)
        {
            //We must first load the movie from the database
            Movie? entity = _repository.GetById(id);
            
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
            
            _repository.Update(entity);

            return ToDto(entity);
        }

        public bool Delete(int id)
        {
            Movie? entity = _repository.GetById(id);
            if(entity is null)
            {
                //False tels our controller no movie was found -- send 404
                return false;
            }

            _repository.Delete(entity);
            return true;
        }
    }
}