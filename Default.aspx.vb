Imports System.Data
Imports ArtVaultDbContext
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dbContext As New ArtVaultDbContext()

            Dim dt As DataTable = dbContext.GetArtPieces()

            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub
End Class


