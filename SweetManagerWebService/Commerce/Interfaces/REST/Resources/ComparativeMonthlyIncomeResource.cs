namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

public record ComparativeMonthlyIncomeResource(int? MonthNumbers, decimal TotalIncome, decimal TotalExpense, decimal TotalProfit);