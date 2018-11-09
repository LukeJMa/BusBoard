using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode)
    {
      PostCode = postCode;
      Stops = new List<StopPoint>();
    }

    public string PostCode { get; set; }
    public List<StopPoint> Stops { get; set; }

      public void AddStop(StopPoint stopPoint, List<BusPrediction> buses)
      {
          stopPoint.NextBuses = buses;
          Stops.Add(stopPoint);
      }
  }
}