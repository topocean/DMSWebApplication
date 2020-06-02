Imports System.Data.SqlClient

Public Class UpdateFileInfoMarkCancel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim HBL As String = ""
        Dim MBL As String = ""
        Dim InvNo As String = ""
        Dim Type As String = ""
        Dim sqlstring As String = ""
        Dim Action As String = ""

        Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

        Try
            HBL = Request.QueryString("BkhBLNo")
            MBL = Request.QueryString("BkhMBLNo")
            InvNo = Request.QueryString("InvNo")
            Type = Request.QueryString("Type")
            Action = Request.QueryString("Action")

            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            If Type = "INVOICE" Or Type = "VOUCHER" Then
                If HBL <> "" And InvNo <> "" Then
                    If Action = "DELETE" Then
                        sqlstring = "update FileInfo set IsCancel = 1 where HBL = '" & HBL & "' and InvNo = '" & InvNo & "';"
                    Else
                        If Action = "NEW" Then
                            sqlstring = "update FileInfo set IsCancel = 0 where HBL = '" & HBL & "' and InvNo = '" & InvNo & "';"
                        End If
                    End If
                End If
            End If

            If Type = "FREIGHTLIST" Then
                If MBL <> "" And InvNo <> "" Then
                    If Action = "DELETE" Then
                        sqlstring = "update FileInfo set IsCancel = 1 where MBL = '" & MBL & "' and InvNo = '" & InvNo & "';"
                    Else
                        If Action = "NEW" Then
                            sqlstring = "update FileInfo set IsCancel = 0 where HBL = '" & HBL & "' and InvNo = '" & InvNo & "';"
                        End If
                    End If
                End If
            End If

            Dim cmd As New SqlCommand(sqlstring, DMSConn)
            Response.Write("Update SQL: " & sqlstring)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()
            adp.Fill(ds)
            cmd.Dispose()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Sub

End Class