using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.VM
{
    public class FlightVM
    {
        public string Origin_1 { get; set; }
        public string Destination_1 { get; set; }
        public string FromDate_1 { get; set; }

        public string Origin_2 { get; set; }
        public string Destination_2 { get; set; }
        public string FromDate_2 { get; set; }
    }

    public class ScheduleVM
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNo { get; set; }
        public string SeatNo { get; set; }
        public string Status { get; set; }
        public int ScheduleID { get; set; }
        public string DepartTime { get; set; }
    }
}