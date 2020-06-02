Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Ionic.Zip

Public Class DMSDownloader02

    Inherits System.Web.UI.Page

    'Creating Connection object and getting connection string
    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Dim URefId As Integer

    Dim HBL As String = ""
    Dim MBL As String = ""
    Dim Office As String = ""

    Private Sub MakeYearDropDown()

        Dim cmd As New SqlCommand("usp_GetBkhYear", DMSConn)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 900

        Dim adp As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()

        adp.Fill(ds)

        DropDownListYear.DataSource = ds
        DropDownListYear.DataTextField = "BkhYear"
        DropDownListYear.DataValueField = "BkhYear"
        DropDownListYear.DataBind()
        DropDownListYear.Items.Insert(0, New ListItem("-- Select --", "0"))

        ds.Clear()
        adp.Dispose()
        cmd.Dispose()

    End Sub

    Private Sub MakeWeekDropDown()

        Dim cmd As New SqlCommand("usp_GetBkhWeek", DMSConn)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 900

        Dim adp As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()

        adp.Fill(ds)

        DropDownListWeek.DataSource = ds
        DropDownListWeek.DataTextField = "WeekNo"
        DropDownListWeek.DataValueField = "WeekNo"
        DropDownListWeek.DataBind()
        DropDownListWeek.Items.Insert(0, New ListItem("-- Select --", "0"))

        ds.Clear()
        adp.Dispose()
        cmd.Dispose()

    End Sub

    Private Sub MakeFTypeDropdown()

        Dim cmd As New SqlCommand("usp_GetFileType", DMSConn)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 900

        Dim adp As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()

        adp.Fill(ds)

        DropDownListFType.DataSource = ds
        DropDownListFType.DataTextField = "val2"
        DropDownListFType.DataValueField = "val1"
        DropDownListFType.DataBind()

        ds.Clear()
        adp.Dispose()
        cmd.Dispose()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not (IsNothing(Session("URefId"))) Then

            URefId = Session("URefId")

            If Not Page.IsPostBack Then
                MakeYearDropDown()

                MakeWeekDropDown()

                MakeFTypeDropdown()
            End If

        Else

            parentDiv.Visible = True
            checktimeout.Visible = False
            lblMessage.Text = "System Away too Long, Please Login Again!"



        End If




    End Sub

    Private Sub bindGridView()

        Dim c1 As New SqlParameter("@URefId", Data.SqlDbType.Int)
        Dim c2 As New SqlParameter("@HBL", Data.SqlDbType.VarChar)
        Dim c3 As New SqlParameter("@MBL", Data.SqlDbType.VarChar)
        Dim c4 As New SqlParameter("@Cnee", Data.SqlDbType.VarChar)
        Dim c5 As New SqlParameter("@ETDFm", Data.SqlDbType.DateTime)
        Dim c6 As New SqlParameter("@ETDTo", Data.SqlDbType.DateTime)
        Dim c7 As New SqlParameter("@ETAFm", Data.SqlDbType.DateTime)
        Dim c8 As New SqlParameter("@ETATo", Data.SqlDbType.DateTime)
        Dim c9 As New SqlParameter("@SCAC", Data.SqlDbType.VarChar)
        Dim c10 As New SqlParameter("@VslName", Data.SqlDbType.VarChar)
        Dim c11 As New SqlParameter("@VoyName", Data.SqlDbType.VarChar)
        Dim c12 As New SqlParameter("@Year", Data.SqlDbType.Int)
        Dim c13 As New SqlParameter("@Week", Data.SqlDbType.Int)
        Dim c14 As New SqlParameter("@FType", Data.SqlDbType.VarChar)
        Dim c15 As New SqlParameter("@InvNo", Data.SqlDbType.VarChar)
        Dim c16 As New SqlParameter("@VouNo", Data.SqlDbType.VarChar)
        Dim c17 As New SqlParameter("@FreNo", Data.SqlDbType.VarChar)

        Try
            Dim cmd As New SqlCommand("usp_FileInfo_DMS_Search2", DMSConn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 900

            c1.Value = URefId
            c2.Value = sch_hbl.Text
            c3.Value = sch_mbl.Text
            c4.Value = sch_cnee.Text
            If sch_etd1.Text <> "" Then
                c5.Value = DateSerial(Mid(sch_etd1.Text, 7, 4), Mid(sch_etd1.Text, 4, 2), Mid(sch_etd1.Text, 1, 2))
            Else
                c5.Value = CDate("01/01/1900")
            End If
            If sch_etd2.Text <> "" Then
                c6.Value = DateSerial(Mid(sch_etd2.Text, 7, 4), Mid(sch_etd2.Text, 4, 2), Mid(sch_etd2.Text, 1, 2))
            Else
                c6.Value = CDate("01/01/1900")
            End If
            If sch_eta1.Text <> "" Then
                c7.Value = DateSerial(Mid(sch_eta1.Text, 7, 4), Mid(sch_eta1.Text, 4, 2), Mid(sch_eta1.Text, 1, 2))
            Else
                c7.Value = CDate("01/01/1900")
            End If
            If sch_eta2.Text <> "" Then
                c8.Value = DateSerial(Mid(sch_eta2.Text, 7, 4), Mid(sch_eta2.Text, 4, 2), Mid(sch_eta2.Text, 1, 2))
            Else
                c8.Value = CDate("01/01/1900")
            End If
            c9.Value = sch_scac.Text
            c10.Value = sch_vessel.Text
            c11.Value = sch_voyage.Text
            c12.Value = DropDownListYear.SelectedValue
            c13.Value = DropDownListWeek.SelectedValue
            c14.Value = DropDownListFType.SelectedValue
            c15.Value = sch_inv.Text
            c16.Value = sch_vou.Text
            c17.Value = sch_fre.Text

            cmd.Parameters.Add(c1)
            cmd.Parameters.Add(c2)
            cmd.Parameters.Add(c3)
            cmd.Parameters.Add(c4)
            cmd.Parameters.Add(c5)
            cmd.Parameters.Add(c6)
            cmd.Parameters.Add(c7)
            cmd.Parameters.Add(c8)
            cmd.Parameters.Add(c9)
            cmd.Parameters.Add(c10)
            cmd.Parameters.Add(c11)
            cmd.Parameters.Add(c12)
            cmd.Parameters.Add(c13)
            cmd.Parameters.Add(c14)
            cmd.Parameters.Add(c15)
            cmd.Parameters.Add(c16)
            cmd.Parameters.Add(c17)

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)

            'If ds.Tables(0).Rows.Count > 0 Then
            G1.DataSource = ds
            G1.EmptyDataText = "No Result Found..."
            G1.DataBind()

            'Else
            'Bind Empty grdview with Columns names in Header and "No employee record found" message if no records are found in the database
            'BindEmptyGridWithHeader(grdEmp, ds)
            'End If
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

        'Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        'Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        'Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Dim downloadfilename As String = ""
        Dim docArray() As String
        ReDim docArray(G1.Rows.Count)
        Dim i As Integer = 0

        Try
            Using zipFile As New ZipFile()
                'Response.Clear()
                'Response.ContentType = "application/zip"
                'Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G1.Rows
                    Dim checkcell As TableCell = G1.Rows(gvr.RowIndex).Cells(1)

                    Dim IsChecked As Boolean = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_Sel"), CheckBox).Checked

                    Dim cell As TableCell = G1.Rows(gvr.RowIndex).Cells(4)

                    'Dim downloadfilename As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value
                    downloadfilename = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value

                    'If File.Exists(downloadfilename) And IsChecked Then
                    '    zipFile.AddFile(downloadfilename, "")
                    'End If
                    If File.Exists(downloadfilename) And IsChecked Then
                        'MsgBox(Array.IndexOf(docArray, downloadfilename))
                        If Array.IndexOf(docArray, downloadfilename) = -1 Then
                            zipFile.AddFile(downloadfilename, "")
                        End If
                    End If

                    docArray(i) = downloadfilename
                    i += 1
                Next

                'zipFile.Save(Response.OutputStream)
                Response.Clear()
                Response.Buffer = False
                Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
                Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
                Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)
                zipFile.Save(Response.OutputStream)
                Response.[End]()
            End Using
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        End Try

    End Sub

    Private Sub DownloadFile()

        'Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
        'Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
        'Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename

        Dim downloadfilename As String = ""
        Dim docArray() As String
        ReDim docArray(G1.Rows.Count)
        Dim i As Integer = 0

        Try
            Using zipFile As New ZipFile()
                'Response.Clear()
                'Response.ContentType = "application/zip"
                'Response.AddHeader("content-disposition", "filename=" & zipfilename)

                For Each gvr As GridViewRow In G1.Rows
                    Dim cell As TableCell = G1.Rows(gvr.RowIndex).Cells(4)

                    'Dim downloadfilename As String = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value
                    downloadfilename = DirectCast(G1.Rows(gvr.RowIndex).FindControl("txt_FullFileName"), HiddenField).Value

                    If File.Exists(downloadfilename) Then
                        'MsgBox(Array.IndexOf(docArray, downloadfilename))
                        If Array.IndexOf(docArray, downloadfilename) = -1 Then
                            zipFile.AddFile(downloadfilename, "")
                        End If
                    End If

                    docArray(i) = downloadfilename
                    i += 1
                Next

                Response.Clear()
                Response.Buffer = False
                Dim zipfilename As String = String.Format("download_{0}.zip", DateTime.UtcNow.Ticks)
                Dim tmpfilePath As String = Server.MapPath("~/temp/") + zipfilename
                Dim zipfilePath As String = Server.MapPath("~/zipfile/") + zipfilename
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "filename=" & zipfilename)
                zipFile.Save(Response.OutputStream)
                Response.[End]()
            End Using
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "\n\n" & downloadfilename.Replace("\", "\\") & "');", True)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        bindGridView()

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        sch_hbl.Text = ""
        sch_mbl.Text = ""
        sch_cnee.Text = ""
        sch_etd1.Text = ""
        sch_etd2.Text = ""
        sch_eta1.Text = ""
        sch_eta2.Text = ""
        sch_scac.Text = ""
        sch_vessel.Text = ""
        sch_voyage.Text = ""
        sch_inv.Text = ""
        sch_vou.Text = ""
        sch_fre.Text = ""
        DropDownListYear.SelectedIndex = -1
        DropDownListWeek.SelectedIndex = -1
        DropDownListFType.SelectedIndex = -1

        G1.DataSource = Nothing
        G1.EmptyDataText = ""
        G1.DataBind()

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

    Protected Sub BtnHome_Click(sender As Object, e As EventArgs) Handles BtnHome.Click

        Response.Redirect("http://dms.topocean.com.hk:8080/")

    End Sub
End Class