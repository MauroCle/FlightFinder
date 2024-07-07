using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class ClassFeature
    {
        public int ClassId { get; set; }
        public int BaggageQuantity { get; set; }
        public bool CarryOn { get; set; }
        public bool Additional { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual FlightClass Class { get; set; } = null!;
    }
}
