Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Reflection.MethodBase
Public Class Contact
    Inherits Page
    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Private Sub Contact_Init(sender As Object, e As EventArgs) Handles Me.Init
        dr_user = Session("dr_user")

        Me.isi_data()
    End Sub

    Private Sub Contact_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub Contact_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Private Sub isi_data()
        str = "select * from tbl_ruang"

        Me.salah = Mod_Utama.isi_data(dt, str, "id_ruang", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_ruang"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub
    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO tbl_ruang ("
        str = str & "id_ruang,no_ruang, nama_ruang "
        str = str & ") VALUES ("
        str = str & "(select isnull(max(id_ruang),0)+1 from tbl_ruang),"
        str = str & "'" & e.NewValues("no_ruang") & "', "
        str = str & "'" & e.NewValues("nama_ruang") & "') "


        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Insert Records pada Menu Master Siswa"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles ASPxGridView1.RowUpdating
        str = "UPDATE tbl_ruang set "
        str = str & "no_ruang = '" & e.NewValues("no_ruang") & "', "
        str = str & "nama_ruang = '" & e.NewValues("nama_ruang") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_ruang = " & e.Keys("id_ruang")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Insert Records pada Menu Master Siswa"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub
    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        Mod_Utama.log_delete("select * From tbl_ruang where id_ruang = " & e.Keys("id_ruang"), "tbl_ruang", dr_user)
        str = "DELETE tbl_ruang "
        str = str & "where id_ruang = " & e.Keys("id_ruang")

        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
            g.CancelEdit()
            Me.isi_data()
            e.Cancel = True
        Else
            salah.er_str = str
            salah.er_menu = "Proses Delete Records pada Menu Master User Authorize"
            salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
            Session("error") = salah
        End If
    End Sub

    Private Sub ASPxGridView1_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs) Handles ASPxGridView1.CustomButtonInitialize
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        e.Image.Height = 30
        e.Image.Width = 30

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app_sta") = False Then
                    e.Image.Url = "~/img/no.png"
                Else
                    e.Image.Url = "~/img/yes.png"
                End If

        End Select
    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        dr = Me.ASPxGridView1.GetDataRow(e.VisibleIndex)
        If dr Is Nothing Then Return

        Select Case e.ButtonID
            Case "bt_app"
                If dr("app_sta") = False Then
                    str = "update tbl_ruang set "
                    str = str & "app_user = '" & dr_user("nama") & "', "
                    str = str & "app_sta = 1, "
                    str = str & "app_date = getdate() "
                    str = str & "where id_ruang = " & dr("id_ruang") & " "
                Else
                    str = "update tbl_ruang set "
                    str = str & "app_user = '" & dr_user("nama") & "', "
                    str = str & "app_sta = 0, "
                    str = str & "app_date = getdate() "
                    str = str & "where id_ruang = " & dr("id_ruang") & " "
                End If

                salah.er_hasil = Mod_Utama.exec_sql(str)
                If salah.er_hasil <> "" Then Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
                Me.isi_data()
        End Select
    End Sub
End Class