Imports System.Data
Imports ArtVaultDbContext
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Create an instance of your DbContext
            Dim dbContext As New ArtVaultDbContext()

            ' Get the data from the database
            Dim dt As DataTable = dbContext.GetArtPieces()

            ' Bind the data to the GridView
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub
End Class
