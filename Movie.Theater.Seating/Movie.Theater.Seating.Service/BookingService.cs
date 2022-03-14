using System;

namespace Movie.Theater.Seating.Service
{
    public class BookingService : IBookingService
    {
        public int FillRows(string reservationNumber, int numberOfSeats)
        {
            Console.WriteLine("Service Layer");
            return numberOfSeats;
        }
    }
}
