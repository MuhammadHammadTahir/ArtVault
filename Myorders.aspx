<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Myorders.aspx.vb" Inherits="Myorders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Orders</title>
    <link rel="stylesheet" type="text/css" href="./StyleSheet.css" />
    <link rel="icon" type="image/png" href="logo_new.png" />
</head>
<body>
    <form id="form1" style="max-width:100%; width:60%" runat="server">
        <h2>Your Orders</h2>
        <div id="message_box" runat="server"></div>
        <div id="orderscontainer" runat="server">
        </div>
    </form>
</body>
</html>
