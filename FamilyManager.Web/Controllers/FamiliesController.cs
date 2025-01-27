using FamilyManager.Application.Families.Commands;
using FamilyManager.Application.Families.Querries;
using FamilyManager.Application.Familys.Commands;
using FamilyManager.Web.Requests;
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

        /// <summary>
        /// Get all Families.
        /// </summary>
        /// <returns>A list of all families.</returns>
        /// <response code="200">Returns the list of families.</response>
        /// <response code="404">If no families are found.</response>
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<List<FamilyDTO>>> GetAllFamilies()
        {
            var query = new GetAllFamiliesQuery();
            var families = await _mediator.Send(query);

            if (families is null or [])
            {
                return NotFound("No families found!");
            }

            return Ok(families);
        }

        /// <summary>
        /// Create a new family.
        /// </summary>
        /// <param name="command">The command to create a family.</param>
        /// <returns>The Id of the created family.</returns>
        /// <response code="200">Returns the Id of the newly created family.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<Guid>> CreateFamily([FromBody] CreateFamilyCommand command)
        {
            var familyId = await _mediator.Send(command);

            if (familyId == Guid.Empty)
            {
                return BadRequest("An error occured!");
            }

            return Ok(familyId);
        }

        /// <summary>
        /// Delete a family by Id.
        /// </summary>
        /// <param name="id">The Id of the family.</param>
        /// <response code="204">Family successfully deleted.</response>
        /// <response code="404">If the family is not found.</response>
        [HttpDelete("{id:guid}")]
        // [Authorize]
        public async Task<ActionResult> Delete(Guid id) //DeleteFamily
        {
            var command = new DeleteFamilyCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update a family.
        /// </summary>
        /// <param name="id">The Id of the family to update.</param>
        /// <param name="command">The command containing updated family data.</param>
        /// <response code="204">Family successfully updated.</response>
        /// <response code="400">If the request is invalid or Ids do not match.</response>
        [HttpPut("{id:guid}")]
        // [Authorize]
        public async Task<IActionResult> FamilyUpdate(Guid id, [FromBody] FamilyUpdateRequest request) //UpdateFamily
        {

            var command = new UpdateFamilyCommand()
            {
                Id = id,
                Name = request.Name,
                Brand = request.Brand,
            };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
