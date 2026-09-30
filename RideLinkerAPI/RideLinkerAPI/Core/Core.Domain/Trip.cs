using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public Location? Departure { get; set; }
        public Location? Destination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Car? Car { get; set; }
        public User? Driver { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public int DepartureId { get; set; }
        public int DestinationId { get; set; }  
        public int CarId { get; set; }
        public int DriverId { get; set; }
    }
}
