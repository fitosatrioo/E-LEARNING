Imports System.Data.SqlClient
Public Class el_pilihan_post
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

    Private Sub el_pilihan_post_Init(sender As Object, e As EventArgs) Handles Me.Init
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



        strg = "select *,(select count(id_isimt) from el_tbl_isi_materi where id_materi = el_tbl_manajemen_quiz.id_materi) as jumlah_materi, "
        strg = strg & "(select count(id_log) from el_tbl_log where sumber = 'materi' and id_mquiz = el_tbl_manajemen_quiz.id_mquiz and c_user = '" & dr_user("id") & "') as materi_dibaca,"
        strg = strg & "(select count(id_hasil) from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_post = el_tbl_manajemen_quiz.id_soal and el_tbl_hasil.id_user =  '" & dr_user("id") & "') as post,"
        strg = strg & "(select link from el_tbl_link where app = 1) as link "
        strg = strg & "from el_tbl_paket inner join el_tbl_manajemen_quiz on el_tbl_paket.id_paket = el_tbl_manajemen_quiz.id_soal where ((el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' or el_tbl_manajemen_quiz.id_dept = '1' ) and (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' or el_tbl_manajemen_quiz.[level] = '1' ) and el_tbl_manajemen_quiz.app = 1 and el_tbl_manajemen_quiz.custom = '1') or ((el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' or el_tbl_manajemen_quiz.id_dept = '1' ) and (el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' or el_tbl_manajemen_quiz.[level] = '1' ) and el_tbl_manajemen_quiz.app = 1 and el_tbl_manajemen_quiz.custom = '2' and ',' + el_tbl_manajemen_quiz.id_user + ',' LIKE '%,' + CAST('" & dr_user("id") & "' AS NVARCHAR(MAX)) + ',%')"
        Dim conn2 As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd2 As SqlCommand = New SqlCommand(strg, conn2)
        Dim sda2 As SqlDataAdapter = New SqlDataAdapter(cmd2)

        Dim dt2 As DataTable = New DataTable()

        sda2.Fill(dt2)

        Repeater2.DataSource = dt2
        Repeater2.DataBind()



    End Sub
    Protected Sub Repeater2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
            Dim lblpre As Label = CType(e.Item.FindControl("lblpre"), Label)
            Dim lblmpre As Label = CType(e.Item.FindControl("lblmpre"), Label)
            Dim lblpost As Label = CType(e.Item.FindControl("lblpost"), Label)
            Dim su27 As HtmlGenericControl = CType(e.Item.FindControl("su27"), HtmlGenericControl)
            Dim su37 As HtmlGenericControl = CType(e.Item.FindControl("su37"), HtmlGenericControl)
            Dim su57 As HtmlGenericControl = CType(e.Item.FindControl("su57"), HtmlGenericControl)
            ' Mengecek nilai label

            If lblmpre.Text <> "0" Then
                su27.Visible = False
                su37.Visible = False
                su57.Visible = True
            Else
                If lblpost.Text >= lblpre.Text Then
                    su27.Visible = True
                    su37.Visible = False
                    su57.Visible = False
                Else
                    su27.Visible = False
                    su37.Visible = True
                    su57.Visible = False
                End If
            End If


            End If
    End Sub
End Class

'Private Sub BindRepeater()



'    strg = "select *,(select count(id_hasil) from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_pre = el_tbl_manajemen_quiz.pre_test and el_tbl_hasil.id_user =  '" & dr_user("id") & "') as pre, "
'    strg = strg & "(select count(id_hasil) from el_tbl_hasil where el_tbl_hasil.id_mquiz = el_tbl_manajemen_quiz.id_mquiz and el_tbl_hasil.id_post = el_tbl_manajemen_quiz.id_soal and el_tbl_hasil.id_user =  '" & dr_user("id") & "') as post, "
'    strg = strg & "(select link from el_tbl_link where app = 1) as link "
'    strg = strg & "from el_tbl_paket inner join el_tbl_manajemen_quiz on el_tbl_paket.id_paket = el_tbl_manajemen_quiz.id_soal where el_tbl_manajemen_quiz.id_dept = '" & dr_user("departement") & "' and el_tbl_manajemen_quiz.[level] = '" & dr_user("level") & "' and el_tbl_manajemen_quiz.app = 1"
'    Dim conn2 As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
'    Dim cmd2 As SqlCommand = New SqlCommand(strg, conn2)
'    Dim sda2 As SqlDataAdapter = New SqlDataAdapter(cmd2)

'    Dim dt2 As DataTable = New DataTable()

'    sda2.Fill(dt2)

'    Repeater2.DataSource = dt2
'    Repeater2.DataBind()



'End Sub
'Protected Sub Repeater2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
'    If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
'        ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
'        Dim lblpre As Label = CType(e.Item.FindControl("lblpre"), Label)
'        Dim lblmpre As Label = CType(e.Item.FindControl("lblmpre"), Label)
'        Dim lblpost As Label = CType(e.Item.FindControl("lblpost"), Label)
'        Dim su27 As HtmlGenericControl = CType(e.Item.FindControl("su27"), HtmlGenericControl)
'        Dim su37 As HtmlGenericControl = CType(e.Item.FindControl("su37"), HtmlGenericControl)
'        Dim su57 As HtmlGenericControl = CType(e.Item.FindControl("su57"), HtmlGenericControl)
'        ' Mengecek nilai label

'        If lblmpre.Text = "" Then
'            If lblpost.Text <> "0" Then
'                su27.Visible = False
'                su37.Visible = False
'                su57.Visible = True
'            Else
'                su27.Visible = True
'                su37.Visible = False
'                su57.Visible = False
'            End If

'        Else
'            If lblpre.Text = "0" Then
'                su27.Visible = False
'                su37.Visible = True
'                su57.Visible = False
'            Else
'                If lblpost.Text <> "0" Then
'                    su27.Visible = False
'                    su37.Visible = False
'                    su57.Visible = True
'                Else
'                    su27.Visible = True
'                    su37.Visible = False
'                    su57.Visible = False
'                End If

'            End If
'        End If

'    End If
'End Sub