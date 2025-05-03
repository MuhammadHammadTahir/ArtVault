
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls.Expressions

Partial Class Myinfo
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

        If Me.IsPostBack = False Then


            Dim constr As String
            constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
            Dim con As New SqlConnection
            con.ConnectionString = constr
            Dim cmd As New SqlCommand
            cmd.Connection = con
            cmd.CommandText = "SELECT PERSON_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NO, GENDER, CITY, STATE, POSTAL_CODE, COUNTRY, ROLE, PASSWORD FROM PERSON WHERE PERSON_ID = '"
            cmd.CommandText += Session_Cnic + "' AND PASSWORD = '" + Session_Password + "'"
            Dim dr As SqlDataReader
            Try
                con.Open()
                dr = cmd.ExecuteReader
                If dr.Read() Then
                    person_id.Value = dr("PERSON_ID").ToString()
                    first_name.Value = dr("FIRST_NAME").ToString()
                    last_name.Value = dr("LAST_NAME").ToString()
                    email.Value = dr("EMAIL").ToString()
                    phone.Value = dr("PHONE_NO").ToString()
                    gender.SelectedValue = dr("GENDER").ToString()
                    city.Value = dr("CITY").ToString()
                    state.Value = dr("STATE").ToString()
                    postal_code.Value = dr("POSTAL_CODE").ToString()
                    country.Value = dr("COUNTRY").ToString()
                    Role_name.SelectedValue = dr("ROLE").ToString()
                    password.Value = dr("PASSWORD").ToString()
                    If dr("ROLE").ToString() = "Customer" Then
                        Dim con2 As New SqlConnection
                        con2.ConnectionString = constr
                        Dim cmd2 As New SqlCommand
                        cmd2.Connection = con2
                        cmd2.CommandText = "SELECT ACCOUNT_CREATION_DATE FROM CUSTOMER WHERE CUSTOMER_ID = '" + Session_Cnic + "'"
                        Dim dr2 As SqlDataReader
                        Try
                            con2.Open()
                            dr2 = cmd2.ExecuteReader
                            If dr2.Read() Then
                                Dim Date_display As New HtmlGenericControl("div")
                                Date_display.Attributes.Add("class", "Date_display")
                                Dim Date_lable As New HtmlGenericControl("label")
                                Date_lable.InnerText = "Account Creation Date: "
                                Dim Date_feild As New HtmlGenericControl("input")
                                Date_feild.Attributes.Add("type", "text")
                                Date_feild.Attributes.Add("value", dr2("ACCOUNT_CREATION_DATE").ToString())
                                Date_feild.Attributes.Add("readonly", "true")
                                Date_feild.Attributes.Add("class", "form-control")
                                Date_display.Controls.Add(Date_lable)
                                Date_display.Controls.Add(Date_feild)
                                grid_container.Controls.Add(Date_display)
                            End If
                        Catch ex As Exception
                            message_box.Style.Add("opacity", "1")
                            message_box.InnerText = ex.Message
                        Finally
                            con2.Close()
                            cmd.Dispose()
                        End Try

                    End If
                End If

            Catch ex As Exception
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = ex.Message
            Finally
                con.Close()
                cmd.Dispose()
            End Try

            person_id.Attributes.Add("readonly", "true")
            first_name.Attributes.Add("readonly", "true")
            last_name.Attributes.Add("readonly", "true")
            email.Attributes.Add("readonly", "true")
            phone.Attributes.Add("readonly", "true")
            gender.Attributes.Add("disabled", "true")
            city.Attributes.Add("readonly", "true")
            state.Attributes.Add("readonly", "true")
            postal_code.Attributes.Add("readonly", "true")
            country.Attributes.Add("readonly", "true")
            Role_name.Attributes.Add("disabled", "true")
            password.Attributes.Add("readonly", "true")
        End If
        edit.Visible = True
        update_btn.Visible = False


    End Sub

    Private Sub Edit_info(sender As Object, e As EventArgs) Handles edit.ServerClick

        first_name.Attributes.Remove("readonly")
        last_name.Attributes.Remove("readonly")
        email.Attributes.Remove("readonly")
        phone.Attributes.Remove("readonly")
        city.Attributes.Remove("readonly")
        state.Attributes.Remove("readonly")
        postal_code.Attributes.Remove("readonly")
        country.Attributes.Remove("readonly")
        password.Attributes.Remove("readonly")

        'edit.Style.Add("display", "none")
        edit.Visible = False
        update_btn.Visible = True

    End Sub

    Private Sub Update(Sender As Object, e As EventArgs) Handles update_btn.ServerClick
        'handle the update logic here
        Dim Session_Cnic As String = CType(Session("Cnic"), String)
        Dim is_error As Boolean = False
        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "UPDATE PERSON SET FIRST_NAME='" & first_name.Value & "', LAST_NAME='" & last_name.Value & "', "
        cmd.CommandText &= "EMAIL= '" & email.Value & "', PHONE_NO='" & phone.Value & "', "
        cmd.CommandText &= "CITY='" & city.Value & "', STATE='" & state.Value & "', "
        cmd.CommandText &= "POSTAL_CODE='" & postal_code.Value & "', COUNTRY='" & country.Value & "', "
        cmd.CommandText &= "PASSWORD='" & password.Value & "' WHERE PERSON_ID = '"
        cmd.CommandText &= Session_Cnic & "'"
        Try
            con.Open()
            Dim insert As Integer = cmd.ExecuteNonQuery()
            If insert > 0 Then
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = "Record Updated Successfully"
            End If
        Catch ex As Exception
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = ex.Message
            is_error = True
        Finally
            con.Close()
            cmd.Dispose()
        End Try
        If is_error = False Then
            Session("Password") = password.Value
            Response.Redirect("Myinfo.aspx")
        End If
    End Sub

End Class




















