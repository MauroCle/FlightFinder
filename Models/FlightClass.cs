using System;
using System.Collections.Generic;

namespace FlightFinder.Models
{
    public partial class FlightClass
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = null!;
        public double? BasePrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
