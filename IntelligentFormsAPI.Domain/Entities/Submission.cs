namespace IntelligentFormsAPI.Domain.Entities
{
    public record Submission
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<SubmissionField> SubmissionFields { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
