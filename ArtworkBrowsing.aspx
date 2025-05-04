<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ArtworkBrowsing.aspx.vb" Inherits="ArtworkBrowsing" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Artworks</title>
    <style type="text/css">
        body {
            display: flex;
            justify-content: center;
            align-items: flex-start;
            min-height: 100vh;
            margin: 0;
            padding: 80px 20px 20px;
            background: linear-gradient(to top right, #1E3A8A, #FACC15);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        #logo {
            width: 170px;
            height: auto;
            position: fixed;
            top: 10px;
            left: 20px;
            z-index: 1000;
        }

        #form1 {
            width: 90%;
            max-width: 1200px;
            background: rgba(255, 255, 255, 0.15);
            backdrop-filter: blur(10px);
            -webkit-backdrop-filter: blur(10px);
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.2);
            border: 1px solid rgba(255, 255, 255, 0.3);
            margin-top: 20px;
        }

        .header {
            text-align: center;
            color: white;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.3);
            margin-bottom: 30px;
        }

        .cart-summary {
            position: fixed;
            top: 20px;
            right: 20px;
            background: rgba(255,255,255,0.9);
            padding: 10px 15px;
            border-radius: 5px;
            color: #1E3A8A;
            font-weight: bold;
            z-index: 1000;
        }

        .artwork-container {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
            gap: 30px;
            padding: 20px 0;
        }

        .artwork-card {
            background: rgba(255, 255, 255, 0.85);
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .artwork-card:hover {
            transform: translateY(-5px);
            box-shadow: 0px 8px 20px rgba(0, 0, 0, 0.15);
        }

        .artwork-image-container {
            height: 200px;
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgba(255, 255, 255, 0.3);
            border-radius: 5px;
            margin-bottom: 15px;
            overflow: hidden;
        }

        .artwork-image {
            max-width: 100%;
            max-height: 100%;
            object-fit: contain;
        }

        .artwork-title {
            font-size: 1.2em;
            color: #333;
            margin: 10px 0;
            text-align: center;
        }

        .artwork-info {
            color: #555;
            margin: 5px 0;
            font-size: 0.9em;
        }

        .artwork-price {
            font-weight: bold;
            color: #1E3A8A;
            font-size: 1.1em;
            margin: 10px 0;
        }

        .add-to-cart {
            background-color: #1E3A8A;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin-top: 15px;
            transition: background-color 0.3s;
            width: 100%;
        }

        .add-to-cart:hover {
            background-color: #152C5B;
        }

        .message-box {
            width: 90%;
            padding: 10px;
            border-radius: 5px;
            text-align: center;
            margin: 10px auto;
            display: none;
        }

        .success {
            background-color: #d4edda;
            color: #155724;
        }

        .error {
            background-color: #f8d7da;
            color: #721c24;
        }

        .welcome-message {
            background-color: #d4a017;
            color: #000;
            padding: 10px;
            border-radius: 5px;
            text-align: center;
            margin-bottom: 20px;
        }

        .owned-label {
            background-color: #28a745;
            color: white;
            padding: 5px 10px;
            border-radius: 4px;
            font-size: 0.9em;
            margin-top: 10px;
        }

        @media (max-width: 768px) {
            .artwork-container {
                grid-template-columns: repeat(auto-fill, minmax(240px, 1fr));
            }
            
            #form1 {
                width: 95%;
                padding: 20px;
            }
        }
    </style>
</head>
<body>
    <img src="logo_new.png" alt="logo" id="logo" />
    
    <div class="cart-summary">
        <span id="cartCount" runat="server">0</span> items in cart | 
        <a href="Cart.aspx" style="color: #1E3A8A; text-decoration: none;">View Cart</a>
    </div>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="header">
            <h1>Artwork Collection</h1>
        </div>

        <div id="messageBox" runat="server" class="message-box"></div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="artwork-container" id="artworkContainer" runat="server">
                    <!-- Artworks will be loaded here -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:HiddenField ID="hdnArtworkId" runat="server" />
        <asp:Button ID="btnAddToCart" runat="server" OnClick="btnAddToCart_Click" style="display:none;" />
    </form>

    <script type="text/javascript">
        function addToCart(artworkId) {
            document.getElementById('<%= hdnArtworkId.ClientID %>').value = artworkId;
            __doPostBack('<%= btnAddToCart.ClientID %>', '');
        }
    </script>
</body>
</html>