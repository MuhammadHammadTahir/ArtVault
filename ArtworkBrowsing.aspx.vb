Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class ArtworkBrowsing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Check if user is authenticated
        If Session("Authenticated") Is Nothing OrElse Not CBool(Session("Authenticated")) Then
            ' Optional: Redirect to login or show message
            ' Response.Redirect("Login.aspx?returnUrl=ArtworkBrowsing.aspx")
        End If

        If Not IsPostBack Then
            LoadArtworks()
            UpdateCartCount()
            DisplayWelcomeMessage()
        End If
    End Sub

    Private Sub DisplayWelcomeMessage()
        If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
            Dim welcomeMessage As New HtmlGenericControl("div")
            welcomeMessage.Attributes("class") = "welcome-message"
            welcomeMessage.InnerText = "Welcome back, " & Session("Cnic").ToString() & "!"
            form1.Controls.AddAt(0, welcomeMessage)
        End If
    End Sub

    Private Sub LoadArtworks()
        Dim constr As String = "Data Source=Eman\SQLEXPRESS; Initial Catalog=ArtVault; Integrated Security = True"
        Dim con As New SqlConnection(constr)

        ' Modified query to include customer-specific data if available
        Dim query As String = "SELECT AW.ARTWORK_ID, AW.TITLE, AW.CATEGORY, AW.STATUS, AW.IMAGE, AP.PRICE, " &
                              "A.FIRST_NAME + ' ' + A.LAST_NAME AS ARTIST_NAME "

        ' Add customer-specific fields if logged in
        If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
            query &= ", CASE WHEN OL.ORDER_ID IS NOT NULL THEN 1 ELSE 0 END AS PURCHASED "
        End If

        query &= "FROM ARTWORK AW " &
                "JOIN ARTWORK_PRICE AP ON AW.ARTWORK_ID = AP.ARTWORK_ID " &
                "JOIN ARTIST AR ON AW.ARTIST_ID = AR.ARTIST_ID " &
                "JOIN PERSON A ON AR.ARTIST_ID = A.PERSON_ID "

        ' Add left join for customer's purchased items if logged in
        If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
            query &= "LEFT JOIN ORDERLINE OL ON AW.ARTWORK_ID = OL.ARTWORK_ID " &
                     "LEFT JOIN ORDER_T O ON OL.ORDER_ID = O.ORDER_ID AND O.CUSTOMER_ID = @customerId "
        End If

        query &= "WHERE AW.STATUS = 'Available' " &
                "AND AP.EFFECTIVE_DATE = (SELECT MAX(EFFECTIVE_DATE) FROM ARTWORK_PRICE WHERE ARTWORK_ID = AW.ARTWORK_ID)"

        Dim cmd As New SqlCommand(query, con)

        ' Add customer ID parameter if logged in
        If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
            cmd.Parameters.AddWithValue("@customerId", Session("Cnic").ToString())
        End If

        Dim dt As New DataTable()
        Try
            con.Open()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim artworkId As String = row("ARTWORK_ID").ToString()
                    Dim title As String = row("TITLE").ToString()
                    Dim category As String = row("CATEGORY").ToString()
                    Dim artist As String = row("ARTIST_NAME").ToString()
                    Dim price As Decimal = Convert.ToDecimal(row("PRICE"))
                    Dim imageBytes As Byte() = If(row("IMAGE") Is DBNull.Value, Nothing, CType(row("IMAGE"), Byte()))
                    Dim purchased As Boolean = False

                    ' Check if artwork was purchased by this customer
                    If Session("Authenticated") IsNot Nothing AndAlso CBool(Session("Authenticated")) Then
                        purchased = Convert.ToBoolean(row("PURCHASED"))
                    End If

                    Dim artworkDiv As New HtmlGenericControl("div")
                    artworkDiv.Attributes("class") = "artwork-card"

                    If purchased Then
                        artworkDiv.Style.Add("border", "2px solid #28a745")
                    End If

                    ' Image container
                    Dim imageDiv As New HtmlGenericControl("div")
                    imageDiv.Attributes("class") = "artwork-image-container"

                    If imageBytes IsNot Nothing AndAlso imageBytes.Length > 0 Then
                        Dim img As New HtmlGenericControl("img")
                        img.Attributes("class") = "artwork-image"
                        img.Attributes("src") = "data:image/jpeg;base64," & Convert.ToBase64String(imageBytes)
                        img.Attributes("alt") = title
                        imageDiv.Controls.Add(img)
                    Else
                        Dim noImage As New HtmlGenericControl("div")
                        noImage.InnerText = "No Image Available"
                        imageDiv.Controls.Add(noImage)
                    End If
                    artworkDiv.Controls.Add(imageDiv)

                    ' Details container
                    Dim detailsDiv As New HtmlGenericControl("div")
                    detailsDiv.Attributes("class") = "artwork-details"

                    Dim titleH3 As New HtmlGenericControl("h3")
                    titleH3.Attributes("class") = "artwork-title"
                    titleH3.InnerText = title
                    detailsDiv.Controls.Add(titleH3)

                    Dim artistP As New HtmlGenericControl("p")
                    artistP.Attributes("class") = "artwork-info"
                    artistP.InnerText = "Artist: " & artist
                    detailsDiv.Controls.Add(artistP)

                    Dim categoryP As New HtmlGenericControl("p")
                    categoryP.Attributes("class") = "artwork-info"
                    categoryP.InnerText = "Category: " & category
                    detailsDiv.Controls.Add(categoryP)

                    Dim priceP As New HtmlGenericControl("p")
                    priceP.Attributes("class") = "artwork-price"
                    priceP.InnerText = String.Format("Price: ${0:N2}", price)
                    detailsDiv.Controls.Add(priceP)

                    ' Add to Cart button (only if not purchased)
                    If Not purchased Then
                        Dim addButton As New HtmlGenericControl("button")
                        addButton.Attributes("class") = "add-to-cart"
                        addButton.Attributes("onclick") = String.Format("addToCart('{0}'); return false;", artworkId)
                        addButton.InnerText = "Add to Cart"
                        detailsDiv.Controls.Add(addButton)
                    Else
                        Dim ownedLabel As New HtmlGenericControl("span")
                        ownedLabel.Attributes("class") = "owned-label"
                        ownedLabel.InnerText = "Already Purchased"
                        detailsDiv.Controls.Add(ownedLabel)
                    End If

                    artworkDiv.Controls.Add(detailsDiv)
                    artworkContainer.Controls.Add(artworkDiv)
                Next
            Else
                messageBox.InnerText = "No artworks available for browsing."
                messageBox.Attributes("class") = "message-box error"
                messageBox.Style.Add("display", "block")
            End If
        Catch ex As Exception
            messageBox.InnerText = "Error loading artworks: " & ex.Message
            messageBox.Attributes("class") = "message-box error"
            messageBox.Style.Add("display", "block")
        Finally
            con.Close()
        End Try
    End Sub

    Protected Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
        ' Verify user is logged in before adding to cart
        If Session("Authenticated") Is Nothing OrElse Not CBool(Session("Authenticated")) Then
            messageBox.InnerText = "Please login to add items to your cart."
            messageBox.Attributes("class") = "message-box error"
            messageBox.Style.Add("display", "block")
            Return
        End If

        Dim artworkId As String = hdnArtworkId.Value

        If Not String.IsNullOrEmpty(artworkId) Then
            Dim cart As Dictionary(Of String, Integer) = Session("Cart")
            If cart Is Nothing Then
                cart = New Dictionary(Of String, Integer)()
            End If

            If cart.ContainsKey(artworkId) Then
                cart(artworkId) += 1
            Else
                cart.Add(artworkId, 1)
            End If

            Session("Cart") = cart
            UpdateCartCount()

            messageBox.InnerText = "Artwork added to cart successfully!"
            messageBox.Attributes("class") = "message-box success"
            messageBox.Style.Add("display", "block")
        End If
    End Sub

    Private Sub UpdateCartCount()
        Dim cart As Dictionary(Of String, Integer) = Session("Cart")
        Dim count As Integer = If(cart Is Nothing, 0, cart.Count)
        cartCount.InnerText = count.ToString()
    End Sub
End Class