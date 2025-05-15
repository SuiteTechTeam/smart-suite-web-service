using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;

namespace SweetManagerIotWebService.API.Commerce.Domain.Services;

public interface IDashboardQueryService
{
    
    Task<IEnumerable<dynamic>> Handle(GetWeeklyIncomesByHotelIdQuery query);
    
    
    Task<IEnumerable<dynamic>> Handle(GetMonthlyIncomesByHotelIdQuery query);
}