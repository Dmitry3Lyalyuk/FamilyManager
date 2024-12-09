using FamilyManager.Application.Users.Commands;
using FamilyManager.Application.Users.Querries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="404">If no users are found.</response>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);
            if (users is null or [])
            {
                return NotFound("No user found!");
            }

            return Ok(users);

        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="command">The command to create a user.</param>
        /// <returns>The Id of the created user.</returns>
        /// <response code="200">Returns the Id of the newly created user.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);
                if (userId == Guid.Empty)
                {
                    return BadRequest("An error occured!");
                }

                return Ok(userId);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a user by Id.
        /// </summary>
        /// <param name="id">The Id of the user to delete.</param>
        /// <response code="204">User successfully deleted.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The Id of the user to update.</param>
        /// <param name="command">The command containing updated user data.</param>
        /// <response code="204">User successfully updated.</response>
        /// <response code="400">If the request is invalid or Ids do not match.</response>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UserApdate(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("User Id in URL doesn't match Id in request body");
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
