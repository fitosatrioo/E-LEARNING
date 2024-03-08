Imports System.Data.SqlClient
Public Class el_pilihan_nilai
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str, strg As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dtj As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()
        End If
    End Sub

    Private Sub el_pilihan_nilai_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.app()
    End Sub
    Private Sub app()
        Dim dtp As New DataTable
        str = "select * from el_tbl_manajemen_quiz where app_sta = 1"
        Me.salah = Mod_Utama.isi_data(dtp, str, "id_mquiz", waktu_query)
        For Each dtr As DataRow In dt.Rows
            str = "UPDATE el_tbl_manajemen_quiz SET app = 1 WHERE GETDATE() BETWEEN tgl_akses AND tgl_akhir AND id_mquiz = '" & dtr("id_mquiz") & "'"
            Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
            str = "UPDATE el_tbl_manajemen_quiz SET app = 0 WHERE GETDATE() NOT BETWEEN tgl_akses AND tgl_akhir AND id_mquiz = '" & dtr("id_mquiz") & "'"
            Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        Next
    End Sub

    Private Sub BindRepeater()
        'Dim id_dept As String = Session("id_user")
        Dim id_dept As String = "1"
        strg = "SELECT *,"
        strg = strg & "(select c_date from el_tbl_log where el_tbl_log.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_log.id_history = el_tbl_manajemen_quiz.pre_test and el_tbl_log.c_user = '" & dr_user("id") & "' and el_tbl_log.sumber = 'Test') as pre,"
        strg = strg & "(select c_date from el_tbl_log where el_tbl_log.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_log.id_history = el_tbl_manajemen_quiz.id_soal and el_tbl_log.c_user = '" & dr_user("id") & "' and el_tbl_log.sumber = 'Test') as post,"
        strg = strg & "(select app from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_user = '" & dr_user("id") & "') as acc "
        strg = strg & "FROM el_tbl_manajemen_quiz "
        strg = strg & "where ((id_dept = '" & dr_user("departement") & "' or id_dept = '1') and (level = '" & dr_user("level") & "' or level = '1') and app='1' and custom = '1') or "
        strg = strg & "((id_dept = '" & dr_user("departement") & "' or id_dept = '1') and (level = '" & dr_user("level") & "' or level = '1') and app='1' and custom = '1' and ',' + el_tbl_manajemen_quiz.id_user + ',' LIKE '%,' + CAST('" & dr_user("id") & "' AS NVARCHAR(MAX)) + ',%')"

        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        Repeater3.DataSource = dt1
        Repeater3.DataBind()
    End Sub

    Protected Sub Repeater3_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
            Dim pre As Label = CType(e.Item.FindControl("pre"), Label)
            Dim post As Label = CType(e.Item.FindControl("post"), Label)
            Dim blm_pre As Label = CType(e.Item.FindControl("blm_pre"), Label)
            Dim blm_post As Label = CType(e.Item.FindControl("blm_post"), Label)
            Dim acc As Label = CType(e.Item.FindControl("acc"), Label)
            Dim blm_acc As Label = CType(e.Item.FindControl("blm_acc"), Label)
            Dim tdk_pre As Label = CType(e.Item.FindControl("tdk_pre"), Label)
            Dim tdk_pre2 As Label = CType(e.Item.FindControl("tdk_pre2"), Label)
            Dim btn_nilai As HtmlContainerControl = CType(e.Item.FindControl("btn_nilai"), HtmlGenericControl)
            If tdk_pre.Text = "" Then
                tdk_pre2.Visible = True
                tdk_pre.Visible = False
                blm_pre.Visible = False
                pre.Visible = False
            ElseIf pre.Text = "" Then
                pre.Visible = False
                tdk_pre2.Visible = False
                blm_pre.Visible = True
                tdk_pre.Visible = False
            Else
                pre.Visible = True
                tdk_pre2.Visible = False
                blm_pre.Visible = False
                tdk_pre.Visible = False
            End If

            If post.Text = "" Then
                blm_post.Visible = True
                post.Visible = False
            Else
                blm_post.Visible = False
                post.Visible = True
            End If

            If acc.Text = "0" Then
                blm_acc.Visible = True
                acc.Visible = False
                btn_nilai.Visible = False
            Else
                blm_acc.Visible = False
                acc.Visible = False
                btn_nilai.Visible = True
            End If





        End If
    End Sub
End Class