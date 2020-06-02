Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient

Public Class DMSHandler
    Implements System.Web.IHttpHandler

    Dim ServerId As String
    Dim UserId As String
    Dim UserName As String
    Dim Deleteflag As String
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)


    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest


        ServerId = context.Request.QueryString("ServerId")
        UserId = context.Request.QueryString("UserId")
        UserName = context.Request.QueryString("UserName")
        Deleteflag = context.Request.QueryString("Deleteflag")
        Try
            If (ServerId <> "") And (UserId <> "") And (UserName <> "") And (Deleteflag <> "") Then
                UpdateDB(ServerId, UserId, UserName, Deleteflag)
            Else
            End If
        Catch ex As Exception
            context.Response.Write("<Script Language='JavaScript'>window.alert('" & ex.Message.ToString() & "');</script>")
        End Try

    End Sub
    Private Sub UpdateDB(ServerId As String, UserId As String, UserName As String, Deleteflag As String)
        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        Dim cmd As New SqlCommand(" select * from ucancel where ServerId='" & ServerId & "' and UserId='" & UserId & "'  ", DMSConn)
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 900
        Dim cmdupdate As New SqlCommand(" update ucancel set UserName='" & UserName & "' , LastUpdateTime = GETDATE(),IsActive=1  where ServerId='" & ServerId & "' and UserId='" & UserId & "'  ", DMSConn)
        cmdupdate.CommandType = CommandType.Text
        cmdupdate.CommandTimeout = 900
        Dim cmddelete As New SqlCommand(" update ucancel set IsActive = 0 , LastUpdateTime = GETDATE() where ServerID='" & ServerId & "' and UserId='" & UserId & "' ", DMSConn)
        cmddelete.CommandType = CommandType.Text
        cmddelete.CommandTimeout = 900
        Dim cmdinsert As New SqlCommand(" INSERT INTO [TD_DOC].[dbo].[UCancel]([ServerId],[UserId],[UserName],[IsActive],[LastUpdateTime])VALUES('" & ServerId & "','" & UserId & "','" & UserName & "','1',GETDATE()) ", DMSConn)
        cmdinsert.CommandType = CommandType.Text
        cmdinsert.CommandTimeout = 900

        Dim ds As New DataSet()
        Dim adaptor As New SqlDataAdapter(cmd)
        adaptor.Fill(ds)
        adaptor.Dispose()


        If (ds.Tables(0).Rows.Count > 0) Then
            If (Deleteflag) Then
                Dim adaptordelete As New SqlDataAdapter(cmddelete)
                adaptordelete.Fill(ds)
                adaptordelete.Dispose()
            Else

                Dim adaptorupdate As New SqlDataAdapter(cmdupdate)
                adaptorupdate.Fill(ds)
                adaptorupdate.Dispose()

            End If
        Else
            Dim adaptorinsert As New SqlDataAdapter(cmdinsert)
            adaptorinsert.Fill(ds)
            adaptorinsert.Dispose()
        End If

        DMSConn.Close()
        ds.Clear()
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class