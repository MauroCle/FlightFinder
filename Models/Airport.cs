using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightDestinations = new HashSet<Flight>();
            FlightOrigins = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<Flight> FlightDestinations { get; set; }
        public virtual ICollection<Flight> FlightOrigins { get; set; }
    }
}
