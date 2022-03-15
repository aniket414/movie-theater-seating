using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movie.Theater.Seating.Service
{
    public class BookingService : IBookingService
    {
        private List<TheaterRow> Rows;
        private List<TheaterRow> rowsWhereBookingCanBeDone;
        private int TotalSeats { get; set; }
        private string Seat { get; set; }

        public BookingService(int numberOfRowsInTheater)
        {
            Rows = new List<TheaterRow>();
            int noOfRowsWhichCanBeBooked = (numberOfRowsInTheater + 1) / 2;
            TotalSeats = noOfRowsWhichCanBeBooked * 20;

            var lastRow = Convert.ToChar(64 + numberOfRowsInTheater);
            for (char row = lastRow; row > lastRow - numberOfRowsInTheater; row--)
            {
                Rows.Add(new TheaterRow(row));
            }
        }

        public string BookSeats(string bookingId, int numberOfSeats)
        {
            StringBuilder result = new StringBuilder();
            List<String> assignedSeats = new List<string>();

            var validationMessage = Validation.Validate(bookingId, numberOfSeats, TotalSeats);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                return validationMessage;
            }

            // Assign seats farthest from screen for maximizing customer satisfaction.
            for (int rowIndex = 0; rowIndex < Rows.Count; rowIndex = rowIndex + 2)
            {
                if (Rows[rowIndex].EmptySeats >= numberOfSeats)
                {
                    (Seat, TotalSeats) = Rows[rowIndex].BookSeatsInRow(numberOfSeats, TotalSeats);
                    assignedSeats.Add(Seat);
                    break;
                }
            }

            // When sufficient seats are not available in one row split them in other rows.
            if (assignedSeats.Count == 0)
            {
                rowsWhereBookingCanBeDone = Rows.Where(row => row.Row % 2 != 0).ToList();

                rowsWhereBookingCanBeDone.Sort(delegate (TheaterRow x, TheaterRow y) {
                    return y.EmptySeats - x.EmptySeats;
                });

                int rowIndex = 0;
                while (rowIndex < rowsWhereBookingCanBeDone.Count && numberOfSeats > 0)
                {
                    if (!rowsWhereBookingCanBeDone[rowIndex].IsFilled)
                    {
                        int remainingSeats = rowsWhereBookingCanBeDone[rowIndex].EmptySeats;
                        (Seat, TotalSeats) = rowsWhereBookingCanBeDone[rowIndex].BookSeatsInRow(numberOfSeats, TotalSeats);
                        assignedSeats.Add(Seat);
                        numberOfSeats = numberOfSeats - Math.Min(remainingSeats, numberOfSeats);
                    }
                    rowIndex++;
                }
            }

            result.Append(bookingId).Append(" ")
                .Append(String.Join(",", assignedSeats))
                .Append("\n");

            return result.ToString();
        }
    }
}
