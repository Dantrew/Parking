using Parking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Parking
{
    internal class Running
    {
        public static void Runningprogram()
        {
            while (true)
            {
                List<ElectricSlots> slots = new();

                Methods.MainMenuOptions();
                Methods.RowBreak();
                Methods.GetAllCars();
                Console.SetCursorPosition(0, 10);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'a':
                        Methods.GetAllCities();
                        Methods.RowBreak();
                        Methods.MenuOptions();

                        key = Console.ReadKey(true);
                        bool runCities = false;

                        while (!runCities)
                        {
                            switch (key.KeyChar)
                            {
                                case 'a':
                                    Methods.RowBreak();
                                    Console.WriteLine("Input city-id: ");
                                    int cityNumber = Convert.ToInt32(Console.ReadLine());
                                    Methods.GetCity(cityNumber);
                                    Methods.GetAllParkingHouses(cityNumber);
                                    Console.WriteLine($"                         ");
                                    Methods.RowBreak();
                                    Console.WriteLine($"Which parkinghouse?" +
                                    "\nHouseid: ") ;
                                    int phId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine();
                                    Methods.ParkingStatus(phId);
                                    Methods.RowBreak();
                                    Console.WriteLine();
                                    ParkCar();
                                    break;
                                case 's':
                                    AddCity();
                                    break;
                                case 'd':
                                    AddParkingHouse();
                                    AddParkingSlots();
                                    break;
                                case 'q':
                                    break;
                                default:
                                    Methods.Instructions();
                                    Console.ReadLine();
                                    break;
                            }
                            runCities = true;
                        }
                        break;
                    case 's':
                        Methods.GetAllElectricOutlets();
                        Console.WriteLine();
                        Methods.GetAllElectricCitySlots();
                        Console.ReadKey();
                        break;
                    case 'd':
                        AddCar();
                        break;
                    case 'f':
                        RemoveCar();
                        break;
                    case 'q':
                        Environment.Exit(0);
                        break;
                    default:
                        Methods.Instructions();
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }

        private static void AddCar()
        {
            Console.WriteLine("Enter plate info: ");
            var plate = Console.ReadLine(); // to upper 
            Console.WriteLine("Enter car brand: ");
            var make = Console.ReadLine();
            Console.WriteLine("Enter car color: ");
            var color = Console.ReadLine();

            var newCar = new Models.Car
            {
                Plate = plate,
                Make = make,
                Color = color
            };
            Methods.AddCar(newCar);
        }
        private static void AddParkingHouse()
        {
            Console.WriteLine("Add new parkinghouse name: ");
            var parkingHouseName = Console.ReadLine();
            Console.WriteLine("Add city id: ");
            int cityId = Convert.ToInt32(Console.ReadLine());

            var parkingHouse = new Models.ParkingHouse
            {
                HouseName = parkingHouseName,
                CityId = cityId
            };
            Methods.AddParkingHouses(parkingHouse);
            Methods.GetAllParkingHouses(cityId);
            Console.WriteLine("Add parkingslots");
        }
        private static void AddCity()
        {
            Console.WriteLine("Add new city name: ");
            var cityName = Console.ReadLine();
            var newCity = new Models.City
            {
                CityName = cityName
            };
            Methods.AddCities(newCity);
        }
        private static void AddParkingSlots()
        {
            Console.WriteLine("Enter ParkinghouseID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Amount of new parkingslots: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many slots with electric outlets? ");
            int number2 = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i > number; i++)
            {
                var slotNumber = i;
                bool electricOutlet = false;

                if (i < (number2 + 1))
                {
                    electricOutlet = true;
                }
                var parkingSlot = new Models.ParkingSlot
                {
                    SlotNumber = slotNumber,
                    ParkingHouseId = id,
                    ElectricOutlet = electricOutlet
                };
                Methods.AddParkingSlots(parkingSlot);
            }
        }
        private static void ParkCar()
        {
            Console.WriteLine("Do you want to park a car? y/n");
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.KeyChar)
            {
                case 'y':
                    Console.WriteLine("Which car do you want to park?");
                    int carId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Which parkingspot?");
                    int slotId = Convert.ToInt32(Console.ReadLine());
                    Methods.ParkCar(carId, slotId);
                    break;
                case 'n':
                    break;
                default:
                    Methods.Instructions();
                    break;
            }
        }
        private static void RemoveCar()
        {
            Console.WriteLine("Which car do you want to remove?");
            int carId2 = Convert.ToInt32(Console.ReadLine());

            Methods.RemoveCar(carId2);
        }
    }
}
