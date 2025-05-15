using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class ComparativeMonthlyIncomeResourceFromEntityAssembler
{
    public static ComparativeMonthlyIncomeResource ToResourceFromEntity(dynamic entity)
    {
        return new ComparativeMonthlyIncomeResource(entity.month_number, entity.total_income, entity.total_expense,
            entity.total_profit);
    }
}