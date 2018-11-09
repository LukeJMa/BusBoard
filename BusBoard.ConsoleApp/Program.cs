using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var keys = File.ReadAllLines("C:\\Work\\Training\\BusBoard\\BusBoard\\keys.txt");
        var tflClient = new RestClient();
        tflClient.BaseUrl = new Uri("https://api.tfl.gov.uk");
        var postcodeClient = new RestClient();
        postcodeClient.BaseUrl = new Uri("https://api.postcodes.io");

        var input = GetPostcode();

        var stops = GetStopsByPostcode(tflClient, postcodeClient, keys, input);
            
        for (var i = 0; i < 2 && i < stops.Count; i++)
        {
            var busArrivals = GetNextBuses(tflClient, keys, stops[i]);
            PrintNextBuses(stops[i], busArrivals);           
        }

        
        

        Console.ReadLine();
    }

      private static string GetPostcode()
      {
          return Console.ReadLine();
      }

      private static void PrintNextBuses(StopPoint stopPoint, List<BusPrediction> busArrivals)
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

      private static List<BusPrediction> GetNextBuses(RestClient tflClient, string[] keys, StopPoint stopPoint)
      {
          var arrivalsRequest = new RestRequest();
          arrivalsRequest.Resource = "StopPoint/" + stopPoint.naptanId + "/Arrivals?app_id=" + keys[0] + "&app_key=" + keys[1];

          var arrivalsResponse = tflClient.Execute<List<BusPrediction>>(arrivalsRequest);
          var busArrivals = arrivalsResponse.Data;
          busArrivals.Sort();
          return busArrivals;
      }

      private static List<StopPoint> GetStopsByPostcode(RestClient tflClient, RestClient postcodeClient, string[] keys, string postcode)
      {
          
          var postcodeRequest = new RestRequest();
          postcodeRequest.Resource = "postcodes/" + postcode;
          var postcodeResponse = postcodeClient.Execute<PostcodeResponse>(postcodeRequest);

          var stopsRequest = new RestRequest();
          stopsRequest.Resource = "StopPoint?stopTypes=NaptanPublicBusCoachTram&lat=" + postcodeResponse.Data.result.latitude + "&lon=" + postcodeResponse.Data.result.longitude + "&radius=200&modes=bus&app_id=" + keys[0] + "&app_key=" + keys[1];
          return tflClient.Execute<StopsResponse>(stopsRequest).Data.stopPoints;
        }
  }
}
