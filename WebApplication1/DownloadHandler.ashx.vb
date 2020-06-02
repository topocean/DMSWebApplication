Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class DownloadHandler

    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'context.Response.ContentType = "text/plain"
        'context.Response.Write("Hello World!")

        Dim strRequest As String = context.Request.QueryString("fid")
        Dim strRequest2 As String = context.Request.QueryString("fid2")
        Dim strRequest3 As String = context.Request.QueryString("fid3")
        Dim pFileName As String = ""

        If strRequest <> "" Then
            'Get File Information From Database
            Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

            'Check and Open Database connection
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim c1 As New SqlParameter("@FilRefId", Data.SqlDbType.Int)

            Dim cmd As New SqlCommand("usp_FileInfo_getByFilRefId", DMSConn)
            Dim rds As SqlDataReader

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = CInt(strRequest)

            cmd.Parameters.Add(c1)
            rds = cmd.ExecuteReader

            rds.Read()

            pFileName = rds.Item("FileName")

            rds.Close()
            cmd.Dispose()

            'Dim Path As String = context.Request.MapPath(context.Request.ApplicationPath & "/PhotoStorage/" & strRequest)
            'Dim Path As String = context.Server.MapPath("D:/FTP/DocServer/FL")
            Dim file As FileInfo = New FileInfo(pFileName) '-- if the file exists on the server

            If file.Exists Then
                Select Case file.Extension
                    Case ".pdf"
                        context.Response.ContentType = "application/pdf"
                    Case ".xls"
                        context.Response.ContentType = "application/vnd.ms-excel"
                    Case ".xlsx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Case ".doc"
                        context.Response.ContentType = "application/msword"
                    Case ".docx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    Case ".jpg", ".jpeg"
                        context.Response.ContentType = "image/jpeg"
                    Case ".png"
                        context.Response.ContentType = "image/x-png"
                    Case ".gif"
                        context.Response.ContentType = "image/gif"
                    Case ".zip"
                        context.Response.ContentType = "application/zip"
                    Case Else
                        context.Response.ContentType = "application/octet-stream"
                End Select

                context.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", file.Name))

                'Dim fs As FileStream = New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim fs As New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim br As New BinaryReader(fs)

                context.Response.Buffer = False

                Dim buffersize As Integer = 8040
                Dim chunk As Byte() = br.ReadBytes(buffersize)

                While chunk.Length > 0
                    context.Response.BinaryWrite(chunk)
                    chunk = br.ReadBytes(buffersize)
                End While

                context.Response.Flush()

                br.Close()
                fs.Close()
            End If
        ElseIf (strRequest2 <> "") Then

            'Get File Information From Database
            Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

            'Check and Open Database connection
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If



            Dim cmd As New SqlCommand(" SELECT FileType, FileName FROM docdeletelog WHERE FilRefId = '" & strRequest2 & "' ", DMSConn)


            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)
            cmd.Dispose()

            If (ds.Tables(0).Rows.Count > 0) Then
                pFileName = ds.Tables(0).Rows(0)("FileName")
            End If

            'Dim Path As String = context.Request.MapPath(context.Request.ApplicationPath & "/PhotoStorage/" & strRequest)
            'Dim Path As String = context.Server.MapPath("D:/FTP/DocServer/FL")
            Dim file As FileInfo = New FileInfo(pFileName) '-- if the file exists on the server

            If file.Exists Then
                Select Case file.Extension
                    Case ".pdf"
                        context.Response.ContentType = "application/pdf"
                    Case ".xls"
                        context.Response.ContentType = "application/vnd.ms-excel"
                    Case ".xlsx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Case ".doc"
                        context.Response.ContentType = "application/msword"
                    Case ".docx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    Case ".jpg", ".jpeg"
                        context.Response.ContentType = "image/jpeg"
                    Case ".png"
                        context.Response.ContentType = "image/x-png"
                    Case ".gif"
                        context.Response.ContentType = "image/gif"
                    Case ".zip"
                        context.Response.ContentType = "application/zip"
                    Case Else
                        context.Response.ContentType = "application/octet-stream"
                End Select

                context.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", file.Name))

                'Dim fs As FileStream = New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim fs As New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim br As New BinaryReader(fs)

                context.Response.Buffer = False

                Dim buffersize As Integer = 8040
                Dim chunk As Byte() = br.ReadBytes(buffersize)

                While chunk.Length > 0
                    context.Response.BinaryWrite(chunk)
                    chunk = br.ReadBytes(buffersize)
                End While

                context.Response.Flush()

                br.Close()
                fs.Close()
            End If
        ElseIf (strRequest3 <> "") Then

            'Get File Information From Database
            Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

            'Check and Open Database connection
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If



            Dim cmd As New SqlCommand(" SELECT FileType, FileName FROM MCSFileInfo WHERE FilRefId = '" & strRequest3 & "' ", DMSConn)


            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)
            cmd.Dispose()

            If (ds.Tables(0).Rows.Count > 0) Then
                pFileName = ds.Tables(0).Rows(0)("FileName")
            End If

            'Dim Path As String = context.Request.MapPath(context.Request.ApplicationPath & "/PhotoStorage/" & strRequest)
            'Dim Path As String = context.Server.MapPath("D:/FTP/DocServer/FL")
            Dim file As FileInfo = New FileInfo(pFileName) '-- if the file exists on the server

            If file.Exists Then
                Select Case file.Extension
                    Case ".pdf"
                        context.Response.ContentType = "application/pdf"
                    Case ".xls"
                        context.Response.ContentType = "application/vnd.ms-excel"
                    Case ".xlsx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Case ".doc"
                        context.Response.ContentType = "application/msword"
                    Case ".docx"
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    Case ".jpg", ".jpeg"
                        context.Response.ContentType = "image/jpeg"
                    Case ".png"
                        context.Response.ContentType = "image/x-png"
                    Case ".gif"
                        context.Response.ContentType = "image/gif"
                    Case ".zip"
                        context.Response.ContentType = "application/zip"
                    Case Else
                        context.Response.ContentType = "application/octet-stream"
                End Select

                context.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", file.Name))

                'Dim fs As FileStream = New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim fs As New FileStream(pFileName, FileMode.OpenOrCreate)
                Dim br As New BinaryReader(fs)

                context.Response.Buffer = False

                Dim buffersize As Integer = 8040
                Dim chunk As Byte() = br.ReadBytes(buffersize)

                While chunk.Length > 0
                    context.Response.BinaryWrite(chunk)
                    chunk = br.ReadBytes(buffersize)
                End While

                context.Response.Flush()

                br.Close()
                fs.Close()
            End If
            Else
                context.Response.Write("Please provide a file to download.")
            End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class