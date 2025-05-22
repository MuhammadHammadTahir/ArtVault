
Imports System.Data
Imports System.Data.SqlClient
Imports System.IdentityModel.Protocols.WSTrust
Imports System.Security.Policy

Partial Class Artist_Registerartwork
    Inherits System.Web.UI.Page
    Private Sub Register(sender As Object, e As EventArgs) Handles Add_Artwork_btn.ServerClick

        Dim Authenticated As Boolean = CType(Session("Authenticated"), Boolean)
        Dim Session_Cnic As String = CType(Session("Cnic"), String)
        Dim Role As String = CType(Session("Role"), String)

        Dim Session_Password As String = CType(Session("Password"), String)

        Dim is_error As Boolean = False

        If Authenticated = False Then
            Response.Redirect("../Login.aspx")
        End If
        If Role Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        If Role <> "Artist" Then
            Response.Redirect("../Login.aspx")
        End If

        Try
            Dim imageData As Byte() = Nothing
            If fileUploadImage.HasFile Then
                imageData = fileUploadImage.FileBytes
            End If

            Dim constr As String
            constr = "Data Source= localhost; Initial Catalog= ARTVAULT_Testing; User ID=Hammad; Password= Hammad"
            Dim con As New SqlConnection
            con.ConnectionString = constr
            Dim cmd As New SqlCommand
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO ARTWORK (ARTWORK_ID, TITLE, CATEGORY, ARTIST_ID, STOCKED_DATE, STATUS, IMAGE) VALUES (@artworkId, @title, @category, @artistId, @stockedDate, @status, @image)"
            cmd.Parameters.AddWithValue("@artworkId", Artwork_id.Value)
            cmd.Parameters.AddWithValue("@title", title.Value)
            cmd.Parameters.AddWithValue("@category", Category.Value)
            cmd.Parameters.AddWithValue("@artistId", Session_Cnic)
            cmd.Parameters.AddWithValue("@stockedDate", DateTime.Parse(Stocked_Date.Value))
            cmd.Parameters.AddWithValue("@status", Status.Value)
            cmd.Parameters.AddWithValue("@image", If(imageData IsNot Nothing, CType(imageData, Object), DBNull.Value))
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
                Dim cmd4 As New SqlCommand
                Dim con4 As New SqlConnection
                con4.ConnectionString = constr
                cmd4.CommandText = "INSERT INTO ARTWORK_PRICE (ARTWORK_ID, PRICE, EFFECTIVE_DATE) VALUES ('"
                cmd4.CommandText &= Artwork_id.Value & "', '"
                cmd4.CommandText &= price.Value + "', '"
                cmd4.CommandText &= DateTime.Parse(effective_date.Value) + "')"
                Try
                    con4.Open()
                    cmd4.Connection = con4
                    Dim insert_C As Integer = cmd4.ExecuteNonQuery()
                    If insert_C > 0 Then
                        message_box.Style.Add("opacity", "1")
                        message_box.InnerText = "Record Updated Successfully"
                    End If
                Catch ex As Exception
                    message_box.Style.Add("opacity", "1")
                    message_box.InnerText = ex.Message
                Finally
                    con4.Close()
                    cmd4.Dispose()
                End Try
                con.Close()
                cmd.Dispose()
            End Try


            message_box.InnerText = "Artwork registered successfully!"
        Catch ex As Exception
            message_box.InnerText = "Error: " & ex.Message
        End Try


    End Sub
End Class
