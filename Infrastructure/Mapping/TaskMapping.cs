using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModels.TaskModelViews;
using Infrastructure.ViewModels.UserAccountManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public class TaskMapping : Profile
    {
        public TaskMapping()
        {
            CreateMap<Tasks, CreateTaskModelView>().ReverseMap();
            CreateMap<Tasks, EditTaskModelView>()
                .ReverseMap();
        }
    }
}
