Public Class clearsession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub clearsession_Init(sender As Object, e As EventArgs) Handles Me.Init
        Response.Redirect("el_pilihan_quiz.aspx")
    End Sub
End Class