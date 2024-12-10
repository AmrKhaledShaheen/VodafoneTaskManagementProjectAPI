using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.ViewModels.TaskModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.TaskService;

namespace VodafoneTask.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        public ITaskService TaskService;

        public TaskController(ITaskService TaskService)
        {
            this.TaskService = TaskService;
        }
        [Authorize]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("EditView/{id}")]
        public IActionResult EditView(int id)
        {
            return View(TaskService.getTaskById(id));
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteTask/{id}")]
        public async Task<JsonResult> DeleteTask(int id)
        {
            return Json(await TaskService.DeleteTaskByIdAsync(id, User));
        }

        [Authorize]
        [HttpPost]
        [Route("BatchDelete")]
        public async Task<JsonResult> BatchDelete([FromBody] FilterTaskModelView filterTaskModelView)
        {
            return Json(await TaskService.BatchDeleteTask(User, filterTaskModelView.DateFrom ?? DateTime.Now, filterTaskModelView.DateTo ?? DateTime.Now));
        }

        [Authorize]
        [HttpPost]
        [Route("RestoreTask")]
        public async Task<JsonResult> RestoreTask()
        {
            return Json(await TaskService.RestoreTask(User));
        }

        [Authorize]
        [Route("BatchDeleteView")]
        public IActionResult BatchDeleteView()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("EditTask")]
        public async Task<ResponseBase> EditTask([FromBody] EditTaskModelView editTaskModelView)
        {
            var result = await TaskService.EditTaskAsync(editTaskModelView, User);
            return result;

        }

        [Authorize]
        [HttpPost]
        [Route("CreateTask")]
        public async Task<ResponseBase> CreateTask([FromBody] CreateTaskModelView createTaskModelView)
        {
            var result = await TaskService.CreateNewTaskAsync(createTaskModelView, User);
            return result;

        }

        [Authorize]
        [HttpGet]
        [Route("GetPaggedData")]
        public async Task<IActionResult> GetPaggedData()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("GetPaggedDataFilter")]
        public JsonResult GetPaggedDataFilter(FilterTaskModelView filterTaskModelView)
        {
            var result = TaskService.GetPaggedDataAsync(filterTaskModelView, User);
            return Json(new
            {
                recordsTotal = 1000,
                recordsFiltered = result.Result.Count,
                data = result.Result
            }); ;
        }


    }
}
