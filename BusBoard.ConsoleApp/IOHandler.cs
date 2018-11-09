using System;
using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
    public static class IOHandler
    {
        public static string GetPostcode()
        {
            return Console.ReadLine();
        }

        public static void PrintNextBuses(StopPoint stopPoint, List<BusPrediction> busArrivals)
        {
            Console.WriteLine("Next buses at: " + stopPoint.commonName + " " + stopPoint.indicator);
            if (busArrivals.Count == 0)
            {
                Console.WriteLine("No buses :(");
            }
            for (int j = 0; j < 5 && j < busArrivals.Count; j++)
            {
                Console.WriteLine(busArrivals[j].ToString());
            }
        }
    }
}