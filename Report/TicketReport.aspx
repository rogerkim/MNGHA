﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketReport.aspx.cs" Inherits="Homework.Report.TicketReport1" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PRINT TICKET</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="TicketViewer" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
