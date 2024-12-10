using Infrastructure.Common;
using Infrastructure.ViewModels.UserAccountManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AccountManagementService
{
    public interface IAccountManagmentService
    {
        public Task<ResponseBase> SignUpAsync(SignUpModelView signUpModelView);
        public Task<ResponseBase> LoginAsync(SignUpModelView signUpModelView);
        public void Logout();
    }
}
