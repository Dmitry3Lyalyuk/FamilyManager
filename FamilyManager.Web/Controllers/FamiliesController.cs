using FamilyManager.Application.Families.Commands;
using FamilyManager.Application.Families.Querries;
using FamilyManager.Application.Familys.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FamiliesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<FamilyDTO>>> GetAllFamilies()
        {
            var query = new GetAllFamiliesQuery();
            var families = await _mediator.Send(query);
            if (families is null or [])
            {
                return NotFound("No user found!");
            }
            return Ok(families);

        }
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateFamily([FromBody] CreateFamilyCommand command)
        {
            try
            {
                var familyId = await _mediator.Send(command);
                if (familyId == Guid.Empty)
                {
                    return BadRequest("An error occured!");
                }
                return Ok(familyId);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteFamilyCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> FamilyUpdate(Guid id, [FromBody] UpdateFamilyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Family Id in URL doesn't match in request body");
            }
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("was not found"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
