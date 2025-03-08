Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Policy
Imports System.ServiceModel.Channels

Partial Class _Default
    Inherits System.Web.UI.Page

    Dim Session_Cnic As String
    Dim Session_Password As String
    Dim Session_Role As String
    Dim Authenticated As Boolean = False

    Dim is_error As Boolean = False
    Private Sub Login(sender As Object, e As EventArgs) Handles login_btn.ServerClick
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "SELECT PERSON_ID, ROLE, PASSWORD FROM PERSON WHERE PERSON_ID = '"
        cmd.CommandText += person_id.Value + "' AND PASSWORD = '" + password.Value + "'"
        Dim dr As SqlDataReader
        Try
            con.Open()
            dr = cmd.ExecuteReader
            If dr.Read() Then
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = "Login Successful"
                Authenticated = True
                Session_Cnic = dr("PERSON_ID")
                Session_Password = dr("PASSWORD")
                Session_Role = dr("ROLE")
                Session("Cnic") = Session_Cnic
                Session("Password") = Session_Password
                Session("Role") = Session_Role
                Session("Authenticated") = Authenticated
            Else
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = "CNIC or Password is Incorrect"
                is_error = True
            End If
        Catch ex As Exception
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = ex.Message
            is_error = True
        Finally

            con.Close()
            cmd.Dispose()
            'from here use if else statments to redirect to the respective page
            If is_error = False Then
                Response.Redirect("Login.aspx")
            End If
        End Try

    End Sub


End Class