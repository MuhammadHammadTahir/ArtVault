
Imports System.Data.SqlClient
Imports System.Drawing

Partial Class allevents
    Inherits System.Web.UI.Page
    Private Sub Myinfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Session_Cnic As String = CType(Session("Cnic"), String)
        Dim Role As String = CType(Session("Role"), String)

        Dim Session_Password As String = CType(Session("Password"), String)

        If Authenticated = False Then
            Response.Redirect("..//Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Admin" Then
            Response.Redirect("../Login.aspx")
        End If
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "SELECT E.EVENT_NAME, E.START_DATE, E.END_DATE, E.EVENT_LOCATION, (P.FIRST_NAME + ' ' + P.LAST_NAME) AS EVENT_EMPLOYEE FROM EVENT E INNER JOIN PERSON P ON P.PERSON_ID = E.EMPLOYEE_ID"
        Dim dr As SqlDataReader
        Try
            con.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                Dim event_grid As New GridView
                event_grid.DataSource = dr
                event_grid.DataBind()
                event_grid.Style("padding") = "10px"
                event_grid.Style("border") = "1px solid #ccc"
                event_grid.Style("margin") = "0 auto"

                event_grid.CellPadding = 8
                eventscontainer.Controls.Add(event_grid)
            Else
                message_box.InnerText = "No events found."
                message_box.Style("opacity") = "1"
            End If
        Finally
            con.Close()
            cmd.Dispose()
        End Try
    End Sub
End Class
