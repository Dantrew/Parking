using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Parking.Models;

namespace Parking
{
    internal class Methods
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = Parking; persist security info = True; Integrated Security = True;";

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
        public static void AddCar(Car car)
        {
            int affectedRows = 0;
            var sql = $"insert into Cars(Plate, Make, Color) values ('{car.Plate}', '{car.Make}', '{car.Color}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
        }
        public static List<City> GetAllCities()
        {
            var sql = "SELECT * FROM Cities";
            var cities = new List<Models.City>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cities = connection.Query<Models.City>(sql).ToList();
            }
            foreach (var c in cities)
            {
                Console.WriteLine($"{c.Id}\t{c.CityName}");
            }
            return cities;
        }

        public static void AddCities(City city)
        {
            int affectedRows = 0;
            var sql = $"insert into Cities(CityName) values ('{city.CityName}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
        }

        public static List<ParkingHouse> GetAllParkingHouses()
        {
            var sql = "SELECT * FROM ParkingHouses";
            var parkingHouses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingHouses = connection.Query<Models.ParkingHouse>(sql).ToList();
            }
            foreach (var ph in parkingHouses)
            {
                Console.WriteLine($"{ph.Id}\t{ph.HouseName}   \t{ph.CityId}");
            }
            return parkingHouses;
        }

        public static void AddParkingHouses(ParkingHouse parkingHouse)
        {
            int affectedRows = 0;
            var sql = $"insert into ParkingHouses(HouseName, CityId) values ('{parkingHouse.HouseName}', '{parkingHouse.CityId}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
        }

        public static List<ParkingSlot> GetAllParkingSlots()
        {
            var sql = "SELECT * FROM ParkingSlots";
            var parkingSlots = new List<Models.ParkingSlot>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingSlots = connection.Query<Models.ParkingSlot>(sql).ToList();
            }
            foreach (var ps in parkingSlots)
            {
                Console.WriteLine($"{ps.Id}\t{ps.SlotNumber}\t{ps.ElectricOutlet}\t{ps.ParkingHouseId}");
            }
            return parkingSlots;
        }

        public static void AddParkingSlots(ParkingSlot parkingSlot)
        {
            int affectedRows = 0;
            var sql = $"insert into ParkingSlots(SlotNumber, ElectricOutlet, ParkingHouseID) values ('{parkingSlot.SlotNumber}', '{parkingSlot.ElectricOutlet}', '{parkingSlot.ParkingHouseId}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
        }


        public static void Instructions()
        {
            Console.WriteLine("Välj funktion från menyn");
        }
    }
}


