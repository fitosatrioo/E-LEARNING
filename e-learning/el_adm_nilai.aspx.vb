Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Reflection.MethodBase
Public Class el_adm_nilai
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idhasil, idpaket As Int64
    Dim sumber As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_adm_nilai_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_adm_nilai_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_adm_nilai_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idhasil = Request.QueryString("idhasil")
            idpaket = Request.QueryString("idpaket")
            sumber = Request.QueryString("sumber")
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

        If Me.sumber = "pre" Then
            strg = "select *,(select nama from tbl_login where tbl_login.id = el_tbl_nilai.id_user) as nama "
            strg = strg & "from el_tbl_hasil inner join el_tbl_paket on el_tbl_paket.id_paket = el_tbl_hasil.id_pre inner join el_tbl_nilai on el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and el_tbl_nilai.id_paket = el_tbl_paket.id_paket inner join el_tbl_soal on el_tbl_soal.id_soal = el_tbl_nilai.id_soal where "
            strg = strg & "el_tbl_hasil.id_hasil = '" & Me.idhasil & "' ORDER BY CASE WHEN el_tbl_soal.jenis_soal = 'pg' THEN 1 ELSE 0 END ASC, el_tbl_soal.jenis_soal DESC, el_tbl_nilai.app_sta ASC "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_nilai", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id_nilai"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        ElseIf Me.sumber = "post" Then
            strg = "select *,(select nama from tbl_login where tbl_login.id = el_tbl_nilai.id_user) as nama "
            strg = strg & "from el_tbl_hasil inner join el_tbl_paket on el_tbl_paket.id_paket = el_tbl_hasil.id_post inner join el_tbl_nilai on el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil and el_tbl_nilai.id_paket = el_tbl_paket.id_paket inner join el_tbl_soal on el_tbl_soal.id_soal = el_tbl_nilai.id_soal where "
            strg = strg & "el_tbl_hasil.id_hasil = '" & Me.idhasil & "' ORDER BY CASE WHEN el_tbl_soal.jenis_soal = 'pg' THEN 1 ELSE 0 END ASC, el_tbl_soal.jenis_soal DESC, el_tbl_nilai.app_sta ASC "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_nilai", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If

            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id_nilai"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)

        End If
    End Sub



    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "app_sta"
                If dr("app_sta") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If

        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "app_sta"
                If dr("app_sta") = False Then
                    str = "update el_tbl_nilai set "
                    str = str & "app_sta = 1 "
                    str = str & "where id_nilai = " & dr("id_nilai") & " "
                Else
                    str = "update el_tbl_nilai set "
                    str = str & "app_sta = 0 "
                    str = str & "where id_nilai = " & dr("id_nilai") & " "
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE el_tbl_nilai set "
        str = str & "skor = '" & e.NewValues("skor") & "' "
        str = str & "where id_nilai = " & e.Keys("id_nilai")
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Update Records pada Menu Manajemen Karyawan"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub
End Class