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
        var client = new RestClient();
        client.BaseUrl = new Uri("https://api.tfl.gov.uk");

        var input = Console.ReadLine();

        var request = new RestRequest();
        var keys = File.ReadAllLines("C:\\Work\\Training\\BusBoard\\BusBoard\\keys.txt");
        request.Resource = "StopPoint/" + input +"/Arrivals?app_id=" + keys[0] +"&app_key="+keys[1];

        var response = client.Execute<List<BusPrediction>>(request);
        var busArrivals = response.Data;
        busArrivals.Sort();

        for (int i = 0; i < 5 && i<= busArrivals.Count; i++)
        {
            Console.WriteLine(busArrivals[i].ToString());
        }
        

        Console.ReadLine();
    }
  }
}
