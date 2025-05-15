namespace SweetManagerIotWebService.API.Commerce.Domain.Repositories;

public interface IDashboardRepository
{
    Task<IEnumerable<dynamic>> FindWeeklyComparativeIncomesAsync(int hotelId);
    Task<IEnumerable<dynamic>> FindMonthlyComparativeIncomesAsync(int hotelId);
}