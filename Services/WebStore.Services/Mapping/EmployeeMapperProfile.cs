using AutoMapper;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
        }   
    }
}