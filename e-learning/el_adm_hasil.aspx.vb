Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Reflection.MethodBase
Public Class el_adm_hasil
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt2 As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_adm_hasil_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_adm_hasil_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_adm_hasil_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")

        Catch ex As Exception
            Response.Redirect("home.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.isi_data()
    End Sub

    Private Sub Jika_error(er_str As String, er_hasil As String, er_menu As String, nopesan As Integer)
        salah.er_str = er_str
        salah.er_menu = er_menu
        salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
        Session("error") = salah

        Select Case nopesan
            Case 1
                Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Query, harap kirim laporan ke MIS via email")
            Case Else
                Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Proses, harap kirim laporan ke MIS via email")
        End Select
    End Sub
    Private Sub isi_data()


        strg = "select *, (select CONCAT(paket_soal,'_',judul_training)  FROM el_tbl_paket where el_tbl_paket.id_paket = el_tbl_manajemen_quiz.pre_test) as pre, "
        strg = strg & "(select CONCAT(paket_soal,'_',judul_training) FROM el_tbl_paket where el_tbl_paket.id_paket = el_tbl_manajemen_quiz.id_soal) as post"
        strg = strg & " FROM el_tbl_manajemen_quiz where id_dept = '" & Me.idrec & "' order by id_mquiz DESC"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_mquiz", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_mquiz"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub
End Class

'strg = "select *,"
'strg = strg & "(select isnull(count(*),0) from el_tbl_soal where id_paket = el_tbl_paket.id_paket) as soal "
'strg = strg & "from el_tbl_paket where id_dept = '" & Me.idrec & "'"

'Me.salah = Mod_Utama.isi_data(dt, strg, "id_paket", waktu_query)
'If salah.er_hasil <> "" Then
'Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
'Exit Sub
'End If

'Me.ASPxGridView1.DataSource = dt
'Me.ASPxGridView1.KeyFieldName = "id_paket"
'Me.ASPxGridView1.DataBind()
'Mod_Utama.Atur_Grid(Me.ASPxGridView1)