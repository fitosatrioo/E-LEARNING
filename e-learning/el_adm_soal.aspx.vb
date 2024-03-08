Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Public Class adm_soal
    Inherits System.Web.UI.Page

    Dim dr_user As DataRow
    Dim str, strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt_detail As New DataTable
    Dim cb As GridViewDataComboBoxColumn
    Dim dr As DataRow
    Dim gridview As ASPxGridView
    Dim idrec, iddetail As Int64
    Dim dt_head As New DataTable
    Private ASPxGridViewDetail1, ASPxGridViewDetail2 As ASPxGridView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    'Protected Sub bt_img_ServerClick(sender As Object, e As EventArgs)


    'End Sub

    Private Sub adm_soal_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_page, uc_footer)
    End Sub

    Private Sub adm_soal_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub adm_soal_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")
        Catch ex As Exception
            Response.Redirect("login.aspx")
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

    Private Function insert_data() As Boolean

        str = "insert into el_tbl_soal ("
        str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, tipe_soal, skor, file_gambar, nama_file, "
        str = str & "c_date, c_user) values ("
        str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & Me.txtsoal.Value & "', "
        str = str & "'" & Me.txtjawaban.Value & "', "
        str = str & "'" & Me.cb_jenis.Value & "', "
        str = str & "'" & Me.cb_tipe.Value & "', "
        str = str & "'" & Me.txskor.Value & "', "
        str = str & "'', "
        str = str & "'', "
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

    Private Function insert_link() As Boolean
        str = "insert into el_tbl_soal ("
        str = str & "id_soal, id_paket , soal, jawaban,file_gambar,nama_file, jenis_soal, tipe_soal, skor, "
        str = str & "c_date, c_user) values ("
        str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & Me.txtsoal.Value & "', "
        str = str & "'" & Me.txtjawaban.Value & "', "
        str = str & "'" & Me.txlink.Value & "', "
        str = str & "'', "
        str = str & "'" & Me.cb_jenis.Value & "', "
        str = str & "'" & Me.cb_tipe.Value & "', "
        str = str & "'" & Me.txskor.Value & "', "
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

    Private Function insert_img(nmfile As String, extion As String) As Boolean
        str = "insert into el_tbl_soal ("
        str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, file_gambar,nama_file, tipe_soal, skor, "
        str = str & "c_date, c_user) values ("
        str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & Me.txtsoal.Value & "', "
        str = str & "'" & Me.txtjawaban.Value & "', "
        str = str & "'" & Me.cb_jenis.Value & "', "
        str = str & "'" & nmfile & extion & "', "
        str = str & "'" & Me.txfile.Value & "', "
        str = str & "'" & Me.cb_tipe.Value & "', "
        str = str & "'" & Me.txskor.Value & "', "
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


    Private Sub resize_img(filenm As HttpPostedFile, extion As String, title As String)
        Dim stream As Stream = filenm.InputStream
        Dim oriimg As Bitmap = New Bitmap(stream)

        Dim newheight As Integer = CInt(oriimg.Height * (CSng(600) / CSng(oriimg.Width)))
        Dim newimg As Bitmap = New Bitmap(600, newheight)
        Dim graf As Graphics = Graphics.FromImage(newimg)
        graf.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        graf.DrawImage(oriimg, 0, 0, 600, newheight)

        Try
            newimg.Save(Server.MapPath("~\gambar\" & title & extion))
        Catch ex As Exception
            Mod_Utama.tampil_error(Me, "Simpan BIG Gambar User Tidak Berhasil")
            Exit Sub
        End Try


        If Me.insert_img(title, extion) = False Then
            Mod_Utama.tampil_error(Me, "Simpan BIG Gambar User Tidak Berhasil")
        End If

        Mod_Utama.tampil_sukses(Me, "Simpan BIG Gambar User Terbaru Berhasil")
        Me.isi_data()

    End Sub
    Private Sub isi_data()
        str = "SELECT *, "
        str = str & "(select isnull(count(*),0) from el_tbl_jawaban where id_soal = el_tbl_soal.id_soal) as opsi, "
        str = str & "(select id from tbl_login where id = '" & dr_user("id") & "') as id "
        str = str & " FROM el_tbl_soal WHERE id_paket = '" & Me.idrec & "'"
        salah = Mod_Utama.isi_data(dt, str, "id_soal", Me.waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_soal"
        Me.ASPxGridView1.DataBind()
        Me.ASPxGridView1.Settings.ShowFooter = True
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)


    End Sub

    Private Sub ASPxGridView1_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles ASPxGridView1.RowDeleting
        If e.Values("tipe_soal") = "link" Or e.Values("tipe_soal") = "text" Then
            Mod_Utama.log_delete("select * from el_tbl_soal where id_soal =" & e.Keys("id_soal"), "el_tbl_soal", dr_user)
            str = "DELETE el_tbl_soal "
            str = str & "WHERE id_soal =" & e.Keys("id_soal")
            str = str & " DELETE el_tbl_jawaban "
            str = str & "WHERE id_soal =" & e.Keys("id_soal")

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
            dr = Me.dt.Rows.Find(e.Keys("id_soal"))
            Try
                File.Delete(Server.MapPath("~\gambar\" & dr("file_gambar")))
            Catch ex As Exception
                Dim g As ASPxGridView = TryCast(sender, ASPxGridView)
                g.CancelEdit()
                e.Cancel = True
                g.Caption = ex.ToString
                Return
            End Try

            Mod_Utama.log_delete("select * from el_tbl_soal where id_soal =" & e.Keys("id_soal"), "el_tbl_soal", dr_user)
            str = "DELETE el_tbl_soal "
            str = str & "WHERE id_soal =" & e.Keys("id_soal")
            str = str & " DELETE el_tbl_jawaban "
            str = str & "WHERE id_soal =" & e.Keys("id_soal")

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
        End If


    End Sub

    Protected Sub btnsave_ServerClick(sender As Object, e As EventArgs)
        If Me.cb_tipe.Value = "gambar" Then
            Dim filepath As String = txfile.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filepath)
            Dim ext As String = Path.GetExtension(filename)

            Dim title As String = "image" & Me.idrec & "_" & Format(Now, "yyyyMMddHHmmss")

            Dim filepost As HttpPostedFile = txfile.PostedFile
            Dim filekb As Integer = filepost.ContentLength

            Dim filecurr As HttpPostedFile
            If ext.ToLower <> ".jpg" And ext.ToLower <> ".png" Then
                filecurr = txfile.PostedFile
            Else
                If filekb > 800000 Then
                    resize_img(txfile.PostedFile, ext, title)
                    Return
                Else
                    filecurr = txfile.PostedFile
                End If
            End If

            Try
                txfile.PostedFile.SaveAs(Server.MapPath("~\gambar\" & title & ext))
            Catch ex As Exception
                Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
                Return
            End Try

            If Me.insert_img(title, ext) = False Then
                Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
                Exit Sub
            End If

            Mod_Utama.tampil_sukses(Me, "Simpan Gambar User Terbaru Telah Berhasil")
            Me.isi_data()
            Response.Redirect(Request.RawUrl)

        ElseIf Me.cb_tipe.Value = "link" Then
            If Me.insert_link() = False Then
                Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
                Exit Sub
            End If
            Response.Redirect(Request.RawUrl)
        Else
            If Me.insert_data() = False Then
                Mod_Utama.tampil_error(Me, "Simpan Standart Gambar User Tidak Berhasil")
                Exit Sub
            End If
            Response.Redirect(Request.RawUrl)
        End If
    End Sub

    Protected Sub btnedit_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback

    End Sub

    Private Sub ASPxGridView1_RowCommand(sender As Object, e As ASPxGridViewRowCommandEventArgs) Handles ASPxGridView1.RowCommand

    End Sub

    Protected Sub ASPxGridView1_RowCommand1(sender As Object, e As ASPxGridViewRowCommandEventArgs)

    End Sub

    Private Sub ASPxGridView1_StartRowEditing(sender As Object, e As ASPxStartRowEditingEventArgs) Handles ASPxGridView1.StartRowEditing
        dr = Me.ASPxGridView1.GetDataRow(e.EditingKeyValue)
    End Sub

    Protected Sub txtesoal_Init(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ASPxGridViewDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Try
            iddetail = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Catch ex As Exception

        End Try
        strg = "SELECT *,(select id from tbl_login where id = '" & dr_user("id") & "') as id FROM el_tbl_jawaban "
        strg = strg & "WHERE id_soal = '" & iddetail & "'"
        Mod_Utama.isi_data(Me.dt_detail, strg, "id_jawaban", waktu_query)

        ASPxGridViewDetail2 = TryCast(sender, ASPxGridView)
        If ASPxGridViewDetail2 Is Nothing Then Exit Sub

        ASPxGridViewDetail2.DataSource = Me.dt_detail
        Mod_Utama.Atur_Grid(Me.ASPxGridViewDetail2)
        ASPxGridViewDetail2.Settings.ShowGroupPanel = False
        ASPxGridViewDetail2.SettingsSearchPanel.Visible = False
        ASPxGridViewDetail2.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        ASPxGridViewDetail2.Settings.ShowFilterRow = False
    End Sub

    Protected Sub btnedit_ServerClick1(sender As Object, e As EventArgs)
        Dim txtInput As HtmlInputText = CType(gridview.FindEditFormTemplateControl("txt_input"), HtmlInputText)
        Dim inputText As String = txtInput.Value

        strg = "hello " & inputText
        salah.er_hasil = Mod_Utama.exec_sql(strg)

    End Sub
End Class