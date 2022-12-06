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
