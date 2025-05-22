<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Registerartwork.aspx.vb" Inherits="Artist_Registerartwork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Artwork</title>
    <link rel="icon" type="image/png" href="../logo_new.png" />
    <link rel="stylesheet" type="text/css" href="../StyleSheet.css" />
</head>
<body>
    <img accesskey="" src="../logo_new.png" alt="logo" id="logo" />
    <form id="form1" runat="server">

        <div id="message_box" runat="server"></div>

        <h2>Register New Artwork</h2>

        <div id="grid_container" class="grid-container" runat="server">
            <div runat="server">
                <label for="Artwork_id">Artwork ID:</label>
                <input id="Artwork_id" type="text" class="form-control" placeholder="ART-001" runat="server" required="required" pattern="ART-\d+" />

                <asp:RequiredFieldValidator ID="rfvArtworkId" runat="server"
                    ControlToValidate="Artwork_id"
                    ErrorMessage="Artwork ID is required."
                    Display="Dynamic"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revArtworkId" runat="server"
                    ControlToValidate="Artwork_id"
                    ErrorMessage="Artwork ID must be in format ART- followed by numbers (e.g., ART-001)."
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationExpression="^ART-\d+$">
                </asp:RegularExpressionValidator>
            </div>

            <div runat="server">
                <label for="title">Title:</label>
                <input id="title" type="text" class="form-control" runat="server" required="required" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                    ControlToValidate="title" 
                    ErrorMessage="Title is required." 
                    Display="Dynamic" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div runat="server">
                <label for="Category">Category:</label>
                <input id="Category" type="text" class="form-control" runat="server" required="required" />
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server"
                    ControlToValidate="Category" 
                    ErrorMessage="Category is required." 
                    Display="Dynamic" >
                    ForeColor="Red"</asp:RequiredFieldValidator>
            </div>

            <div runat="server">
                <label for="Status">Status:</label>
                <input id="Status" type="text" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ID="rfvStatus" runat="server"
                    ControlToValidate="Status" 
                    ErrorMessage="Status is required." 
                    Display="Dynamic" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div runat="server">
                <label for="Stocked_Date">Stocked Date:</label>
                <input id="Stocked_Date" type="date" class="form-control" runat="server" required />
                <asp:RequiredFieldValidator ID="rfvStockedDate" runat="server"
                    ControlToValidate="Stocked_Date" 
                    ErrorMessage="Stocked Date is required." 
                    Display="Dynamic" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div>
                <asp:Label ID="lblImage" runat="server" Text="Artwork Image: " Style="font-weight: bold" />
                <asp:FileUpload ID="fileUploadImage" runat="server" /><br />
                <br />
                <asp:RequiredFieldValidator ID="rfvImage" runat="server"
                    ControlToValidate="fileUploadImage"
                    ErrorMessage="Please upload an image."
                    Display="Dynamic"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div runat="server">
                <label for="price">Price:</label>
                <input id="price" type="text" class="form-control" runat="server" required />
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                    ControlToValidate="price" 
                    ErrorMessage="Price is required." 
                    Display="Dynamic" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div runat="server">
                <label for="effective_date">Effective Data:</label>
                <input id="effective_date" type="date" class="form-control" runat="server" required />
                <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server"
                    ControlToValidate="effective_date" 
                    ErrorMessage="Effective Date is required." 
                    Display="Dynamic" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div id="button_holder" class="submit-container" runat="server">
            <input id="Add_Artwork_btn" type="submit" value="Add Artwork" runat="server" />
        </div>
    </form>
</body>
</html>
