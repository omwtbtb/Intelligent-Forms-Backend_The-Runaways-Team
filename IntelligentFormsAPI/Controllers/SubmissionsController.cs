using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models.Submission;
using IntelligentFormsAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IntelligentFormsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/"), ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ILogger<SubmissionsController> logger;
        private readonly ISubmissionsService submissionService;

        public SubmissionsController(ILogger<SubmissionsController> logger, ISubmissionsService submissionService)
        {
            this.logger = logger;
            this.submissionService = submissionService;
        }

        [SwaggerOperation(Summary = "Create a submission")]
        [HttpPost, Route("submissions")]
        public async Task<IActionResult> CreateSubmissionAsync(SubmissionDto submissionDto, [FromQuery]  Guid formId)
        {
            try
            {
                var submission = await submissionService.CreateSubmissionAsync(submissionDto, formId);

                return Ok(submission);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get a submission by id")]
        [HttpGet, Route("submissions/{submissionId}")]
        public async Task<IActionResult> GetSubmissionByIdAsync(Guid submissionId)
        {
            try
            {
                var submission = await submissionService.GetSubmissionByIdAsync(submissionId);

                return Ok(submission);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Get all submissions by form id")]
        [HttpGet, Route("submissions")]
        public async Task<IActionResult> GetSubmissionByFormIdAsync([FromQuery]Guid formId)
        {
            try
            {
                var submissions = await submissionService.GetSubmissionByFormIdAsync(formId);

                return Ok(submissions);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [SwaggerOperation(Summary = "Delete a submission by id")]
        [HttpDelete, Route("Submissions/{submissionId}")]
        public async Task<IActionResult> DeleteSubmissionByIdAsync(Guid submissionId)
        {
            try
            {
                await submissionService.DeleteSubmissionByIdAsync(submissionId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
        }

    }
}
