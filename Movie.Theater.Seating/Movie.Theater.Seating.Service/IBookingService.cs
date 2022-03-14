using System;
namespace Movie.Theater.Seating.Service
{
    public interface IBookingService
    {
        int FillRows(string reservationNumber, int numberOfSeats);
    }
}
