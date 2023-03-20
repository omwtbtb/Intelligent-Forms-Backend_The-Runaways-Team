
﻿using AutoMapper;
using IntelligentFormsAPI.Application.Interfaces;
using IntelligentFormsAPI.Application.Models.Submission;
using IntelligentFormsAPI.Domain.Entities;
using IntelligentFormsAPI.Infrastructure.Interfaces;

namespace IntelligentFormsAPI.Application.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly ISubmissionsRepository submissionRepository;
        private readonly IFormsRepository formRepository;
        private readonly IMapper mapper;

        public SubmissionsService(ISubmissionsRepository submissionRepository, IFormsRepository formsRepository,
            IMapper mapper)
        {
            this.submissionRepository = submissionRepository;
            this.formRepository = formsRepository;
            this.mapper = mapper;
        }

        public async Task<SubmissionRequestDto> GetSubmissionByIdAsync(Guid id)
        {
            var submission = await submissionRepository.GetSubmissionByIdAsync(id);

            return mapper.Map<SubmissionRequestDto>(submission);
        }

        public async Task DeleteSubmissionByIdAsync(Guid id)
        {
            var submission = await submissionRepository.GetSubmissionByIdAsync(id);

            await submissionRepository.DeleteSubmissionAsync(submission);
        }

        public async Task<SubmissionRequestDto> CreateSubmissionAsync(SubmissionDto submissionDto, Guid formId)
        {

            var form = await formRepository.GetFormByIdAsync(formId);

            if (form is null)
                throw new ArgumentException("Form not found");

            var submission = mapper.Map<Submission>(submissionDto);
            submission.FormId = formId;
            submission.TimeStamp = DateTime.UtcNow;

            var savedSubmission = await submissionRepository.CreateSubmissionAsync(submission);

            return mapper.Map<SubmissionRequestDto>(savedSubmission);
        }

        public async Task<List<SubmissionRequestDto>> GetSubmissionByFormIdAsync(Guid formId)
        {
            var submissions = await submissionRepository.GetSubmissionByFormIdAsync(formId);
            submissions = submissions.OrderByDescending(s => s.TimeStamp).ToList();

            return mapper.Map<List<SubmissionRequestDto>>(submissions);
        }
    }
}
