using Infrastructure.Common;
using Infrastructure.ViewModels.SubscriptionModelViews;
using Infrastructure.ViewModels.TaskModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.BackgroundServiceForEmail;
using Services.SubscriptionService;
using Services.TaskService;

namespace VodafoneTask.Controllers
{
    public class SubscriptionController : Controller
    {
        ISubscriptionService subscriptionService;
        EmailService emailService;
        public SubscriptionController(ISubscriptionService _subscriptionService, EmailService emailService)
        {
            subscriptionService = _subscriptionService;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ResponseBase> CreateSubscription([FromBody] SubscribeModelView subscribeModelView)
        {
            var result = await subscriptionService.CreateNewSubscriptionAsync(subscribeModelView, User);
            return result;

        }

        [Authorize]
        public async Task<IActionResult> SendEmail()
        {
            string body = @"
                <html>
                    <body>
                        <h1>Test email from .net</h1>
                        <p style = 'color: green'> this is paragraph</p>
                        <p style = 'color: blue'> this is paragraph</p>
                    </body>
                </html>
            ";
            await emailService.SendEmailAsync("shaheen.amr@yahoo.com",null);
            return Json(new {x=5});

        }

        [Authorize]
        [HttpPost]
        public async Task<ResponseBase> UnSubscribe()
        {
            var result = await subscriptionService.UnSubscribeAsync(User);
            return result;

        }
    }
}
