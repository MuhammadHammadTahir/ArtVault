Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class Cart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("Cart") Is Nothing OrElse CType(Session("Cart"), Dictionary(Of String, Integer)).Count = 0 Then
                pnlEmptyCart.Visible = True
                pnlCartSummary.Visible = False
            Else
                LoadCartItems()
                pnlEmptyCart.Visible = False
                pnlCartSummary.Visible = True
            End If
        End If
    End Sub

    Private Sub LoadCartItems()
        Dim cart As Dictionary(Of String, Integer) = CType(Session("Cart"), Dictionary(Of String, Integer))
        Dim constr As String = "Data Source=localhost; Initial Catalog=ARTVAULT; User ID=Hammad; Password=Hammad"
        Dim con As New SqlConnection(constr)
        Dim total As Decimal = 0

        Try
            con.Open()

            For Each item As KeyValuePair(Of String, Integer) In cart
                Dim cmd As New SqlCommand("SELECT AW.ARTWORK_ID, AW.TITLE, AP.PRICE, A.FIRST_NAME + ' ' + A.LAST_NAME AS ARTIST_NAME, AW.IMAGE " &
                                         "FROM ARTWORK AW " &
                                         "JOIN ARTWORK_PRICE AP ON AW.ARTWORK_ID = AP.ARTWORK_ID " &
                                         "JOIN ARTIST AR ON AW.ARTIST_ID = AR.ARTIST_ID " &
                                         "JOIN PERSON A ON AR.ARTIST_ID = A.PERSON_ID " &
                                         "WHERE AW.ARTWORK_ID = @artworkId AND " &
                                         "AP.EFFECTIVE_DATE = (SELECT MAX(EFFECTIVE_DATE) FROM ARTWORK_PRICE WHERE ARTWORK_ID = AW.ARTWORK_ID)", con)
                cmd.Parameters.AddWithValue("@artworkId", item.Key)

                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Dim artworkId As String = reader("ARTWORK_ID").ToString()
                    Dim title As String = reader("TITLE").ToString()
                    Dim price As Decimal = Convert.ToDecimal(reader("PRICE"))
                    Dim artist As String = reader("ARTIST_NAME").ToString()
                    Dim imageBytes As Byte() = If(reader("IMAGE") Is DBNull.Value, Nothing, CType(reader("IMAGE"), Byte()))
                    Dim itemTotal As Decimal = price * item.Value

                    total += itemTotal

                    Dim cartItemDiv As New HtmlGenericControl("div")
                    cartItemDiv.Attributes("class") = "cart-item"

                    ' Image
                    Dim imageDiv As New HtmlGenericControl("div")
                    If imageBytes IsNot Nothing AndAlso imageBytes.Length > 0 Then
                        Dim img As New HtmlGenericControl("img")
                        img.Attributes("class") = "cart-item-image"
                        img.Attributes("src") = "data:image/jpeg;base64," & Convert.ToBase64String(imageBytes)
                        img.Attributes("alt") = title
                        imageDiv.Controls.Add(img)
                    Else
                        Dim noImage As New HtmlGenericControl("div")
                        noImage.Attributes("class") = "cart-item-image"
                        noImage.InnerText = "No Image"
                        imageDiv.Controls.Add(noImage)
                    End If
                    cartItemDiv.Controls.Add(imageDiv)

                    ' Details
                    Dim detailsDiv As New HtmlGenericControl("div")
                    detailsDiv.Attributes("class") = "cart-item-details"

                    Dim titleH3 As New HtmlGenericControl("h3")
                    titleH3.Attributes("class") = "cart-item-title"
                    titleH3.InnerText = title
                    detailsDiv.Controls.Add(titleH3)

                    Dim artistP As New HtmlGenericControl("p")
                    artistP.Attributes("class") = "cart-item-info"
                    artistP.InnerText = "Artist: " & artist
                    detailsDiv.Controls.Add(artistP)

                    Dim priceP As New HtmlGenericControl("p")
                    priceP.Attributes("class") = "cart-item-price"
                    priceP.InnerText = String.Format("${0:N2} x {1} = ${2:N2}", price, item.Value, itemTotal)
                    detailsDiv.Controls.Add(priceP)

                    cartItemDiv.Controls.Add(detailsDiv)

                    ' Remove button
                    Dim removeButton As New Button()
                    removeButton.Text = "Remove"
                    removeButton.CssClass = "remove-button"
                    removeButton.CommandArgument = artworkId
                    removeButton.OnClientClick = String.Format("return confirm('Remove {0} from cart?');", title)
                    AddHandler removeButton.Click, AddressOf RemoveButton_Click
                    cartItemDiv.Controls.Add(removeButton)

                    pnlCartItems.Controls.Add(cartItemDiv)
                End If
                reader.Close()
            Next

            lblTotal.Text = String.Format("Total: ${0:N2}", total)
        Catch ex As Exception
            messageBox.InnerText = "Error loading cart items: " & ex.Message
            messageBox.Attributes("class") = "message-box error"
            messageBox.Style.Add("display", "block")
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim artworkId As String = button.CommandArgument

        Dim cart As Dictionary(Of String, Integer) = CType(Session("Cart"), Dictionary(Of String, Integer))
        If cart.ContainsKey(artworkId) Then
            cart.Remove(artworkId)
            Session("Cart") = cart
        End If

        Response.Redirect("Cart.aspx")
    End Sub

    Protected Sub btnCheckout_Click(sender As Object, e As EventArgs)
        ' Check if user is logged in
        If Session("Authenticated") Is Nothing OrElse Not CBool(Session("Authenticated")) Then
            Response.Redirect("Login.aspx?returnUrl=Cart.aspx")
            Return
        End If

        ' Create order
        Dim constr As String = "Data Source=Eman\SQLEXPRESS; Initial Catalog=ArtVault; Integrated Security = True"
        Dim con As New SqlConnection(constr)
        Dim cart As Dictionary(Of String, Integer) = CType(Session("Cart"), Dictionary(Of String, Integer))

        Try
            con.Open()
            Dim transaction As SqlTransaction = con.BeginTransaction()

            Try
                ' Create order header
                Dim orderId = "ORD-" & DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim customerId = Session("Cnic").ToString()
                Dim orderDate = DateTime.Now.ToString("yyyy-MM-dd")

                Dim cmdOrder As New SqlCommand("INSERT INTO ORDER_T (ORDER_ID, CUSTOMER_ID, RECIPIENT, ORDER_DATE, ORDER_STATUS, PAYMENT_METHOD) " &
                                              "VALUES (@orderId, @customerId, @recipient, @orderDate, 'Pending', 'Credit Card')", con, transaction)
                cmdOrder.Parameters.AddWithValue("@orderId", orderId)
                cmdOrder.Parameters.AddWithValue("@customerId", customerId)
                cmdOrder.Parameters.AddWithValue("@recipient", "Customer") ' Should be replaced with actual recipient
                cmdOrder.Parameters.AddWithValue("@orderDate", orderDate)
                cmdOrder.ExecuteNonQuery()

                ' Create order lines
                For Each item In cart
                    Dim cmdOrderLine As New SqlCommand("INSERT INTO ORDERLINE (ORDER_ID, ARTWORK_ID) VALUES (@orderId, @artworkId)", con, transaction)
                    cmdOrderLine.Parameters.AddWithValue("@orderId", orderId)
                    cmdOrderLine.Parameters.AddWithValue("@artworkId", item.Key)
                    cmdOrderLine.ExecuteNonQuery()

                    ' Update artwork status
                    Dim cmdArtwork As New SqlCommand("UPDATE ARTWORK SET STATUS = 'Sold' WHERE ARTWORK_ID = @artworkId", con, transaction)
                    cmdArtwork.Parameters.AddWithValue("@artworkId", item.Key)
                    cmdArtwork.ExecuteNonQuery()
                Next

                transaction.Commit()

                ' Clear cart and show success message
                Session("Cart") = New Dictionary(Of String, Integer)()
                messageBox.InnerText = "Thank you for your purchase! Your order has been placed (Order #: " & orderId & ")."
                messageBox.Attributes("class") = "message-box success"
                messageBox.Style.Add("display", "block")

                pnlCartItems.Controls.Clear()
                pnlEmptyCart.Visible = True
                pnlCartSummary.Visible = False
            Catch ex As Exception
                transaction.Rollback()
                messageBox.InnerText = "Error processing your order: " & ex.Message
                messageBox.Attributes("class") = "message-box error"
                messageBox.Style.Add("display", "block")
            End Try
        Catch ex As Exception
            messageBox.InnerText = "Error connecting to database: " & ex.Message
            messageBox.Attributes("class") = "message-box error"
            messageBox.Style.Add("display", "block")
        Finally
            con.Close()
        End Try
    End Sub
End Class