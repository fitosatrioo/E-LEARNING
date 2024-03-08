Imports System.Data.SqlClient
Public Class el_pilihan_materi1
    Inherits System.Web.UI.Page
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim salah As er_custom
    Dim str As String
    Dim dr_user As DataRow
    Dim strg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()
        End If
        'Me.isi_data()
        Cache("id_mquiz1") = Request.QueryString("id_mquiz1")

    End Sub

    Private Sub BindRepeater()
        strg = "select *,(ROW_NUMBER() OVER (ORDER BY id_isimt) - 1) AS nomor_urutan ,(select c_date from el_tbl_log where c_user = '" & dr_user("id") & "' and id_history = el_tbl_isi_materi.id_isimt and id_mquiz = '" & Request.QueryString("id_mquiz1") & "' and sumber = 'materi') as tgl_akses from el_tbl_isi_materi where id_materi = '" + Request.QueryString("id_materi") + "'"


        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        dt1.Columns.Add("id_mquiz", GetType(Integer))

        ' Mengisi nilai 1 pada setiap baris kolom baru
        For Each row As DataRow In dt1.Rows
            row("id_mquiz") = Request.QueryString("id_mquiz1")
        Next

        Repeater1.DataSource = dt1
        Repeater1.DataBind()

    End Sub

    Private Sub el_pilihan_materi1_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")

    End Sub

    Protected Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Mendapatkan kontrol-kontrol di dalam ItemTemplate
            Dim lbl_log As Label = CType(e.Item.FindControl("lbl_log"), Label)
            Dim lbl_blm As Label = CType(e.Item.FindControl("lbl_blm"), Label)
            Dim lbl_sdh As Label = CType(e.Item.FindControl("lbl_sdh"), Label)
            Dim lbl_sdh2 As Label = CType(e.Item.FindControl("lbl_sdh2"), Label)

            ' Mengecek nilai label

            If lbl_log.Text = "" Then
                lbl_blm.Visible = True
                lbl_sdh.Visible = False
                lbl_sdh2.Visible = False
            Else
                lbl_blm.Visible = False
                lbl_sdh.Visible = True
                lbl_sdh2.Visible = True

            End If

        End If
    End Sub
End Class