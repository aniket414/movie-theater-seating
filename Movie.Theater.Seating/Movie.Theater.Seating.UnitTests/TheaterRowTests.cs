using System;
using FluentAssertions;
using Movie.Theater.Seating.Service;
using Xunit;

namespace Movie.Theater.Seating.UnitTests
{
    public class TheaterRowTests
    {
        TheaterRow theaterRow = new TheaterRow('J');

        [Fact]
        public void GivenTotalSeatsAndBookSeatsCount_ShouldReturnBookedSeats()
        {
            //Arrange
            int totalSeats = 20;
            int numberOfSeats = 5;
            string response;

            //Act
            (response, totalSeats) = theaterRow.BookSeatsInRow(numberOfSeats, totalSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("J0,J1,J2,J3,J4");
            Assert.StartsWith("J0", response);
            Assert.True(totalSeats == 12);
            Assert.True(theaterRow.IsFilled == false);
        }

        [Fact]
        public void GivenTotalSeatsAndBookSeatsCount_ShouldReturnBookedSeatsAndUpdateRowAsWellTotalSeatStatus()
        {
            //Arrange
            int totalSeats = 25;
            int numberOfSeats = 20;
            string response;

            //Act
            (response, totalSeats) = theaterRow.BookSeatsInRow(numberOfSeats, totalSeats);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("J0,J1,J2,J3,J4,J5,J6,J7,J8,J9,J10,J11,J12,J13,J14,J15,J16,J17,J18,J19");
            Assert.StartsWith("J0", response);
            Assert.True(theaterRow.IsFilled == true);
            Assert.True(totalSeats == 5);
        }

        [Fact]
        public void GivenTotalSeatsAndBookSeatsCountForMultipleBooking_ShouldReturnBookedSeatsAndUpdateRowAsWellTotalSeatStatus()
        {
            //Arrange
            int totalSeats = 25;
            int numberOfSeatsInFirstBooking = 15;
            int numberOfSeatsInSecondBooking = 1;
            string responseFirst, responseSecond;

            //Act
            (responseFirst, totalSeats) = theaterRow.BookSeatsInRow(numberOfSeatsInFirstBooking, totalSeats);
            (responseSecond, totalSeats) = theaterRow.BookSeatsInRow(numberOfSeatsInSecondBooking, totalSeats);

            //Assert
            responseFirst.Should().NotBeNull();
            responseFirst.Should().Be("J0,J1,J2,J3,J4,J5,J6,J7,J8,J9,J10,J11,J12,J13,J14");
            Assert.StartsWith("J0", responseFirst);

            responseSecond.Should().NotBeNull();
            responseSecond.Should().Be("J18");
            Assert.StartsWith("J18", responseSecond);

            Assert.True(theaterRow.IsFilled == true);
            Assert.True(totalSeats == 5);
        }
    }
}
