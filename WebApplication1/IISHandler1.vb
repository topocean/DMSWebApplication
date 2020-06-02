Imports System.Web
Public Class IISHandler1
    Implements IHttpHandler

    ''' <summary>
    '''  您將需要在您 Web 的 Web.config 檔中設定此處理常式，
    '''  並且向 IIS 註冊該處理程式，才能使用它。如需詳細資訊，
    '''  請參閱下列連結: http://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "IHttpHandler 成員"

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            ' 如果您的 Managed 處理常式無法重新使用於其他要求，則傳回 false。
            ' 如果您有針對要求保留的一些狀態資訊，通常是 false。
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        ' 在這裡寫下您的處理常式實作。

    End Sub

#End Region

End Class
