using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using RestSharp;

namespace BusBoard.Api
{
    public class ApiAccessor
    {
        public string[] Keys;
        public RestClient TflClient;
        public RestClient PostcodeClient;

        public ApiAccessor()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Keys = File.ReadAllLines("C:\\Work\\Training\\BusBoard\\BusBoard\\keys.txt");
            TflClient = new RestClient();
            TflClient.BaseUrl = new Uri("https://api.tfl.gov.uk");
            PostcodeClient = new RestClient();
            PostcodeClient.BaseUrl = new Uri("https://api.postcodes.io");
        }

        public List<BusPrediction> GetNextBuses(StopPoint stopPoint)
        {
            var arrivalsRequest = new RestRequest();
            arrivalsRequest.Resource = "StopPoint/" + stopPoint.naptanId + "/Arrivals?app_id=" + Keys[0] + "&app_key=" + Keys[1];

            var arrivalsResponse = TflClient.Execute<List<BusPrediction>>(arrivalsRequest);
            var busArrivals = arrivalsResponse.Data;
            busArrivals.Sort();
            return busArrivals;
        }

        public List<StopPoint> GetStopsByPostcode(string postcode)
        {

            var postcodeRequest = new RestRequest();
            postcodeRequest.Resource = "postcodes/" + postcode;
            var postcodeResponse = PostcodeClient.Execute<PostcodeResponse>(postcodeRequest);

            var stopsRequest = new RestRequest();
            stopsRequest.Resource = "StopPoint?stopTypes=NaptanPublicBusCoachTram&lat=" + postcodeResponse.Data.result.latitude + "&lon=" + postcodeResponse.Data.result.longitude + "&radius=200&modes=bus&app_id=" + Keys[0] + "&app_key=" + Keys[1];
            return TflClient.Execute<PostcodeStopsResponse>(stopsRequest).Data.stopPoints;
        }

        public List<StopPoint> GetStopsByName(string name)
        {

            var stopsRequest = new RestRequest();
            stopsRequest.Resource = "StopPoint/Search?query=" + name + "&app_id=" + Keys[0] + "&app_key=" + Keys[1];
            var commonNameStopResponse = TflClient.Execute<CommonNameStopResponse>(stopsRequest).Data.matches;
            var stops = new List<StopPoint>();
            foreach (var x in commonNameStopResponse)
            {
                var stopIdRequest = new RestRequest();
                stopIdRequest.Resource = "StopPoint/" + x.id + "?app_id=" + Keys[0] + "&app_key=" + Keys[1];
                return TflClient.Execute<StopIdResponse>(stopIdRequest).Data.children;

            }

            return stops;
        }
    }
}