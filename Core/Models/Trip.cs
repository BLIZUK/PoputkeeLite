using System;


namespace PoputkeeLite.Core.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string DriverLogin { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
