Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Reflection.MethodBase
Public Class dept
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_detail, dt_detail2 As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec, id_materi, id_bank, id_mquiz, id_hasil, id_isimt As Int64
    Private ASPxGridViewDetail1, ASPxGridViewDetail2 As ASPxGridView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub dept_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub dept_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub dept_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")
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
        If Me.idrec = "1" Then
            strg = "select *,"
            strg = strg & "(select isnull(count(*),0) from el_tbl_materi where id_dept = tbl_departement.id_dept) as jumlah "
            strg = strg & "from tbl_departement "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_dept", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If
            Me.div1.Visible = True
            Me.div2.Visible = False
            Me.div3.Visible = False
            Me.div4.Visible = False
            Me.ASPxGridView2.Visible = False
            Me.ASPxGridView3.Visible = False
            Me.ASPxGridView4.Visible = False
            Me.ASPxGridView1.DataSource = dt
            Me.ASPxGridView1.KeyFieldName = "id_dept"
            Me.ASPxGridView1.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        ElseIf Me.idrec = "2" Then
            strg = "select *,"
            strg = strg & "(select isnull(count(*),0) from el_tbl_paket where id_dept = tbl_departement.id_dept) as quiz "
            strg = strg & "from tbl_departement "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_dept", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If
            Me.div1.Visible = False
            Me.div2.Visible = True
            Me.div3.Visible = False
            Me.div4.Visible = False
            Me.ASPxGridView1.Visible = False
            Me.ASPxGridView3.Visible = False
            Me.ASPxGridView4.Visible = False
            Me.ASPxGridView2.DataSource = dt
            Me.ASPxGridView2.KeyFieldName = "id_dept"
            Me.ASPxGridView2.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView2)
        ElseIf Me.idrec = "3" Then
            strg = "select *,"
            strg = strg & "(select isnull(count(*),0) from el_tbl_manajemen_quiz where id_dept = tbl_departement.id_dept) as quiz "
            strg = strg & "from tbl_departement "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_dept", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If
            Me.div1.Visible = False
            Me.div2.Visible = False
            Me.div3.Visible = True
            Me.div4.Visible = False
            Me.ASPxGridView1.Visible = False
            Me.ASPxGridView2.Visible = False
            Me.ASPxGridView4.Visible = False
            Me.ASPxGridView3.DataSource = dt
            Me.ASPxGridView3.KeyFieldName = "id_dept"
            Me.ASPxGridView3.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView3)

        ElseIf Me.idrec = "4" Then
            strg = "select *,"
            strg = strg & "(select isnull(count(*),0) from el_tbl_manajemen_quiz where id_dept = tbl_departement.id_dept) as quiz "
            strg = strg & "from tbl_departement "

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_dept", waktu_query)
            If salah.er_hasil <> "" Then
                Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Exit Sub
            End If
            Me.div1.Visible = False
            Me.div2.Visible = False
            Me.div3.Visible = False
            Me.div4.Visible = True
            Me.ASPxGridView1.Visible = False
            Me.ASPxGridView2.Visible = False
            Me.ASPxGridView3.Visible = False
            Me.ASPxGridView4.DataSource = dt
            Me.ASPxGridView4.KeyFieldName = "id_dept"
            Me.ASPxGridView4.DataBind()
            Mod_Utama.Atur_Grid(Me.ASPxGridView4)

        End If

    End Sub


    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO tbl_departement ("
        str = str & "id_dept,dept_name, dept_head, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_dept),0)+1 from tbl_departement),"
        str = str & "'" & e.NewValues("dept_name") & "', "
        str = str & "'" & e.NewValues("dept_head") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "


        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Insert Records pada Menu Master Siswa"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE tbl_departement set "
        str = str & "dept_name = '" & e.NewValues("dept_name") & "', "
        str = str & "dept_head = '" & e.NewValues("dept_head") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_dept = " & e.Keys("id_dept")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Insert Records pada Menu Master Siswa"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Protected Sub ASPxGridViewDetail12_BeforePerformDataSelect1(sender As Object, e As EventArgs)
        Try
            id_isimt = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        str = "select *, (select id from tbl_login where id = '" & dr_user("id") & "') as id "
        str = str & "from el_tbl_isi_materi "
        str = str & "where id_materi = " & id_isimt & " "
        str = str & "order by id_isimt desc"
        Mod_Utama.isi_data(Me.dt_detail2, str, "id_isimt", waktu_query)

        ASPxGridViewDetail2 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail2 Is Nothing Then Exit Sub

        ASPxGridViewDetail2.DataSource = Me.dt_detail2
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail2)
        ASPxGridViewDetail2.Settings.ShowGroupPanel = False
        ASPxGridViewDetail2.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail2.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail2.Settings.ShowFilterRow = False
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From tbl_departement where id_dept = " & e.Keys("id_dept"), "tbl_departement", dr_user)
        str = "DELETE tbl_departement "
        str = str & "where id_dept = " & e.Keys("id_dept")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Delete Records pada Menu Master User Authorize"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Protected Sub ASPxGridViewDetail3_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
        dr = Me.ASPxGridViewDetail1.GetDataRow(e.VisibleIndex)
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

    Protected Sub ASPxGridViewDetail12_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            id_isimt = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        str = "select *, (select id from tbl_login where id = '" & dr_user("id") & "') as id "
        str = str & "from el_tbl_isi_materi "
        str = str & "where id_materi = " & id_isimt & " "
        str = str & "order by id_isimt desc"
        Mod_Utama.isi_data(Me.dt_detail2, str, "id_isimt", waktu_query)

        ASPxGridViewDetail2 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail2 Is Nothing Then Exit Sub

        ASPxGridViewDetail2.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail2)
        ASPxGridViewDetail2.Settings.ShowGroupPanel = False
        ASPxGridViewDetail2.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail2.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail2.Settings.ShowFilterRow = False
    End Sub

    Protected Sub ASPxGridView5_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            id_hasil = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        strg = "select *, (select CONCAT(paket_soal,'_',judul_training)  FROM el_tbl_paket where el_tbl_paket.id_paket = el_tbl_manajemen_quiz.pre_test) as pre, "
        strg = strg & "(select CONCAT(paket_soal,'_',judul_training) FROM el_tbl_paket where el_tbl_paket.id_paket = el_tbl_manajemen_quiz.id_soal) as post"
        strg = strg & " FROM el_tbl_manajemen_quiz where id_dept = '" & id_hasil & "'"
        Mod_Utama.isi_data(Me.dt_detail, strg, "id_mquiz", waktu_query)

        ASPxGridViewDetail1 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail1 Is Nothing Then Exit Sub

        ASPxGridViewDetail1.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail1)
        ASPxGridViewDetail1.Settings.ShowGroupPanel = False
        ASPxGridViewDetail1.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail1.Settings.ShowFilterRow = False
    End Sub

    Protected Sub ASPxGridViewDetail3_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs)
        dr = Me.ASPxGridViewDetail1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app") = False Then
                    str = "update el_tbl_manajemen_quiz set "
                    str = str & "app = 1 "
                    str = str & "where id_mquiz = " & dr("id_mquiz") & " "
                Else
                    str = "update el_tbl_manajemen_quiz set "
                    str = str & "app = 0 "
                    str = str & "where id_mquiz = " & dr("id_mquiz") & " "
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub

    Protected Sub ASPxGridViewDetail3_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            id_mquiz = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        strg = "select *, "
        strg = strg & "(select CONCAT(paket_soal,'_',judul_training) from el_tbl_paket where id_paket = el_tbl_manajemen_quiz.pre_test) as pre,"
        strg = strg & "(select CONCAT(paket_soal,'_',judul_training) from el_tbl_paket where id_paket = el_tbl_manajemen_quiz.id_soal) as post,"
        strg = strg & "(select judul_materi from el_tbl_materi where id_materi = el_tbl_manajemen_quiz.id_materi) as materi,"
        strg = strg & "(select nama_level from el_tbl_level where id = el_tbl_manajemen_quiz.level) as lvl "
        strg = strg & "FROM el_tbl_manajemen_quiz where id_dept = '" & id_mquiz & "'"
        Mod_Utama.isi_data(Me.dt_detail, strg, "id_mquiz", waktu_query)

        ASPxGridViewDetail1 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail1 Is Nothing Then Exit Sub

        ASPxGridViewDetail1.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail1)
        ASPxGridViewDetail1.Settings.ShowGroupPanel = False
        ASPxGridViewDetail1.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail1.Settings.ShowFilterRow = False
    End Sub

    Protected Sub ASPxGridViewDetail1_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            id_materi = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        strg = "select *,"
        strg = strg & "(select isnull(count(*),0) from el_tbl_isi_materi where id_materi = el_tbl_materi.id_materi) as image "
        strg = strg & "from el_tbl_materi where id_dept = '" & id_materi & "'"
        Mod_Utama.isi_data(Me.dt_detail, strg, "id_materi", waktu_query)

        ASPxGridViewDetail1 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail1 Is Nothing Then Exit Sub

        ASPxGridViewDetail1.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail1)
        ASPxGridViewDetail1.Settings.ShowGroupPanel = False
        ASPxGridViewDetail1.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail1.Settings.ShowFilterRow = False
    End Sub
    Protected Sub ASPxGridViewDetail2_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            id_bank = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try
        strg = "select *,"
        strg = strg & "(select isnull(count(*),0) from el_tbl_soal where id_paket = el_tbl_paket.id_paket) as soal "
        strg = strg & "from el_tbl_paket where id_dept = '" & id_bank & "'"
        Mod_Utama.isi_data(Me.dt_detail, strg, "id_paket", waktu_query)

        ASPxGridViewDetail1 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail1 Is Nothing Then Exit Sub

        ASPxGridViewDetail1.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail1)
        ASPxGridViewDetail1.Settings.ShowGroupPanel = False
        ASPxGridViewDetail1.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail1.Settings.ShowFilterRow = False
    End Sub
End Class