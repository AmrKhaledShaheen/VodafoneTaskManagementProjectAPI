using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.ViewModels;
using Infrastructure.ViewModels.SubscriptionModelViews;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        public Task<ResponseBase> CreateNewSubscriptionAsync(SubscribeModelView createTaskModelView, System.Security.Claims.ClaimsPrincipal user);
        public Task<ResponseBase> UnSubscribeAsync(ClaimsPrincipal user);
        /*public Task SendEmailAsync(string toEmail, string subject, string body);*/

    }
}
