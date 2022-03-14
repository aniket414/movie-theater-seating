using System;
using Xunit;
using Movie.Theater.Seating.Service;

namespace Movie.Theater.Seating.UnitTests
{
    public class BookingServiceTests
    {
        BookingService bookingService = new BookingService();

        [Fact]
        public void SampleTest()
        {
            Assert.Equal(12, bookingService.FillRows("R001", 12));
        }
    }
}
