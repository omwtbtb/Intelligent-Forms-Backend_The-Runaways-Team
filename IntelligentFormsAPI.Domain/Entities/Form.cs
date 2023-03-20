namespace IntelligentFormsAPI.Domain.Entities
{
    public record Form
    {
        public DateTime? TimeStamp { get; set; }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FormTitle { get; set; } = null!;
        public int DataRetentionPeriod { get; set; }
        public List<Section> Sections { get; set; } = null!;
    }
}
