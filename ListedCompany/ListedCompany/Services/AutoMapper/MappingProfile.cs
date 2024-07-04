using AutoMapper;

namespace ListedCompany.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeViewModel>();


    }
}
