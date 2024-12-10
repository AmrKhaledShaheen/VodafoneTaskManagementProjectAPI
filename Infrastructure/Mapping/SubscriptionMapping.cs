using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModels.SubscriptionModelViews;
using Infrastructure.ViewModels.TaskModelViews;
using Infrastructure.ViewModels.UserAccountManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public class SubscriptionMapping : Profile
    {
        public SubscriptionMapping()
        {
            CreateMap<Subscriptions, SubscribeModelView>().ReverseMap();
        }
    }
}
