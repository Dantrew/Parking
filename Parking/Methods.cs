using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
            int count = 2;
            var sql = "SELECT * FROM Cars";
            var cars = new List<Models.Car>();
            using (var connection = new SqlConnection(connString))
            {
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            foreach (var car in cars)
            {
                Console.SetCursorPosition(60,count);
                Console.WriteLine($"Car-id: {car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\tParking-id: {car.ParkingSlotsId}");
                count++;
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
        public static List<City> GetCity(int cityNumber)
        {
            var sql = $"SELECT * FROM Cities WHERE Id = ('{cityNumber}')";
            var cities = new List<Models.City>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cities = connection.Query<Models.City>(sql).ToList();
            }
            foreach (var c in cities)
            {
                Console.WriteLine($"City: {c.CityName}");
            }
            Console.WriteLine();
            return cities;
        }

        public static List<City> GetAllCities()
        {
            var sql = $"SELECT * FROM Cities";
            var cities = new List<Models.City>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                cities = connection.Query<Models.City>(sql).ToList();
            }
            foreach (var c in cities)
            {
                Console.WriteLine($"City-id: {c.Id}\t{c.CityName}");
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

        public static List<ParkingHouse> GetAllParkingHouses(int cityNumber)
        {
            var sql = $"SELECT * FROM ParkingHouses WHERE CityId = ('{cityNumber}')";
            var parkingHouses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingHouses = connection.Query<Models.ParkingHouse>(sql).ToList();
            }
            foreach (var ph in parkingHouses)
            {
                Console.WriteLine($"House-id: {ph.Id}\t{ph.HouseName}");
                var houseIdNumber = ph.Id;
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

        public static List<ElectricSlots> GetAllElectricOutlets()
        {
            var sql = $"SELECT COUNT(ElectricOutlet) as 'ElectricOutlet',Hs.HouseName,C.CityName FROM cities C JOIN ParkingHouses HS on C.Id = hs.CityId JOIN ParkingSlots PS on HS.Id = ps.ParkingHouseId WHERE ElectricOutlet = 1 GROUP BY hs.HouseName, C.CityName";
            var electricSlots = new List<ElectricSlots>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                electricSlots = connection.Query<Models.ElectricSlots>(sql).ToList();
            }

            foreach (var es in electricSlots)
            {
                Console.WriteLine($"{es.HouseName} in {es.CityName} has {es.ElectricOutlet} electrical outlets.");
            }

            return electricSlots;
        }

        public static List<ElectricSlots> GetAllElectricCitySlots()
        {
            var sql = $"SELECT COUNT(ElectricOutlet) as 'ElectricOutlet',C.CityName FROM cities C JOIN ParkingHouses HS on C.Id = hs.CityId JOIN ParkingSlots PS on HS.Id = ps.ParkingHouseId WHERE ElectricOutlet = 1 GROUP BY C.CityName Order BY C.CityName";
            var electricSlots = new List<ElectricSlots>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                electricSlots = connection.Query<Models.ElectricSlots>(sql).ToList();
            }

            foreach (var es in electricSlots)
            {
                Console.WriteLine($"{es.CityName} has a total of {es.ElectricOutlet} electrical spots.");
            }

            return electricSlots;
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
                Console.WriteLine($"{ps.Id}\t{ps.SlotNumber}\tElectric outlet = {ps.ElectricOutlet}\tHouse-id: {ps.ParkingHouseId}"); 
            }
            return parkingSlots;
        }

        public static List<ParkingSlotsStatus> ParkingStatus(int place)
        {
            var sql = $"SELECT HouseName,PS.Id,PS.ElectricOutlet,Cs.CityName,C.Plate FROM ParkingSlots PS JOIN ParkingHouses PH on ps.ParkingHouseId = PH.Id JOIN Cities Cs on ph.CityId = cs.Id LEFT JOIN Cars C on PS.Id = C.ParkingSlotsId where ParkingHouseId = {place}";
            var parkingSlotsStatus = new List<Models.ParkingSlotsStatus>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingSlotsStatus = connection.Query<Models.ParkingSlotsStatus>(sql).ToList();
            }

            foreach (var p in parkingSlotsStatus)
            {
                Console.WriteLine($"SlotId: {p.Id}\t Occupied by: {p.Plate}"); 
            }
            return parkingSlotsStatus;
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
            Console.WriteLine("Välj funktion från menyn ");
        }

        public static int ParkCar(int carId, int parkingSlotId)
        {
            int affectedRow = 0;

            string sql = $"UPDATE Cars SET parkingSlotsId = {parkingSlotId} WHERE Id = {carId}";

            using (var connection = new SqlConnection(connString))
            {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow;
        }

        public static int RemoveCar(int carId)
        {
            int affectedRow = 0;

            string sql = $"DELETE Cars WHERE Id = {carId}";

            using (var connection = new SqlConnection(connString))
            {
                affectedRow = connection.Execute(sql);
            }
            return affectedRow;
        }
    }
}


