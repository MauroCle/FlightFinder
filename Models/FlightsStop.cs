using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class FlightsStop
    {
        public int FlightId { get; set; }
        public int AirportId { get; set; }
        public TimeSpan StopTime { get; set; }
        public int StopNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Flight Flight { get; set; } = null!;
    }
}
