
using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

//Our Controller Layer: Takes in HTTP Requests and Sends back HTTP Response.
//It only talks to our Services, Never to our DB.

namespace api.Controllers
{
    [ApiController] //This line turns on our API Features like Automatic 400 status code
    [Route("api/[controller]")] //Makes every url endpoint start with api/movie
    public class MovieController : ControllerBase
    {


        private readonly IMovieServices _services;

        public MovieController(IMovieServices services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<List<MovieResponseDto>> GetAll()
        {
            return Ok(_services.GetAll());
        }

        //Get api/movie/getbyid/5
        //return 200 with the movie, or 400 if no movie is in database
        [HttpGet("GetById/{id}")]

        public ActionResult<MovieResponseDto> GetById(int id)
        {
            MovieResponseDto? movie = _services.GetById(id);

            if (movie is null)
            {
                return NotFound(); // sends a 404 response
            }

            return Ok(movie);
        }
        //Post api/movie/addmovie
        //JSON body becomes our CreateMovieDto
        //return 201 created with the new movie, plus our location headers.
        [HttpPost("AddMovie")]
        public ActionResult<MovieResponseDto> AddMovie([FromBody] CreateMovieDto dto)
        {
            MovieResponseDto movie = _services.Create(dto);
             
            //CreatedAtAction
            return CreatedAtAction(nameof(GetById), new {id = movie.Id}, movie);
        }
    }
}
