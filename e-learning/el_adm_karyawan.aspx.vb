Imports System.Reflection.MethodBase
Imports DevExpress.Web

Public Class adm_elarning
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
    Dim dt_level, dt_dept As New DataTable
    Dim cb As GridViewDataComboBoxColumn

    Private Sub adm_elarning_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub adm_elarning_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
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

    Private Sub Isi_filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub adm_elarning_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim ucMenu As New HtmlGenericControl("uc_menu")
        ucMenu.Attributes("class") = "active"

        Dim menuContainer As HtmlGenericControl = CType(FindControl("karyawan"), HtmlGenericControl)
        If menuContainer IsNot Nothing Then
            menuContainer.Controls.Add(ucMenu)
        End If

        dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        Me.Isi_filter()
        Me.isi_data()
        Me.isi_level()
        Me.isi_dept()
    End Sub

    Private Sub isi_data()
        strg = "SELECT *,(select dept_name from tbl_departement where id_dept = tbl_login.departement) as dept FROM tbl_login order by id DESC"
        Me.salah = Mod_Utama.isi_data(dt, strg, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        If e.NewValues("email").contains("@gmail.com") Then
            str = "INSERT INTO tbl_login ("
            str = str & "id,nik, nama, username, email,jabatan,password, "
            str = str & "no_telp,departement,level) VALUES ("
            str = str & "(select isnull(max(id),0)+1 from tbl_login),"
            str = str & "'" & e.NewValues("nik") & "', "
            str = str & "'" & e.NewValues("nama") & "', "
            str = str & "'" & e.NewValues("username") & "', "
            str = str & "'" & e.NewValues("email") & "', "
            str = str & "'" & e.NewValues("jabatan") & "', "
            str = str & "'" & e.NewValues("password") & "', "
            str = str & "'" & e.NewValues("no_telp") & "', "
            str = str & "'" & e.NewValues("departement") & "', "
            str = str & "'" & e.NewValues("level") & "') "
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
        Else
            ASPxGridView1.JSProperties("cpShowError") = "Email tidak valid."
        End If


    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE tbl_login set "
        str = str & "nik = '" & e.NewValues("nik") & "', "
        str = str & "nama = '" & e.NewValues("nama") & "', "
        str = str & "username = '" & e.NewValues("username") & "', "
        str = str & "email = '" & e.NewValues("email") & "', "
        str = str & "jabatan = '" & e.NewValues("jabatan") & "', "
        str = str & "no_telp = '" & e.NewValues("no_telp") & "', "
        str = str & "password = '" & e.NewValues("password") & "', "
        str = str & "departement = '" & e.NewValues("departement") & "', "
        str = str & "level = '" & e.NewValues("level") & "' "
        str = str & "where id = " & e.Keys("id")
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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From tbl_login where id = " & e.Keys("id"), "tbl_login", dr_user)
        str = "DELETE tbl_login "
        str = str & "where id = " & e.Keys("id")

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

    Private Sub isi_level()
        str = "select * from el_tbl_level"

        Me.salah = Mod_Utama.isi_data(dt_level, str, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        cb = Me.ASPxGridView1.Columns("level")
        cb.PropertiesComboBox.DataSource = Me.dt_level
        cb.PropertiesComboBox.ValueField = "id"
        cb.PropertiesComboBox.TextField = "nama_level"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_dept()
        str = "select * from tbl_departement"

        Me.salah = Mod_Utama.isi_data(dt_dept, str, "id_dept", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        cb = Me.ASPxGridView1.Columns("departement")
        cb.PropertiesComboBox.DataSource = Me.dt_dept
        cb.PropertiesComboBox.ValueField = "id_dept"
        cb.PropertiesComboBox.TextField = "dept_name"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
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
            Case "departement"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_dept
                cb.ValueField = "id_dept"
                cb.TextField = "dept_name"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()


        End Select
    End Sub
End Class