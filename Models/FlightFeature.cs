using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class FlightFeature
    {
        public int FlightId { get; set; }
        public int? StopsQuantity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual Flight Flight { get; set; } = null!;
    }
}
