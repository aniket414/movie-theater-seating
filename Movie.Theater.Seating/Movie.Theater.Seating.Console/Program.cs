using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Movie.Theater.Seating.Service;

namespace Movie.Theater.Seating
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = SetupDI();
            StringBuilder result = new StringBuilder();

            var bookingService = serviceProvider.GetService<IBookingService>();
            string[] bookingRequests = ReadFile(args, out string path);

            foreach (string bookingRequest in bookingRequests)
            {
                List<string> booking = bookingRequest.Split(' ').ToList();
                var bookingId = booking[0];
                var countOfSeats = booking[1];

                if (Int32.TryParse(countOfSeats, out int noOfSeats))
                {
                    var output = bookingService.BookSeats(bookingId, noOfSeats);
                    result.AppendLine(output);
                }
                else
                {
                    result.AppendLine("Seat count is invalid");
                }
            }

            path = path.Substring(0, path.LastIndexOf('/')) + "/output.txt";
            File.WriteAllText(path, result.ToString());
            Console.WriteLine("The output file is at: " + path);
        }

        private static ServiceProvider SetupDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBookingService>(x => new BookingService(10))
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static string[] ReadFile(string[] args, out string path)
        {
            var defaultPath = "/Users/aniket/movie-theater-seating/booking/input.txt";
            bool success = false;
            string[] bookingRequests;
            try
            {
                bookingRequests = File.ReadAllLines(args[0]);
                success = true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot find the specified file! Using the default file.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Cannot find the specified directory! Using the default file.");
            }
            finally
            {
                bookingRequests = File.ReadAllLines(defaultPath);
            }

            path = success ? args[0] : defaultPath;
            return bookingRequests;
        }
    }
}
