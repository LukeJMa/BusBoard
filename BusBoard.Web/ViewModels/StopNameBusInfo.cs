using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class StopNameBusInfo : BusInfo
    {
        public StopNameBusInfo(string search)
            : base()
        {
            Search = search;
        }

        public string Search { get; set; }
    }
}