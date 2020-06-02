Imports System.Data
Imports System.Data.SqlClient

Public Class Login1

    Inherits System.Web.UI.Page

    'Creating Connection object and getting connection string
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click

        Dim UserId As String
        Dim UserPass As String
        Dim Validate As Integer

        Dim c1 As New SqlParameter("@UserId", Data.SqlDbType.VarChar)
        Dim c2 As New SqlParameter("@UserPass", Data.SqlDbType.VarChar)

        UserId = UserName.Text
        UserPass = Password.Text

        'Check and Open Database connection
        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        Try
            Dim cmd As New SqlCommand("usp_CheckLogin", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = UserId
            c2.Value = UserPass

            cmd.Parameters.Add(c1)
            cmd.Parameters.Add(c2)

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Validate = ds.Tables(0).Rows(0).Item("URefId")
            Else
                Validate = 0
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

        If Validate <> 0 Then
            Session("URefId") = Validate

            Response.Redirect("DMSDownloader02.aspx")
        End If

        ' If we reach here, the user's credentials were invalid
        lblNotice.Visible = True

    End Sub

    Private Sub Login1_Load(sender As Object, e As EventArgs) Handles Me.Load

        lblNotice.Visible = False

    End Sub

End Class