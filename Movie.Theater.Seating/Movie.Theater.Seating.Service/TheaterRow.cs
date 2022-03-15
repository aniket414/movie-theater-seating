using System;
using System.Text;

namespace Movie.Theater.Seating.Service
{
    public class TheaterRow
    {
        public char Row { get; set; }
        public int EmptySeats { get; set; }
        public int FilledSeats { get; set; }
        public bool IsFilled { get; set; }

        public TheaterRow(char row)
        {
            this.Row = row;
            this.EmptySeats = 20;
            this.FilledSeats = -1;
            this.IsFilled = false;
        }

        public (string, int) BookSeatsInRow(int numberOfSeats, int totalSeats)
        {
            StringBuilder result = new StringBuilder();

            int seatIndex = Math.Min(numberOfSeats, EmptySeats);

            result.Append(Row).Append(++FilledSeats);
            for (int i = 1; i < seatIndex; i++)
            {
                result.Append("," + Row).Append(++FilledSeats);
            }

            FilledSeats += 3;
            EmptySeats -= seatIndex + 3;
            totalSeats = (totalSeats - (seatIndex + (EmptySeats < 0 ? (EmptySeats + 3) : 3)));
            IsFilled = EmptySeats < 0 ? true : false;

            return (result.ToString(), totalSeats);
        }
    }
}
