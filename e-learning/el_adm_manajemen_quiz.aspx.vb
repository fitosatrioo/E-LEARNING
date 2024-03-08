Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class el_adm_manajemen_quiz
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_level, dt_pre, dt_materi, dt_soal, dt_check As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Dim cb As GridViewDataComboBoxColumn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_adm_manajemen_quiz_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_adm_manajemen_quiz_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_adm_manajemen_quiz_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")
        Catch ex As Exception
            Response.Redirect("home.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.isi_data()
        Me.isi_level()
        Me.isi_pre()
        Me.isi_materi()
        Me.isi_soal()
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
        strg = "select *,(select count(id_sertif) from el_tbl_sertifikat where id_mquiz = el_tbl_manajemen_quiz.id_mquiz) as sertif FROM el_tbl_manajemen_quiz where id_dept = '" & Me.idrec & "' order by id_mquiz DESC"
        Me.salah = Mod_Utama.isi_data(dt, strg, "id_mquiz", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_mquiz"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
        Me.su57.Visible = False

    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_app2"
                If dr("app_sta") = False Then
                    str = "update el_tbl_manajemen_quiz set "
                    str = str & "app_sta = 1 "
                    str = str & "where id_mquiz = " & dr("id_mquiz") & " "
                Else
                    str = "update el_tbl_manajemen_quiz set "
                    str = str & "app_sta = 0 "
                    str = str & "where id_mquiz = " & dr("id_mquiz") & " "
                End If
                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select


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

        Select Case e.ButtonID
            Case "bt_app2"
                If dr("app_sta") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If
        End Select

    End Sub


    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO el_tbl_manajemen_quiz ("
        str = str & "id_mquiz, id_soal, id_materi, pre_test, tgl_akses, tgl_akhir, judul, "
        str = str & "level, id_dept, c_date, c_user) VALUES ("
        str = str & "(select isnull(max(id_mquiz),0)+1 from el_tbl_manajemen_quiz),"
        str = str & "'" & e.NewValues("id_soal") & "', "
        str = str & "'" & e.NewValues("id_materi") & "', "
        str = str & "'" & e.NewValues("pre_test") & "', "
        str = str & "'" & e.NewValues("tgl_akses") & "', "
        str = str & "'" & e.NewValues("tgl_akhir") & "', "
        str = str & "'" & e.NewValues("judul") & "', "
        str = str & "'" & e.NewValues("level") & "', "
        str = str & "'" & Me.idrec & "', "
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
            salah.er_menu = "Proses Insert Records pada Menu Manajemen Karyawan"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If

    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE el_tbl_manajemen_quiz set "
        str = str & "id_soal = '" & e.NewValues("id_soal") & "', "
        str = str & "id_materi = '" & e.NewValues("id_materi") & "', "
        str = str & "pre_test = '" & e.NewValues("pre_test") & "', "
        str = str & "tgl_akses = '" & e.NewValues("tgl_akses") & "', "
        str = str & "tgl_akhir = '" & e.NewValues("tgl_akhir") & "', "
        str = str & "judul = '" & e.NewValues("judul") & "', "
        str = str & "level = '" & e.NewValues("level") & "', "
        str = str & "c_date = getdate(), "
        str = str & "c_user = '" & dr_user("nama") & "' "
        str = str & "where id_mquiz = " & e.Keys("id_mquiz")
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

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Select Case e.Column.FieldName
            Case "level"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_level
                cb.ValueField = "id"
                cb.TextField = "nama_level"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "pre_test"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_pre
                cb.ValueField = "id_paket"
                cb.TextField = "judul_training"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "id_materi"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_materi
                cb.ValueField = "id_materi"
                cb.TextField = "judul_materi"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
            Case "id_soal"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_soal
                cb.ValueField = "id_paket"
                cb.TextField = "judul_training"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select
    End Sub

    Private Sub isi_level()
        str = "select * from el_tbl_level "

        Me.salah = Mod_Utama.isi_data(dt_level, str, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If
        For Each row As DataRow In dt_level.Rows
            Dim item As New ListItem()
            item.Text = row("nama_level").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item
            item.Value = row("id").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmb_level.Items.Add(item)
        Next
        For Each row As DataRow In dt_level.Rows
            Dim item As New ListItem()
            item.Text = row("nama_level").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item
            item.Value = row("id").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmbe_lvl.Items.Add(item)
        Next

        cb = Me.ASPxGridView1.Columns("level")
        cb.PropertiesComboBox.DataSource = Me.dt_level
        cb.PropertiesComboBox.ValueField = "id"
        cb.PropertiesComboBox.TextField = "nama_level"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Protected Sub cmb_level_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub bt_img_ServerClick(sender As Object, e As EventArgs)
        If rbOptions.SelectedValue = "1" Then
            str = "INSERT INTO el_tbl_manajemen_quiz ("
            str = str & "id_mquiz, id_soal, id_materi, pre_test, tgl_akses, tgl_akhir, judul,custom,id_user, "
            str = str & "level, id_dept, c_date, c_user) VALUES ("
            str = str & "(select isnull(max(id_mquiz),0)+1 from el_tbl_manajemen_quiz),"
            str = str & "'" & Me.cmb_post.SelectedValue & "', "
            str = str & "'" & Me.cmb_materi.SelectedValue & "', "
            str = str & "'" & Me.cmb_pre.SelectedValue & "', "
            str = str & "'" & Me.txakses.Text & "', "
            str = str & "'" & Me.txakhir.Text & "', "
            str = str & "'" & Me.tx_judul.Value & "', "
            str = str & "'" & Me.rbOptions.SelectedValue & "', "
            str = str & "'', "
            str = str & "'" & Me.cmb_level.SelectedValue & "', "
            str = str & "'" & Me.idrec & "', "
            str = str & "getdate(), "
            str = str & "'" & dr_user("nama") & "') "
            salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
            If salah.er_hasil = "" Then
                Mod_Utama.tampil_sukses(Me, "Berhasil Menambahkan Data Baru")
            Else
                Mod_Utama.tampil_error(Me, "Terjadi Kesalahan Pada Query Silahkan Hubungin MIS")

            End If
        Else
            Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
            conn.Open()
            Dim dt6, dt7 As New DataTable
            str = "INSERT INTO el_tbl_manajemen_quiz (id_mquiz, id_soal, id_materi, pre_test, tgl_akses, tgl_akhir, judul, custom, id_user, level, id_dept, c_date, c_user) VALUES ((select isnull(max(id_mquiz),0)+1 from el_tbl_manajemen_quiz), @id_soal, @id_materi, @pre_test, @tgl_akses, @tgl_akhir, @judul, @custom, @id_user, @level, @id_dept, getdate(), @c_user)"
            Dim cmd As New SqlCommand(str, conn)
            cmd.Parameters.AddWithValue("@id_soal", Me.cmb_post.SelectedValue)
            cmd.Parameters.AddWithValue("@id_materi", Me.cmb_materi.SelectedValue)
            cmd.Parameters.AddWithValue("@pre_test", Me.cmb_pre.SelectedValue)
            cmd.Parameters.AddWithValue("@tgl_akses", Me.txakses.Text)
            cmd.Parameters.AddWithValue("@tgl_akhir", Me.txakhir.Text)
            cmd.Parameters.AddWithValue("@judul", Me.tx_judul.Value)
            cmd.Parameters.AddWithValue("@custom", Me.cmb_level.SelectedValue)
            cmd.Parameters.AddWithValue("@level", Me.rbOptions.SelectedValue)

            If String.IsNullOrEmpty(Request.Form("checkbox1")) Then
                cmd.Parameters.AddWithValue("@id_user", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@id_user", Request.Form("checkbox1"))
            End If

            cmd.Parameters.AddWithValue("@id_dept", Me.idrec)
            cmd.Parameters.AddWithValue("@c_user", dr_user("nama"))

            ' Eksekusi pernyataan SQL
            Try
                cmd.ExecuteNonQuery()
                Mod_Utama.tampil_sukses(Me, "sukses")
            Catch ex As Exception
                Mod_Utama.tampil_error(Me, "gagal")
            Finally
                conn.Close()
            End Try
        End If

    End Sub

    Protected Sub btncopy_Click(sender As Object, e As EventArgs)
        Dim dtquiz As New DataTable
        Dim button As ASPxButton = TryCast(sender, ASPxButton)
        Dim container As GridViewDataItemTemplateContainer = TryCast(button.NamingContainer, GridViewDataItemTemplateContainer)

        ' Mendapatkan nilai id dari baris yang memicu event btncopy_Click
        Dim id As Object = container.KeyValue
        Me.lblid.InnerText = id
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenModalScript", "openModal();", True)

        strg = "select * from el_tbl_manajemen_quiz where id_mquiz = '" & id & "'"
        Me.salah = Mod_Utama.isi_data(dtquiz, strg, "id_mquiz", waktu_query)

        For Each dtq As DataRow In dtquiz.Rows
            Me.txejdul.Value = dtq("judul")
            Me.lbakses.InnerText = dtq("tgl_akses")
            Me.lbakhir.InnerText = dtq("tgl_akhir")
            Me.cmbe_pre.SelectedValue = dtq("pre_test")
            Me.cmbe_materi.SelectedValue = dtq("id_materi")
            Me.cmbe_post.SelectedValue = dtq("id_soal")
            Me.cmbe_lvl.SelectedValue = dtq("level")
            Me.rbeOptions.SelectedValue = dtq("custom")
            If rbeOptions.SelectedValue = "1" Then
                For Each dtr As DataRow In dt.Rows
                    strg2 = ""
                    Me.su37.Visible = False
                Next
            ElseIf rbeOptions.SelectedValue = "2" Then
                Me.su37.Visible = True
                str = "select * from tbl_login where departement = '" & Me.idrec & "' and level = '" & Me.cmbe_lvl.SelectedValue & "' "
                Me.salah = Mod_Utama.isi_data(dt_check, str, "id", waktu_query)
                For Each dtr As DataRow In dt_check.Rows
                    strg2 = strg2 & "<div class='form-check'>"
                    strg2 = strg2 & "<input class='form-check-input' type='checkbox' id='checkbox2' name='checkbox2' value='" & dtr("id") & "' "
                    If dtq("id_user").ToString().Split(","c).Contains(dtr("id").ToString()) Then
                        strg2 = strg2 & " checked "
                    End If
                    strg2 = strg2 & "/>"
                    strg2 = strg2 & "<label class='form-check-label' for='checkbox2'>"
                    strg2 = strg2 & dtr("nama")
                    strg2 = strg2 & "</label>"
                    strg2 = strg2 & "</div>"
                Next
            End If

            Me.su37.InnerHtml = strg2
        Next

    End Sub

    Protected Sub rbeOptions_SelectedIndexChanged(sender As Object, e As EventArgs)
        If rbOptions.SelectedValue = "1" Then
            For Each dtr As DataRow In dt.Rows
                strg2 = ""
                Me.su37.Visible = False
            Next
        ElseIf rbOptions.SelectedValue = "2" Then
            Me.su37.Visible = True
            str = "select * from tbl_login where departement = '" & Me.idrec & "' and level = '" & Me.cmb_level.SelectedValue & "' "
            Me.salah = Mod_Utama.isi_data(dt_check, str, "id", waktu_query)
            For Each dtr As DataRow In dt_check.Rows
                strg2 = strg2 & "<div class='form-check'>"
                strg2 = strg2 & "<input class='form-check-input' type='checkbox' id='checkbox2' name='checkbox2' value='" & dtr("id") & "'/>"
                strg2 = strg2 & "<label class='form-check-label' for='checkbox2'>"
                strg2 = strg2 & dtr("nama")
                strg2 = strg2 & "</label>"
                strg2 = strg2 & "</div>"
            Next
        End If

        Me.su37.InnerHtml = strg2
    End Sub

    Protected Sub btnsave_ServerClick(sender As Object, e As EventArgs)
        If rbeOptions.SelectedValue = "1" Then
            str = "UPDATE el_tbl_manajemen_quiz SET "
            str = str & "id_soal = '" & Me.cmbe_post.SelectedValue & "', "
            str = str & "id_materi = '" & Me.cmbe_materi.SelectedValue & "', "
            str = str & "pre_test = '" & Me.cmbe_pre.SelectedValue & "', "
            If Me.txe_akses.Text = "" Then
                str = str & "tgl_akses = '" & lbakses.InnerText & "',"
            Else
                str = str & "tgl_akses = '" & Me.txe_akses.Text & "', "
            End If

            If Me.txe_akhir.Text = "" Then
                str = str & "tgl_akhir = '" & lbakhir.InnerText & "',"
            Else
                str = str & "tgl_akhir = '" & Me.txe_akhir.Text & "', "
            End If
            str = str & "judul = '" & Me.txejdul.Value & "', "
            str = str & "custom = '" & Me.rbeOptions.SelectedValue & "', "
            str = str & "level = '" & Me.cmbe_lvl.SelectedValue & "', "
            str = str & "c_date = getdate(), "
            str = str & "c_user = '" & dr_user("nama") & "' "
            str = str & "WHERE id_mquiz = " & lblid.InnerText

            salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")

            If salah.er_hasil = "" Then
                Mod_Utama.tampil_sukses(Me, "Berhasil Mengedit Data")
            Else
                Mod_Utama.tampil_error(Me, "Terjadi Kesalahan Pada Query, Silahkan Hubungi MIS")
            End If
        Else
            Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
            conn.Open()

            str = "UPDATE el_tbl_manajemen_quiz SET "
            str = str & "id_soal = @id_soal, "
            str = str & "id_materi = @id_materi, "
            str = str & "pre_test = @pre_test, "
            If Me.txe_akses.Text = "" Then
                str = str & "tgl_akses = '" & lbakses.InnerText & "',"
            Else
                str = str & "tgl_akses = '" & Me.txe_akses.Text & "', "
            End If

            If Me.txe_akhir.Text = "" Then
                str = str & "tgl_akhir = '" & lbakhir.InnerText & "',"
            Else
                str = str & "tgl_akhir = '" & Me.txe_akhir.Text & "', "
            End If
            str = str & "judul = @judul, "
            str = str & "custom = @custom, "
            str = str & "id_user = @id_user, "
            str = str & "level = @level, "
            str = str & "c_date = getdate(), "
            str = str & "c_user = @c_user "
            str = str & "WHERE id_mquiz = '" & lblid.InnerText & "'"

            Dim cmd As New SqlCommand(str, conn)
            cmd.Parameters.AddWithValue("@id_soal", Me.cmbe_post.SelectedValue)
            cmd.Parameters.AddWithValue("@id_materi", Me.cmbe_materi.SelectedValue)
            cmd.Parameters.AddWithValue("@pre_test", Me.cmbe_pre.SelectedValue)
            cmd.Parameters.AddWithValue("@tgl_akses", Me.txe_akses.Text)
            cmd.Parameters.AddWithValue("@tgl_akhir", Me.txe_akhir.Text)
            cmd.Parameters.AddWithValue("@level", Me.cmbe_lvl.SelectedValue)
            cmd.Parameters.AddWithValue("@judul", Me.txejdul.Value)
            cmd.Parameters.AddWithValue("@custom", Me.rbeOptions.SelectedValue)

            If String.IsNullOrEmpty(Request.Form("checkbox2")) Then
                cmd.Parameters.AddWithValue("@id_user", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@id_user", Request.Form("checkbox2"))
            End If

            cmd.Parameters.AddWithValue("@id_dept", Me.idrec)
            cmd.Parameters.AddWithValue("@c_user", dr_user("nama"))

            ' Execute the SQL statement

            Try
                cmd.ExecuteNonQuery()
                Mod_Utama.tampil_sukses(Me, "Berhasil Mengedit Data")
            Catch ex As Exception
                Mod_Utama.tampil_error(Me, "Terjadi Kesalahan, Gagal Mengedit Data")
            Finally
                conn.Close()
            End Try

        End If
        Response.Redirect(Request.RawUrl)
    End Sub

    Private Sub isi_pre()
        str = "select * from el_tbl_paket where jenis_test = 'PRE TEST' and id_dept = '" & Me.idrec & "' order by id_paket DESC"

        Me.salah = Mod_Utama.isi_data(dt_pre, str, "id_paket", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each row As DataRow In dt_pre.Rows
            Dim item As New ListItem()
            item.Text = String.Concat("Nama Paket: ", row("paket_soal").ToString(), ", Judul Training: ", row("judul_training").ToString())
            item.Value = row("id_paket").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmb_pre.Items.Add(item)
        Next
        For Each row As DataRow In dt_pre.Rows
            Dim item As New ListItem()
            item.Text = String.Concat("Nama Paket: ", row("paket_soal").ToString(), ", Judul Training: ", row("judul_training").ToString())
            item.Value = row("id_paket").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmbe_pre.Items.Add(item)
        Next

        cb = Me.ASPxGridView1.Columns("pre_test")
        cb.PropertiesComboBox.DataSource = Me.dt_pre
        cb.PropertiesComboBox.ValueField = "id_paket"
        cb.PropertiesComboBox.TextField = "judul_training"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub



    Private Sub isi_materi()
        str = "select * from el_tbl_materi where id_dept = '" & Me.idrec & "' order by id_materi DESC"

        Me.salah = Mod_Utama.isi_data(dt_materi, str, "id_materi", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If
        For Each row As DataRow In dt_materi.Rows
            Dim item As New ListItem()
            item.Text = row("judul_materi").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item
            item.Value = row("id_materi").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmb_materi.Items.Add(item)
        Next
        For Each row As DataRow In dt_materi.Rows
            Dim item As New ListItem()
            item.Text = row("judul_materi").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item
            item.Value = row("id_materi").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmbe_materi.Items.Add(item)
        Next
        cb = Me.ASPxGridView1.Columns("id_materi")
        cb.PropertiesComboBox.DataSource = Me.dt_materi
        cb.PropertiesComboBox.ValueField = "id_materi"
        cb.PropertiesComboBox.TextField = "judul_materi"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_soal()
        str = "select * from el_tbl_paket where jenis_test = 'POST TEST' and id_dept = '" & Me.idrec & "' order by id_paket DESC"

        Me.salah = Mod_Utama.isi_data(dt_soal, str, "id_paket", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each row As DataRow In dt_soal.Rows
            Dim item As New ListItem()
            item.Text = String.Concat("Nama Paket: ", row("paket_soal").ToString(), ", Judul Training: ", row("judul_training").ToString())
            item.Value = row("id_paket").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmb_post.Items.Add(item)
        Next
        For Each row As DataRow In dt_soal.Rows
            Dim item As New ListItem()
            item.Text = String.Concat("Nama Paket: ", row("paket_soal").ToString(), ", Judul Training: ", row("judul_training").ToString())
            item.Value = row("id_paket").ToString() ' Ganti dengan nilai yang ingin digunakan sebagai nilai item

            cmbe_post.Items.Add(item)
        Next

        cb = Me.ASPxGridView1.Columns("id_soal")
        cb.PropertiesComboBox.DataSource = Me.dt_soal
        cb.PropertiesComboBox.ValueField = "id_paket"
        cb.PropertiesComboBox.TextField = "judul_training"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From el_tbl_manajemen_quiz where id_mquiz = " & e.Keys("id_mquiz"), "el_tbl_manajemen_quiz", dr_user)
        str = "DELETE el_tbl_manajemen_quiz "
        str = str & "where id_mquiz = " & e.Keys("id_mquiz")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Delete Records pada Menu Manajemen Karyawan"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub
    Protected Sub rbOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If rbOptions.SelectedValue = "1" Then
            For Each dtr As DataRow In dt.Rows
                strg2 = ""
                Me.su57.Visible = False
            Next
        ElseIf rbOptions.SelectedValue = "2" Then
            Me.su57.Visible = True
            str = "select * from tbl_login where departement = '" & Me.idrec & "' and level = '" & Me.cmb_level.SelectedValue & "' "
            Me.salah = Mod_Utama.isi_data(dt_check, str, "id", waktu_query)
            For Each dtr As DataRow In dt_check.Rows
                strg2 = strg2 & "<div class='form-check'>"
                strg2 = strg2 & "<input class='form-check-input' type='checkbox' id='checkbox1' name='checkbox1' value='" & dtr("id") & "'/>"
                strg2 = strg2 & "<label class='form-check-label' for='checkbox1'>"
                strg2 = strg2 & dtr("nama")
                strg2 = strg2 & "</label>"
                strg2 = strg2 & "</div>"
            Next
        End If

        Me.su57.InnerHtml = strg2
    End Sub






End Class