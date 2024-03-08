Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class el_hasil
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt2, dt_level, dt_pre, dt_materi, dt_soal As New DataTable
    Dim dr As DataRow
    Dim idrec As Int64
    Dim id_mquiz As Int64
    Dim sumber As String
    Dim cb As GridViewDataComboBoxColumn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_hasil_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_hasil_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_hasil_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            sumber = Request.QueryString("sumber")
            idrec = Request.QueryString("idrec")
            id_mquiz = Request.QueryString("id_mquiz")
        Catch ex As Exception
            Response.Redirect("login.aspx")
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
        If Me.sumber = "pre" Then
            strg = "select *,(select kkm from el_tbl_paket where id_paket = el_tbl_hasil.id_pre  ) as kkm,(select nama from tbl_login where id = el_tbl_hasil.id_user) as nama,"
            strg = strg & "(select judul from el_tbl_manajemen_quiz where id_mquiz = el_tbl_hasil.id_mquiz) as judul,"
            strg = strg & "(select CONCAT(paket_soal,'_',judul_training) from el_tbl_paket where id_paket =el_tbl_hasil.id_pre) as paket, "
            strg = strg & "(select sum(skor) from el_tbl_nilai where el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.app_sta = '1') as skor, "
            strg = strg & "(select CASE WHEN el_tbl_paket.jenis_test = 'PRE TEST' THEN 'pre' ELSE el_tbl_paket.jenis_test END AS jenis_test from el_tbl_paket where id_paket = el_tbl_hasil.id_pre) as jenis "
            strg = strg & "FROM el_tbl_hasil where id_pre = '" & Me.idrec & "' and id_mquiz = '" & Me.id_mquiz & "' order by id_hasil DESC"
            Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If



            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id_hasil"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        ElseIf Me.sumber = "post" Then
            strg = "select *,(select nama from tbl_login where id = el_tbl_hasil.id_user) as nama,"
            strg = strg & "(select judul from el_tbl_manajemen_quiz where id_mquiz = el_tbl_hasil.id_mquiz) as judul,"
            strg = strg & "(select CONCAT(paket_soal,'_',judul_training) from el_tbl_paket where id_paket =el_tbl_hasil.id_post) as paket, "
            strg = strg & "(select sum(skor) from el_tbl_nilai where el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.app_sta = '1') as skor, "
            strg = strg & "(select CASE WHEN el_tbl_paket.jenis_test = 'POST TEST' THEN 'post' ELSE el_tbl_paket.jenis_test END AS jenis_test from el_tbl_paket where id_paket = el_tbl_hasil.id_post) as jenis "
            strg = strg & "FROM el_tbl_hasil where id_post = '" & Me.idrec & "' and id_mquiz = " & Me.id_mquiz & ""
            Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If


            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id_hasil"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Else
            Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Query, harap kirim laporan ke MIS via email")

        End If


    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app") = False Then
                    str = "update el_tbl_hasil set "
                    str = str & "app = 1 "
                    str = str & "where id_hasil = '" & dr("id_hasil") & "'"
                Else
                    str = "update el_tbl_hasil set "
                    str = str & "app = 0 "
                    str = str & "where id_hasil = '" & dr("id_hasil") & "'"
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub


    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If

        End Select
    End Sub
End Class