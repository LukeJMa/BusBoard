using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult PostcodeBusInfo(Selection selection)
    {
      var apiAccessor = new ApiAccessor();
      List<StopPoint> stops = new List<StopPoint>();
      stops = apiAccessor.GetStopsByPostcode(selection.Postcode);
      
      var info = new PostcodeBusInfo(selection.Postcode);
      for (var i = 0; i < 2 && i < stops.Count; i++)
      {
          var buses = apiAccessor.GetNextBuses(stops[i]);
          info.AddStop(stops[i], buses);
      }
      
      return View(info);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Information about this site";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Contact us!";

      return View();
    }

      public ActionResult ErrorPage()
      {
          ViewBag.Message = "Error!";

          return View();
      }

      public ActionResult StopNameBusInfo(Selection selection)
      {
      var apiAccessor = new ApiAccessor();
      List<StopPoint> stops = new List<StopPoint>();
      stops = apiAccessor.GetStopsByName(selection.StopName);

      var info = new StopNameBusInfo(selection.StopName);
      for (var i = 0; i < 2 && i < stops.Count; i++)
      {
          var buses = apiAccessor.GetNextBuses(stops[i]);
          info.AddStop(stops[i], buses);
      }

      return View(info);
      }
  }
}