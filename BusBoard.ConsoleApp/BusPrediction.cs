using System;
using System.Net;

namespace BusBoard.ConsoleApp
{
    public class BusPrediction : IComparable<BusPrediction>
    {
        public BusPrediction()
        {
        }

        public string stationName { get; set; }
        public string lineId { get; set; }
        public string lineName { get; set; }
        public string platformName { get; set; }
        public string direction { get; set; }
        public string destinationName { get; set; }
        public DateTime timestamp { get; set; }
        public int timeToStation { get; set; }
        public string currentLocation { get; set; }
        public string towards { get; set; }
        public DateTime expectedArrival { get; set; }

        public override string ToString()
        {
            return lineName + " arriving at " + expectedArrival;
        }

        public int CompareTo(BusPrediction other)
        {
            return this.expectedArrival.CompareTo(other.expectedArrival);
        }
    }
}