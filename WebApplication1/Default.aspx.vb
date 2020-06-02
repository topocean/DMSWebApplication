Imports System.IO

Public Class _Default

    Inherits System.Web.UI.Page

    Public ReadOnly Property maxRequestLength As Integer

        Get
            Return 4096 * 1024
        End Get

    End Property

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

    End Sub

End Class