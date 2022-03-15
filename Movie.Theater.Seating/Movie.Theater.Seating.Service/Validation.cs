using System;
using System.Text;

namespace Movie.Theater.Seating.Service
{
    public static class Validation
    {
        public static string Validate(string bookingId, int numberOfSeats, int totalSeats)
        {
            StringBuilder result = new StringBuilder();

            if (numberOfSeats <= 0 || string.IsNullOrWhiteSpace(bookingId))
            {
                result.Append("Invalid values, please check the input.\n");
                return result.ToString();
            }

            if (numberOfSeats > 20)
            {
                result.Append(bookingId + " ").Append("For group/corporate booking contact customer support.\n");
                return result.ToString();
            }

            if (totalSeats < numberOfSeats)
            {
                result.Append(bookingId + " ").Append("Seats unavailable for the requested number of tickets.\n");
                return result.ToString();
            }

            return result.ToString();
        }
    }
}
