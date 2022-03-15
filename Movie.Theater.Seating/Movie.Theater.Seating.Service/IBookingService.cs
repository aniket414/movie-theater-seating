using System;
namespace Movie.Theater.Seating.Service
{
    public interface IBookingService
    {
        string BookSeats(string bookingId, int numberOfSeats);
    }
}
