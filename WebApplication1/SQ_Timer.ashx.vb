Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient

Public Class SQ_Timer
    Implements System.Web.IHttpHandler
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        Dim customernum As Integer = Convert.ToInt32(context.Request.Params("num"))
        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        Dim cmd As New SqlCommand("select FilRefid from mcsfileinfo where Active = 1 and DATEDIFF(day, ImportDate, GETDATE())<=14 and CustomerNumber='" & customernum & "'", DMSConn)
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 900

        Dim ds As New DataSet()
        Dim adaptor As New SqlDataAdapter(cmd)
        adaptor.Fill(ds)
        adaptor.Dispose()

        If (ds.Tables(0).Rows.Count > 0) Then

            context.Response.Write("1")
        Else

            context.Response.Write("0")

        End If


    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class