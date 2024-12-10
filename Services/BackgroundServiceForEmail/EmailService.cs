using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

namespace Services.BackgroundServiceForEmail
{
    public class EmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, List<Tasks>? tasks)
        {
            string body = getBody(tasks);
            var email = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:Username"] ?? ""),
                Subject = "This Automated Email For Your Last Tasks",
                Body = body,
                IsBodyHtml = true
            };


            email.To.Add(toEmail);


            var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    _configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
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

        private string getBody(List<Tasks>? tasks)
        {
            if (tasks == null)
                return "No Content";
            string head = @"
<head>
 <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        table {
            width: 80%;
            margin: auto;
            border-collapse: collapse;
            text-align: left;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 10px;
        }

        th {
            background-color: #f4f4f4;
            color: #333;
        }

        tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        tr:hover {
            background-color: #f1f1f1;
        }

        caption {
            margin-bottom: 10px;
            font-size: 1.5em;
            font-weight: bold;
            color: #555;
        }
    </style>
</head>
";
            string caption = @"<caption>Your Tasks Statics</caption>";
            string headers = @"
        <thead>
            <tr>
                <th>#</th>
                <th>Title</th>
                <th>Description</th>
                <th>Status</th>
                <th>Start Date</th>
                <th>Due Date</th>
                <th>Complete Date</th>
            </tr>
        </thead>";
            string rows = "<tbody>";
            foreach (var task in tasks)
            {
                rows += "<tr>";
                rows += "<td>" + task.Id + "</td>";
                rows += "<td>" + task.Title + "</td>";
                rows += "<td>" + task.Description + "</td>";
                if (task.Status == 1)
                    rows += "<td> Pending </td>";
                else if (task.Status == 2)
                    rows += "<td> Completed </td>";
                else if (task.Status == 3)
                    rows += "<td> Over Due </td>";
                rows += "<td>" + task.StartDate.ToString() + "</td>";
                rows += "<td>" + task.DueDate.ToString() + "</td>";
                rows += "<td>" + task.CompletionDate.ToString() + "</td>";
                rows += "</tr>";
            }
            rows += "</tbody>";

            string body = "<body><table>"+caption+headers+rows+"</table></body>";
            return head + body;
        }
    }
}
