using IntelligentFormsAPI.Domain.Entities;

namespace IntelligentFormsAPI.Infrastructure.Interfaces
{
    public interface IFormsRepository
    {
        public Task<Form> CreateFormAsync(Form form);
        public Task<Form?> GetFormByIdAsync(Guid Id);
        public Task<List<Form>?> GetFormsByUserIdAsync(Guid userId);
        public Task<Form> UpdateFormByIdAsync(Form form);
        public Task DeleteFormByIdAsync(Form formTemplate);
        public Task<List<Form>> GetAllFormsAsync();
    }
}
