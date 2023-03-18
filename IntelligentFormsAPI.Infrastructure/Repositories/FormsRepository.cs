using IntelligentFormsAPI.Domain.Entities;
using IntelligentFormsAPI.Infrastructure.Contexts;
using IntelligentFormsAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntelligentFormsAPI.Infrastructure.Repositories
{
    public class FormsRepository : IFormsRepository
    {

        private readonly EFContext efContext;

        public FormsRepository(EFContext efContext)
        {
            this.efContext = efContext;
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            efContext.FormTemplates.Add(form);
            await efContext.SaveChangesAsync();

            return form;
        }

        public async Task DeleteFormByIdAsync(Form formTemplate)
        {
            efContext.FormTemplates.Remove(formTemplate);

            await efContext.SaveChangesAsync();
        }

        public Task<List<Form>> GetAllFormsAsync()
        {
            return efContext.FormTemplates.ToListAsync();
        }

        public async Task<Form?> GetFormByIdAsync(Guid Id)
        {
            var form = await efContext.FormTemplates.FindAsync(Id);

            return form;
        }

        public async Task<List<Form>?> GetFormsByUserIdAsync(Guid userId)
        {
            var forms = await efContext.FormTemplates.Where(x => x.UserId == userId).ToListAsync();

            return forms;
        }

        public async Task<Form> UpdateFormByIdAsync(Form form)
        {
            efContext.FormTemplates.Update(form);
            await efContext.SaveChangesAsync();

            return form;
        }


    }
}
