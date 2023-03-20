using IntelligentFormsAPI.Application.Models.FormTemplate;

namespace IntelligentFormsAPI.Application.Models.Form
{
    public class FormCreateResponseDto
    {
        public Guid  Id { get; set; }
        public string? TimeStamp { get; set; }
        public string FormTitle { get; set; } = null!;
        public int DataRetentionPeriod { get; set; }
        public List<SectionDto> Sections { get; set; } = null!;
    }
}
