Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class TUploader

    Inherits System.Web.UI.UserControl

    Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If DMSConn.State = ConnectionState.Closed Then
            DMSConn.Open()
        End If

        If Not IsPostBack Then
            bindDropDown()
        End If

    End Sub

    Private Sub BindDropDown()

        Try
            Dim cmd As New SqlCommand("SELECT val1, val2 FROM Argv WHERE key1 = 'filetype' and active = 1 order by val1", DMSConn)

            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 900

            Dim adp As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            adp.Fill(ds)

            For Each ctl As Control In Controls
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

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try

    End Sub

    Protected Sub bunUpload_Click(ByVal sender As Object, ByVal e As EventArgs)

        Response.Write("Press Upload")

        Dim UploadFileType As String
        Dim UploadFileName As String

        Dim ftdl As New DropDownList
        Dim fnup As New FileUpload

        For i = 1 To 5
            ftdl = Me.FindControl(String.Format("ft{0}", i))

            UploadFileType = ftdl.SelectedItem.Value

            fnup = Me.FindControl(String.Format("updr{0}", i))

            UploadFileName = fnup.FileName

        Next
        '{
        '    Hashtable ht = new Hashtable();

        '    for (int i = 1; i <= 5; i++)
        '    {
        '        DropDownList ft = (DropDownList)this.FindControl(string.Format("ft{0}", i));
        '        DropDownList ag = (DropDownList)this.FindControl(string.Format("agt{0}", i));
        '        FileUpload upd = (FileUpload)this.FindControl(string.Format("updr{0}", i));
        '        Label sts = (Label)this.FindControl(string.Format("sts{0}", i));

        '        string filefmt = CArgs.GetMore("dmsupload", "filetype", ft.Text);
        '        string filename = filefmt.Replace("$A$", ag.Text).Replace("$M$", PSession1.MBL).Replace("$H$", PSession1.HBL);

        '        uploadRoot = CArgs.GetVal1("uploader1", "rootpath");
        '        string desfile = string.Format("{0}{1}\\{2}_{3}", uploadRoot, ft.Text, filename, DateTime.Now.Ticks);

        '        UploadFile(ft.Text, upd, desfile, sts);

        '        if (refresh != null) refresh();
        '    }
        '}

    End Sub



End Class