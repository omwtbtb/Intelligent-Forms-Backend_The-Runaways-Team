using AutoMapper;
using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models;
using IntelligentFormsAPI.Application.Models.Form;
using IntelligentFormsAPI.Domain.Entities;
using IntelligentFormsAPI.Infrastructure.Interfaces;

namespace IntelligentFormsAPI.Application.Services
{
    public class FormsService : IFormsService
    {
        private readonly IFormsRepository formsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly ISubmissionsRepository submissionsRepository;
        private readonly IMapper mapper;

        public FormsService(IFormsRepository formRepository, IUsersRepository usersRepository,
        ISubmissionsRepository submissionsRepository, IMapper mapper)
        {
            this.formsRepository = formRepository;
            this.usersRepository = usersRepository;
            this.submissionsRepository = submissionsRepository;
            this.mapper = mapper;

        }

        public async Task<FormCreateResponseDto?> CreateFormAsync(FormDto formDto, Guid userId)
        {
            var user = await usersRepository.GetUserByIdAsync(userId);

            if (user is null)
                throw new ArgumentException("User not found");

            var form = mapper.Map<Form>(formDto);
            form.UserId = userId;
            form.TimeStamp = DateTime.UtcNow;
            
            var savedForm = await formsRepository.CreateFormAsync(form);
        

            return mapper.Map<FormCreateResponseDto>(savedForm);
        }

        public async Task<List<FormCreateResponseDto>?> GetFormsByUserIdAsync(Guid userID)
        {
            var forms = await formsRepository.GetFormsByUserIdAsync(userID);

            var formsDto = mapper.Map<List<FormCreateResponseDto>>(forms);

            var sortedformsDto = formsDto.OrderByDescending(f => f.TimeStamp==""? 0:1).ToList();

            return sortedformsDto;
        }

        public async Task DeleteFormByIdAsync(Guid Id)
        {
            var form = await formsRepository.GetFormByIdAsync(Id);

            await formsRepository.DeleteFormByIdAsync(form);

            var submissions = await submissionsRepository.GetSubmissionByFormIdAsync(form.Id);

            await submissionsRepository.DeleteSubmissionsAsync(submissions);
        }
        
        public async Task UpdateFormByIdAsync(Guid Id, FormDto formDto)
        {

            var form = await formsRepository.GetFormByIdAsync(Id);

            if (form is null)
                throw new ArgumentException("Form does not exist");

            mapper.Map(formDto, form);

            await formsRepository.UpdateFormByIdAsync(form);

        }

        public async Task<FormCreateResponseDto?> GetFormByIdAsync(Guid Id)
        {
            var form = await formsRepository.GetFormByIdAsync(Id);

            return mapper.Map<FormCreateResponseDto>(form);
        }

    }
}
