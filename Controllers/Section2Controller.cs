using Homework.Models;
using Homework.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework.Controllers
{
    public class Section2Controller : Controller
    {
        private HWEntities db = new HWEntities();

        // GET: Section2
        public ActionResult Index()
        {
            // Dropdown list for City
            List<SelectListItem> cityList = new List<SelectListItem>();
            foreach (var city in db.CITies)
            {
                cityList.Add(new SelectListItem() { Text = "[" + city.ID + "] " + city.Description, Value = city.ID });
            }
            ViewBag.CityList = cityList;

            return View();
        }

        [HttpPost]
        public int Book(string scheduleID, string flightID, string seatID, string customerName)
        {
            var ticket = new Ticket();
            ticket.ScheduleID = int.Parse(scheduleID);
            ticket.FlightID = flightID;
            ticket.SeatID = seatID;
            ticket.CustomerName = customerName;

            db.Tickets.Add(ticket);
            db.SaveChanges();

            return ticket.ID;
        }

        public JsonResult Schedule(FlightVM flightInfo)
        {
            string origin = flightInfo.Origin_1;
            string destination = flightInfo.Destination_1;
            DateTime departTime = DateTime.Parse(flightInfo.FromDate_1);

            var scheduleList = db.Schedules
                .Where(x => x.Origin == origin && x.Destination == destination 
                 && DbFunctions.TruncateTime(x.DepartTime) == departTime.Date);

            var result = new List<ScheduleVM>();

            foreach(var schedule in scheduleList)
            {
                var seatList = schedule.Flight.Seats;
                foreach(var seat in seatList)
                {
                    var item = new ScheduleVM();
                    item.FlightNo = schedule.FlightID;
                    item.Origin = schedule.Origin;
                    item.Destination = schedule.Destination;
                    item.SeatNo = seat.SeatID;
                    item.ScheduleID = schedule.ID;
                    item.DepartTime = schedule.DepartTime.ToString("yyyy-MM-dd HH:mm");
                    if (!IsSeatReserved(item))
                    {
                        result.Add(item);
                    }
                } 
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private Boolean IsSeatReserved(ScheduleVM param)
        {
            var ticketList = db.Tickets
                .Where(x => x.FlightID == param.FlightNo && x.ScheduleID == param.ScheduleID
                 && x.SeatID == param.SeatNo);

            return (ticketList.Count() >= 1) ? true : false;
        }
    }
}