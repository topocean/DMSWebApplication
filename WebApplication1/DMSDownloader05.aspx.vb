Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports Ionic.Zip

Public Class DMSDownloader05

    Inherits System.Web.UI.Page

    'Creating Connection object and getting connection string
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Dim HBL As String = ""
    Dim MBL As String = ""
    Dim Office As String = ""
    Dim DMSUser As String = ""
    Dim DMSloginUser As String = ""
    Dim DMSloginUserNo As String = ""
    Dim BkhRefId As Integer = 0
    Dim UReFid As String = ""

    Sub handleFileUpload()

        Dim UploadRegion = Request.QueryString("UploadRegion")

        Dim UploadPath As String
        Dim RequestStream As Stream
        Dim UploadFileName As String = Request.Headers("X-File-Name")

        If Not String.IsNullOrEmpty(UploadFileName) Then
            If UploadRegion = 1 Then
                UploadPath = Server.MapPath("/Upload1")
            Else
                UploadPath = Server.MapPath("/Upload")
            End If

            RequestStream = Request.InputStream

            Dim fileStream As FileStream = New FileStream(UploadPath + "\" + UploadFileName, FileMode.OpenOrCreate)

            RequestStream.CopyTo(fileStream)

            fileStream.Close()
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        handleFileUpload()

        HBL = Request.QueryString("BkhBLNo")
        MBL = Request.QueryString("BkhMBLNo")
        Office = Request.QueryString("usrbrh")
        DMSUser = Request.QueryString("dmsuser")
        BkhRefId = Request.QueryString("BkhRefId")
        DMSloginUser = Request.QueryString("usrname")
        DMSloginUserNo = Request.QueryString("usr")
        btnD4.Attributes.Add("onclick", "if (confirm('Are you sure to delete this File?')){return true} else {return false}")

        'Check and Open Database connection
        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        If Not Page.IsPostBack Then

            If (IsactiveUser()) Then
                btnD4.Visible = True
            End If

            btnD3.Attributes.Add("OnClick", "self.close()")

            txt_hblno.Text = HBL
            txt_mblno.Text = MBL

            'Bind gridview existing records
            If HBL <> "" Then
                bindGridView()
                bindTrashGridView()
                BindDropDown()
            End If
        End If

    End Sub

    Private Sub bindGridView()

        Dim c1 As New SqlParameter("@HBL", Data.SqlDbType.VarChar)
        Dim c2 As New SqlParameter("@MBL", Data.SqlDbType.VarChar)
        Dim c3 As New SqlParameter("@Office", Data.SqlDbType.VarChar)
        Dim c4 As New SqlParameter("@DMSUser", Data.SqlDbType.VarChar)
        Dim c5 As New SqlParameter("@BkhRefId", Data.SqlDbType.Int)

        Try
            Dim cmd As New SqlCommand("usp_FileInfo_DMS_Search1", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = HBL
            c2.Value = MBL
            c3.Value = Office
            c4.Value = DMSUser
            c5.Value = BkhRefId

            cmd.Parameters.Add(c1)
            cmd.Parameters.Add(c2)
            cmd.Parameters.Add(c3)
            cmd.Parameters.Add(c4)
            cmd.Parameters.Add(c5)

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                G1.DataSource = ds
                G1.DataBind()
                ds.Clear()
            Else
                'Bind Empty grdview with Columns names in Header and "No employee record found" message if no records are found in the database
                'BindEmptyGridWithHeader(grdEmp, ds)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Sub

    Private Sub bindTrashGridView()

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim cmd As New SqlCommand(" select a.FilRefid, a.FileType,a.FileName as FullFileName, Reverse(substring(reverse(filename), 0, charindex('\', reverse(filename), 0))) As FileName,b.UserName as DeletedBy,a.logcreDte as DeletedDate from docdeletelog a left outer join Ucancel b on a.URefId=b.URefId where a.HBL='" & HBL & "' and a.MBL='" & MBL & "' ", DMSConn)
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)
            cmd.Dispose()

            'Response.Write("<Script Language='JavaScript'>window.alert('" & ds.Tables(0).Rows.Count & "');</script>")

            If (ds.Tables(0).Rows.Count > 0) Then
                G2.DataSource = ds.Tables(0)
                G2.DataBind()

            End If
            ds.Clear()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Sub

    Private Sub BindDropDown()

        Try
            Dim cmd As New SqlCommand("SELECT val1, val2 FROM Argv WHERE key1 = 'filetype' and active = 1 order by val1", DMSConn)

            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)

            For Each ctl As Control In Form.Controls
                If TypeOf ctl Is DropDownList Then
                    If ctl.ID.IndexOf("ft") >= 0 Then
                        Dim dl As DropDownList = CType(ctl, DropDownList)

                        dl.Items.Clear()

                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            dl.Items.Add(New ListItem(ds.Tables(0).Rows(i).Item("Val2"), ds.Tables(0).Rows(i).Item("Val1")))
                        Next
                    End If
                End If
            Next

            cmd.Dispose()

            'Dim cmd As New SqlCommand("SELECT distinct DestOffice FROM BookingInfo WHERE DestOffice <> '' Order by DestOffice", DMSConn)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Sub

    Sub G1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim FilRefId As Long

            For Each ctl As Control In e.Row.Cells(0).Controls
                If TypeOf ctl Is HiddenField Then
                    Dim hf As HiddenField = CType(ctl, HiddenField)

                    FilRefId = hf.Value
                End If
            Next

            For Each ctl As Control In e.Row.Cells(3).Controls
                If TypeOf ctl Is HyperLink Then

                    Dim link As HyperLink = CType(ctl, HyperLink)

                    link.NavigateUrl = "~/DownloadHandler.ashx?fid=" & FilRefId
                End If
            Next
        End If

    End Sub

    Private Sub DownloadSelectedFile()

        Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Try
            Using zipFile As New ZipFile()
                Response.Clear()
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G1.Rows
                    Dim checkcell As TableCell = G1.Rows(gvr.RowIndex).Cells(1)

                    Dim IsChecked As Boolean = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_Sel"), CheckBox).Checked

                    Dim cell As TableCell = G1.Rows(gvr.RowIndex).Cells(4)

                    Dim downloadfilename As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value

                    If File.Exists(downloadfilename) And IsChecked Then
                        zipFile.AddFile(downloadfilename, "")
                    End If
                Next

                zipFile.Save(Response.OutputStream)
            End Using
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        End Try

    End Sub

    Private Sub DownloadFile()

        Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Try
            Using zipFile As New ZipFile()
                Response.Clear()
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G1.Rows
                    Dim cell As TableCell = G1.Rows(gvr.RowIndex).Cells(4)

                    Dim downloadfilename As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value

                    If File.Exists(downloadfilename) Then
                        zipFile.AddFile(downloadfilename, "")
                    End If
                Next

                zipFile.Save(Response.OutputStream)
            End Using
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        End Try

    End Sub

    Private Sub btnD1_Click(sender As Object, e As EventArgs) Handles btnD1.Click

        DownloadSelectedFile()

    End Sub

    Private Sub btnD2_Click(sender As Object, e As EventArgs) Handles btnD2.Click

        DownloadFile()

    End Sub

    Private Sub btnD3_Click(sender As Object, e As EventArgs) Handles btnD3.Click

        Response.Write("<script language='javascript'> { self.close() }</script>")

    End Sub

    Private Function StoreFile(ByVal FileType As String, ByVal fname As String, ByVal Upload As Stream) As Integer

        Dim ret As Integer = 0
        Dim bufferLen As Integer = 8040
        Dim fname2 As String = Path.GetFileName(fname)
        Dim tmpPath As String = Server.MapPath("~/Temp/").Replace("\\\\", "\\")
        Dim tmpFname As String = String.Format("{0}_{1}", tmpPath, Path.GetFileName(fname))
        '    string tmpFname = string.Format("{0}_{1}_{2}_{3}", tmpPath, PSession1.ServerId, DateTime.UtcNow.Ticks, Path.GetFileName(fname));

        If (Not File.Exists(fname)) Then
            Dim br As New BinaryReader(Upload)
            Dim bw As New BinaryWriter(File.Create(tmpFname))

            Try
                Dim chunk As Byte() = br.ReadBytes(bufferLen)

                While chunk.Length > 0
                    bw.Write(chunk, 0, chunk.Length)
                    chunk = br.ReadBytes(bufferLen)
                End While

                bw.Flush()
                bw.Close()

                br.Close()

                File.Copy(tmpFname, fname)
                File.Delete(tmpFname)

            Catch ex As Exception
                ret = 1
            End Try
        Else
            ret = 2
        End If

        Return ret

    End Function

    Function GetDestFileFormat(ByVal FileType As String) As String

        Dim ret As String = ""

        Dim c1 As New SqlParameter("@FileType", Data.SqlDbType.VarChar)

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim cmd As New SqlCommand("usp_FileInfo_GetFileFormat", DMSConn)
            Dim rds As SqlDataReader

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = FileType

            cmd.Parameters.Add(c1)

            rds = cmd.ExecuteReader

            rds.Read()

            ret = rds.Item("more")

            rds.Close()
            cmd.Dispose()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

        Return ret

    End Function

    Private Sub UpdateDMS(ByVal FileType As String, ByVal FileName As String)

        Dim c1 As New SqlParameter("@FileType", Data.SqlDbType.VarChar)
        Dim c2 As New SqlParameter("@FileName", Data.SqlDbType.VarChar)
        Dim c3 As New SqlParameter("@AgentCd", Data.SqlDbType.VarChar)
        Dim c4 As New SqlParameter("@MBL", Data.SqlDbType.VarChar)
        Dim c5 As New SqlParameter("@HBL", Data.SqlDbType.VarChar)
        Dim c6 As New SqlParameter("@RevCount", Data.SqlDbType.Int)
        Dim c7 As New SqlParameter("@InvNo", Data.SqlDbType.VarChar)

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim cmd As New SqlCommand("usp_FileInfo_UploadUpdate", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = FileType
            c2.Value = FileName
            c3.Value = ""
            c4.Value = MBL
            c5.Value = HBL
            c6.Value = 0
            c7.Value = ""

            cmd.Parameters.Add(c1)
            cmd.Parameters.Add(c2)
            cmd.Parameters.Add(c3)
            cmd.Parameters.Add(c4)
            cmd.Parameters.Add(c5)
            cmd.Parameters.Add(c6)
            cmd.Parameters.Add(c7)

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

    Private Sub UploadFile(ByVal FileType As String, FU As FileUpload, ByVal DestFile As String)

        If Not String.IsNullOrEmpty(FU.FileName) Then
            Dim fname As String = String.Format("{0}{1}", DestFile, Path.GetExtension(FU.FileName).ToUpper)

            Dim ret As Integer = StoreFile(FileType, fname, FU.FileContent)

            UpdateDMS(FileType, fname)
        End If

    End Sub

    Private Sub bunUpload_Click1(sender As Object, e As EventArgs) Handles bunUpload.Click

        Dim UploadFileType As String
        Dim UploadFileName As String
        Dim UploadRoot As String
        Dim DestFileFormat As String
        Dim DestFileName As String

        Dim newHBL As String
        Dim newMBL As String

        Dim ftdl As New DropDownList
        Dim fnup As New FileUpload

        UploadRoot = "D:\FTP\DocServer\"

        For i = 1 To 5
            ftdl = Me.FindControl(String.Format("ft{0}", i))
            UploadFileType = ftdl.SelectedItem.Value

            fnup = Me.FindControl(String.Format("updr{0}", i))
            UploadFileName = fnup.FileName

            If UploadFileName <> "" Then
                newHBL = HBL.Replace("\", "+").Replace("/", "+")
                newMBL = MBL.Replace("\", "+").Replace("/", "+")

                DestFileFormat = GetDestFileFormat(UploadFileType)
                DestFileName = String.Format("{0}{1}\{2}_{3}", UploadRoot, UploadFileType, DestFileFormat.Replace("$H$", newHBL).Replace("$M$", newMBL), DateTime.UtcNow.Ticks)

                'Response.Write(DestFileName)
                'Response.End()

                UploadFile(UploadFileType, fnup, DestFileName)
            End If

            'Select UploadFileType
            '    Case "HBL"
            '        DestFileName = String.Format("{0}{1}\{2}", UploadRoot, UploadFileType, DestFileFormat.Replace("$H$", HBL).Replace("$M$", MBL))
            '    Case "MBL"
            '        DestFileName = String.Format("{0}{1}\{2}", UploadRoot, UploadFileType, DestFileFormat.Replace("$M$", MBL))
            '    Case Else
            '        DestFileName = ""
            'End Select
        Next
        bindGridView()
        bindTrashGridView()
        BindDropDown()

    End Sub

    Protected Sub btnD4_Click(sender As Object, e As EventArgs) Handles btnD4.Click

        'Response.Write("<Script Language='JavaScript'>window.alert('ok');</script>")
        DeleteSelectedFile()
        bindGridView()
        bindTrashGridView()
        BindDropDown()

    End Sub

    Private Sub DeleteSelectedFile()

        Try

            If IsactiveUser() Then

                For Each gvr As GridViewRow In G1.Rows

                    Dim IsChecked As Boolean = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_Sel"), CheckBox).Checked

                    If (IsChecked) Then

                        Dim FilRefid As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FilRefId"), HiddenField).Value

                        Dim downloadfilename As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value

                        Dim Newdownloadfilename As String = downloadfilename.Replace("D:\FTP\DocServer\", "D:\FTP\DocServer\TRASH\")

                        Dim Newdownloadpath As String = System.IO.Path.GetDirectoryName(Newdownloadfilename)

                        If Not System.IO.Directory.Exists(Newdownloadpath) Then
                            System.IO.Directory.CreateDirectory(Newdownloadpath)
                        End If

                        'If System.IO.File.Exists(Newdownloadfilename) Then
                        ' System.IO.File.Delete(Newdownloadfilename)
                        'End If


                        'Remove file location and delete file on DB

                        Dim ext As String = System.IO.Path.GetExtension(Newdownloadfilename)
                        Dim filenewname As String = System.IO.Path.GetFileNameWithoutExtension(Newdownloadfilename) & "_" & System.DateTime.Now.ToString("yyyyMMddHHmmss")

                        System.IO.File.Move(downloadfilename, Newdownloadpath & "\" & filenewname & ext)
                        DeleteDMS(FilRefid, UReFid, Newdownloadpath & "\" & filenewname & ext)

                    End If

                Next
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        End Try

    End Sub

    Private Function IsactiveUser() As Boolean

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim cmd As New SqlCommand("select * from Ucancel WHERE UserId = '" & DMSloginUserNo.ToString().Trim() & "' and ServerId='" & DMSUser.ToString().Trim() & "' and IsActive = 1 ", DMSConn)

            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)
            cmd.Dispose()

            If (ds.Tables(0).Rows.Count > 0) Then
                UReFid = ds.Tables(0).Rows(0)("URefid")
                Return True
            Else
                Return False
            End If
            ds.Clear()

        Catch ex As Exception
            Return False
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Function

    Private Sub DeleteDMS(FilRefid As String, UReFid As String, NewFilePath As String)

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim c1 As New SqlParameter("@FilRefId", Data.SqlDbType.BigInt)
            Dim c2 As New SqlParameter("@URefId", Data.SqlDbType.Int)
            Dim c3 As New SqlParameter("@NewFilePath", Data.SqlDbType.NVarChar)

            Dim cmd As New SqlCommand("usp_FileInfo_DelByFilRefId_User", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = FilRefid
            c2.Value = UReFid
            c3.Value = NewFilePath

            cmd.Parameters.Add(c1)
            cmd.Parameters.Add(c2)
            cmd.Parameters.Add(c3)

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

    Protected Sub G2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles G2.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim FilRefId As Long

            For Each ctl As Control In e.Row.Cells(0).Controls
                If TypeOf ctl Is HiddenField Then
                    Dim hf As HiddenField = CType(ctl, HiddenField)

                    FilRefId = hf.Value
                End If
            Next

            For Each ctl As Control In e.Row.Cells(2).Controls
                If TypeOf ctl Is HyperLink Then

                    Dim link As HyperLink = CType(ctl, HyperLink)

                    link.NavigateUrl = "~/DownloadHandler.ashx?fid2=" & FilRefId
                End If
            Next
        End If

    End Sub

End Class