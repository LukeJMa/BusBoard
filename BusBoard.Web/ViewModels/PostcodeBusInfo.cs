using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class PostcodeBusInfo: BusInfo
    {
        public PostcodeBusInfo(string postcode)
            :base()
        {
            Postcode = postcode;
        }

        public string Postcode { get; set; }
    }
}