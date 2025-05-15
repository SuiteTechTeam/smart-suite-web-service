using SweetManagerIotWebService.API.Shared.Domain.Repositories;

namespace SweetManagerIotWebService.API.Reservations.Domain.Repositories;

public  interface IBookingRepository : IBaseRepository<Booking>
{
    Task <IEnumerable<Booking>> FindAllByHotelIdAsync(int? hotelId);
    
    Task<IEnumerable<Booking>> FindByCustomerIdAsync(int customerid);
    
    Task<IEnumerable<Booking>> FindByHotelIdAndStateAsync(int hotelid, string state);
    
    
    Task<bool> UpdateBookingEndDateAsync(int id, DateTime endDate);
    Task<bool> UpdateBookingStateAsync(int id, string state);
    
}