<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Myinfo.aspx.vb" Inherits="Myinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Information</title>
    <link rel="icon" type="image/png" href="logo_new.png" />
    <link rel="stylesheet" type="text/css" href="./StyleSheet.css" />
</head>
<body>
    <img accesskey="" src="logo_new.png" alt="logo" id="logo" />
    <form id="form1" runat="server">

        <div id="message_box" runat="server"></div>

        <h2>My Information</h2>

        <div id="grid_container" class="grid-container" runat="server">
            <div runat="server">
                <label for="person_id">CNIC:</label>
                <input id="person_id" type="text" placeholder="e.g., 12345-1234567-1" class="form-control" runat="server" required />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="person_id" ErrorMessage="CNIC is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="person_id" ErrorMessage="Invalid CNIC format." Display="Dynamic" ForeColor="Red" ValidationExpression="\d{5}-\d{7}-\d{1}"></asp:RegularExpressionValidator>
            
            </div>

            <div runat="server">
                <label for="first_name">First Name:</label>
                <input id="first_name" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="first_name" ErrorMessage="First Name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="last_name">Last Name:</label>
                <input id="last_name" type="text" class="form-control" runat="server" />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="last_name" ErrorMessage="Last Name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>

            <div runat="server">
                <label for="email">Email:</label>
                <input id="email" type="text" class="form-control" runat="server" required />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="email" ErrorMessage="Email is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="email" ErrorMessage="Invalid email formate." Display="Dynamic" ForeColor="Red" ValidationExpression="\S+@\S+\.\S+"></asp:RegularExpressionValidator>
            </div>

            <div runat="server">
                <label for="phone">Phone No:</label>
                <input id="phone" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="phone" ErrorMessage="Phone is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="gender">Gender:</label>
                <asp:DropDownList ID="gender" runat="server" class="form-control" required>
                    <asp:ListItem Value="M">M</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="gender" ErrorMessage="Gender is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="city">City:</label>
                <input id="city" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="city" ErrorMessage="city is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="state">State:</label>
                <input id="state" type="text" placeholder="e.g., CA" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="state" ErrorMessage="State is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="postal_code">Postal Code:</label>
                <input id="postal_code" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="postal_code" ErrorMessage="Postal Code is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="country">Country:</label>
                <input id="country" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="country" ErrorMessage="Country is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="role">Role_name:</label>
                <asp:DropDownList id="Role_name" runat="server" class="form-control" required>
                    <asp:ListItem Value="User" Selected="True" Disabled="True">User</asp:ListItem>
                    <asp:ListItem Value="Artist">Artist</asp:ListItem>
                    <asp:ListItem Value=" Employee"> Employee</asp:ListItem>
                    <asp:ListItem Value="Customer">Customer</asp:ListItem>
                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Role_name" ErrorMessage="Role is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>

            <div runat="server">
                <label for="password">Password:</label>
                <input id="password" type="text" class="form-control" runat="server" required />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="password" ErrorMessage="Password is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div id="button_holder" class="submit-container" runat="server">
            <input id="edit" type="submit" value="Edit" runat="server" />
            <input id="update_btn" type="submit" value="Update" runat="server" />
        </div>
    </form>
</body>
</html>
