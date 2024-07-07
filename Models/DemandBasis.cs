using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class DemandBasis
    {
        public DemandBasis()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string DemandName { get; set; } = null!;
        public double PriceMultiplier { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
