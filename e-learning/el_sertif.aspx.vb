Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class el_sertif
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_level, dt_pre, dt_materi, dt_soal As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Dim cb As GridViewDataComboBoxColumn
    Dim id_user As Int64
    Dim id_mquiz As Int64
    Dim tanggal As DateTime = DateTime.Now ' Ganti dengan tanggal yang ingin Anda tampilkan

    Dim tanggalFormatted As String = tanggal.ToString("dd MMMM yyyy")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_sertif_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            id_mquiz = Request.QueryString("id_mquiz")
            id_user = Request.QueryString("id_user")
        Catch ex As Exception
            Response.Redirect("el_pilihan_materi2.aspx")
        End Try
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
        strg = "select *,(select nama from tbl_login where id='" & Me.id_user & "') as nama, "
        strg = strg & "(select urutan from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_user='" & Me.id_user & "' and el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' and el_tbl_paket.jenis_test = 'POST TEST') as urutan, "
        strg = strg & "(select dept_name from tbl_departement where id_dept = el_tbl_manajemen_quiz.id_dept) as dept "
        strg = strg & "from el_tbl_manajemen_quiz where id_mquiz = '" & Me.id_mquiz & "'"
        Me.salah = Mod_Utama.isi_data(dt, strg, "id_mquiz", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each dtr As DataRow In dt.Rows
            Me.nama.InnerHtml = dtr("nama")
            Me.judul.InnerHtml = dtr("judul")
            If dtr("urutan") <= 9 Then
                Me.lblid.InnerHtml = dtr("dept") & "/0000" & dtr("urutan")
            ElseIf dtr("urutan") <= 99 Then
                Me.lblid.InnerHtml = dtr("dept") & "/000" & dtr("urutan")
            ElseIf dtr("urutan") <= 999 Then
                Me.lblid.InnerHtml = dtr("dept") & "/00" & dtr("urutan")
            ElseIf dtr("urutan") <= 9999 Then
                Me.lblid.InnerHtml = dtr("dept") & "/0" & dtr("urutan")
            ElseIf dtr("urutan") <= 99999 Then
                Me.lblid.InnerHtml = dtr("dept") & "/" & dtr("urutan")
            End If

            Me.tgl.InnerHtml = "yang diselenggarakan pada tanggal " & tanggalFormatted
        Next




    End Sub
End Class