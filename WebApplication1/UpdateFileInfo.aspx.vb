Imports System.Data.SqlClient

Public Class UpdateFileInfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim HBL As String = ""
        Dim MBL As String = ""
        Dim Office As String = ""
        Dim BkhRefId As Integer = 0
        Dim TitanUserName As String = ""
        Dim KeyRefId As String = ""
        Dim SupplierInvNo As String = ""


        Dim DMSConn As New SqlConnection(ConfigurationManager.ConnectionStrings("DMSConnectionString").ConnectionString)

        Try
            HBL = Request.QueryString("BkhBLNo")
            MBL = Request.QueryString("BkhMBLNo")
            Office = Request.QueryString("usrbrh")
            BkhRefId = Request.QueryString("BkhRefId")
            TitanUserName = Request.QueryString("TitanUserName")
            SupplierInvNo = Request.QueryString("SupplierInvNo")


            If MBL <> "" Then
                If DMSConn.State = ConnectionState.Closed Then
                    DMSConn.Open()
                End If
                ' select a HRefId aims to capture the InvoceNo to insert into FileInfo table
                Dim cmd2 As New SqlCommand("select top 1 HRefId from BookingHBL where HBL = '" & HBL & "' ", DMSConn)
                'Dim cmd2 As New SqlCommand("select top 1 HRefId from BookingHBL where HBL = 'QDOSFOD70147' ", DMSConn) 
                cmd2.CommandType = CommandType.Text
                cmd2.CommandTimeout = 900

                Dim adp2 As New SqlDataAdapter(cmd2)
                Dim ds2 As New DataSet()
                adp2.Fill(ds2)
                cmd2.Dispose()
                If ds2.Tables(0).Rows.Count > 0 Then

                    KeyRefId = ds2.Tables(0).Rows(0)(0)

                End If


                TextBox1.Text = SupplierInvNo



                Dim cmd As New SqlCommand("INSERT INTO [dbo].[FileInfo]
           ([FileType]
           ,[FileName]
           ,[AgentCd]
           ,[MBL]
           ,[HBL]
           ,[IssueDate]
           ,[Active]
           ,[RevCount]
           ,[LastDate]
           ,[InvNo]
           ,[LastUTC]
           ,[KeyRefId]
           ,[BType]
           ,[Status])
             VALUES
           ('SPI'
           ,''
           ,'" & Office & "'
           ,'" & MBL & "'
           ,'" & HBL & "'
           ,CURRENT_TIMESTAMP
           ,'1'
           ,'0'
           ,CURRENT_TIMESTAMP
           ,'" & SupplierInvNo.Replace("'", "''") & "'
           ,CURRENT_TIMESTAMP
           ,'" & KeyRefId & "'
           ,'H'
           ,1)", DMSConn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 900

                Dim adp As New SqlDataAdapter(cmd)
                Dim ds As New DataSet()
                adp.Fill(ds)
                cmd.Dispose()

            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Page.[GetType](), Guid.NewGuid().ToString(), "alert('" & ex.Message.ToString() & "');", True)
        Finally
            DMSConn.Close()
        End Try




    End Sub

End Class