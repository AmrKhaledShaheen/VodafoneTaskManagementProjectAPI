using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Repository;
using Infrastructure.ViewModels.TaskModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly IGenericRepository<Domain.Entities.Tasks> Repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private UserManager<VodafoneUser> userManager;

        public TaskService(IGenericRepository<Domain.Entities.Tasks> Repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<VodafoneUser> userManager)
        {
            this.Repository = Repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<ResponseBase> BatchDeleteTask(ClaimsPrincipal _user, DateTime DateFrom, DateTime DateTo)
        {
            try 
            {
                var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {

                var result = Repository.GetFiltered(
                    x => !x.IsDeleted &&
                    x.VodafoneUserId == user.Id &&
                    x.DueDate >= DateFrom &&
                    x.DueDate <= DateTo 
                    ,c => new Tasks
                    {
                        Id = c.Id,
                        DueDate = c.DueDate,
                        StartDate = c.StartDate,
                        Description = c.Description,
                        Title = c.Title,
                        Status = c.Status,
                        CompletionDate = c.CompletionDate,
                        VodafoneUserId = c.VodafoneUserId,
                        IsDeleted = true,
                        DeletedDate = DateTime.Now,
                    },"").ToList();
                if (result != null)
                {
                    try
                    {
                        Repository.UpdateRange(result);
                        if (unitOfWork.SaveChanges())
                        {
                            return new ResponseBase()
                            {
                                Message = "Tasks Deleted Successfully",
                                Succeeded = true,
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        string ee = e.Message;
                    }

                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Delete Tasks",
                Succeeded = false,
            };
            } catch (Exception e)
            {

            }
            return new ResponseBase()
            {
                Message = "Cannot Delete Tasks",
                Succeeded = false,
            };
        }

        public async Task<ResponseBase> CreateNewTaskAsync(Infrastructure.ViewModels.TaskModelViews.CreateTaskModelView createTaskModelView, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {
                Tasks NewTask = mapper.Map<Tasks>(createTaskModelView);
                NewTask.VodafoneUser = user;
                Repository.Add(NewTask);
                if (unitOfWork.SaveChanges())
                {
                    return new ResponseBase()
                    {
                        Message = "Task Created Successfully",
                        Succeeded = true,
                    };

                }
                else
                {
                    return new ResponseBase()
                    {
                        Message = "Cannot Create Task",
                        Succeeded = false,
                    };
                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Create Task",
                Succeeded = false,
            };
        }

        public async Task<ResponseBase> DeleteTaskByIdAsync(int id, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {

                Tasks result = Repository.GetFiltered(x =>!x.IsDeleted && x.VodafoneUserId == user.Id && x.Id == id).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        result.IsDeleted = true;
                        result.DeletedDate = DateTime.Now;
                        Repository.Update(result);
                        if (unitOfWork.SaveChanges())
                        {
                            return new ResponseBase()
                            {
                                Message = "Task Updated Successfully",
                                Succeeded = true,
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        string ee = e.Message;
                    }

                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Update the Task",
                Succeeded = false,
            };
         
        }

        public async Task<ResponseBase> EditTaskAsync(EditTaskModelView editTaskModelView, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {

                Tasks result = Repository.GetFiltered(x => x.VodafoneUserId == user.Id && x.Id == editTaskModelView.Id).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        result.Title = editTaskModelView.Title;
                        result.Description = editTaskModelView.Description;
                        result.Status = editTaskModelView.Status;
                        result.StartDate = editTaskModelView.StartDate;
                        result.DueDate = editTaskModelView.DueDate;
                        result.CompletionDate = editTaskModelView.CompletionDate;
                        Repository.Update(result);
                        if (unitOfWork.SaveChanges())
                        {
                            return new ResponseBase()
                            {
                                Message = "Task Updated Successfully",
                                Succeeded = true,
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        string ee = e.Message;
                    }

                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Update the Task",
                Succeeded = false,
            };
        }

        public async Task<List<Tasks>> GetPaggedDataAsync(FilterTaskModelView filterTaskModelView, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {
                var result = Repository.GetFiltered(
                    x => !x.IsDeleted &&
                    x.VodafoneUserId == user.Id
                    );
                if (filterTaskModelView.Status != null)
                    result = result.Where(x => x.Status == filterTaskModelView.Status);
                if (filterTaskModelView.FilterBy != null && filterTaskModelView.DateFrom != null && filterTaskModelView.DateTo != null)
                {
                    if (filterTaskModelView.FilterBy == 1)
                        result = result.Where(x => x.StartDate >= filterTaskModelView.DateFrom && x.StartDate <= filterTaskModelView.DateTo);
                    if (filterTaskModelView.FilterBy == 2)
                        result = result.Where(x => x.DueDate >= filterTaskModelView.DateFrom && x.DueDate <= filterTaskModelView.DateTo);
                    if (filterTaskModelView.FilterBy == 3)
                        result = result.Where(x => x.CompletionDate >= filterTaskModelView.DateFrom && x.CompletionDate <= filterTaskModelView.DateTo);
                }
                result = result.Skip(filterTaskModelView.PageNumber*filterTaskModelView.PageSize??0);
                return result.ToList();
            }
            return null;
        }

        public EditTaskModelView getTaskById(int id)
        {
            var result = Repository.Get(id);
            return mapper.Map<EditTaskModelView>(result);
        }

        public async Task<ResponseBase> RestoreTask(ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            if (user != null)
            {

                Tasks result = Repository.GetFiltered(x => x.VodafoneUserId == user.Id && x.IsDeleted)
                    .OrderByDescending(x=>x.DeletedDate)
                    .FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        result.IsDeleted =false;
                        result.DeletedDate =null;
                        Repository.Update(result);
                        if (unitOfWork.SaveChanges())
                        {
                            return new ResponseBase()
                            {
                                Message = "Task Restored Successfully",
                                Succeeded = true,
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        string ee = e.Message;
                    }

                }
            }
            return new ResponseBase()
            {
                Message = "Cannot Restore the Task",
                Succeeded = false,
            };
        }
    }
}
