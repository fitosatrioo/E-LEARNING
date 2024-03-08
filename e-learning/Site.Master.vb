Public Class SiteMaster
    Inherits MasterPage
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim salah As er_custom
    Dim str As String
    Dim dr_user As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        lb_user.InnerText = dr_user("nama")

        If dr_user("level") = 2 Or dr_user("level") = 3 Or dr_user("level") = 4 Then

        Else
            Response.Redirect("el_pilihan_quiz.aspx")
        End If
        a_publish.InnerText = "Publish Date : " & Format(System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).Date, "yyyy-MM-dd")
        'a_logout.HRef = Mod_Utama.page_login

        Page.MaintainScrollPositionOnPostBack = True


    End Sub

    Private Sub SiteMaster_Init(sender As Object, e As EventArgs) Handles Me.Init
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
    Public Sub hide_menu()
        Me.body_site.Attributes.Add("class", "sidebar-wide")
        Me.div_container.Attributes.Add("class", "page-container sidebar-hidden")
    End Sub
    Public Sub small_menu()
        Me.body_site.Attributes.Add("class", "sidebar-narrow")
        Me.div_container.Attributes.Add("class", "page-container")
    End Sub

    Public Sub callback_pesan(hal As Object, pesan As String)
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        sb.Append("<script language='javascript'>")
        sb.Append("$.jGrowl('" & pesan & "', { header: 'Notification !', life: 10000 });")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(hal, Me.GetType(), "callback_pesan", sb.ToString, False)
    End Sub
    Public Sub callback_sukses(hal As Object, pesan As String)
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        sb.Append("<script language='javascript'>")
        sb.Append("$.jGrowl('" & pesan & "', { theme: 'growl-success', header: 'Success !', life: 10000 });")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(hal, Me.GetType(), "callback_sukses", sb.ToString, False)
    End Sub
    Public Sub callback_error(hal As Object, pesan As String)
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        sb.Append("<script language='javascript'>")
        sb.Append("$.jGrowl('" & pesan & "', { theme: 'growl-error', header: 'Error !', life: 10000 });")
        sb.Append("</script>")
        ScriptManager.RegisterStartupScript(hal, Me.GetType(), "callback_error", sb.ToString, False)
    End Sub
    Private Sub a_logout_ServerClick(sender As Object, e As EventArgs) Handles a_logout.ServerClick
        HttpContext.Current.Session.Clear()
        HttpContext.Current.Session.Abandon()
        HttpContext.Current.Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("login.aspx")
    End Sub
End Class