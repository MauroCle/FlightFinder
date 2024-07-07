using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class OccupancyStatus
    {
        public int FlightId { get; set; }
        public int ClassId { get; set; }
        public int Slots { get; set; }
        public int? AvailableSlots { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual FlightClass Class { get; set; } = null!;
        public virtual Flight Flight { get; set; } = null!;
    }
}
