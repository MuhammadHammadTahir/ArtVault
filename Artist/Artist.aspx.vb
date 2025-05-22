
Partial Class Artist_Artist
    Inherits System.Web.UI.Page
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Role As String = CType(Session("Role"), String)

        If Authenticated = False Then
            Response.Redirect("../Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Artist" Then
            Response.Redirect("../Login.aspx")
        End If

        Dim First_Name As String = CType(Session("First_Name"), String)
        Dim Last_Name As String = CType(Session("Last_Name"), String)
        Dim greeting_string As String = "Hi " & First_Name & " " & Last_Name & " !"
        greeting_Name.InnerText = greeting_string
        greeting_text.InnerText = "Welcome to the ArtVault Artist Panel — where your creativity becomes timeless and your art finds its audience."
    End Sub
End Class
