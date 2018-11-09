using System;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
        var apiAccessor = new ApiAccessor();

        var input = IOHandler.GetPostcode();

        var stops = apiAccessor.GetStopsByPostcode(input);
            
        for (var i = 0; i < 2 && i < stops.Count; i++)
        {
            var busArrivals = apiAccessor.GetNextBuses(stops[i]);
            IOHandler.PrintNextBuses(stops[i], busArrivals);           
        }
        
        Console.ReadLine();
    }
      
  }
}
