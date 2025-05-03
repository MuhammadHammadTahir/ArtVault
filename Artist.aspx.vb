
Imports System.Drawing.Imaging

Partial Class Artist
    Inherits System.Web.UI.Page

    Private Sub Customer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Role As String = CType(Session("Role"), String)

        If Authenticated = False Then
            Response.Redirect("Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("Login.aspx")
        End If
        If Role <> "Artist" Then
            Response.Redirect("Login.aspx")
        End If

        Dim First_Name As String = CType(Session("First_Name"), String)
        Dim Last_Name As String = CType(Session("Last_Name"), String)
        Dim greeting_string As String = "Hi " & First_Name & " " & Last_Name & " !"
        greeting_Name.InnerText = greeting_string
        greeting_text.InnerText = "Welcome to ArtVault — where every masterpiece tells a story. Discover art that speaks to your soul and experience a journey of inspiration and beauty."

    End Sub
End Class


