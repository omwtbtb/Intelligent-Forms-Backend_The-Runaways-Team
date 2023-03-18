
using IntelligentFormsAPI.Application.Models;
using IntelligentFormsAPI.Application.Models.Form;

namespace IntelligentFormsAPI.Application.Interfaces
{
    public interface IFormsService
    {

        public Task<FormCreateResponseDto?> CreateFormAsync(FormDto form, Guid userId);
        public Task<FormCreateResponseDto?> GetFormByIdAsync(Guid Id);
        public Task<List<FormCreateResponseDto>?> GetFormsByUserIdAsync(Guid userID);
        public Task UpdateFormByIdAsync(Guid Id, FormDto form);
        public Task DeleteFormByIdAsync(Guid Id);
    }
}
