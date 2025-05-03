Imports Oracle.ManagedDataAccess.Client
Imports System.Data

Public Class ArtVaultDbContext

    Private connectionString As String = System.Configuration.ConfigurationManager.ConnectionStrings("OracleDbContext").ConnectionString

    Public Function GetArtPieces() As DataTable
        Dim dt As New DataTable()

        Try
            Using conn As New OracleConnection(connectionString)
                conn.Open()

                Dim query As String = "SELECT * FROM PERSON"

                Using cmd As New OracleCommand(query, conn)
                    Using adapter As New OracleDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle or log error as needed
            Throw
        End Try

        Return dt
    End Function

End Class
