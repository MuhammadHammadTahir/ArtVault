Imports System.Data.SqlClient
Imports System.Security.Policy
Imports System.ServiceModel.Channels

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim is_error As Boolean = False
    Private Sub Register(sender As Object, e As EventArgs) Handles add.ServerClick
        Dim constr As String
        constr = "Data Source=AALLEYY-PROBOOK\SQLEXPRESS;Initial Catalog=ArtVault;Integrated Security=True;Encrypt=False;"
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
        cmd.CommandText &= country.Value + "', '"
        cmd.CommandText &= role.Value + "', '"
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
            If role.Value = "Customer" And is_error = False Then
                Dim account_creation_date As Date = Date.Now

                Dim formatted_date As String = account_creation_date.ToString("yyyy-MM-dd")
                ' Response.Write("Account Creation Date: " & formatted_date)

                cmd.CommandText = "INSERT INTO CUSTOMER (CUSTOMER_ID, ACCOUNT_CREATION_DATE) VALUES"
                cmd.CommandText &= "('" + person_id.Value + "', '"
                cmd.CommandText &= formatted_date + " ')"
                Dim insert_C As Integer = cmd.ExecuteNonQuery()
                'If insert_C > 0 Then
                '    Response.Write("C Inserted Successfully")
                'Else
                '    Response.Write("C Insertion Failed")
                'End If
            End If
            con.Close()
            cmd.Dispose()
            If is_error = False Then
                Response.Redirect("Login.aspx")
            End If
        End Try

    End Sub


End Class