
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

        //async Task<...>: the action awaits the service, so it returns a task
        //ASP.NET Core awaits the Task for us, then sends the response.

        [HttpGet]
        public async Task<ActionResult<List<MovieResponseDto>>> GetAll()
        {

            //await frees this thread while the database works,
            // so the server can handle other requests in the meantime
            return Ok(await _services.GetAllAsync());
        }

        //Get api/movie/getbyid/5
        //return 200 with the movie, or 400 if no movie is in database
        [HttpGet("GetById/{id}")]

        public async Task<ActionResult<MovieResponseDto>> GetById(int id)
        {
            MovieResponseDto? movie = await _services.GetByIdAsync(id);

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
        public async Task<ActionResult<MovieResponseDto>> AddMovie([FromBody] CreateMovieDto dto)
        {
            // if(await _services.Exists(dto))
            // {
            //     return Conflict("The movie with that title already exists.");
            // }

            MovieResponseDto? movie = await _services.CreateAsync(dto);

            if(movie is null)
            {
                return Conflict(new { status = "The movie with that title already exists."});
            }
             
            //CreatedAtAction sends 201, It uses GetById action and the new Id
            //To build the location headers
            return CreatedAtAction(nameof(GetById), new {id = movie.Id}, movie);
        }

        [HttpPut("Update/{id}")]

        public async Task<ActionResult<MovieResponseDto>> Update(int id, [FromBody] UpdateMovieDto dto)
        {
            MovieResponseDto? movie = await _services.UpdateAsync(id, dto);

            if(movie is null)
            {
                return NotFound(); //Movie is not found in database  -- send a 404
            }

            return Ok(movie);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             bool isDeleted = await _services.DeleteAsync(id);

             if(!isDeleted)
            {
                return NotFound();
            }
           
           //This worked but nothing to send back -- sends back a 204 status code
            return NoContent();
        }
    }
}
