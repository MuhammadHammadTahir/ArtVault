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
    Dim First_Name As String
    Dim Last_Name As String

    Dim is_error As Boolean = False
    Private Sub Login(sender As Object, e As EventArgs) Handles login_btn.ServerClick
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        'cmd.CommandText = "SELECT PERSON_ID, ROLE, PASSWORD, FIRST_NAME, LAST_NAME FROM PERSON WHERE PERSON_ID = '"
        'cmd.CommandText += person_id.Value + "' AND PASSWORD = '" + password.Value + "'"
        cmd.CommandText = "SELECT PERSON_ID, ROLE, PASSWORD, FIRST_NAME, LAST_NAME FROM PERSON WHERE PERSON_ID = @user_id AND PASSWORD = @password"
        cmd.Parameters.AddWithValue("@user_id", person_id.Value)
        cmd.Parameters.AddWithValue("@password", password.Value)
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
                First_Name = dr("FIRST_NAME")
                Last_Name = dr("LAST_NAME")
                Session("Cnic") = Session_Cnic
                Session("Password") = Session_Password
                Session("Role") = Session_Role
                Session("Authenticated") = Authenticated
                Session("First_Name") = First_Name
                Session("Last_Name") = Last_Name
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
            If is_error = False And Session_Role = "Customer" Then
                Response.Redirect("Customer.aspx")
            End If
            If is_error = False And Session_Role = "Admin" Then
                Response.Redirect("./Admin/Admin.aspx")
            End If
            'there is space in Employee
            If is_error = False And Session_Role = "Employee" Then
                Response.Redirect("./Employee/Employee.aspx")
            End If
            If is_error = False And Session_Role = "Artist" Then
                Response.Redirect("./Artist/Artist.aspx")
            End If
        End Try

    End Sub
End Class