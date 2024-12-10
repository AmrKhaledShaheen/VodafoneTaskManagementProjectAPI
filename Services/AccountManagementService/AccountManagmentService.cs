using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Repository;
using Infrastructure.ViewModels.UserAccountManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AccountManagementService
{
    public class AccountManagmentService : IAccountManagmentService
    {
        private readonly IGenericRepository<VodafoneUser> attendanceRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private UserManager<VodafoneUser> userManager;
        private SignInManager<VodafoneUser> signInManager;

        public AccountManagmentService(IGenericRepository<VodafoneUser> _attendanceRepository,
            IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<VodafoneUser> userManager,
            SignInManager<VodafoneUser> signInManager)
        {
            this.attendanceRepository = _attendanceRepository;
            this.unitOfWork = _unitOfWork;
            this.mapper = _mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<ResponseBase> SignUpAsync(SignUpModelView signUpModelView)
        {
            var user = await userManager.FindByEmailAsync(signUpModelView.Email);
            if (user == null)
            {
                var vodafoneUser = mapper.Map<VodafoneUser>(signUpModelView);
                var result = await userManager.CreateAsync(vodafoneUser, signUpModelView.Password);
                if (result.Succeeded)
                {
                    return new ResponseBase
                    {
                        Succeeded = true,
                        Message = "You Have Signed Up Successfully, Now Login",
                        Title = signUpModelView.Email
                    };
                }
                else
                {
                    return new ResponseBase
                    {
                        Succeeded = false,
                        Message = "Error, Try Again",
                    };
                }
            }
            else
            {
                return new ResponseBase
                {
                    Succeeded = false,
                    Message = "You Already Have An Account Try To Login",
                };
            }
        }

        public async Task<ResponseBase> LoginAsync(SignUpModelView signUpModelView)
        {
            var user = await userManager.FindByEmailAsync(signUpModelView.Email);
            if (user == null)
            {
                return new ResponseBase
                {
                    Succeeded = false,
                    Message = "Error, Try Again",
                };
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, signUpModelView.Password, false, false);
            if (result.Succeeded)
            {
                return new ResponseBase
                {
                    Succeeded = true,
                    Message = "Login Succeded",
                    Title = signUpModelView.Email
                };
            }
            else
            {
                return new ResponseBase
                {
                    Succeeded = false,
                    Message = "Error, Try Again",
                };
            }
        }

        public void Logout()
        {
            signInManager.SignOutAsync();
        }
    }
}
