using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo()
    {
      Stops = new List<StopPoint>();
    }

    public List<StopPoint> Stops { get; set; }

      public void AddStop(StopPoint stopPoint, List<BusPrediction> buses)
      {
          stopPoint.NextBuses = buses;
          Stops.Add(stopPoint);
      }
  }
}