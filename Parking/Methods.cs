using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Parking.Models;

namespace Parking
{
    internal class Methods
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = Parking; persist security info = True; Integrated Security = True;";
        new Car car = new();

        public static List<Models.Car> GetAllCars()
        {
            var sql = "SELECT * FROM Cars";
            var cars = new List<Models.Car>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\t{car.ParkingSlotsId}");
            }
            return cars;
        }
        public static List<Models.Car> AddCar(List<Car> cars, Car car)
        {
            var sql = $"insert into Cars(Plate, Make, Color) values ('{car.Plate}', '{car.Make}', '{car.Color}')";

            var newCar = new Models.Car
            {
                Plate = Console.ReadLine(),
                Make = Console.ReadLine(),
                Color = Console.ReadLine()
            };
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            return cars;
        }
        public static void Instructions()
        {
            Console.WriteLine("Välj funktion från menyn");
        }
    }
}
