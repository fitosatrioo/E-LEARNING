Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class el_link_form
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_level, dt_pre, dt_materi, dt_soal As New DataTable
    Dim dr As DataRow
    Dim idrec As Int64
    Dim id_mquiz As Int64
    Dim cb As GridViewDataComboBoxColumn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_link_form_Init(sender As Object, e As EventArgs) Handles Me.Init

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
        strg = "select * from el_tbl_link"
        Me.salah = Mod_Utama.isi_data(dt, strg, "id_link", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_link"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app") = False Then
                    str = "update el_tbl_link set app = 0 where app = 1 "
                    str = str & "update el_tbl_link set "
                    str = str & "app = 1 "
                    str = str & "where id_link = " & dr("id_link") & " "
                Else
                    str = "update el_tbl_link set "
                    str = str & "app = 0 "
                    str = str & "where id_link = " & dr("id_link") & " "
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
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO el_tbl_link ("
        str = str & "id_link, link, "
        str = str & "Keterangan ) VALUES ("
        str = str & "(select isnull(max(id_link),0)+1 from el_tbl_link),"
        str = str & "'" & e.NewValues("link") & "', "
        str = str & "'" & e.NewValues("Keterangan") & "') "
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
        str = "UPDATE el_tbl_link set "
        str = str & "link = '" & e.NewValues("link") & "', "
        str = str & "Keterangan = '" & e.NewValues("Keterangan") & "' "
        str = str & "where id_link = " & e.Keys("id_link")
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

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From el_tbl_link where id_link = " & e.Keys("id_link"), "el_tbl_link", dr_user)
        str = "DELETE el_tbl_link "
        str = str & "where id_link = " & e.Keys("id_link")

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
End Class