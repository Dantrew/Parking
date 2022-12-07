﻿using System;
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
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            foreach (var car in cars)
            {
                Console.WriteLine($"Car-id: {car.Id}\t{car.Plate}\t{car.Make}\t{car.Color}\tParking-id: {car.ParkingSlotsId}");
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
                Console.WriteLine($"{ps.Id}\t{ps.SlotNumber}\tElectric outlet = {ps.ElectricOutlet}\tHouse-id: {ps.ParkingHouseId}"); // måste vi ha med både ps id och slotnumber?
            }
            return parkingSlots;
        }

        public static List<ParkingSlot> GetAllParkingSlots2(int place)
        {
            var sql = $"SELECT HouseName,PS.Id,PS.ElectricOutlet,Cs.CityName,C.Plate FROM ParkingSlots PS JOIN ParkingHouses PH on ps.ParkingHouseId = PH.Id JOIN Cities Cs on ph.CityId = cs.Id LEFT JOIN Cars C on PS.Id = C.ParkingSlotsId where ParkingHouseId = {place}";
            var parkingSlots = new List<Models.ParkingSlot>();
            var parkingHouses = new List<Models.ParkingHouse>();
            var cities = new List<Models.City>();
            var cars = new List<Models.Car>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                parkingSlots = connection.Query<Models.ParkingSlot>(sql).ToList();
                parkingHouses = connection.Query<Models.ParkingHouse>(sql).ToList();
                cities = connection.Query<Models.City>(sql).ToList();
                cars = connection.Query<Models.Car>(sql).ToList();
            }

            foreach (var p in parkingHouses)
            {
                foreach (var c in cities)
                {

                    foreach (var car in cars)
                    {
                        Console.WriteLine($"ParkingHouse Name = {p.HouseName}\t Cityname: {c.CityName}\t Occupied by: {car.Plate}"); // måste vi ha med både ps id och slotnumber?
                    }
                    break;
                }
                break;
            }
            foreach (var ps in parkingSlots)
            {
                Console.WriteLine($"SlotId: {ps.Id}");
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


