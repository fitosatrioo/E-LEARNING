Imports System.Data.SqlClient
Public Class el_pilihan_materi2
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
        'Me.isi_data()
    End Sub
    Private Sub el_pilihan_materi2_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")
        Me.app()
    End Sub

    Protected Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
            Dim lblpre As Label = CType(e.Item.FindControl("lblpre"), Label)
            Dim lblmpre As Label = CType(e.Item.FindControl("lblmpre"), Label)
            Dim su25 As HtmlGenericControl = CType(e.Item.FindControl("su25"), HtmlGenericControl)
            Dim su37 As HtmlGenericControl = CType(e.Item.FindControl("su37"), HtmlGenericControl)
            ' Mengecek nilai label

            If lblmpre.Text = "" Then
                su25.Visible = True
                su37.Visible = False
            Else
                If lblpre.Text = "0" Then
                    su25.Visible = False ' Menonaktifkan button
                    su37.Visible = True
                Else
                    su25.Visible = True ' Menonaktifkan button
                    su37.Visible = False
                End If
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



    Private Sub BindRepeater()
        'strg = "SELECT *,(select count(id_nilai) as pre from el_tbl_nilai where id_user = '" & dr_user("id") & "' and el_tbl_nilai.id_soal = el_tbl_manajemen_quiz.pre_test) as pre FROM el_tbl_manajemen_quiz where id_dept = '" & dr_user("departement") & "' and level = '" & dr_user("level") & "' and app = 1"
        'strg = "SELECT * FROM el_tbl_manajemen_quiz where id_dept = '" & dr_user("departement") & "' and level = '" & dr_user("level") & "' and app = 1"


        strg = "SELECT *,(select count(id_hasil) from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_pre = el_tbl_manajemen_quiz.pre_test and el_tbl_hasil.id_user =  '" & dr_user("id") & "' ) as pre,"
        strg = strg & "(select count(id_hasil) from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_pre = el_tbl_manajemen_quiz.id_soal and el_tbl_hasil.id_user =  '" & dr_user("id") & "' ) as post "
        strg = strg & "FROM el_tbl_materi inner join el_tbl_manajemen_quiz on el_tbl_materi.id_materi = el_tbl_manajemen_quiz.id_materi  where (el_tbl_manajemen_quiz.app = 1 and (el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' or el_tbl_manajemen_quiz.id_dept='1') and (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' or el_tbl_manajemen_quiz.[level] = '1' ) and el_tbl_manajemen_quiz.custom = '1') OR (el_tbl_manajemen_quiz.app = 1 and (el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' or el_tbl_manajemen_quiz.id_dept='1') and (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' or el_tbl_manajemen_quiz.[level] = '1') and el_tbl_manajemen_quiz.custom = '2' and ',' + el_tbl_manajemen_quiz.id_user + ',' LIKE '%,' + CAST('" & dr_user("id") & "' AS NVARCHAR(MAX)) + ',%')  "

        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        Repeater1.DataSource = dt1
        Repeater1.DataBind()







    End Sub


End Class