using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CameraFinder.Web.Models
{
    public class RequestStatistics
    {
        public HttpMethod Method { get; set; }
        public string Uri { get; set; }

        public DateTime RequestStart { get; set; }
        public DateTime? RequestEnd { get; set; }
        public int RequestMilliseconds { get; set; }

        public DateTime? ActionStart { get; set; }
        public DateTime? ActionEnd { get; set; }
        public int ActionMilliseconds { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}