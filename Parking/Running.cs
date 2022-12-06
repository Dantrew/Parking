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
                Console.WriteLine("Meny");
                Console.WriteLine("==========");
                Console.WriteLine("[A] Choose specific city");
                Console.WriteLine("[S] Add a car.");
                Console.WriteLine("[D] Add parkinghouse.");
                Console.WriteLine("[F] Add city.");
                Console.WriteLine("[G] Add Parkingslots");
                Console.WriteLine("[H] Park an existing car");
                Console.WriteLine("[J] Remove a parked car");
                Console.WriteLine("[Q] Quit.");
                Console.WriteLine("===========");
                Methods.GetAllCars();
                Console.WriteLine("===========");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'a':
                        Methods.GetAllCities();
                        Console.WriteLine("Input city-id: ");
                        int cityNumber = Convert.ToInt32(Console.ReadLine()); //ternary ifall du ej anger ett stadsid så visas alla? 
                        Methods.GetCity(cityNumber);
                        Methods.GetAllParkingHouses(cityNumber);
                        //get all parkingslots där ph-id ==
                        Console.ReadLine();
                        break;
                    case 's':
                        AddCar();
                        break;
                    case 'd':
                        AddParkingHouse();
                        break;
                    case 'f':
                        AddCity();
                        break;
                    case 'g':
                        AddParkingSlots();
                        break;
                    case 'h':
                        Methods.GetAllParkingSlots();
                        ParkCar();
                        break;
                    case 'j':
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
            Console.WriteLine("Which car do you want to park?");
            int carId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which parkingspot?");
            int slotId = Convert.ToInt32(Console.ReadLine());

            Methods.ParkCar(carId, slotId);
        }
        private static void RemoveCar()
        {
            Console.WriteLine("Which car do you want to remove?");
            int carId2 = Convert.ToInt32(Console.ReadLine());

            Methods.RemoveCar(carId2);
        }
    }
}
