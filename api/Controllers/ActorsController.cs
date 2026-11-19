
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

        public ActionResult<List<ActorsResponseDto>> GetAll()
        {
            return Ok(_services.GetAll());
        }

        [HttpGet("GetById/{id}")]

        public ActionResult<ActorsResponseDto> GetById(int id)
        {
            ActorsResponseDto? actors = _services.GetById(id);

            if(actors is null)
            {
                return NotFound();
            }

            return Ok(actors);
        }

        [HttpPost("AddActor")]

        public ActionResult<ActorsResponseDto> AddActor([FromBody] CreateActorsDto dto)
        {
            ActorsResponseDto actor = _services.Create(dto);

            return CreatedAtAction(nameof(GetById), new {id = actor.Id}, actor);
        }

        [HttpPut("Update/{id}")]

        public ActionResult<ActorsResponseDto> Update(int id, [FromBody] UpdateActorsDto dto)
        {
            ActorsResponseDto? actor = _services.Update(id, dto);

            if(actor is null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
             bool isDeleted = _services.Delete(id);

             if(!isDeleted)
            {
                return NotFound();
            }
           
           
            return NoContent();
        }
    }
}