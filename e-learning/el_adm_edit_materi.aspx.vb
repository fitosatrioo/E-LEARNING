Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class el_adm_edit_materi
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_level, dt_pre, dt_materi, dt_soal As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Dim cb As GridViewDataComboBoxColumn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub el_adm_edit_materi_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim usernm As String = ""
        Dim userid As String = ""

        userid = Request.QueryString("id")


        Dim dtuser As New DataTable
        str = "select * from tbl_login "
        str = str & "where id = '" & userid & "' "

        salah = Mod_Utama.isi_data(dtuser, str, "id", waktu_query)
        If salah.er_hasil <> "" Then
            Mod_Utama.tampil_error(Me.Page, "Ada Error saat Page Init, Site1.Master")
            Return
        End If


        Session("dr_user") = dtuser.Rows(0)
        dr_user = Session("dr_user")
        Try
            idrec = Request.QueryString("idrec")
        Catch ex As Exception
            Response.Redirect("home.aspx")
        End Try
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
        str = "select * from el_tbl_isi_materi where id_isimt = '" & Me.idrec & "'"
        Me.salah = Mod_Utama.isi_data(dt, str, "id_isimt", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        For Each dtr As DataRow In dt.Rows
            Me.txjudul.Value = dtr("nama_materi")

            If dtr("tipe") = "pdf" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideElement", "$('#linkContainer').hide();", True)
                Me.cb_pilihan.Value = "file"
                Me.cb_pilihan.SelectedIndex = "0"
                Me.lb_file.Text = dtr("nm_file")
                Me.txtid.Value = dtr("id_isimt")
                Me.txtnamafile.Value = dtr("nama_file")
            ElseIf dtr("tipe") = "ppt" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideElement", "$('#linkContainer').hide();", True)
                Me.cb_pilihan.Value = "file"
                Me.cb_pilihan.SelectedIndex = "0"
                Me.lb_file.Text = dtr("nm_file")
                Me.txtid.Value = dtr("id_isimt")
                Me.txtnamafile.Value = dtr("nama_file")
            ElseIf dtr("tipe") = "link" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideElement", "$('#fileContainer').hide();", True)
                Me.cb_pilihan.Value = "link"
                Me.cb_pilihan.SelectedIndex = "1"
                Me.txlink.Value = dtr("nm_file")
                Me.txtid.Value = dtr("id_isimt")
            ElseIf dtr("tipe") = "link_youtube" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideElement", "$('#fileContainer').hide();", True)
                Me.cb_pilihan.Value = "link"
                Me.cb_pilihan.SelectedIndex = "1"
                Me.txlink.Value = dtr("nm_file")
                Me.txtid.Value = dtr("id_isimt")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideElement", "$('#fileContainer').hide();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideInputFile", "$('#linkContainer').hide();", True)
            End If
        Next
    End Sub

    Protected Sub Unnamed_ServerClick(sender As Object, e As EventArgs)
        If Me.cb_pilihan.Value = "link" Or Me.cb_pilihan.Value = "link_youtube" Then
            If Me.txtnamafile.Value <> "" Then
                Try
                    File.Delete(Server.MapPath("~\file\" & Me.txtnamafile.Value))
                Catch ex As Exception
                    Mod_Utama.tampil_error(Me, "Terjadi Kesalahan Query, Silahkan Hubungi Staff MIS")
                End Try
            End If
            Me.updatelink()
            Response.Redirect(Request.RawUrl)
        Else
            If Me.txfile.Value = "" Then
                Me.updatefiles()

            Else
                Try
                    File.Delete(Server.MapPath("~\file\" & Me.txtnamafile.Value))
                Catch ex As Exception
                    Me.funcfile()
                End Try
                Me.funcfile()


            End If
        End If
    End Sub

    Private Sub funcfile()
        Dim filepath As String = txfile.PostedFile.FileName
        Dim filext As String = Path.GetFileName(filepath)
        Dim filename As String = Path.GetFileNameWithoutExtension(filepath)
        Dim ext As String = Path.GetExtension(filext)

        Dim title As String = filename & "_" & Format(Now, "yyyyMMddHHmmss")

        Dim filepost As HttpPostedFile = txfile.PostedFile
        Dim filekb As Integer = filepost.ContentLength

        Dim filecurr As HttpPostedFile
        If ext.ToLower <> ".pdf" And ext.ToLower <> ".ppt" Then
            filecurr = txfile.PostedFile
        End If

        Try
            txfile.PostedFile.SaveAs(Server.MapPath("~\file\" & title & ext))
        Catch ex As Exception
            Mod_Utama.tampil_error(Me, "File Tidak Dapat Di Upload")
            Return
        End Try

        If Me.updatefile(title, ext) = False Then
            Mod_Utama.tampil_error(Me, "File Tidak Dapat Di Upload")
            Exit Sub
        End If

        Mod_Utama.tampil_sukses(Me, "Simpan Gambar User Terbaru Telah Berhasil")
        Me.isi_data()
    End Sub

    Private Sub updatelink()
        str = "UPDATE el_tbl_isi_materi set "
        str = str & "nama_materi = '" & Me.txjudul.Value & "', "
        str = str & "nama_file = '" & Me.txlink.Value & "', "
        str = str & "nm_file = '" & Me.txlink.Value & "', "
        str = str & "tipe = '" & Me.cb_pilihan.Value & "', "
        str = str & "c_date = getdate(), "
        str = str & "c_user = '" & dr_user("nama") & "' "
        str = str & "where id_isimt = " & Me.txtid.Value
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Mod_Utama.tampil_sukses(Me, "Update Berhasil")
        Else
            Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Proses, harap kirim laporan ke MIS via email")
        End If
    End Sub

    Private Function updatefile(nmfile As String, extion As String) As Boolean
        str = "UPDATE el_tbl_isi_materi set "
        str = str & "nama_materi = '" & Me.txjudul.Value & "', "
        str = str & "nama_file = '" & nmfile & extion & "', "
        str = str & "nm_file = '" & Me.txfile.Value & "', "
        str = str & "tipe = '" & Me.cb_pilihan.Value & "', "
        str = str & "c_date = getdate(), "
        str = str & "c_user = '" & dr_user("nama") & "' "
        str = str & "where id_isimt = " & Me.txtid.Value
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Mod_Utama.tampil_sukses(Me, "Update Berhasil")
        Else
            Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Proses, harap kirim laporan ke MIS via email")
        End If

        Return True
    End Function

    Private Function updatefiles() As Boolean
        str = "UPDATE el_tbl_isi_materi set "
        str = str & "nama_materi = '" & Me.txjudul.Value & "', "
        str = str & "tipe = '" & Me.cb_pilihan.Value & "', "
        str = str & "c_date = getdate(), "
        str = str & "c_user = '" & dr_user("nama") & "' "
        str = str & "where id_isimt = " & Me.txtid.Value
        salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
        If salah.er_hasil = "" Then
            Mod_Utama.tampil_sukses(Me, "Update Berhasil")
        Else
            Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Proses, harap kirim laporan ke MIS via email")
        End If

        Return True
    End Function
End Class