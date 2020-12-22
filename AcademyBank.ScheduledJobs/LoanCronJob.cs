using AcademyBank.BLL.Services.Interfaces;
using AcademyBank.ScheduledJobs.CronJobConfig.Abstract;
using AcademyBank.ScheduledJobs.CronJobConfig.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcademyBank.ScheduledJobs
{
    public class LoanCronJob : CronJobService, IDisposable
    {
        private readonly ILogger<LoanCronJob> _logger;
        private IServiceScope _scope;
        private readonly IServiceProvider _provider;

        public LoanCronJob(IScheduleConfig<LoanCronJob> config, ILogger<LoanCronJob> logger, IServiceProvider service)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _scope = service.CreateScope();
            _logger = logger;
            _provider = service;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Loan CronJob 1 starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Loan CronJob 1 is working.");
            _scope = _provider.CreateScope();
            using (var loanService = _scope.ServiceProvider.GetRequiredService<IAccountLoanService>())
            {
                await loanService.EvaluateAmount();
            }

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Loan CronJob 1 is stopping.");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _scope?.Dispose();
            base.Dispose();
        }
    }
}
