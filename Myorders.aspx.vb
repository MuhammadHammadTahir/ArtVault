Imports System.Data.SqlClient

Partial Class Myorders
    Inherits System.Web.UI.Page
    Private Sub Myinfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Session_Cnic As String = CType(Session("Cnic"), String)
        Dim Role As String = CType(Session("Role"), String)

        Dim Session_Password As String = CType(Session("Password"), String)

        If Authenticated = False Then
            Response.Redirect("Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        If Role <> "Customer" Then
            Response.Redirect("Login.aspx")
        End If
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "SELECT ORDER_T.ORDER_ID, ORDER_T.CUSTOMER_ID, ORDER_T.RECIPIENT, " &
                 "ORDER_T.ORDER_DATE, ORDER_T.ORDER_STATUS, ORDER_T.PAYMENT_METHOD, " &
                 "ORDERLINE.ARTWORK_ID " &
                 "FROM ORDER_T " &
                 "LEFT JOIN ORDERLINE ON ORDER_T.ORDER_ID = ORDERLINE.ORDER_ID " &
                 "WHERE CUSTOMER_ID = @CustomerId"

        cmd.Parameters.AddWithValue("@CustomerId", Session_Cnic)
        Dim dr As SqlDataReader
        Try
            con.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                Dim orders_grid As New GridView
                orders_grid.DataSource = dr
                orders_grid.DataBind()
                orders_grid.CellPadding = 8
                orders_grid.Style("margin") = "0 auto"
                orders_grid.Style("border") = "1px solid #ccc"
                orderscontainer.Controls.Add(orders_grid)
            Else
                message_box.InnerText = "No orders found."
                message_box.Style("opacity") = "1"
            End If
        Finally
            con.Close()
            cmd.Dispose()
        End Try
    End Sub
End Class









