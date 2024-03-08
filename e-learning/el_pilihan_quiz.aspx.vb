Imports System.Data.SqlClient
Public Class el_pilihan_quiz
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim strg, str As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dtj, dtps As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()
        End If
    End Sub

    Protected Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
            Dim lblpre As Label = CType(e.Item.FindControl("lblpre"), Label)
            Dim su25 As HtmlGenericControl = CType(e.Item.FindControl("su25"), HtmlGenericControl)
            Dim su35 As HtmlGenericControl = CType(e.Item.FindControl("su35"), HtmlGenericControl)
            ' Mengecek nilai label


            If lblpre.Text = "0" Then
                su25.Visible = True ' Menonaktifkan button
                su35.Visible = False
            Else
                su25.Visible = False ' Menonaktifkan button
                su35.Visible = True
            End If


        End If
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


    Private Sub el_pilihan_quiz_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.app()
    End Sub

    Private Sub BindRepeater()

        strg = "SELECT *, (SELECT COUNT(id_hasil) FROM el_tbl_hasil WHERE el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz AND el_tbl_hasil.id_pre = el_tbl_manajemen_quiz.pre_test AND el_tbl_hasil.id_user = '" & dr_user("id") & "') AS pre "
        strg = strg & "FROM el_tbl_paket INNER JOIN el_tbl_manajemen_quiz ON el_tbl_paket.id_paket = el_tbl_manajemen_quiz.pre_test "
        strg = strg & "WHERE ((el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' OR el_tbl_manajemen_quiz.id_dept = '1') AND (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' OR el_tbl_manajemen_quiz.[level] = '1') AND el_tbl_manajemen_quiz.app = 1 and el_tbl_manajemen_quiz.custom = '1') OR (el_tbl_manajemen_quiz.custom = '2' and (el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' OR el_tbl_manajemen_quiz.id_dept = '1') AND (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' OR el_tbl_manajemen_quiz.[level] = '1') AND el_tbl_manajemen_quiz.app = 1 and',' + el_tbl_manajemen_quiz.id_user + ',' LIKE '%,' + CAST('" & dr_user("id") & "' AS NVARCHAR(MAX)) + ',%')"

        Dim conn1 As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd1 As SqlCommand = New SqlCommand(strg, conn1)
        Dim sda1 As SqlDataAdapter = New SqlDataAdapter(cmd1)

        Dim dt1 As DataTable = New DataTable()

        sda1.Fill(dt1)

        Repeater1.DataSource = dt1
        Repeater1.DataBind()







    End Sub


End Class