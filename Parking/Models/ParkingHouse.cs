using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Models
{
    internal class ParkingHouse
    {
        public int Id { get; set; }
        public int ParkingHouseId { get; set; }
        public int CityId { get; set; }
        public string HouseName { get; set; }
        public string CityName { get; set; }
        public int FreeSlots { get; set; }
    }
}
