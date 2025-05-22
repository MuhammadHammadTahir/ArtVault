
Partial Class Employee_Employee
    Inherits System.Web.UI.Page

    Private Sub Employee_Employee_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        'space before Employee
        If Role <> "Employee" Then
            Response.Redirect("../Login.aspx")
        End If

        Dim First_Name As String = CType(Session("First_Name"), String)
        Dim Last_Name As String = CType(Session("Last_Name"), String)
        Dim greeting_string As String = "Hi " & First_Name & " " & Last_Name & " !"
        greeting_Name.InnerText = greeting_string
        greeting_text.InnerText = "Welcome to the ArtVault Employee Panel — where your dedication brings every masterpiece to life."
    End Sub
End Class
