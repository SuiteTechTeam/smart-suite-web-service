namespace SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;

public class BookingDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public decimal? PriceRoom { get; set; }
    public decimal? Amount { get; set; }
    public string? State { get; set; }
    public int? PreferenceTemperature { get; set; }
}
