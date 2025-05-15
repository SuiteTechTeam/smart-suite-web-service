namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Exceptions.Supply;

public class InvalidSupplyStockException : Exception
{
    public InvalidSupplyStockException(string message) : base(message)
    {
    }
}