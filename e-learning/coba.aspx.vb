Public Class coba
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dtj, dt1, dt2, dt3, dt4 As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private Sub coba_Init(sender As Object, e As EventArgs) Handles Me.Init

        Dim usernm As String = ""
        Dim userid As String = ""
        'userid = Request.QueryString("id")
        'userid = 1


        Dim dtuser As New DataTable
        str = "select * from tbl_login "
        str = str & "where id = '2' "

        salah = Mod_Utama.isi_data(dtuser, str, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Mod_Utama.tampil_error(Me.Page, "Ada Error saat Page Init, Site1.Master")
            Return
        End If


        Session("dr_user") = dtuser.Rows(0)

        dr_user = Session("dr_user")
        Me.waktu()
    End Sub

    Private Sub waktu()

        strg = "select * from el_tbl_paket where id_paket = '1'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_paket", waktu_query)

        For Each dtam As DataRow In dt.Rows
            minutes.Value = dtam("waktu")
        Next
    End Sub


End Class