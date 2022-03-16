# Movie Theater Seating

Tech Stack: C#, DotNet Core 3.1, xUnit\
The console application is designed for a movie theater seating that maximizes customer satisfaction as well as customer safety.

## Assumptions

1. Seats will be booked in FIFO fasion i.e., on a first-come first-serve basis.
2. To maximize customer satisfaction we will book seat farthest from the screen first and keep on moving forward as the booking requests increases.
3. To maximise the customer satisfaction we also make sure to book the seats together for each booking if possible.
4. If the next available fartest row from the screen can't fit all the seats of a reservation then we move on to the next available row.
5. When the requested number of seats in a single booking is more than 20 i.e., more than row capacity we inform the customer to consult the customer support for group/corporate booking.
6. If the number of seats requested are more than the available seats in the theater we inform the customer that the theater is housefull.
7. If booking for seats is not possible in one single row then we split the booking into two different rows which have seats available.
8. To ensure maximum customer safety we keep a buffer of three seats and one row between each booking request.

## How to run the console app via terminal

1. Open Terminal at movie-theater-seating/Movie.Theater.Seating
2. To build the solution execute the below command\
    Command:
    > dotnet build Movie.Theater.Seating.sln  
    Or  
    > dotnet msbuild Movie.Theater.Seating.sln
3. To run the console app\
    Command:
    > dotnet run --project ./Movie.Theater.Seating.Console/Movie.Theater.Seating.Console.csproj -- "/Users/aniket/movie-theater-seating/booking/input.txt"  
    If file path is not specified, the program will pick the default input file present inside booking folder.  
    After executing the above command the path to the output file will be displayed on the terminal.  
4. To run the unit tests\
    Command:
    > dotnet test ./Movie.Theater.Seating.UnitTests/Movie.Theater.Seating.UnitTests.csproj

## Solution Description:

1. Movie.Theater.Seating.Console: It has the main logic of driving the program. It reads the input file calls the service layer for booking and returns output.
2. Movie.Theater.Seating.Service: This is the service layer implementation where all the main logic of booking seats in theater is written.
    - IBookingService: Interface which specifies contract for booking seats. It is implemented by BookingService.
    - BookingService: Process the booking of individual request and implements the IBookingService contracts.
    - TheaterRow: Has the logic of operations which can be performed on each row such as book seats in row and keep a buffer of three seats for customer safety.
3. Movie.Theater.Seating.UnitTests: Unit tests for various scenarios are written here.