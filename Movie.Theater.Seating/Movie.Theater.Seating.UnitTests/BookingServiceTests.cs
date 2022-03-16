using System;
using Xunit;
using Movie.Theater.Seating.Service;
using FluentAssertions;

namespace Movie.Theater.Seating.UnitTests
{
    public class BookingServiceTests
    {
        BookingService bookingService = new BookingService(10);

        [Fact]
        public void GivenSeatsCountMoreThan20_ShouldReturnCorporateBookingMessage()
        {
            //Arrange
            string bookingId = "R001";
            int numberOfSeats = 21;

            //Act
            var response = bookingService.BookSeats(bookingId, numberOfSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("R001 For group/corporate booking contact customer support.\n");
            Assert.StartsWith("R001", response);
        }

        [Fact]
        public void GivenSeatsCountIs0_ShouldReturnInvalidRequestMessage()
        {
            //Arrange
            string bookingId = "R001";
            int numberOfSeats = 0;

            //Act
            var response = bookingService.BookSeats(bookingId, numberOfSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("R001 Invalid values, please check the input.\n");
            Assert.StartsWith("R001", response);
        }

        [Fact]
        public void GivenSeatsCountIsMoreThanAvailableSeats_ShouldReturnSeatsUnavailableMessage()
        {
            //Arrange
            string bookingId = "R001";
            int numberOfSeats = 15;
            bookingService.TotalSeats = 10;

            //Act
            var response = bookingService.BookSeats(bookingId, numberOfSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("R001 Seats unavailable for the requested number of tickets.\n");
            Assert.StartsWith("R001", response);
        }

        [Fact]
        public void GivenValidSeatsCountAndBookingRequest_ShouldReturnBookedSeats()
        {
            //Arrange
            string bookingId = "R001";
            int numberOfSeats = 15;
            bookingService.TotalSeats = 30;

            //Act
            var response = bookingService.BookSeats(bookingId, numberOfSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("R001 J0,J1,J2,J3,J4,J5,J6,J7,J8,J9,J10,J11,J12,J13,J14\n");
            Assert.StartsWith("R001", response);
            Assert.True(bookingService.TotalSeats == 12);
        }

        [Fact]
        public void GivenValidSeatsCountAndBookingRequest_ShouldReturnBookedSeatsInDifferentRows()
        {
            //Arrange
            BookingService customBookingService = new BookingService(4);

            //Act
            var response = customBookingService.BookSeats("R001", 13);
            response = customBookingService.BookSeats("R002", 15);
            response = customBookingService.BookSeats("R003", 6);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("R003 D16,D17,D18,D19,B18,B19\n");
            Assert.StartsWith("R003", response);
            Assert.True(customBookingService.TotalSeats == 0);
        }
    }
}
