using IntelligentFormsAPI.Domain.Entities;
using IntelligentFormsAPI.Infrastructure.Contexts;
using IntelligentFormsAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntelligentFormsAPI.Infrastructure.Repositories
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        private readonly EFContext eFContext;

        public SubmissionsRepository(EFContext eFContext)
        {
            this.eFContext = eFContext;
        }

        public async Task<Submission> CreateSubmissionAsync(Submission submission)
        {
            await eFContext.Submissions.AddAsync(submission);
            await eFContext.SaveChangesAsync();
            
            return submission;
        }

        public async Task DeleteSubmissionAsync(Submission submission)
        {
            eFContext.Submissions.Remove(submission);
            
            await eFContext.SaveChangesAsync();
        }

        public async Task DeleteSubmissionsAsync(List<Submission> submissions)
        {
            eFContext.Submissions.RemoveRange(submissions);

            await eFContext.SaveChangesAsync(); 
        }

        public async Task<List<Submission>> GetSubmissionByFormIdAsync(Guid formId)
        {
            return await eFContext.Submissions.Where(s => s.FormId.Equals(formId)).ToListAsync();
        }

        public async Task<Submission> GetSubmissionByIdAsync(Guid id)
        {
            return await eFContext.Submissions.FindAsync(id);
        }

    }
}
