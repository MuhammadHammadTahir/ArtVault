
Imports System.Data.SqlClient

Partial Class Admin_RegisterEmployee
    Inherits System.Web.UI.Page
    Dim is_error As Boolean = False
    Private Sub Register(sender As Object, e As EventArgs) Handles add.ServerClick
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "INSERT INTO PERSON (PERSON_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NO, GENDER, CITY, STATE, POSTAL_CODE, COUNTRY, ROLE, PASSWORD) "
        cmd.CommandText &= "VALUES ('" + person_id.Value + "', '"
        cmd.CommandText &= first_name.Value + "', '"
        cmd.CommandText &= last_name.Value + "', '"
        cmd.CommandText &= email.Value + "', '"
        cmd.CommandText &= phone.Value + "', '"
        cmd.CommandText &= gender.Value + "', '"
        cmd.CommandText &= city.Value + "', '"
        cmd.CommandText &= state.Value + "', '"
        cmd.CommandText &= postal_code.Value + "', '"
        cmd.CommandText &= country.Value + "', 'Employee"
        cmd.CommandText &= "', '"
        cmd.CommandText &= password.Value + "')"
        Try
            con.Open()
            Dim insert As Integer = cmd.ExecuteNonQuery()
            If insert > 0 Then
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = "Record Inserted Successfully"
            Else
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = insert + " Record Inserted"
            End If
        Catch ex As Exception
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = ex.Message
            is_error = True
        Finally
            Dim con2 As New SqlConnection
            con2.ConnectionString = constr
            Dim cmd2 As New SqlCommand
            cmd2.Connection = con2
            cmd2.CommandText = "INSERT INTO EMPLOYEE (EMPLOYEE_ID, ROLE, SALARY, STATUS, JOINING_DATE) " &
                   "VALUES (@employeeId, @role, @salary, @status, @joiningDate)"

            cmd2.Parameters.AddWithValue("@employeeId", person_id.Value)
            cmd2.Parameters.AddWithValue("@role", role.SelectedIndex.ToString())
            cmd2.Parameters.AddWithValue("@salary", CType(Convert.ToDecimal(salary.Value), Decimal))
            cmd2.Parameters.AddWithValue("@status", status.SelectedIndex.ToString())
            cmd2.Parameters.AddWithValue("@joiningDate", DateTime.Now.ToString("yyyy-MM-dd"))

            Try
                con2.Open()
                Dim insert2 As Integer = cmd2.ExecuteNonQuery()
                If insert2 > 0 Then
                    message_box.Style.Add("opacity", "1")
                    message_box.InnerText = "Record Inserted Successfully"
                Else
                    message_box.Style.Add("opacity", "1")
                    message_box.InnerText = insert2 + " Record Inserted"
                End If
            Catch ex As Exception
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = ex.Message
                is_error = True
            Finally
                con2.Close()
                cmd2.Dispose()
            End Try

            con.Close()
            cmd.Dispose()
            If is_error = False Then
                Response.Redirect("Admin.aspx")
            End If
        End Try

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Role As String = CType(Session("Role"), String)

        If Authenticated = False Then
            Response.Redirect("../Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Admin" Then
            Response.Redirect("../Login.aspx")
        End If
    End Sub
End Class
