using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.ViewModels.TaskModelViews;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.TaskService
{
    public interface ITaskService
    {
        public Task<ResponseBase> CreateNewTaskAsync(Infrastructure.ViewModels.TaskModelViews.CreateTaskModelView createTaskModelView, System.Security.Claims.ClaimsPrincipal user);
        public Task<ResponseBase> EditTaskAsync(EditTaskModelView editTaskModelView, ClaimsPrincipal user);
        public Task<List<EditTaskModelView>> GetPaggedDataAsync(FilterTaskModelView filterTaskModelView, ClaimsPrincipal _user);

        public EditTaskModelView getTaskById(int id);
        public Task<ResponseBase> RestoreTask(ClaimsPrincipal user);
        public Task<ResponseBase> DeleteTaskByIdAsync(int id, ClaimsPrincipal user);
        public Task<ResponseBase> BatchDeleteTask(ClaimsPrincipal user, DateTime DateFrom, DateTime DateTo);

    }
}
