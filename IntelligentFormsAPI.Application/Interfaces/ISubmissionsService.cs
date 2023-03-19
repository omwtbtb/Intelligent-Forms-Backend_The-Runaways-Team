using IntelligentFormsAPI.Application.Models.Submission;

namespace IntelligentFormsAPI.Application.Interfaces
{
    public interface ISubmissionsService
    {
        public Task<SubmissionRequestDto> GetSubmissionByIdAsync(Guid id);
        public Task DeleteSubmissionByIdAsync(Guid id);
        public Task<SubmissionRequestDto> CreateSubmissionAsync(SubmissionDto submissionDto, Guid formId);
        public Task<List<SubmissionRequestDto>> GetSubmissionByFormIdAsync(Guid formId);
    }
}
