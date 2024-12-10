using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels.UserAccountManagement;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Services.AccountManagementService;

namespace VodafoneTask.Controllers
{
    public class AccountManagementController : Controller
    {
        private IAccountManagmentService  accountManagmentService;

        public AccountManagementController(IAccountManagmentService accountManagmentService)
        {
            this.accountManagmentService = accountManagmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpModelView signUpModelView)
        {
            var returnValue = accountManagmentService.SignUpAsync(signUpModelView).Result;
            return Json(returnValue);
        }

        [HttpPost]
        public IActionResult Login([FromBody] SignUpModelView signUpModelView)
        {
            var returnValue = accountManagmentService.LoginAsync(signUpModelView).Result;
            return Json(returnValue);
        }

        public IActionResult Logout()
        {
            accountManagmentService.Logout();
            return RedirectToAction("Index","AccountManagement");
        }

    }
}
