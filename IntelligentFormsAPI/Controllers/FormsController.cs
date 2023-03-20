using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IntelligentFormsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/forms")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormsService formService;
        private readonly ILogger<FormsController> logger;

        public FormsController(IFormsService formService, ILogger<FormsController> logger)
        {
            this.formService = formService;
            this.logger = logger;
        }

        [SwaggerOperation(Summary = "Get a form by id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormTemplateByIdAsync(Guid id)
        {
            try
            {
                var form = await formService.GetFormByIdAsync(id);

                return Ok(form);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get all forms by user id")]
        [HttpGet]
        public async Task<IActionResult> GetFormTemplatesByUserIdAsync([FromQuery] Guid userid)
        {
            try
            {
                var forms = await formService.GetFormsByUserIdAsync(userid);

                return Ok(forms);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Create a new form")]
        [HttpPost]
        public async Task<IActionResult> CreateFormAsync(FormDto formDto, [FromQuery] Guid userId)
        {
            try
            {
                var form = await formService.CreateFormAsync(formDto, userId);

                return Ok(form);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Update a form")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormByIdAsync([FromBody] FormDto formDto, Guid id)
        {
            try
            {
                await formService.UpdateFormByIdAsync(id, formDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Delete a form")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormByIdAsync(Guid id)
        {
            try
            {
                await formService.DeleteFormByIdAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }
    }
}
