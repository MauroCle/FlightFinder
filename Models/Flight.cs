using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class Flight
    {
        public int Id { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool DemandBasedPricing { get; set; }
        public int? DemandBasedId { get; set; }
        public TimeSpan? FlightTime { get; set; }
        public bool Valid { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual DemandBasis? DemandBased { get; set; }
        public virtual Airport Destination { get; set; } = null!;
        public virtual Airport Origin { get; set; } = null!;
    }
}
