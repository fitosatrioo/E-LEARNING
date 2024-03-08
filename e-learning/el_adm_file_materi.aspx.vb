Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class adm_file_materi
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim dr As DataRow

    Dim idrec As Int64
    Dim service As String
    Dim dt_head As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub bt_img_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub adm_file_materi_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub adm_file_materi_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub adm_file_materi_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")
        Catch ex As Exception
            Response.Redirect("tqa_trs_mapel.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.isi_data()
    End Sub
    Private Function insert_data(nmfile As String, extion As String) As Boolean
        str = "INSERT INTO el_tbl_isi_materi ("
        str = str & "id_isimt, id_materi, nama_file, nama_materi, nm_file, tipe, keterangan, "
        str = str & "c_date, c_user) VALUES ("
        str = str & "(select isnull(max(id_isimt),0)+1 from el_tbl_isi_materi),"
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & nmfile & extion & "', "
        str = str & "'" & Me.tx_ket.Value & "', "
        str = str & "'" & Me.tx_fileimg.Value & "', "
        str = str & "'" & Me.cmb_doc.Value & "', "
        str = str & "'" & Me.txtket.Value & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str)

        If salah.er_hasil <> "" Then
            MsgBox(salah.er_hasil)
            Me.Jika_error(str, salah.er_hasil, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return False
        End If

        Return True
    End Function

    Private Function insert_datalink() As Boolean
        str = "INSERT INTO el_tbl_isi_materi ("
        str = str & "id_isimt, id_materi, nama_file, nama_materi, nm_file, tipe, keterangan, "
        str = str & "c_date, c_user) VALUES ("
        str = str & "(select isnull(max(id_isimt),0)+1 from el_tbl_isi_materi),"
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & Me.tx_link.Value & "', "
        str = str & "'" & Me.tx_ket.Value & "', "
        str = str & "'" & Me.tx_link.Value & "', "
        str = str & "'" & Me.cmb_doc.Value & "', "
        str = str & "'" & Me.txtket.Value & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "

        salah.er_hasil = Mod_Utama.exec_sql(str)

        If salah.er_hasil <> "" Then
            MsgBox(salah.er_hasil)
            Me.Jika_error(str, salah.er_hasil, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Return False
        End If

        Return True
    End Function

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
        str = "select *, (select id from tbl_login where id = '" & dr_user("id") & "') as id "
        str = str & "from el_tbl_isi_materi "
        str = str & "where id_materi = " & Me.idrec & " "
        str = str & "order by id_isimt desc"
        salah = Mod_Utama.isi_data(dt, str, "id_isimt", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_isimt"
        Me.ASPxGridView1.DataBind()
        Me.ASPxGridView1.Settings.ShowFooter = True
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        If e.Values("tipe") = "file" Then
            Mod_Utama.log_delete("select * From el_tbl_isi_materi where id_isimt =" & e.Keys("id_isimt"), "el_tbl_isi_materi", dr_user)
            dr = Me.dt.Rows.Find(e.Keys("id_isimt"))
            Try
                File.Delete(Server.MapPath("~\file\" & dr("nama_file")))
            Catch ex As Exception
                Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
                g.CancelEdit()
                e.Cancel = True
                g.Caption = ex.ToString
                Return
            End Try

            Mod_Utama.log_delete("select * From el_tbl_isi_materi where id_isimt =" & e.Keys("id_isimt"), "el_tbl_isi_materi", dr_user)
            str = "DELETE el_tbl_isi_materi "
            str = str & "WHERE id_isimt =" & e.Keys("id_isimt")

            salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "file")
            If salah.er_hasil = "" Then
                Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
                g.CancelEdit()
                Me.isi_data()
                e.Cancel = True
            Else
                salah.er_str = str
                salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
                salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
                Session("error") = salah
            End If
        Else
            Mod_Utama.log_delete("select * From el_tbl_isi_materi where id_isimt =" & e.Keys("id_isimt"), "el_tbl_isi_materi", dr_user)
            str = "DELETE el_tbl_isi_materi "
            str = str & "WHERE id_isimt =" & e.Keys("id_isimt")

            salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")
            If salah.er_hasil = "" Then
                Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
                g.CancelEdit()
                Me.isi_data()
                e.Cancel = True
            Else
                salah.er_str = str
                salah.er_menu = Me.Page.ToString & " // " & GetCurrentMethod.Name
                salah.er_waktu = Mod_Utama.str_waktu(Me.waktu_query, Me.waktu_page)
                Session("error") = salah
            End If
        End If

    End Sub

    Protected Sub bt_img_ServerClick1(sender As Object, e As EventArgs)

        If Me.cmb_doc.Value = "none" Then
            Mod_Utama.tampil_error(Me, "Terjadinya kesalahan pada Query, harap kirim laporan ke MIS via email")
        ElseIf Me.cmb_doc.Value = "link" Then
            Me.insert_datalink()
            Me.isi_data()
            Response.Redirect(Request.RawUrl)
        ElseIf Me.cmb_doc.Value = "link_youtube" Then
            Me.insert_datalink()
            Me.isi_data()
            Response.Redirect(Request.RawUrl)
        Else
            Dim filepath As String = tx_fileimg.PostedFile.FileName
            Dim filext As String = Path.GetFileName(filepath)
            Dim filename As String = Path.GetFileNameWithoutExtension(filepath)
            Dim ext As String = Path.GetExtension(filext)

            Dim title As String = filename & "_" & Format(Now, "yyyyMMddHHmmss")

            Dim filepost As HttpPostedFile = tx_fileimg.PostedFile
            Dim filekb As Integer = filepost.ContentLength

            Dim filecurr As HttpPostedFile
            If ext.ToLower <> ".pdf" And ext.ToLower <> ".ppt" And ext.ToLower <> ".pptx" Then
                filecurr = tx_fileimg.PostedFile

            End If

            Try
                tx_fileimg.PostedFile.SaveAs(Server.MapPath("~\file\" & title & ext))
            Catch ex As Exception
                Mod_Utama.tampil_error(Me, "File Tidak Dapat Di Upload")
                Return
            End Try


            If Me.insert_data(title, ext) = False Then
                Mod_Utama.tampil_error(Me, "File Tidak Dapat Di Upload")
                Exit Sub
            End If


            Mod_Utama.tampil_sukses(Me, "Simpan Gambar User Terbaru Telah Berhasil")
            Me.isi_data()
            Response.Redirect(Request.RawUrl)
        End If


    End Sub

End Class