using IntelligentFormsAPI.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var ts = new TimeSpan();
            var forms = await formsRepository.GetAllFormsAsync();
            foreach(var form in forms)
            {
                var formDataRetentionPeriod = form.DataRetentionPeriod;
                var submissions = await submissionsRepository.GetSubmissionByFormIdAsync(form.Id);

                foreach(var submission in submissions)
                {
                    ts = DateTime.Now - submission.TimeStamp;

                    if(ts.Days > formDataRetentionPeriod)
                    {
                        await submissionsRepository.DeleteSubmissionAsync(submission);
                    }  
                }
            }
        }
    }
}
