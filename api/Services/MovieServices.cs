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
    }
}