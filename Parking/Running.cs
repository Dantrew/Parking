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
                List<ElectricSlots> slots = new List<ElectricSlots>();

                Console.WriteLine($" ==========Menu===========" +
                "\n                         " +
                "\n [A] Show all cities" +
                "\n [S] Show all parkingspots" +
                "\n [D] Add a car" +
                "\n [F] Remove a parked car" +
                "\n [Q] Quit." +
                "\n                         " +
                "\n =========================");
                Methods.GetAllCars();
                Console.SetCursorPosition(0, 10);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'a':
                        Methods.GetAllCities();
                        Console.WriteLine($"\n==================================================");
                        Console.WriteLine($"\nPress [A] to to show parkinghouses in specific city" +
                        "\nPress [S] to add another City" +
                        "\nPress [D] to add a Parkinghouse" +
                        "\nPress [Q] to get back to menu");
                        key = Console.ReadKey(true);
                        bool runCities = false;
                        while (!runCities)
                        {
                            switch (key.KeyChar)
                            {
                                case 'a':
                                    Console.WriteLine($"\n==================================================\n");
                                    Console.Write("Input city-id: ");
                                    int cityNumber = Convert.ToInt32(Console.ReadLine());
                                    Methods.GetCity(cityNumber);
                                    Methods.GetAllParkingHouses(cityNumber);
                                    Console.WriteLine();
                                    Console.WriteLine($"==================================================\n\nWhich parkinghouse?");
                                    Console.Write("Houseid: "); int phId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine();
                                    Methods.ParkingStatus(phId);
                                    Console.WriteLine($"\n==================================================\n");
                                    ParkCar();
                                    break;
                                case 's':
                                    AddCity();
                                    break;
                                case 'd':
                                    AddParkingHouse();
                                    Console.WriteLine("Add parkingslots.");
                                    AddParkingSlots();
                                    break;
                                case 'q':
                                    break;
                                default:
                                    Methods.Instructions();
                                    break;
                            }
                            Console.ReadLine();
                            runCities = true;
                        }
                        break;
                    case 's':
                        Methods.GetAllElectricOutlets();
                        Console.WriteLine("==================================================");
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
                        break;
                }
                Console.Clear();
            }
        }
        private static void AddCar()
        {
            Console.WriteLine("Enter plate info: ");
            var plate = Console.ReadLine();
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
            Console.WriteLine("Add new parkinghouse: ");
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
            Console.WriteLine("Enter ParkinghouseID");
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
