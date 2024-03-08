Public Class cobauser
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dtj, dt1, dt2, dt3, dt4 As New DataTable
    Dim str_menu As String = ",2,"

    Protected Sub btnselesai_Click(sender As Object, e As EventArgs)

    End Sub

    Dim dr As DataRow
    Dim idrec As Int64
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub cobauser_Init(sender As Object, e As EventArgs) Handles Me.Init
        me.waktu
    End Sub
    Private Sub waktu()

        strg = "select * from el_tbl_paket where id_paket = '1'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_paket", waktu_query)

        For Each dtam As DataRow In dt.Rows
            apage.Value = dtam("waktu")
        Next
    End Sub
End Class