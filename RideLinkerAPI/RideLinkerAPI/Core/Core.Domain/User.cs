using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Core.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        public string? MobileNr { get; set; }
        public Role UserRole { get; set; }
        [JsonIgnore]
        public ICollection<Trip> TripsAsDriver { get; set; } = new List<Trip>();
    }
}
