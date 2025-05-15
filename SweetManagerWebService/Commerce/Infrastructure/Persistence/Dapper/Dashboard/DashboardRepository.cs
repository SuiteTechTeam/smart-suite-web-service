using System.Data;
using Dapper;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;

namespace SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.Dapper.Dashboard;

public class DashboardRepository(IDbConnection dbConnection) : IDashboardRepository
{
    public async Task<IEnumerable<dynamic>> FindWeeklyComparativeIncomesAsync(int hotelId)
    {
        string query = $"SELECT " +
                       $"WEEK(pc.CreatedAt) AS week_number, " +
                       $"YEAR(pc.CreatedAt) AS year_number, " +
                       $"SUM(pc.final_amount) AS total_income, " +
                       $"SUM(po.final_amount) AS total_expense, " +
                       $"(SUM(pc.final_amount) - SUM(po.final_amount)) AS total_profit " +
                       $"FROM payment_customers pc " +
                       $"LEFT JOIN payment_owners po ON WEEK(pc.CreatedAt) = WEEK(po.CreatedAt) AND YEAR(pc.CreatedAt) = YEAR(po.CreatedAt) " +
                       $"JOIN hotels ho ON po.owner_id = ho.owner_id " +
                       $"WHERE ho.id = {hotelId} " +
                       $"GROUP BY WEEK(pc.CreatedAt), YEAR(pc.CreatedAt) " +
                       $"ORDER BY year_number, week_number";

        var result = await dbConnection.QueryAsync<dynamic>(query, commandType: CommandType.Text);
        return result;
    }

    public async Task<IEnumerable<dynamic>> FindMonthlyComparativeIncomesAsync(int hotelId)
    {
        string query = $"SELECT " +
                       $"MONTH(pc.CreatedAt) AS month_number, " +
                       $"YEAR(pc.CreatedAt) AS year_number, " +
                       $"SUM(pc.final_amount) AS total_income, " +
                       $"SUM(po.final_amount) AS total_expense, " +
                       $"(SUM(pc.final_amount) - SUM(po.final_amount)) AS total_profit " +
                       $"FROM payment_customers pc " +
                       $"LEFT JOIN payment_owners po ON MONTH(pc.CreatedAt) = MONTH(po.CreatedAt) AND YEAR(pc.CreatedAt) = YEAR(po.CreatedAt) " +
                       $"JOIN hotels ho ON po.owner_id = ho.owner_id " +
                       $"WHERE ho.id = {hotelId} " +
                       $"GROUP BY MONTH(pc.CreatedAt), YEAR(pc.CreatedAt) " +
                       $"ORDER BY year_number, month_number";

        var result = await dbConnection.QueryAsync<dynamic>(query, commandType: CommandType.Text);
        return result;
    }

}