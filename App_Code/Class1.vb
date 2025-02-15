Imports Oracle.ManagedDataAccess.Client
Imports System.Data

Public Class ArtVaultDbContext

    ' Reads the connection string from your web.config
    Private connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("OracleDbContext").ConnectionString

    ' Method to retrieve data (returns a DataTable)
    Public Function GetArtPieces() As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New OracleConnection(connectionString)
                conn.Open()

                ' Example query - replace "ArtPieces" with your actual table name
                ' and specify the columns you want to retrieve
                Dim query As String = "SELECT * FROM PERSON"

                Using cmd As New OracleCommand(query, conn)
                    Using adapter As New OracleDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle or log error as needed
            ' For now, just rethrow or set dt to Nothing
            Throw
        End Try

        Return dt
    End Function

End Class
