<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Addevent.aspx.vb" Inherits="Admin_Addevent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add an Event</title>
    <link rel="icon" type="image/png" href="../logo_new.png" />
    <link rel="stylesheet" type="text/css" href="../StyleSheet.css" />
</head>
<body>

    <img accesskey="" src="../logo_new.png" alt="logo" id="logo" />

    <form id="form1" runat="server">
        <div id="alleventsdata" class="submit-container" runat="server">
            <asp:Button ID="allevents" runat="server" Text="View All Events" CausesValidation="false"/>
        </div>
        <div id="message_box" runat="server"></div>

        <h2>Add Event</h2>


        <div class="grid-container">
            <div runat="server">
                <label for="Event_name">Event Name:</label>
                <input id="Event_name" type="text" placeholder="Timeless Strokes" class="form-control" runat="server" causesvalidation="false" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Event_name" ErrorMessage="Event name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="start_date">Start Date:</label>
                <input id="start_date" type="date" class="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="start_date" ErrorMessage="Start Date is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="end_date">End Date:</label>
                <input id="end_date" type="date" class="form-control" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="end_date" ErrorMessage="End date is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="location">Location:</label>
                <input id="location" type="text" class="form-control" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="location" ErrorMessage="Location is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server" id="employee_selection">
                <label for="event_head">Select Event Head:</label>
            </div>
        </div>


        <div class="submit-container" runat="server">
            <input id="add" type="submit" value="Add Event" runat="server" />
        </div>
    </form>
</body>
</html>
