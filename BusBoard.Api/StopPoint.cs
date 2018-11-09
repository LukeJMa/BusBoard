using System.Collections.Generic;

namespace BusBoard.Api
{
    public class StopPoint
    {
        public string naptanId { get; set; }
        public string commonName { get; set; }
        public string indicator { get; set; }
        public decimal distance { get; set; }
        public List<BusPrediction> NextBuses { get; set; }
    }
}