using AutoMapper;
using ClosedXML.Excel;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Repository;
using Infrastructure.ViewModels.SubscriptionModelViews;
using Infrastructure.ViewModels.TaskModelViews;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IGenericRepository<Domain.Entities.Subscriptions> Repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private UserManager<VodafoneUser> userManager;
        private readonly IConfiguration configuration;

        public SubscriptionService(IGenericRepository<Domain.Entities.Subscriptions> Repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<VodafoneUser> userManager,
        IConfiguration configuration)
        {
            this.Repository = Repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }
/*
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MailMessage
            {
                From = new MailAddress("amrkhaledshaheen@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };


            email.To.Add(toEmail);


            var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("amrkhaledshaheen@gmail.com", "gzkzvcdmisoytavh"),
                EnableSsl = true
            };
            try
            {
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
            finally
            {
                smtp.Dispose();
            }
        }

        public byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName = "Sheet1")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            // Get properties of the object
            var properties = typeof(T).GetProperties();

            // Write the header row
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Write data rows
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item);
                    worksheet.Cell(row, col + 1).Value = (XLCellValue)value;
                }
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray(); // Return the Excel file as a byte array
        }
*/

        public async Task<ResponseBase> CreateNewSubscriptionAsync(SubscribeModelView subscribeModelView, ClaimsPrincipal _user)
        {
            if (subscribeModelView.ReportTime.Minutes != 0)
            {
                return new ResponseBase()
                {
                    Succeeded = false,
                    Message = "Report Time Must contain No Minutes"
                };
            }
            subscribeModelView.StartDate.AddHours(subscribeModelView.ReportTime.Hours);
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {
                var result = Repository.GetFiltered(x => x.VodafoneUserId == user.Id).FirstOrDefault();
                if (result != null)
                {
                    result.StartDate = subscribeModelView.StartDate;
                    result.Frequency = subscribeModelView.Frequency;
                    result.ReportTime = subscribeModelView.ReportTime.Hours;
                    Repository.Update(result);
                }
                else
                {
                    Subscriptions newSubscription = new Subscriptions()
                    {
                        StartDate = subscribeModelView.StartDate,
                        Frequency = subscribeModelView.Frequency,
                        ReportTime = subscribeModelView.ReportTime.Hours,
                        VodafoneUserId = user.Id
                    };
                    Repository.Add(newSubscription);
                }
                if (unitOfWork.SaveChanges())
                {
                    return new ResponseBase()
                    {
                        Message = "You Subscribed Successfully",
                        Succeeded = true,
                    };

                }
                else
                {
                    return new ResponseBase()
                    {
                        Message = "Cannot Subscribe",
                        Succeeded = false,
                    };
                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Subscribe",
                Succeeded = false,
            };
        }

        public async Task<ResponseBase> UnSubscribeAsync(ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {
                var subscriptions = Repository.GetFiltered(x => x.VodafoneUserId == user.Id).FirstOrDefault();
                if (subscriptions != null)
                {
                    Repository.Delete(subscriptions.Id);
                }
                else
                {
                    return new ResponseBase()
                    {
                        Message = "There is no Subscription to delete",
                        Succeeded = false,
                    };
                }
                if (unitOfWork.SaveChanges())
                {
                    return new ResponseBase()
                    {
                        Message = "Sucscription Deleted Successfully",
                        Succeeded = true,
                    };

                }
                else
                {
                    return new ResponseBase()
                    {
                        Message = "Cannot Delete Subscription",
                        Succeeded = false,
                    };
                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Delete Subscription",
                Succeeded = false,
            };
        }
    }
}
