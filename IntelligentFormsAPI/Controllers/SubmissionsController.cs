using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models.Submission;
using IntelligentFormsAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost, Route("submissions")]
        public async Task<IActionResult> CreateSubmission(SubmissionDto submissionDto, [FromQuery]  Guid formId)
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

        [HttpGet, Route("submissions/{submissionId}")]
        public async Task<IActionResult> GetSubmissionById(Guid submissionId)
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

        [HttpGet, Route("submissions")]
        public async Task<IActionResult> GetSubmissionByFormIdAsync([FromQuery]Guid formId)
        {
            try
            {
                var submissions = await submissionService.GetSubmissionByFormId(formId);

                return Ok(submissions);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("Submissions/{submissionId}")]
        public async Task<IActionResult> DeleteSubmission(Guid submissionId)
        {
            try
            {
                await submissionService.DeleteSubmissionAsync(submissionId);

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
