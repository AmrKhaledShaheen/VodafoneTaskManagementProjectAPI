using AutoMapper;
using Domain.Entities;
using Infrastructure.ViewModels.UserAccountManagement;

namespace Infrastructure.Mapping
{
    public class AccountManagementMap : Profile
    {
        public AccountManagementMap() { 
        
            CreateMap<VodafoneUser,SignUpModelView>().ReverseMap();
        }
    }
}
