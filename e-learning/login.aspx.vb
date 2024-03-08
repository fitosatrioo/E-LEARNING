Public Class register
    Inherits System.Web.UI.Page
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dr As DataRow
    Dim dr_user As DataRow
    Dim id_comp As Integer = 0
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.pesan_div.Visible = False
        End If
    End Sub

    Protected Sub bt_login_ServerClick(sender As Object, e As EventArgs)
        Dim username As String = Me.tx_user.Value
        Dim pass As String = Me.tx_pass.Value
        'Dim comp As String = Me.cb_comp.Value
        Dim sta_bacaulang As Boolean = False

        Dim dtuser As New DataTable
        str = "select * from tbl_login "
        str = str & "where username = '" & username.ToLower & "' "
        str = str & "and password = '" & pass.ToLower & "' "

        salah = Mod_Utama.isi_data(dtuser, str, "id", waktu_query)

        If salah.er_hasil <> "" Then
            Me.pesan_lb.InnerText = "Terjadi Kesalahan: " & salah.er_hasil
            Return
        End If

        If dtuser.Rows.Count = 0 Then
            Me.pesan_div.Visible = True
            Me.pesan_lb.InnerText = "Nama atau Password Tidak Ditemukan"
            Me.tx_user.Focus()
            Return
        End If

        dr = dtuser.Rows(0)
        If dr("password") <> Me.tx_pass.Value Then
            Me.pesan_div.Visible = True
            Me.pesan_lb.InnerText = "Passwordd tidak sesuai, coba lagi! "
            Me.tx_pass.Focus()
            Return
        End If
        str = "select * from el_tbl_manajemen_quiz where app_sta = 1"
        Me.salah = Mod_Utama.isi_data(dt, str, "id_mquiz", waktu_query)
        For Each dtr As DataRow In dt.Rows
            str = "UPDATE el_tbl_manajemen_quiz SET app = 1 WHERE GETDATE() BETWEEN tgl_akses AND tgl_akhir AND id_mquiz = '" & dtr("id_mquiz") & "'"
            Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
            str = "UPDATE el_tbl_manajemen_quiz SET app = 0 WHERE GETDATE() NOT BETWEEN tgl_akses AND tgl_akhir AND id_mquiz = '" & dtr("id_mquiz") & "'"
            Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        Next

        For Each dtr As DataRow In dtuser.Rows
            If dtr("level") = "1" Then
                dr_user = dtuser.Rows(0)
                HttpContext.Current.Session("dr_user") = dr
                'Session("dr_user") = dtuser.Rows(0)
                Response.Redirect("el_adm_karyawan.aspx")
            ElseIf dtr("level") = "2" Then
                dr_user = dtuser.Rows(0)
                HttpContext.Current.Session("dr_user") = dr
                'Session("dr_user") = dtuser.Rows(0)
                Response.Redirect("el_adm_karyawan.aspx")
            ElseIf dtr("level") = "3" Then
                dr_user = dtuser.Rows(0)
                HttpContext.Current.Session("dr_user") = dr
                'Session("dr_user") = dtuser.Rows(0)
                Response.Redirect("el_adm_karyawan.aspx")
            Else
                dr_user = dtuser.Rows(0)
                HttpContext.Current.Session("dr_user") = dr
                'Session("dr_user") = dtuser.Rows(0)
                Response.Redirect("el_pilihan_materi2.aspx")
            End If
        Next


    End Sub

    Protected Sub Unnamed_ServerClick(sender As Object, e As EventArgs)
        Me.pesan_div.Visible = False

    End Sub
End Class