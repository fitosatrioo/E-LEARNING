Public Class Site1
    Inherits System.Web.UI.MasterPage
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim salah As er_custom
    Dim str As String
    Dim dr_user As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lb_user.InnerText = dr_user("nama")
        a_publish.InnerText = "Publish Date : " & Format(System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).Date, "yyyy-MM-dd")
        'a_logout.HRef = "el_logout.aspx"

        Page.MaintainScrollPositionOnPostBack = True
    End Sub

    Private Sub Site1_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        If dr_user Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        Dim usernm As String = ""
        Dim userid As String = ""
        'userid = Request.QueryString("id")
        userid = dr_user("id")
        'userid = 1


        Dim dtuser As New DataTable
        str = "select * from tbl_login "
        str = str & "where id = '" & userid & "' "

        salah = Mod_Utama.isi_data(dtuser, str, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Mod_Utama.tampil_error(Me.Page, "Ada Error saat Page Init, Site1.Master")
            Return
        End If


        Session("dr_user") = dtuser.Rows(0)

        dr_user = Session("dr_user")
    End Sub

    Private Sub a_logout_ServerClick(sender As Object, e As EventArgs) Handles a_logout.ServerClick
        HttpContext.Current.Session.Clear()
        HttpContext.Current.Session.Abandon()
        HttpContext.Current.Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("login.aspx")
    End Sub
End Class