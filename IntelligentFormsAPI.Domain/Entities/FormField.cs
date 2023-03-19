namespace IntelligentFormsAPI.Domain.Entities
{
    public class FormField
    {
        public string DynamicField_Key { get; set; } = null!;
        public string PlaceHolder_Key { get; set; } = null!;
        public bool Mandatory { get; set; }
        public string FieldType { get; set; } = null!;
        public List<string>? Options { get; set; } = null!;
        public string? Document_KeyWords { get; set; }
    }
}
