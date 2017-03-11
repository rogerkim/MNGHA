using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Homework.Report
{
    public partial class TicketReport1 : System.Web.UI.Page
    {
        private Homework.Models.HWEntities db = new Homework.Models.HWEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(Server.MapPath("TicketReport.rpt"));
            rptDoc.SetDataSource(GetData());
            rptDoc.SetParameterValue("ImgURL", "./img/saudia.png");
            TicketViewer.ReportSource = rptDoc;
        }

        private TicketDataSet GetData()
        {
            int id = int.Parse(Request["ticketID"]);
            string customerName = Request["customerName"].ToString();

            var ticket = db.Tickets.Find(id);
            string ticketID = id.ToString("D5");
            string orgin = ticket.Schedule.Origin;
            string destination = ticket.Schedule.Destination;
            string departTime = String.Format("{0:yyyy/MM/dd HH:mm}", ticket.Schedule.DepartTime);
            string flightNo = ticket.FlightID;
            string seatNo = ticket.SeatID;

            var result = new TicketDataSet();
            result.TicketDT.AddTicketDTRow(ticketID, orgin, destination, departTime, flightNo, seatNo, customerName);

            return result;
        }
    }
}