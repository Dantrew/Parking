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
            Car car = new();
            List<Car> cars = new();
            Console.WriteLine("Meny");
            Console.WriteLine("====");
            Console.WriteLine("A Show all cars.");
            Console.WriteLine("S Add a car.");
            Console.WriteLine("D Parkinghouses.");
            Console.WriteLine("F Cities.");
            Console.WriteLine("Q Quit.");

            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();

            switch (key.KeyChar)
            {
                case 'a':
                    Methods.GetAllCars();
                    break;
                case 's':
                    Methods.AddCar(cars, car);
                    break;
                case 'd':
                    //Parkeringshus
                    break;
                case 'f':
                    //Städer
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
