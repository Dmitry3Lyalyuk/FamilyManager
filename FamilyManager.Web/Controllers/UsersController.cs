using FamilyManager.Application.Users.Queries;
using FamilyManager.Web.Requests;
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
        /// Delete a user by Id.
        /// </summary>
        /// <param name="id">The Id of the user to delete.</param>
        /// <response code="204">User successfully deleted.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpDelete("{id:guid}")]
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
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
        {
            var command = new UpdateUserCommand()
            {
                Id = id,
                Country = request.Country,
                Email = request.Email
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
