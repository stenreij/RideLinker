using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        [JsonIgnore]
        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}
