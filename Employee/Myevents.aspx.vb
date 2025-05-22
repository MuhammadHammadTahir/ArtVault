
Imports System.Data.SqlClient

Partial Class Employee_Myevents
    Inherits System.Web.UI.Page

    Private Sub Myinfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Session_Cnic As String = CType(Session("Cnic"), String)
        Dim Role As String = CType(Session("Role"), String)

        Dim Session_Password As String = CType(Session("Password"), String)

        If Authenticated = False Then
            Response.Redirect("../Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Employee" Then
            Response.Redirect("../Login.aspx")
        End If
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "SELECT E.EVENT_NAME, E.START_DATE, E.END_DATE, E.EVENT_LOCATION, (P.FIRST_NAME + ' ' + P.LAST_NAME) AS EVENT_EMPLOYEE FROM EVENT E INNER JOIN PERSON P ON P.PERSON_ID = E.EMPLOYEE_ID WHERE E.START_DATE >= @Today AND EMPLOYEE_ID = @session_id"
        cmd.Parameters.AddWithValue("@Today", DateTime.Now)
        cmd.Parameters.AddWithValue("@session_id", Session_Cnic)
        Dim dr As SqlDataReader
        Try
            con.Open()
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                Dim myevent_grid As New GridView
                myevent_grid.DataSource = dr
                myevent_grid.DataBind()
                myevent_grid.Style("padding") = "10px"
                myevent_grid.Style("border") = "1px solid #ccc"
                myevent_grid.Style("margin") = "0 auto"

                myevent_grid.CellPadding = 8
                myeventscontainer.Controls.Add(myevent_grid)
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
