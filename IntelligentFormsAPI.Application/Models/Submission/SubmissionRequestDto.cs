using IntelligentFormsAPI.Domain.Entities;

namespace IntelligentFormsAPI.Application.Models.Submission
{
    public class SubmissionRequestDto
    {
        public Guid Id { get; set; }
        public string TimeStamp { get; set; } = null!;
        public List<SubmissionField> SubmissionFields { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
