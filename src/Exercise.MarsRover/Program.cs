using System;
using System.Configuration;
using System.IO;
using Exercise.MarsRover.Models;
using Exercise.MarsRover.Models.Enums;
using Exercise.MarsRover.Services;
using Exercise.MarsRover.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Exercise.MarsRover
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Mission started.");

            // create service collection
            RegisterServices();

            var roverService = _serviceProvider.GetService<IRoverService>();

            bool IsContinue;

            Console.WriteLine("Please enter the upper-right coordinates of the plateau.(Format=X Y)");
            Console.WriteLine("Info: Consider that the lower-left coordinates are assumed to be 0,0");
            string upperRightCoordinates = Console.ReadLine();
            var _plateauCoordinates = upperRightCoordinates.Split(" ");

            do
            {
                Console.WriteLine("Please enter the current coordinates and direction(N/E/S/W). (Format=X Y D)");
                string currentCoordinates = Console.ReadLine();
                var _currentCoordinates = currentCoordinates.Split(" ");
                Enum.TryParse(_currentCoordinates[2], out DirectionEnum _direction);

                Console.WriteLine("Please enter the your intsructions. (L=left, R=right, M=Move)");
                string command = Console.ReadLine();

                var plateau = new PlateauModel(Convert.ToInt32(_plateauCoordinates[0]), Convert.ToInt32(_plateauCoordinates[1]));
                var rover = new RoverModel(Convert.ToInt32(_currentCoordinates[0]), Convert.ToInt32(_currentCoordinates[1]), _direction, command);

                var response = roverService.Run(plateau, rover);

                if (!response.HasError)
                {
                    Console.WriteLine($"Current location info: {response.Data.CurrentXCoordinate} {response.Data.CurrentYCoordinate} {response.Data.Direction}");

                    Console.WriteLine("Do you want to move new rover? (Y/N)");
                    string moveNewRover = Console.ReadLine();

                    if (moveNewRover.ToUpper() == "Y")
                    {
                        IsContinue = true;
                    }
                    else
                    {
                        IsContinue = false;
                    }
                }
                else
                {
                    Console.WriteLine($"{response.Errors}");
                    IsContinue = false;
                }
                
            } while (IsContinue);

            Console.WriteLine("Mission completed.");
            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IRoverService, RoverService>();

            ILoggerFactory loggerFactory = new LoggerFactory();

            collection.AddSingleton(loggerFactory);
            collection.AddLogging(); // Allow ILogger<T>

            _serviceProvider = collection.BuildServiceProvider();
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
