Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class el_adm_laporan
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
    Dim id_mquiz, id_level, id_dept As Int64
    Dim cb As GridViewDataComboBoxColumn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub ASPxGridView1_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        'If e.Column.FieldName = "nilai_pre" Then
        '    If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
        '        e.DisplayText = "0"
        '    End If
        'End If
        'If e.Column.FieldName = "nilai_post" Then
        '    If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
        '        e.DisplayText = "0"
        '    End If
        'End If
        If e.Column.Caption = "Kategori Pre Test" Then
            Dim nilai As Integer
            If Integer.TryParse(e.GetFieldValue("nilai_pre").ToString(), nilai) Then
                If nilai >= 90 Then
                    e.DisplayText = "A"
                ElseIf nilai >= 70 AndAlso nilai <= 89 Then
                    e.DisplayText = "B"
                ElseIf nilai <= 69 Then
                    e.DisplayText = "C"
                End If
            End If
        End If

        If e.Column.Caption = "Kategori Post Test" Then
            Dim nilai As Integer
            If Integer.TryParse(e.GetFieldValue("nilai_post").ToString(), nilai) Then
                If nilai >= 90 Then
                    e.DisplayText = "A"
                ElseIf nilai >= 70 AndAlso nilai <= 89 Then
                    e.DisplayText = "B"
                ElseIf nilai <= 69 Then
                    e.DisplayText = "C"
                End If
            End If
        End If

        If e.Column.Caption = "Penyerapan Materi" Then
            Dim nilaiPre As Integer
            Dim nilaiPost As Integer

            If Integer.TryParse(e.GetFieldValue("nilai_pre").ToString(), nilaiPre) AndAlso Integer.TryParse(e.GetFieldValue("nilai_post").ToString(), nilaiPost) Then
                Dim kenaikan As Integer = nilaiPost - nilaiPre
                Dim presentasiKenaikan As Double = (kenaikan / nilaiPre) * 100
                e.DisplayText = presentasiKenaikan.ToString("0.00") & "%"
            End If
        End If

        If e.Column.Caption = "Kategori Penyerapan Materi" Then
            Dim nilaiPre As Integer
            Dim nilaiPost As Integer

            If Integer.TryParse(e.GetFieldValue("nilai_pre").ToString(), nilaiPre) AndAlso Integer.TryParse(e.GetFieldValue("nilai_post").ToString(), nilaiPost) Then
                Dim kenaikan As Integer = nilaiPost - nilaiPre
                Dim presentaseKenaikan As Double = (kenaikan / nilaiPre) * 100

                If presentaseKenaikan >= 90 Then
                    e.DisplayText = "HIGH"
                ElseIf presentaseKenaikan >= 70 AndAlso presentaseKenaikan <= 89 Then
                    e.DisplayText = "MEDIUM"
                ElseIf presentaseKenaikan <= 69 Then
                    e.DisplayText = "LOW"
                End If
            End If
        End If

    End Sub

    Private Sub el_adm_laporan_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_adm_laporan_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_adm_laporan_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            id_mquiz = Request.QueryString("idrec")
            id_level = Request.QueryString("idlvl")
            id_dept = Request.QueryString("id_dept")
        Catch ex As Exception
            Response.Redirect("home.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1
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
        If Me.id_level = "1" And Me.id_dept = "1" Then
            strg = "SELECT *, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and   el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.app_sta = '1') as nilai_pre, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and  el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.app_sta = '1') as nilai_post "
            strg = strg & "FROM tbl_login inner join el_tbl_hasil on tbl_login.id = el_tbl_hasil.id_user ineer join el_tbl_manajemen_quiz on el_tbl_manajemen_quiz.id_mquiz = el_tbl_hasil.id_mquiz and el_tbl_manajemen_quiz.level = tbl_login.level "
            strg = strg & "where  (el_tbl_manajemen_quiz.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_manajemen_quiz.id_mquiz IS NULL ) and (el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_hasil.id_mquiz IS NULL ) and tbl_login.departement = '" & Me.id_dept & "' "
            Me.salah = Mod_Utama.isi_data(dt, strg, "id", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        ElseIf Me.id_level = "1" Then
            strg = "SELECT *, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and   el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.app_sta = '1') as nilai_pre, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and  el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.app_sta = '1') as nilai_post "
            strg = strg & "FROM tbl_login inner join el_tbl_hasil on tbl_login.id = el_tbl_hasil.id_user inner join el_tbl_manajemen_quiz on el_tbl_manajemen_quiz.id_mquiz = el_tbl_hasil.id_mquiz and el_tbl_manajemen_quiz.level = tbl_login.level "
            strg = strg & "where  (el_tbl_manajemen_quiz.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_manajemen_quiz.id_mquiz IS NULL ) and (el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_hasil.id_mquiz IS NULL )  "
            Me.salah = Mod_Utama.isi_data(dt, strg, "id", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        ElseIf Me.id_dept = "1" Then
            strg = "SELECT *, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and   el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.app_sta = '1') as nilai_pre, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and  el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.app_sta = '1') as nilai_post "
            strg = strg & "FROM tbl_login inner join el_tbl_hasil on tbl_login.id = el_tbl_hasil.id_user inner join el_tbl_manajemen_quiz on el_tbl_manajemen_quiz.id_mquiz = el_tbl_hasil.id_mquiz and el_tbl_manajemen_quiz.level = tbl_login.level "
            strg = strg & "where tbl_login.level = '" & Me.id_level & "' and (el_tbl_manajemen_quiz.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_manajemen_quiz.id_mquiz IS NULL ) and (el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_hasil.id_mquiz IS NULL )  "
            Me.salah = Mod_Utama.isi_data(dt, strg, "id", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Else
            strg = "SELECT *, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and   el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_pre and el_tbl_nilai.app_sta = '1') as nilai_pre, "
            strg = strg & "(select ISNULL(SUM(skor), 0) from el_tbl_nilai where el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and  el_tbl_nilai.id_user = tbl_login.id and el_tbl_nilai.id_paket = el_tbl_hasil.id_post and el_tbl_nilai.app_sta = '1') as nilai_post "
            strg = strg & "FROM tbl_login inner join el_tbl_hasil on tbl_login.id = el_tbl_hasil.id_user inner join el_tbl_manajemen_quiz on el_tbl_manajemen_quiz.id_mquiz = el_tbl_hasil.id_mquiz and el_tbl_manajemen_quiz.level = tbl_login.level "
            strg = strg & "where tbl_login.level = '" & Me.id_level & "' and (el_tbl_manajemen_quiz.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_manajemen_quiz.id_mquiz IS NULL ) and (el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' or el_tbl_hasil.id_mquiz IS NULL ) and tbl_login.departement = '" & Me.id_dept & "'  "
            Me.salah = Mod_Utama.isi_data(dt, strg, "id", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        End If

    End Sub
End Class