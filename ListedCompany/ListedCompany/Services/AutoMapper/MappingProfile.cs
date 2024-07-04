using AutoMapper;
using ListedCompany.Models;
using ListedCompany.ViewModels;

namespace ListedCompany.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MonRevenue, MonRevenueViewModel> ();
        CreateMap<CompanyData, MonRevenueViewModel>();



    }
}
