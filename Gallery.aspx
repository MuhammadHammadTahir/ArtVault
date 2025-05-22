<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Gallery.aspx.vb" Inherits="Gallery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Art Gallery</title>
    <link href="GalleryStyleSheet.css" rel="stylesheet" />
     <link rel="icon" type="image/png" href="./logo_new.png" />
</head>
<body>
    <form id="form1" runat="server">
        <h2 id="gallery_heading" runat="server">Gallery </h2>
        <div class="gallery-container">
            <asp:Repeater ID="rptGallery" runat="server">
                <ItemTemplate>
                    <div class="art-card">
                        <img src='<%# "data:image/png;base64," & Convert.ToBase64String(CType(Eval("IMAGE"), Byte())) %>' alt="Artwork" class="art-image" />
                        <div class="art-info">
                            <h3><%# Eval("TITLE") %></h3>
                            <p><b>Category:</b> <%# Eval("CATEGORY") %></p>
                            <p><b>Status:</b> <%# Eval("STATUS") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
