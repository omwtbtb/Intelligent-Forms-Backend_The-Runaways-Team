using IntelligentFormsAPI.Infrastructure.Interfaces;
using Quartz;

namespace IntelligentFormsAPI.Infrastructure.Quartz
{
    public class DeleteOldSubmissionsJob : IJob
    {
        private readonly IFormsRepository formsRepository;
        private readonly ISubmissionsRepository submissionsRepository;

        public DeleteOldSubmissionsJob(IFormsRepository formsRepository, ISubmissionsRepository submissionsRepository)
        {
            this.formsRepository = formsRepository;
            this.submissionsRepository = submissionsRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var forms = await formsRepository.GetAllFormsAsync();
            foreach (var form in forms)
            {
                var formDataRetentionPeriod = form.DataRetentionPeriod;
                var submissions = await submissionsRepository.GetSubmissionByFormIdAsync(form.Id);

                await submissionsRepository.DeleteSubmissionsAsync(submissions.Where(s =>
                (DateTime.Now - s.TimeStamp).Days >= formDataRetentionPeriod).ToList());
            }
        }
    }
}
