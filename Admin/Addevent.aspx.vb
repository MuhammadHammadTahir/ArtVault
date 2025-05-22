
Imports System.Data.SqlClient

Partial Class Admin_Addevent
    Inherits System.Web.UI.Page

    Private Sub Admin_Addevent_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        Dim constr As String
        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con

        cmd.CommandText = "SELECT * FROM PERSON WHERE ROLE = 'Employee'"
        Dim dr As SqlDataReader
        Try
            con.Open()
            dr = cmd.ExecuteReader
            Dim eventheadDropDown As New DropDownList()
            eventheadDropDown.ID = "event_head"
            eventheadDropDown.CssClass = "form-control"
            eventheadDropDown.Attributes.Add("required", "true")

            eventheadDropDown.Items.Add(New ListItem("Choose Event Head", "Selection") With {
                .Selected = True,
                .Enabled = False
            })

            While (dr.Read())
                Dim person_id As String = dr("PERSON_ID").ToString()
                Dim first_name As String = dr("FIRST_NAME").ToString()
                Dim last_name As String = dr("LAST_NAME").ToString()
                Dim full_name As String = first_name + " " + last_name
                eventheadDropDown.Items.Add(New ListItem(full_name, person_id))
            End While

            employee_selection.Controls.Add(eventheadDropDown)
            Dim validation As New RequiredFieldValidator()
            validation.ID = "event_head_validation"
            validation.ControlToValidate = eventheadDropDown.ID
            validation.ErrorMessage = "Please select an event head."
            validation.ForeColor = Drawing.Color.Red
        Catch ex As Exception
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = ex.Message
        Finally
            con.Close()
            cmd.Dispose()
        End Try
    End Sub

    Private Sub Register(sender As Object, e As EventArgs) Handles add.ServerClick
        Dim is_error As Boolean = False
        Dim constr As String

        Dim startDateTime As DateTime = DateTime.Parse(start_date.Value)
        Dim endDateTime As DateTime = DateTime.Parse(end_date.Value)

        ' Validate: start date must be in the future
        If startDateTime <= DateTime.Now Then
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = "Start date must be in the future."
            Exit Sub
        End If

        ' Validate: end date must be after start date
        If endDateTime <= startDateTime Then
            message_box.Style.Add("opacity", "1")
            message_box.InnerText = "End date must be after start date."
            Exit Sub
        End If

        constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
        Dim con As New SqlConnection
        con.ConnectionString = constr
        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = "INSERT INTO EVENT (EVENT_NAME, START_DATE, END_DATE, EVENT_LOCATION, EMPLOYEE_ID) VALUES (@eventName, @startDate, @endDate, @eventLocation, @employeeId)"

        cmd.Parameters.AddWithValue("@eventName", Event_name.Value)
        cmd.Parameters.AddWithValue("@startDate", startDateTime)
        cmd.Parameters.AddWithValue("@endDate", endDateTime)
        cmd.Parameters.AddWithValue("@eventLocation", location.Value)
        Dim eventheadDropDown As DropDownList = CType(employee_selection.FindControl("event_head"), DropDownList)
        Dim even As ListItem = eventheadDropDown.SelectedItem
        cmd.Parameters.AddWithValue("@employeeId", even.Value)
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
            con.Close()
            cmd.Dispose()
            If is_error = False Then
                Response.Redirect("Admin.aspx")
            End If
        End Try

    End Sub

    Private Sub displayAlleventsdata(sender As Object, e As EventArgs) Handles allevents.Click
        Response.Redirect("allevents.aspx")
    End Sub
End Class
