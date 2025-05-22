
Imports System.Data
Imports System.Data.SqlClient

Partial Class Gallery
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadArtworkGallery()
        End If
    End Sub

    Private Sub LoadArtworkGallery()
        Dim constr As String = "Data Source=localhost; Initial Catalog=ARTVAULT_Testing; User ID=Hammad; Password=Hammad"
        Dim dt As New DataTable()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("SELECT IMAGE, TITLE, CATEGORY, STATUS FROM ARTWORK", con)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        rptGallery.DataSource = dt
        rptGallery.DataBind()
    End Sub
End Class