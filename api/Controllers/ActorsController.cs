
using api.Dtos;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsServices _services;

        public ActorsController(IActorsServices services)
        {
            _services = services;
        }

        [HttpGet]

        public async Task<ActionResult<List<ActorsResponseDto>>> GetAll()
        {
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]

        public async Task<ActionResult<ActorsResponseDto>> GetById(int id)
        {
            ActorsResponseDto? actors = await _services.GetByIdAsync(id);

            if(actors is null)
            {
                return NotFound();
            }

            return Ok(actors);
        }

        [HttpPost("AddActor")]

        public async Task<ActionResult<ActorsResponseDto>> AddActor([FromBody] CreateActorsDto dto)
        {
            ActorsResponseDto actor = await _services.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor);
        }

        [HttpPut("Update/{id}")]

        public async Task<ActionResult<ActorsResponseDto>> Update(int id, [FromBody] UpdateActorsDto dto)
        {
            ActorsResponseDto? actor = await _services.UpdateAsync(id, dto);

            if(actor is null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
             bool isDeleted = await _services.DeleteAsync(id);

             if(!isDeleted)
            {
                return NotFound();
            }
           
           
            return NoContent();
        }
    }
}