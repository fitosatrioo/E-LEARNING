Public Class uc_footer
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub uc_footer_Init(sender As Object, e As EventArgs) Handles Me.Init
        If HttpContext.Current.Request.FilePath <> "/page_error.aspx" Then HttpContext.Current.Session("time_query") = ""

    End Sub
End Class