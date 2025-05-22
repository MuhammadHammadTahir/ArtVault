
Imports System.ComponentModel
Imports System.Data
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
            Response.Redirect("../Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Artist" Then
            Response.Redirect("../Login.aspx")
        End If

        If Me.IsPostBack = False Then


            Dim constr As String
            constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
            Dim con, con2 As New SqlConnection
            con.ConnectionString = constr
            Dim cmd, cmd2 As New SqlCommand
            cmd.Connection = con
            cmd.CommandText = "SELECT PERSON_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NO, GENDER, CITY, STATE, POSTAL_CODE, COUNTRY, ROLE, PASSWORD " &
                 "FROM PERSON WHERE PERSON_ID = @PersonId AND PASSWORD = @Password"
            cmd.Parameters.AddWithValue("@PersonId", Session_Cnic)
            cmd.Parameters.AddWithValue("@Password", Session_Password)
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

                    con2.ConnectionString = constr
                    cmd2.Connection = con2
                        cmd2.CommandText = "SELECT STYLE FROM ARTIST WHERE ARTIST_ID = '" + Session_Cnic + "'"
                        Dim dr2 As SqlDataReader
                        Try
                            con2.Open()
                            dr2 = cmd2.ExecuteReader
                            If dr2.Read() Then
                                Artist_style.SelectedValue = dr2("STYLE").ToString()
                            End If
                        Catch ex As Exception
                            message_box.Style.Add("opacity", "1")
                            message_box.InnerText = ex.Message
                        Finally
                            con2.Close()
                            cmd.Dispose()
                        End Try

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
            Artist_style.Attributes.Add("disabled", "true")
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
        Artist_style.Attributes.Remove("disabled")
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
        Dim con, con2, con3, con4 As New SqlConnection
        con.ConnectionString = constr
        con2.ConnectionString = constr
        con3.ConnectionString = constr
        con4.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "UPDATE PERSON SET FIRST_NAME=@FirstName, LAST_NAME=@LastName, " &
                 "EMAIL=@Email, PHONE_NO=@Phone, CITY=@City, STATE=@State, " &
                 "POSTAL_CODE=@PostalCode, COUNTRY=@Country, PASSWORD=@Password " &
                 "WHERE PERSON_ID = @PersonId"

        cmd.Parameters.AddWithValue("@FirstName", first_name.Value)
        cmd.Parameters.AddWithValue("@LastName", last_name.Value)
        cmd.Parameters.AddWithValue("@Email", email.Value)
        cmd.Parameters.AddWithValue("@Phone", phone.Value)
        cmd.Parameters.AddWithValue("@City", city.Value)
        cmd.Parameters.AddWithValue("@State", state.Value)
        cmd.Parameters.AddWithValue("@PostalCode", postal_code.Value)
        cmd.Parameters.AddWithValue("@Country", country.Value)
        cmd.Parameters.AddWithValue("@Password", password.Value)
        cmd.Parameters.AddWithValue("@PersonId", Session_Cnic)
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
            Dim cmd2, cmd3, cmd4 As New SqlCommand
            cmd2.CommandText = "Select STYLE FROM ARTIST WHERE ARTIST_ID = '"
            cmd2.CommandText &= Session_Cnic & "'"
            Dim dr2 As SqlDataReader
            Try
                con2.Open()
                cmd2.Connection = con2
                dr2 = cmd2.ExecuteReader
                If dr2.Read() Then
                    Dim style As String = dr2("STYLE").ToString()
                    If style <> Artist_style.SelectedValue Then
                        cmd3.CommandText = "UPDATE ARTIST SET STYLE='" & Artist_style.SelectedValue & "' WHERE PERSON_ID = '"
                        cmd3.CommandText &= Session_Cnic & "'"
                        con3.Open()
                        cmd3.Connection = con3
                        Dim insert_C As Integer = cmd3.ExecuteNonQuery()
                        If insert_C > 0 Then
                            message_box.Style.Add("opacity", "1")
                            message_box.InnerText = "Record Updated Successfully"
                        End If
                    End If
                Else
                    cmd4.CommandText = "INSERT INTO ARTIST (ARTIST_ID, STYLE) VALUES ('"
                    cmd4.CommandText &= Session_Cnic & "', '"
                    cmd4.CommandText &= Artist_style.SelectedValue + "')"
                    con4.Open()
                    cmd4.Connection = con4
                    Dim insert_C As Integer = cmd4.ExecuteNonQuery()
                    If insert_C > 0 Then
                        message_box.Style.Add("opacity", "1")
                        message_box.InnerText = "Record Updated Successfully"
                    End If
                End If
            Catch ex As Exception
                message_box.Style.Add("opacity", "1")
                message_box.InnerText = ex.Message
            Finally
                con.Close()
                con2.Close()
                con3.Close()
                con4.Close()
                cmd.Dispose()
                cmd2.Dispose()
                cmd3.Dispose()
                cmd4.Dispose()
            End Try
            con.Close()
            cmd.Dispose()
        End Try
        If is_error = False Then
            Session("Password") = password.Value
            Response.Redirect("Myinfo.aspx")
        End If
    End Sub

End Class




















