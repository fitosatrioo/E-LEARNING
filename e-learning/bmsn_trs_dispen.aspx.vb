Imports System.Reflection.MethodBase
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class bmsn_trs_dispen
    Inherits System.Web.UI.Page
    Dim str As String
    Dim salah As er_custom
    Dim dr_user As DataRow
    Dim dt As New DataTable
    Dim dt_detail As New DataTable
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dr As DataRow
    Dim dr_rekap As DataRow
    Dim cb As GridViewDataComboBoxColumn
    Dim str_menu As String = ",2010,"

    Dim dt_user As New DataTable
    Dim dt_section As New DataTable
    Dim dt_dept As New DataTable
    Dim iddispen As Int64
    Private grid_detail As ASPxGridView

    Private Sub page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        mod_utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub Jika_Error(er_str As String, er_hasil As String, er_menu As String, nopesan As Integer)
        salah.er_str = er_str
        salah.er_menu = er_menu
        salah.er_waktu = mod_utama.str_waktu(Me.waktu_query, Me.waktu_page)
        Session("error") = salah

        CType(Me.Master, Site1).show_pesan("Terdapat Kesalahan Page Master User", er_str, er_menu)
    End Sub

    Private Sub ASPxGridView1_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles ASPxGridView1.CustomErrorText
        e.ErrorText = salah.er_hasil
    End Sub

    Private Sub Isi_Filter()
        uc_header.filter_cb3.Items.Clear()
        uc_header.filter_cb3.Items.Add("UPDATED")
        'VALUE
        uc_header.filter_cb3.Items(0).Value = "u_date"

    End Sub

    Private Sub bmsn_trs_dispen_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.dr_user = Session("dr_user")
        Me.uc_header.grid = Me.ASPxGridView1

        str = "<li><a href='home.aspx'>Home</a></li>"
        str = str & "<li class='active'>Transactions</li>"
        str = str & "<li class='active'><a href='bmsn_trs_dispen.aspx'>Pengajuan Dispensasi</a></li>"

        Me.uc_header.list_menu.InnerHtml = str

        Me.Isi_Filter()
        Me.isi_data()
        Me.isi_user()
        Me.isi_section()
        Me.isi_dept()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub isi_user()
        str = "Select * from mst_user order by nama asc"
        salah = mod_utama.isi_data(Me.dt_user, str, "id_user", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("pemohon")
        cb.PropertiesComboBox.DataSource = Me.dt_user
        cb.PropertiesComboBox.ValueField = "id_user"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_section()
        str = "select * from mst_section where id_dept in ('13','14','16') order by nama asc"
        salah = mod_utama.isi_data(Me.dt_section, str, "id_section", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_section")
        cb.PropertiesComboBox.DataSource = Me.dt_section
        cb.PropertiesComboBox.ValueField = "id_section"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains

    End Sub

    Private Sub isi_dept()
        str = "select * from mst_dept where id_dept is not null and id_dept in ('13','14','16') "
        salah = mod_utama.isi_data(Me.dt_dept, str, "id_dept", Me.waktu_query)
        If salah.er_hasil <> "" Then Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)

        cb = Me.ASPxGridView1.Columns("id_dept")
        cb.PropertiesComboBox.DataSource = Me.dt_dept
        cb.PropertiesComboBox.ValueField = "id_dept"
        cb.PropertiesComboBox.TextField = "nama"
        cb.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains
    End Sub

    Private Sub isi_data()
        Dim fi_lim As uc_header.filter_limit = uc_header.filtertext

        str = "with contoh as( "
        str = str & "select " & fi_lim.str_limit & " *, "
        str = str & "isnull((select count(id_files) from arap_log_files where id_sumber = A.id_dispen and sumber = 'DBSO'),0) as jml_files, "
        str = str & "(select count(id_dispen_dtl) from bmsn_trs_dispen_dtl where id_dispen = A.id_dispen) as jml, "
        str = str & "(select DATEADD(day, 3, fin_date)) as tanggal_minus, "
        str = str & "(select COUNT(id_libur) FROM mst_libur_new WHERE tot_hari <> 0 "
        str = str & "and tgl_awal BETWEEN (SELECT fin_date FROM bmsn_trs_dispen WHERE id_dispen = A.id_dispen) "
        str = str & "AND DATEADD(day, 3, fin_date)) as count_libur, "
        str = str & "(DATEDIFF(Day, 0, DATEADD(day, 3, fin_date))/7 - DATEDIFF(Day, 0, fin_date)/7) as count_minggu "
        str = str & "from bmsn_trs_dispen A where id_dispen Is Not null) "
        str = str & "select *, "
        str = str & "(select DATEADD(day, 3 + count_libur + count_minggu, fin_date)) as tgl_selesai "
        str = str & "from contoh "
        str = str & fi_lim.str_filter
        str = str & "order by id_dispen desc"
        salah = mod_utama.isi_data(Me.dt, str, "id_dispen", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_Error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return
        End If

        Me.dt.DefaultView.RowFilter = Session("rowfilter")
        Me.ASPxGridView1.DataSource = Me.dt
        Me.ASPxGridView1.KeyFieldName = "id_dispen"
        Me.ASPxGridView1.DataBind()
        mod_utama.Atur_Grid(Me.ASPxGridView1, True)
        ASPxGridView1.Settings.VerticalScrollableHeight = 500
        ASPxGridView1.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden

    End Sub

    Private Sub ASPxGridView1_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ASPxGridView1.CellEditorInitialize
        Dim dr As DataRow = CType(sender, ASPxGridView).GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.Column.FieldName
            Case "id_section"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_section
                cb.ValueField = "id_section"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()

            Case "id_dept"
                Dim cb As ASPxComboBox = e.Editor
                cb.DataSource = Me.dt_dept
                cb.ValueField = "id_dept"
                cb.TextField = "nama"
                cb.IncrementalFilteringMode = IncrementalFilteringMode.Contains
                cb.DataBind()
        End Select

    End Sub

    Private Sub ASPxGridView1_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles ASPxGridView1.HtmlDataCellPrepared
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.DataColumn.FieldName
            Case "id_dispen"
                If dr("pemohon") <> dr_user("id_user") Then
                    e.Cell.Enabled = False
                End If
        End Select
    End Sub

    Private Sub ASPxGridView1_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles ASPxGridView1.InitNewRow
        e.NewValues("tgl") = Now.Date
        e.NewValues("pemohon") = dr_user("id_user")
    End Sub

    Private Sub ASPxGridView1_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles ASPxGridView1.CommandButtonInitialize
        Dim dr As DataRow = CType(sender, ASPxGridView).GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonType
            Case ColumnCommandButtonType.New
                If CStr(dr_user("baru")).Contains(str_menu) = False Then e.Visible = False
            Case ColumnCommandButtonType.Edit
                If CStr(dr_user("ubah")).Contains(str_menu) = False Then e.Visible = False
                If dr("staf_sta") = True Then e.Visible = False
                If dr_user("id_user") <> dr("pemohon") Then e.Enabled = False
            Case ColumnCommandButtonType.Delete
                If CStr(dr_user("hapus")).Contains(str_menu) = False Then e.Visible = False
                If dr("staf_sta") = True Then e.Visible = False
                If dr("jml") > 0 Then e.Visible = False
                If dr_user("id_user") <> dr("pemohon") Then e.Enabled = False
        End Select

    End Sub

    Private Sub ASPxGridView1_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ASPxGridView1.CustomCallback
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        If e.Parameters.StartsWith("bt") Then
            Dim isi() As String
            Dim idkey As Integer
            Try
                isi = Split(e.Parameters, ";")
                idkey = Convert.ToInt32(isi(1))
                dr = Me.dt.Rows.Find(idkey)
            Catch ex As Exception
                grid.JSProperties("cpTitle") = "gagal"
                grid.JSProperties("cpContent") = "Gagal mendapat data yang akan diproses"
                Return
            End Try


            Select Case isi(0)
                Case "bt_mgr"
                    If dr("staf_sta") = 0 Then
                        str = "update bmsn_trs_dispen set "
                        str = str & "staf_user = '" & dr_user("nama") & "',"
                        str = str & "staf_sta = 1 ,"
                        str = str & "staf_date= getdate() "
                        str = str & "where id_dispen = " & dr("id_dispen")

                        str = str & "update bmsn_trs_dispen_dtl set "
                        str = str & "parameter= 1 "
                        str = str & "where id_dispen = " & dr("id_dispen")
                    Else
                        str = "update bmsn_trs_dispen set "
                        str = str & "staf_user = '" & dr_user("nama") & "',"
                        str = str & "staf_sta = 0 ,"
                        str = str & "staf_date= getdate() "
                        str = str & "where id_dispen = " & dr("id_dispen")

                        str = str & "update bmsn_trs_dispen_dtl set "
                        str = str & "parameter= 0 "
                        str = str & "where id_dispen = " & dr("id_dispen")
                    End If
                    mod_utama.exec_sql(str, dr_user, "DISPENSASI")


                Case "bt_fin"
                    If dr("fin_sta") = 0 Then
                        str = "update bmsn_trs_dispen set "
                        str = str & "fin_user = '" & dr_user("nama") & "',"
                        str = str & "fin_sta = 1 ,"
                        str = str & "fin_date= getdate() "
                        str = str & "where id_dispen = " & dr("id_dispen")
                    Else
                        str = "update bmsn_trs_dispen set "
                        str = str & "fin_user = '" & dr_user("nama") & "',"
                        str = str & "fin_sta = 0 ,"
                        str = str & "fin_date= getdate() "
                        str = str & "where id_dispen = " & dr("id_dispen")
                    End If
                    mod_utama.exec_sql(str, dr_user, "DISPENSASI")

                    str = "update bmsn_trs_dispen_dtl set "
                    str = str & "tgl_baru = getdate()"
                    str = str & "where id_dispen = " & dr("id_dispen")
                    mod_utama.exec_sql(str, dr_user, "DISPENSASI")

                    Dim tbl_check As New DataTable
                    str = "select * "
                    str = str & "from bmsn_trs_dispen_dtl "
                    str = str & "where id_dispen = " & dr("id_dispen")
                    salah = mod_utama.isi_data(tbl_check, str, "id_dispen_dtl", Me.waktu_query)
                    For Each dtr As DataRow In tbl_check.Rows
                        str = "update bmsn_trs_bso set "
                        str = str & "tgl_exp = '" & dtr("tgl_baru") & "'"
                        str = str & "where id_bso = " & dtr("id_bso")
                        mod_utama.exec_sql(str, dr_user, "DISPENSASI")
                    Next
            End Select

            Me.isi_data()
        End If
    End Sub

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        Dim dr As DataRow = CType(sender, ASPxGridView).GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        If e.CellType = GridViewTableCommandCellType.Filter Then Return
        Select Case e.ButtonID
            Case "bt_mgr"
                If dr("staf_sta") = True Then
                    e.Image.Url = "~/img/yes.png"
                Else
                    e.Image.Url = "~/img/no.png"
                End If
                If dr("jml") = 0 Then e.Enabled = False : e.Image.ToolTip = "Detail Masih Kosong" : 
                If dr("fin_sta") = True Then e.Enabled = False
                If CStr(dr_user("mgr")).Contains(str_menu) = False Then e.Enabled = False

            Case "bt_fin"
                If dr("fin_sta") = True Then
                    e.Image.Url = "~/img/yes.png"
                Else
                    e.Image.Url = "~/img/no.png"
                End If
                If dr("fin_sta") = True Then e.Enabled = False
                If dr("staf_sta") = False Then e.Enabled = False : e.Image.ToolTip = "MGR Belum Approve" : 
                If CStr(dr_user("fin")).Contains(str_menu) = False Then e.Enabled = False

        End Select

        e.Image.Height = 30
        e.Image.Width = 30
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        Dim dt_urut As New DataTable
        Dim dr_urut As DataRow
        str = "SELECT isnull(max(nourut),0) + 1 as urut "
        str = str & "from bmsn_trs_dispen where year(tgl) = year('" & e.NewValues("tgl") & "') "
        Me.salah = mod_utama.isi_data(dt_urut, str, "id_dispen", waktu_query)
        If Me.salah.er_id < 0 Then
            salah.er_hasil = "Terjadi kesalahan pada penentuan No. DISPENSASI"
            Return
        End If

        dr_urut = dt_urut.Rows(0)
        Dim nomemo As String
        nomemo = "ARV/MEMO-" & Format(e.NewValues("tgl"), "yyMM") & Right("000000" & dr_urut("urut"), 6)

        str = "INSERT INTO bmsn_trs_dispen ("
        str = str & "id_dispen, tgl, nourut, no_memo, pemohon, id_section, id_dept, ket, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_dispen),0) + 1 from bmsn_trs_dispen), "
        str = str & "'" & e.NewValues("tgl") & "', "
        str = str & "'" & dr_urut("urut") & "', "
        str = str & "'" & nomemo & "', "
        str = str & "'" & e.NewValues("pemohon") & "', "
        str = str & "'" & e.NewValues("id_section") & "', "
        str = str & "(select id_dept from mst_section where id_section = " & e.NewValues("id_section") & "), "
        str = str & "'" & e.NewValues("ket") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = mod_utama.exec_sql(str, dr_user, "DISPENSASI")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = mod_utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating

        str = "UPDATE bmsn_trs_dispen SET "
        str = str & "id_section = '" & e.NewValues("id_section") & "', "
        str = str & "id_dept = (select id_dept from mst_section where id_section = " & e.NewValues("id_section") & "), "
        str = str & "ket = '" & e.NewValues("ket") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_dispen = " & e.Keys("id_dispen")
        salah.er_hasil = mod_utama.exec_sql(str, dr_user, "DISPENSASI")

        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = mod_utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting

        str = "delete bmsn_trs_dispen "
        str = str & "where id_dispen = " & e.Keys("id_dispen")

        salah.er_hasil = mod_utama.exec_sql(str, dr_user, "DISPENSASI")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
            salah.er_waktu = mod_utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("Error") = salah
        End If
    End Sub

    Protected Sub grid_detail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            iddispen = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception
        End Try

        str = "select *, "
        str = str & "(select no_bso from bmsn_trs_bso where id_bso = A.id_bso) as no_bso "
        str = str & "from bmsn_trs_dispen_dtl A "
        str = str & "where id_dispen = " & iddispen
        mod_utama.isi_data(Me.dt_detail, str, "id_bso_dtl", waktu_query)

        grid_detail = TryCast(sender, ASPxGridView)
        If grid_detail Is Nothing Then Exit Sub

        grid_detail.DataSource = Me.dt_detail
        mod_utama.Atur_Grid(grid_detail)
        grid_detail.Settings.ShowGroupPanel = False
        grid_detail.SettingsSearchPanel.Visible = False
        grid_detail.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        grid_detail.Settings.ShowFilterRow = False
    End Sub

    Protected Function GetTgl(container As GridViewPreviewRowTemplateContainer, fldnm As String, usernm As String) As String
        Dim tgl_str As String = Convert.ToString(container.Grid.GetRowValues(container.VisibleIndex, fldnm))
        Dim tgl_nm As String = Convert.ToString(container.Grid.GetRowValues(container.VisibleIndex, usernm))
        Dim tgl_date As DateTime

        If tgl_str <> "" Then
            tgl_date = tgl_str
            Return Format(tgl_date, "yyyy-MM-dd HH:mm:ss") & " BY " & tgl_nm
        End If

        Return ""
    End Function

End Class