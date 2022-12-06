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
            Console.WriteLine("Meny");
            Console.WriteLine("====");
            Console.WriteLine("A Show all cars.");
            Console.WriteLine("S Add a car.");
            Console.WriteLine("D Parkinghouses.");
            Console.WriteLine("F Add parkinghouse.");
            Console.WriteLine("G Cities.");
            Console.WriteLine("H Add city.");
            Console.WriteLine("I Parkingslots. ");
            Console.WriteLine("J Add Parkingslots");
            Console.WriteLine("Q Quit.");

            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();

            switch (key.KeyChar)
            {
                case 'a':
                    Methods.GetAllCars();
                    break;
                case 's':
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
                case 'd':
                    Methods.GetAllParkingHouses();
                    break;
                case 'f':
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
                case 'g':
                    Methods.GetAllCities();
                    break;
                case 'h':
                    Console.WriteLine("Add new city name: ");
                    var cityName = Console.ReadLine();
                    var newCity = new Models.City
                    {
                        CityName = cityName
                    };
                    Methods.AddCities(newCity);
                    break;
                case 'i':
                    Methods.GetAllParkingSlots();
                    break;
                case 'j':
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
                
                case 'k':
                    Console.WriteLine("Which car do you want to park?");
                    int carId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Which parkingspot?");
                    int slotId = Convert.ToInt32(Console.ReadLine());

                    Methods.ParkCar(carId, slotId);
                    break;

                case 'l':
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
            Console.WriteLine();
        }
    }
}
