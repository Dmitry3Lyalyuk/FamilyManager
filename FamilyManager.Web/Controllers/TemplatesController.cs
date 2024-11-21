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
        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand command)
        {
            var templateId = await _mediator.Send(command);
            if (templateId == Guid.Empty)
            {
                return BadRequest("An error occurred!");
            }

            return Ok(templateId);
        }
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
