using System;
using Microsoft.Extensions.DependencyInjection;
using Movie.Theater.Seating.Service;

namespace Movie.Theater.Seating
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = SetupDI();

            var bookingService = serviceProvider.GetService<IBookingService>();
            var output = bookingService.FillRows("R001", 2);

            Console.WriteLine("Done " + output);
            Console.WriteLine(args[0]);
        }

        private static ServiceProvider SetupDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBookingService, BookingService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
