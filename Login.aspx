<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="icon" type="image/png" href="logo_new.png" />
    <link rel="stylesheet" type="text/css" href="./StyleSheet.css" />
</head>
<body>
    <img accesskey="" src="logo_new.png" alt="logo" id="logo" />

    <form id="form2" runat="server">
        
        <div id="message_box" runat="server"></div>
        
        <h2>Login</h2>

        <div class="login-container">
            <div runat="server">
                <label for="person_id">CNIC:</label>
                <input id="person_id" type="text" placeholder="e.g., 12345-1234567-1" class="form-control" runat="server" required="required" pattern="\d{5}-\d{7}-\d{1}" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="person_id" ErrorMessage="CNIC is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="person_id" ErrorMessage="Invalid CNIC format." Display="Dynamic" ForeColor="Red" ValidationExpression="\d{5}-\d{7}-\d{1}"></asp:RegularExpressionValidator>
            
                </div>

            <div runat="server">
                <label for="password">Password:</label>
                <input id="password" type="password" class="form-control" runat="server" required="required" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="person_id" ErrorMessage="Password is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

            </div>
        </div>

        <div class="submit-container" runat="server">
            <input id="login_btn" type="submit" value="Login" runat="server" />
        </div>

        <div class="sign-up">
            <p>Don't have an account? <a href="Registration.aspx">Register Here</a></p>
            
        </div>
       <p>Customer -> cnic : 33102-7125686-9 , password : 1234 </p>
       <p>Admin -> cnic : 33102-7125686-5 , password : 12345 </p>
       <p>Employee -> cnic : 33102-7121686-5 , password : 12345 </p>
       <p>Artist -> cnic : 33102-7126686-9 , password : 1234567 </p>
    </form>
</body>
</html>
