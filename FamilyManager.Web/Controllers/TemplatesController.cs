using FamilyManager.Application.Templates.Commands;
using FamilyManager.Application.Templates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get details of a specific template by Id.
        /// </summary>
        /// <param name="templateId">The Id of the template.</param>
        /// <returns>The details of the specified template.</returns>
        /// <response code="200">Returns the template details.</response>
        /// <response code="404">If the template is not found.</response>
        [HttpGet("{templateId:guid}")]
        public async Task<IActionResult> GetTemplateDeteils(Guid templateId)
        {
            var query = new GetTemplatesDetailsQuery { Id = templateId };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Create a new template.
        /// </summary>
        /// <param name="command">The command to create a template.</param>
        /// <returns>The Id of the created template.</returns>
        /// <response code="200">Returns the Id of the newly created template.</response>
        /// <response code="400">If the request is invalid or an error occurs.</response>
        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand command)
        {
            try
            {
                var templateId = await _mediator.Send(command);

                return Ok(templateId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Update an existing template.
        /// </summary>
        /// <param name="id">The Id of the template to update.</param>
        /// <param name="command">The command containing updated template data.</param>
        /// <response code="204">Template successfully updated.</response>
        /// <response code="400">If the request is invalid or Ids do not match.</response>
        /// <response code="404">If the template is not found.</response>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTemplate(Guid id, [FromBody] UpdateTemplateCommand command)
        {
            if (id != command.TemplateId)
            {
                return BadRequest("Tempalte Id in URL does not match Id in request body.");
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
        }

    }
}
