using IntelligentFormsAPI.Domain.Entities;

namespace IntelligentFormsAPI.Infrastructure.Interfaces
{
    public interface ISubmissionsRepository
    {
        public Task<Submission> GetSubmissionByIdAsync(Guid id);
        public Task DeleteSubmissionAsync(Submission submission);
        public Task DeleteSubmissionsAsync(List<Submission> submissions);
        public Task<Submission> CreateSubmissionAsync(Submission submission);
        public Task<List<Submission>> GetSubmissionByFormIdAsync(Guid formId);
    }
}
