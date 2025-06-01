using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoputkeeLite.Core.Models
{
    public class Booking
    {
        public int TripId { get; set; }
        public string PassengerLogin { get; set; }
        public string Status { get; set; } // Active, Completed, Canceled
    }
}
