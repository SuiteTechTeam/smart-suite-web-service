using SweetManagerIotWebService.API.Commerce.Domain.Model.Queries;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;

namespace SweetManagerIotWebService.API.Commerce.Application.Internal.QueryServices;

public class DashboardQueryService(IDashboardRepository dashboardRepository) : IDashboardQueryService
{
    public async Task<IEnumerable<dynamic>> Handle(GetWeeklyIncomesByHotelIdQuery query)
    {
        return await dashboardRepository.FindWeeklyComparativeIncomesAsync(query.HotelId);
    }

    public async Task<IEnumerable<dynamic>> Handle(GetMonthlyIncomesByHotelIdQuery query)
    {
        return await dashboardRepository.FindMonthlyComparativeIncomesAsync(query.HotelId);
    }
}