Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports Ionic.Zip
Imports System.Net.Mail

Public Class DMSDownloader03

    Inherits System.Web.UI.Page

    'Creating Connection object and getting connection string
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Dim Uploaduser As String = ""
    Dim CustomerNumber As String = ""
    Dim CustomerName As String = ""
    Dim ServerID As String = ""
    Dim UsrSQRight As String = ""

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
        'Response.Write("<Script Language='JavaScript'>window.alert('here');</script>")
        ' Split POST strings
        If Not (Request.QueryString("String") = "") Then

            Dim s As String = DecodeBase64(Request.QueryString("String"))

            Dim lines As String() = s.Split(New Char() {"&"c})

            Dim line As String
            For Each line In lines

                Dim components As String() = line.Split(New Char() {"="c})
                Dim component As String
                For Each component In components

                    'Response.Write("<Script Language='JavaScript'>window.alert('" & components(0) & "');</script>")

                    If (components(0).ToString().Trim() = "UploadUserName") Then
                        Uploaduser = components(1).ToString().Trim().Replace("+", " ")
                        Exit For


                    ElseIf (components(0).ToString().Trim() = "CustomerNumber") Then
                        CustomerNumber = components(1).ToString().Trim()
                        Exit For

                    ElseIf (components(0).ToString().Trim() = "CustomerName") Then
                        CustomerName = components(1).ToString().Trim().Replace("$", "&")
                        CustomerName = CustomerName.Replace("+", " ")
                        Exit For

                    ElseIf (components(0).ToString().Trim() = "UsrSQRight") Then
                        UsrSQRight = components(1).ToString().Trim()
                        Exit For

                    ElseIf (components(0).ToString().Trim() = "ServerID") Then
                        ServerID = components(1).ToString().Trim()
                        Exit For

                    ElseIf (components(0).ToString().Trim() = "title") Then
                        Title = components(1).ToString().Trim().Replace("$", "&")
                        Title = Title.Replace("+", " ")
                    End If



                Next
            Next

        End If



        'If Not (Request.QueryString("title") = "") Then
        '    Title = Request.QueryString("title").Replace("$", "&")
        'End If
        'If Not (Request.QueryString("UploadUserName") = "") Then
        '    Uploaduser = Request.QueryString("UploadUserName")
        'End If
        'If Not (Request.QueryString("CustomerNumber") = "") Then
        '    CustomerNumber = Request.QueryString("CustomerNumber")
        'End If
        'If Not (Request.QueryString("CustomerName") = "") Then
        '    CustomerName = Request.QueryString("CustomerName").Replace("$", "&")
        'End If
        'If Not (Request.QueryString("ServerID") = "") Then
        '    ServerID = Request.QueryString("ServerID")
        'End If
        'If Not (Request.QueryString("UsrSQRight") = "") Then
        '    UsrSQRight = Request.QueryString("UsrSQRight")
        'End If




        'Check and Open Database connection
        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        If Not Page.IsPostBack Then

            If (UsrSQRight.ToString() = "4") Then

                Response.Write("<Script Language='JavaScript'>window.alert('SOP and quotation access denied, please contact your department head or TITAN coordinator to assist.');</script>")
                Response.Write("<script language='javascript'> { self.close() }</script>")
                Response.Write("<script language='javascript'> { window.open('no_permission.html', '_self');}</script>")

            Else

                btnD3.Attributes.Add("OnClick", "self.close()")
                ' bunUpload.Attributes.Add("OnClick", "return checkforupload();")

                txt_customername.Text = CustomerName

                If (UsrSQRight.ToString() = "3") Then

                    bunUpload.Visible = False

                    displayflag.Value = "1"


                End If

                'Bind gridview existing records
                If Uploaduser <> "" Then
                    updrr1.Visible = True
                    updrr2.Visible = True
                    updrr1.Enabled = True
                    updrr1.Enabled = True

                    bindGridView()
                    BindDropDown()
                End If



            End If


        End If

    End Sub
    Public Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input))
    End Function
    Private Sub bindGridView()

        Dim cmdstr As String = ""

        Try

            Dim c1 As New SqlParameter("@CustomerNumber", Data.SqlDbType.VarChar)


            Dim cmd As New SqlCommand("usp_MCSFileInfo_UploadSelect", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            c1.Value = CustomerNumber
            cmd.Parameters.Add(c1)

            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)



            If ds.Tables(0).Rows.Count > 0 Then
                G1.DataSource = ds.Tables(0)
                G1.DataBind()
                If ds.Tables(1).Rows.Count > 0 Then
                    G2.DataSource = ds.Tables(1)
                    G2.DataBind()
                End If
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



    Private Sub BindDropDown()

        Try
            Dim cmd As New SqlCommand("SELECT val1, val2 FROM Argv WHERE key1 = 'mcsfiletype' and active = 1 order by displayorder", DMSConn)

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

    Private Sub DownloadSelectedFile2()

        Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Try
            Using zipFile As New ZipFile()
                Response.Clear()
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G2.Rows
                    Dim checkcell As TableCell = G2.Rows(gvr.RowIndex).Cells(1)

                    Dim IsChecked As Boolean = DirectCast(G2.Rows(gvr.RowIndex).FindControl("txt_sel0"), CheckBox).Checked

                    Dim cell As TableCell = G2.Rows(gvr.RowIndex).Cells(4)

                    Dim downloadfilename As String = DirectCast(G2.Rows(gvr.RowIndex).FindControl("txt_FullFileName0"), HiddenField).Value

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
    Private Sub DownloadFile2()

        Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Try
            Using zipFile As New ZipFile()
                Response.Clear()
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G2.Rows
                    Dim cell As TableCell = G2.Rows(gvr.RowIndex).Cells(4)

                    Dim downloadfilename As String = DirectCast(G2.Rows(gvr.RowIndex).FindControl("txt_FullFileName0"), HiddenField).Value

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

    Private Sub UpdateMCS(ByVal FileType As String, ByVal FileName As String, ByVal UploadFileEffDay As String, ByVal UploadFileExpDay As String)

        Dim c1 As New SqlParameter("@FileType", Data.SqlDbType.VarChar)
        Dim c2 As New SqlParameter("@FileName", Data.SqlDbType.VarChar)
        Dim c3 As New SqlParameter("@CustomerName", Data.SqlDbType.VarChar)
        Dim c4 As New SqlParameter("@CustomerNumber", Data.SqlDbType.VarChar)
        Dim c5 As New SqlParameter("@UploadUsr", Data.SqlDbType.VarChar)
        Dim c6 As New SqlParameter("@UploadFileEffDay", Data.SqlDbType.VarChar)
        Dim c7 As New SqlParameter("@UploadFileExpDay", Data.SqlDbType.VarChar)

        Try
            If DMSConn.State = ConnectionState.Closed Then
                DMSConn.Open()
            End If

            Dim cmd As New SqlCommand("usp_MCSFileInfo_UploadUpdate", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = FileType
            c2.Value = FileName
            c3.Value = CustomerName
            c4.Value = CustomerNumber
            c5.Value = Uploaduser
            c6.Value = UploadFileEffDay
            c7.Value = UploadFileExpDay


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

    Private Sub UploadFile(ByVal FileType As String, FU As FileUpload, ByVal DestFile As String, ByVal UploadFileEffDay As String, ByVal UploadFileExpDay As String)

        If Not String.IsNullOrEmpty(FU.FileName) Then
            Dim fname As String = String.Format("{0}{1}", DestFile, Path.GetExtension(FU.FileName).ToUpper)

            Dim ret As Integer = StoreFile(FileType, fname, FU.FileContent)

            UpdateMCS(FileType, fname, UploadFileEffDay, UploadFileExpDay)
        End If

    End Sub




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


            For Each ctl As Control In e.Row.Cells(4).Controls
                If TypeOf ctl Is HyperLink Then

                    Dim link As HyperLink = CType(ctl, HyperLink)

                    link.NavigateUrl = "~/DownloadHandler.ashx?fid3=" & FilRefId
                End If
            Next
        End If

    End Sub

    Protected Sub bunRefresh_Click(sender As Object, e As EventArgs) Handles bunRefresh.Click

    End Sub

    Protected Sub G1_RowDataBound1(sender As Object, e As GridViewRowEventArgs) Handles G1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim FilRefId As Long

            For Each ctl As Control In e.Row.Cells(0).Controls
                If TypeOf ctl Is HiddenField Then
                    Dim hf As HiddenField = CType(ctl, HiddenField)

                    FilRefId = hf.Value
                End If
            Next


            For Each ctl As Control In e.Row.Cells(4).Controls
                If TypeOf ctl Is HyperLink Then

                    Dim link As HyperLink = CType(ctl, HyperLink)

                    link.NavigateUrl = "~/DownloadHandler.ashx?fid3=" & FilRefId
                End If
            Next
        End If

    End Sub


    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DownloadSelectedFile2()
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DownloadFile2()
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")
    End Sub

    Protected Sub bunUpload_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub bunSendMail_Click(sender As Object, e As EventArgs) Handles bunSendMail.Click
    End Sub

    Sub SendMail()

        Dim smtp As SmtpClient
        Dim mailMsg As New MailMessage
        Dim mailSubject As String = ""
        Dim mailBody As String = ""
        Dim mailTo As String = ""
        Dim mailCC As String = ""
        Dim SopCount As Integer = 0
        Dim QuotationCount As Integer = 0
        Dim mailType As String = ""



        Try

            If ft1.Text <> "" Then
                If (ft1.Text.ToUpper() = "SOP") Then
                    SopCount = 1
                Else
                    QuotationCount = 1
                End If
            End If

            If ft2.Text <> "" Then
                If (ft2.Text.ToUpper() = "SOP") Then
                    SopCount = 1
                Else
                    QuotationCount = 1
                End If
            End If

            'bunSendMail.Visible = False

            'change to Uploaduser when launch to live
            mailTo = "it@topocean.com.hk"
            mailSubject = "MCS SOP & Quotation Updated – C/" & CustomerName & ""
            mailBody = "Dear All,<br /><br />"
            mailBody &= "Please kindly be informed that a updated SOP / Quotation is uploaded to MCS.<br />"
            mailBody &= "Customer Name: " & CustomerName & "<br />"
            mailBody &= "Updated Document: "
            If (QuotationCount = 0 And SopCount = 1) Then
                mailBody &= "SOP"
                mailType = "SOP"
            ElseIf (QuotationCount = 1 And SopCount = 0) Then
                mailBody &= "Quotation"
                mailType = "Quotation"
            ElseIf (QuotationCount = 1 And SopCount = 1) Then
                mailBody &= "SOP & Quotation"
                mailType = "SOP & Quotation"
            End If
            mailBody &= "<br /><br />Thanks & Best Regards,<br />"
            mailBody &= "MCS Update Service<br />"
            mailBody &= "------------------------------------------------------------------------------------<br />"
            mailBody &= "<font color=""red""><b>This Is an automated system email, please Do Not reply.</b></font>"


            mailMsg.From = New Net.Mail.MailAddress("mail@topocean.com.hk", "mail@topocean.com.hk")
            mailMsg.To.Add(mailTo)
            mailMsg.Subject = mailSubject
            mailMsg.Body = mailBody
            mailMsg.IsBodyHtml = True

            smtp = New SmtpClient
            smtp.Host = "smtp.topocean.com.hk"
            smtp.Port = 25
            smtp.UseDefaultCredentials = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Send(mailMsg)
            emailinfoimport(mailTo, mailType, CustomerNumber, CustomerName, mailSubject, mailBody, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 1, 0, "")
        Catch ex As Exception
            emailinfoimport(mailTo, mailType, CustomerNumber, CustomerName, mailSubject, mailBody, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 0, 1, ex.ToString())

        End Try
    End Sub

    Private Sub emailinfoimport(mailTo As String, mailType As String, CustomerNumber As Integer, CustomerName As String, mailSubject As String, mailBody As String, sendtime As DateTime, successtime As Integer, failtime As Integer, failreason As String)

        Try
            Dim cmd As New SqlCommand()
            DMSConn.Open()
            cmd.CommandText = "INSERT INTO [TD_DOC].[dbo].[MCSEmailInfo]
           ([MailTo]
           ,[MailType]
           ,[ClientRefId]
           ,[CustomerName]
           ,[MailSubject]
           ,[MailBody]
           ,[EmailSendTime]
           ,[EmailSendSuccessTime]
           ,[EmailSendFailTime]
           ,[FailReason])
     VALUES
           (@mailTo,@mailType,@CustomerNumber,@CustomerName,@mailSubject,@mailBody,@sendtime,@successtime,@failtime,@failreason)"

            Dim param1 As New SqlParameter()
            param1.ParameterName = "@mailTo"
            param1.Value = mailTo
            cmd.Parameters.Add(param1)

            Dim param2 As New SqlParameter()
            param2.ParameterName = "@mailType"
            param2.Value = mailType
            cmd.Parameters.Add(param2)

            Dim param3 As New SqlParameter()
            param3.ParameterName = "@CustomerNumber"
            param3.Value = CustomerNumber
            cmd.Parameters.Add(param3)

            Dim param4 As New SqlParameter()
            param4.ParameterName = "@CustomerName"
            param4.Value = CustomerName
            cmd.Parameters.Add(param4)

            Dim param5 As New SqlParameter()
            param5.ParameterName = "@mailSubject"
            param5.Value = mailSubject
            cmd.Parameters.Add(param5)

            Dim param6 As New SqlParameter()
            param6.ParameterName = "@mailBody"
            param6.Value = mailBody
            cmd.Parameters.Add(param6)

            Dim param7 As New SqlParameter()
            param7.ParameterName = "@sendtime"
            param7.Value = sendtime
            cmd.Parameters.Add(param7)

            Dim param8 As New SqlParameter()
            param8.ParameterName = "@successtime"
            If successtime = 1 Then
                param8.Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            Else
                param8.Value = DBNull.Value
            End If

            cmd.Parameters.Add(param8)

            Dim param9 As New SqlParameter()
            param9.ParameterName = "@failtime"
            If failtime = 1 Then
                param9.Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            Else
                param9.Value = DBNull.Value
            End If

            cmd.Parameters.Add(param9)

            Dim param10 As New SqlParameter()
            param10.ParameterName = "@failreason"
            param10.Value = failreason
            cmd.Parameters.Add(param10)

            cmd.Connection = DMSConn
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            DMSConn.Close()
        Catch ex As Exception
            'MsgBox(ex.ToString())
            SendMailtoIT()
        End Try

    End Sub


    Protected Sub bunUpload_onClick(sender As Object, e As EventArgs) Handles bunUpload.Click
        'bunUpload.Attributes.Add("onclick", "this.disabled=true;")

        Dim UploadFileType As String
        Dim UploadFileName As String
        Dim UploadRoot As String
        Dim UploadFileEffDay As String
        Dim UploadFileExpDay As String
        'Dim DestFileFormat As String
        Dim DestFileName As String

        Dim ftdl As New DropDownList
        Dim fnup As New FileUpload
        Dim feffday As New TextBox
        Dim fexpday As New TextBox

        If (UsrSQRight.ToString() = "1") Then


            UploadRoot = "D:\FTP\DocServer\MCS\"


            For i = 1 To 5
                ftdl = Me.FindControl(String.Format("ft{0}", i))
                UploadFileType = ftdl.SelectedItem.Value

                fnup = Me.FindControl(String.Format("updrr{0}", i))
                UploadFileName = fnup.FileName

                feffday = Me.FindControl(String.Format("dp{0}", i))
                UploadFileEffDay = feffday.Text.ToString()

                fexpday = Me.FindControl(String.Format("dpp{0}", i))
                UploadFileExpDay = fexpday.Text.ToString()

                If UploadFileName <> "" Then

                    DestFileName = String.Format("{0}{1}\{2}_{3}", UploadRoot, UploadFileType, Path.GetFileNameWithoutExtension(UploadFileName), DateTime.Now.ToString("yyyyMMddHHmmss"))



                    UploadFile(UploadFileType, fnup, DestFileName, UploadFileEffDay, UploadFileExpDay)


                    ' SendMail()


                End If


            Next
            bindGridView()
            'bindTrashGridView()
            BindDropDown()
        Else
            Response.Write("<Script Language='JavaScript'>window.alert('No permission to upload');</script>")
            Response.Write("<script language='javascript'> { self.close() }</script>")
            Response.Write("<script language='javascript'> { window.open('no_permission.html', '_self');}</script>")
        End If
    End Sub


    Sub SendMailtoIT()

        Dim smtp As SmtpClient
        Dim mailMsg As New MailMessage
        Dim mailSubject As String = ""
        Dim mailBody As String = ""
        Dim mailTo As String = ""
        Dim mailCC As String = ""
        Dim SopCount As Integer = 0
        Dim QuotationCount As Integer = 0
        Dim mailType As String = ""


        Try

            If ft1.Text <> "" Then
                If (ft1.Text.ToUpper() = "SOP") Then
                    SopCount = 1
                Else
                    QuotationCount = 1
                End If
            End If

            If ft2.Text <> "" Then
                If (ft2.Text.ToUpper() = "SOP") Then
                    SopCount = 1
                Else
                    QuotationCount = 1
                End If
            End If

            'bunSendMail.Visible = False

            'change to IT when launch to live
            mailTo = "terence_cheung@topocean.com.hk"
            mailSubject = "MCS DB Connection problem"
            mailBody = "Dear All,<br /><br />"
            mailBody &= "Please kindly be informed that MCS server has connection problem <br />"

            mailBody &= "<br /><br />Thanks & Best Regards,<br />"
            mailBody &= "MCS Update Service<br />"
            mailBody &= "------------------------------------------------------------------------------------<br />"
            mailBody &= "<font color=""red""><b>This Is an automated system email, please Do Not reply.</b></font>"


            mailMsg.From = New Net.Mail.MailAddress("mail@topocean.com.hk", "mail@topocean.com.hk")
            mailMsg.To.Add(mailTo)
            mailMsg.Subject = mailSubject
            mailMsg.Body = mailBody
            mailMsg.IsBodyHtml = True


            smtp = New SmtpClient
            smtp.Host = "smtp.topocean.com.hk"
            smtp.Port = 25
            smtp.UseDefaultCredentials = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Send(mailMsg)
            'emailinfoimport(mailTo, mailType, CustomerNumber, CustomerName, mailSubject, mailBody, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 1, 0)
        Catch ex As Exception
            'emailinfoimport(mailTo, mailType, CustomerNumber, CustomerName, mailSubject, mailBody, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 0, 1)

        End Try
    End Sub



End Class