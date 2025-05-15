namespace SweetManagerIotWebService.API.Inventory.Domain.Model.Exceptions.Supply;

public class InvalidSupplyNameException : Exception
{
    public InvalidSupplyNameException(string message) : base(message)
    {
    }
}