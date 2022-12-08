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
            int count = 1;
            Console.SetCursorPosition(49, (count - 1));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===== Cars ===========================================");
            Console.ResetColor();

            var sql = "SELECT * FROM Cars";
            var cars = new List<Models.Car>();
            using (var connection = new SqlConnection(connString))
            {
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            foreach (var car in cars)
            {
                Console.SetCursorPosition(50, count);
                Console.WriteLine($"Car-id: {car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\tParking-id: {car.ParkingSlotsId}");
                count++;
            }
            Console.SetCursorPosition(49, (count + 1));
            LongRowBreak();
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
            var sql = $"SELECT \r\nMAX(PS.SlotNumber) - Count(ca.Plate) as 'FreeSlots'\r\n,Hs.Id, Hs.HouseName\r\n,C.CityName\r\nFROM cities C\r\nJOIN ParkingHouses HS on C.Id = hs.CityId\r\nJOIN ParkingSlots PS on HS.Id = ps.ParkingHouseId\r\nLEFT JOIN Cars Ca on  PS.Id = Ca.ParkingSlotsId\r\nWHERE C.Id = {cityNumber}\r\nGROUP BY hs.HouseName, C.CityName, Hs.Id";
            var parkingHouses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingHouses = connection.Query<Models.ParkingHouse>(sql).ToList();
            }
            foreach (var ph in parkingHouses)
            {
                Console.WriteLine($"House-id: {ph.Id}\t{ph.HouseName}, with {ph.FreeSlots} free spots");
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
                Console.WriteLine();
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
                Console.WriteLine();
            }

            return electricSlots;
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
        public static void RowBreak()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=========================");
            Console.ResetColor();
        }
        public static void LongRowBreak()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("======================================================");
            Console.ResetColor();
        }
        public static void MainMenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"========= Menu ==========");
            Console.ResetColor();
            Console.WriteLine($"" +
            "\n [A] Show all cities" +
            "\n [S] Show all parkingspots" +
            "\n [D] Add a car" +
            "\n [F] Remove a parked car" +
            "\n [Q] Quit." +
            "\n                         ");
        }
        public static void MenuOptions()
        {
            Console.WriteLine($"          " +
                        "\nPress [A] to to show parkinghouses in specific city" +
                        "\nPress [S] to add another City" +
                        "\nPress [D] to add a Parkinghouse" +
                        "\nPress [Q] to get back to menu");
        }

    }
}


