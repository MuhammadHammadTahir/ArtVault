<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Admin.aspx.vb" Inherits="Admin_Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin panel</title>
    <link rel="icon" type="image/png" href="../logo_new.png" />
    <link rel="stylesheet" type="text/css" href="../StyleSheet2.css" />
</head>
<body class="customer-body">
    <form id="form2" runat="server">
        <div id="form1" runat="server">
            <img accesskey="" src="../log01.png" alt="logo" id="logo" />
            <div class="navbar">
                <ul class="navbar-list">
                    <li class="navbar-list-items"><a class="link" href="RegisterEmployee.aspx">Add Employees</a></li>
                    <li class="navbar-list-items"><a class="link" href="./Addevent.aspx">Add Events Data</a></li>
                    <li class="navbar-list-items"><a class="link" href="VeiwOrders.aspx">Veiw Orders</a></li>
                    <li class="navbar-list-items glow-link"><a class="link" href="../Gallery.aspx"><span class="gallery">Veiw Gallery</span></a></li>
                </ul>
            </div>
        </div>
        <div class="greeting" id="greeting" runat="server">
            <h1 id="greeting_Name" runat="server"></h1>

            <p id="greeting_text" runat="server"></p>
        </div>

    </form>
</body>
</html>
