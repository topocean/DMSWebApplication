Public Class DMSService

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim field As String = "<input id='{0}' name='{1}' type='text' value='{2}'/>"
        Dim htmtmp As String = "<tr><th>{0}</th><td>{1}</td></tr>"
        Dim id As String
        Dim val() As String
        Dim args As String = ""

        For i = 0 To Request.Form.Count
            id = Request.Form.GetKey(i)
            val = Request.Form.GetValues(i)
            'args &= String.Format(htmtmp, id, String.Format(field, id, id, val))
        Next


    End Sub

End Class