using AutoMapper;
using ListedCompany.Models;
using ListedCompany.ViewModels;

namespace ListedCompany.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 映射 MonRevenue 到 MonRevenueViewModel
            CreateMap<MonRevenue, MonRevenueViewModel>()
                .ForMember(dest => dest.CompanyName, opt => opt.Ignore())
                .ForMember(dest => dest.Industry, opt => opt.Ignore())
                .ReverseMap();

            // 映射 CompanyData 到 MonRevenueViewModel
            CreateMap<CompanyData, MonRevenueViewModel>()
                .ForMember(dest => dest.DataYearMonth, opt => opt.Ignore())
                .ForMember(dest => dest.RevenueCurrentMonth, opt => opt.Ignore())
                .ForMember(dest => dest.RevenuePreviousMonth, opt => opt.Ignore())
                .ForMember(dest => dest.RevenueSameMonthLastYear, opt => opt.Ignore())
                .ForMember(dest => dest.RevenueChangePreviousMonth, opt => opt.Ignore())
                .ForMember(dest => dest.RevenueChangeSameMonthLastYear, opt => opt.Ignore())
                .ForMember(dest => dest.CumulativeRevenueCurrentMonth, opt => opt.Ignore())
                .ForMember(dest => dest.CumulativeRevenueLastYear, opt => opt.Ignore())
                .ForMember(dest => dest.CumulativeRevenueChangePreviousPeriod, opt => opt.Ignore())
                .ForMember(dest => dest.Notes, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
