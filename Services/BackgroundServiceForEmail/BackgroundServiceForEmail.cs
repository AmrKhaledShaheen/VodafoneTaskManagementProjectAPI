using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BackgroundServiceForEmail
{
    public class BackgroundServiceForEmail : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundServiceForEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<DbContextTask>();
                        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                        // Get pending emails
                        var pendingEmails = dbContext.Subscriptions.ToList();

                        foreach (var subscribe in pendingEmails)
                        {
                            if (checkSendingEMail(subscribe))
                            {
                                List<Domain.Entities.Tasks>? tasks = getTasks(subscribe, dbContext);
                                string UserEmail = getEmail(subscribe,dbContext);
                                await emailService.SendEmailAsync(UserEmail,tasks);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    // Log errors
                    Console.WriteLine($"Error in EmailBackgroundService: {ex.Message}");
                }

                // Wait for the next check
                TimeSpan time = DateTime.Now.TimeOfDay;
                int MinutesRemainingtoHour = 60 - time.Minutes;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(MinutesRemainingtoHour), stoppingToken);
            }
        }

        private string getEmail(Subscriptions subscribe, DbContextTask dbContext)
        {
            var email = dbContext.Subscriptions
                .Include(s => s.VodafoneUser)
                .Where(x => x.Id == subscribe.Id)
                .Select(x => x.VodafoneUser)
                .FirstOrDefault();
            if (email == null)
            {
                return "";
            }
            else
            {
                return email.Email;
            }
        }

        private List<Domain.Entities.Tasks>? getTasks(Subscriptions subscribe, DbContextTask dbContext)
        {
            DateTime LastDate = DateTime.Now;
            if(subscribe.Frequency==1)//Daily
            {
                LastDate = LastDate.AddDays(-1);
                LastDate = LastDate.AddHours(subscribe.ReportTime-LastDate.Hour);
            }
            else if (subscribe.Frequency == 2)//weekly
            {
                LastDate = LastDate.AddDays(-7);
                LastDate = LastDate.AddHours(subscribe.ReportTime - LastDate.Hour);
            }
            else if (subscribe.Frequency == 3)//Monthly
            {
                LastDate = LastDate.AddDays(-30);
                LastDate = LastDate.AddHours(subscribe.ReportTime - LastDate.Hour);
            }
            var tasks = dbContext.Subscriptions
                .Include(s => s.VodafoneUser)
                .ThenInclude(u => u.Tasks)
                .Where(x => x.Id == subscribe.Id )
                .SelectMany(x => x.VodafoneUser.Tasks)
                .Where(t => t.DueDate>LastDate && t.DueDate <= DateTime.Now).ToList();
            return tasks;
        }

        private bool checkSendingEMail(Subscriptions subscribe)
        {
            DateTime Now = DateTime.Now;
            TimeSpan Difference = Now - subscribe.StartDate;
            int DifferenceDays = Difference.Days;
            int frequency = subscribe.Frequency;
            if (frequency == 1 && Now.Hour-1 == subscribe.ReportTime) //Daily Reports
            {
                return true;
            }
            else if (frequency == 2 && Now.Hour-1 == subscribe.ReportTime && DifferenceDays % 7 == 0) //Weekly
            {
                return true;
            }/*
            else if (frequency == 3 && Now.Hour == subscribe.ReportTime && subscribe.StartDate.Day == Now.Day) //Monthly
            {
                return true;
            }*/
            else if (frequency == 3 && Now.Hour-1 == subscribe.ReportTime && DifferenceDays % 30 == 0) //Monthly
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
