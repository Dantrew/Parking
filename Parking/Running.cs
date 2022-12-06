using Parking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Console.WriteLine("[A] Add a car.");
                Console.WriteLine("[S] Add parkinghouse.");
                Console.WriteLine("[D] Add city.");
                Console.WriteLine("[F] Add Parkingslots");
                Console.WriteLine("[G] Park an existing car");
                Console.WriteLine("[H] Remove a parked car");
                Console.WriteLine("Q Quit.");
                Console.WriteLine("===========");
                Methods.GetAllCars();
                Console.WriteLine("===========");
                Methods.GetAllCities();
                Console.WriteLine("===========");
                Methods.GetAllParkingHouses();
                Console.WriteLine("===========");
                Methods.GetAllParkingSlots();
                Console.WriteLine("===========");


                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'a':
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
                        break;
                    case 's':
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
                        break;
                    case 'd':
                        Console.WriteLine("Add new city name: ");
                        var cityName = Console.ReadLine();
                        var newCity = new Models.City
                        {
                            CityName = cityName
                        };
                        Methods.AddCities(newCity);
                        break;
                    case 'f':
                        Console.WriteLine("Enter ParkinghouseID");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Amount of new parkingslots: ");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many slots with electric outlets? ");
                        int number2 = Convert.ToInt32(Console.ReadLine());
                        for (int i = 1; i == number; i++)
                        {
                            var slotNumber = i;
                            bool electricOutlet = false;


                            if (i < number2)
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
                        break;

                    case 'g':
                        Console.WriteLine("Which car do you want to park?");
                        int carId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Which parkingspot?");
                        int slotId = Convert.ToInt32(Console.ReadLine());

                        Methods.ParkCar(carId, slotId);
                        break;

                    case 'h':
                        Console.WriteLine("Which car do you want to remove?");
                        int carId2 = Convert.ToInt32(Console.ReadLine());

                        Methods.RemoveCar(carId2);
                        break;

                    case 'q':
                        //Avsluta
                        break;
                    default:
                        Methods.Instructions();
                        break;

                }
                Console.Clear();
            }
        }
    }
}
