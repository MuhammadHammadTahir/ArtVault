<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VeiwOrders.aspx.vb" Inherits="Admin_VeiwOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Orders</title>
    <link rel="stylesheet" type="text/css" href="../StyleSheet.css" />
    <link rel="icon" type="image/png" href="../logo_new.png" />
</head>
<body>
     <form id="form1" style="max-width:100%; width:60%" runat="server">
     <h2>Orders</h2>
     <div id="message_box" runat="server"></div>
     <div id="orderscontainer" runat="server">
     </div>
 </form>
</body>
</html>
