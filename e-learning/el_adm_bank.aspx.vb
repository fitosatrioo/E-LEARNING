Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection.MethodBase

Public Class el_adm_bank
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Public Property AutoPostBack As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()

        End If
    End Sub

    Private Sub el_adm_bank_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
        Mod_Utama.master_waktu(Me.waktu_query, Me.waktu_query, uc_footer)
    End Sub

    Private Sub el_adm_bank_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()
    End Sub

    Private Sub el_adm_bank_Init(sender As Object, e As EventArgs) Handles Me.Init
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
        strg = "select *,"
        strg = strg & "(select isnull(count(*),0) from el_tbl_soal where id_paket = el_tbl_paket.id_paket) as soal "
        strg = strg & "from el_tbl_paket where id_dept = '" & Me.idrec & "' order by id_paket DESC"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_paket", waktu_query)
        If salah.er_hasil <> "" Then
            Me.Jika_error(str, salah.er_str, Me.Page.ToString & " // " & GetCurrentMethod.Name, 1)
            Exit Sub
        End If

        Me.ASPxGridView1.DataSource = dt
        Me.ASPxGridView1.KeyFieldName = "id_paket"
        Me.ASPxGridView1.DataBind()
        Mod_Utama.Atur_Grid(Me.ASPxGridView1)
    End Sub

    Private Sub ASPxGridView1_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles ASPxGridView1.RowInserting
        str = "INSERT INTO el_tbl_paket ("
        str = str & "id_paket,paket_soal, judul_training,id_dept,jenis_test, waktu,kkm, "
        str = str & "c_date, c_user, u_date, u_user) VALUES ("
        str = str & "(select isnull(max(id_paket),0)+1 from el_tbl_paket),"
        str = str & "'" & e.NewValues("paket_soal") & "', "
        str = str & "'" & e.NewValues("judul_training") & "', "
        str = str & "'" & Me.idrec & "', "
        str = str & "'" & e.NewValues("jenis_test") & "', "
        str = str & "'" & e.NewValues("waktu") & "', "
        str = str & "'" & e.NewValues("kkm") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "', "
        str = str & "getdate(), "
        str = str & "'" & dr_user("nama") & "') "


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
        str = "UPDATE el_tbl_paket set "
        str = str & "paket_soal = '" & e.NewValues("paket_soal") & "', "
        str = str & "judul_training = '" & e.NewValues("judul_training") & "', "
        str = str & "jenis_test = '" & e.NewValues("jenis_test") & "', "
        str = str & "waktu = '" & e.NewValues("waktu") & "', "
        str = str & "kkm = '" & e.NewValues("kkm") & "', "
        str = str & "u_date = getdate(), "
        str = str & "u_user = '" & dr_user("nama") & "' "
        str = str & "where id_paket = " & e.Keys("id_paket")

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
        Mod_Utama.log_delete("select * From el_tbl_paket where id_paket = " & e.Keys("id_paket"), "el_tbl_paket", dr_user)
        str = "DELETE el_tbl_paket "
        str = str & "where id_paket = " & e.Keys("id_paket")

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

    Private Sub ASPxGridView1_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles ASPxGridView1.CustomButtonCallback
        If e.ButtonID = "btnCustom" Then

            Dim id As String = ASPxGridView1.GetRowValues(e.VisibleIndex, "id_paket").ToString()
            strg = id

        End If
    End Sub

    Protected Sub btncopy_Click(sender As Object, e As EventArgs)
        Dim button As ASPxButton = TryCast(sender, ASPxButton)
        Dim container As GridViewDataItemTemplateContainer = TryCast(button.NamingContainer, GridViewDataItemTemplateContainer)

        ' Mendapatkan nilai id dari baris yang memicu event btncopy_Click
        Dim id As Object = container.KeyValue
        Me.lblid.InnerText = id
        popupControl.ShowOnPageLoad = True

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim dtpaket, dtsoal, dtjwb As New DataTable
        strg = "select * from el_tbl_soal where id_paket = '" & radioList.SelectedValue & "'"
        Me.salah = Mod_Utama.isi_data(dtsoal, strg, "id_soal", waktu_query)

        For Each dts As DataRow In dtsoal.Rows
            If dts("jenis_soal") = "pg" Then
                If dts("tipe_soal") = "gambar" Then
                    Dim sourceFilePath As String = Server.MapPath("~/gambar/" & dts("file_gambar"))
                    Dim destinationFolder As String = Server.MapPath("~/gambar/")
                    Dim fileName As String = Path.GetFileNameWithoutExtension(sourceFilePath)
                    Dim fileExtension As String = Path.GetExtension(sourceFilePath)
                    Dim newFileName As String = fileName & lblid.InnerText & fileExtension
                    Dim destinationFilePath As String = Path.Combine(destinationFolder, newFileName)

                    ' Melakukan copy file gambar
                    Try
                        File.Copy(sourceFilePath, destinationFilePath, True)
                        ' Jika file tujuan sudah ada, Anda dapat menggunakan parameter ketiga pada File.Copy untuk menindih file yang sudah ada
                        ' Jika parameter ketiga diatur ke False, maka File.Copy akan menghasilkan exception jika file tujuan sudah ada
                    Catch ex As Exception
                        Mod_Utama.tampil_error(Me, "Terjadi Kesalahan ketika Copy Image Silahkan Hubungi MIS Via Email")
                        Return
                    End Try

                    str = "insert into el_tbl_soal ("
                    str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, tipe_soal, skor, file_gambar, nama_file, "
                    str = str & "c_date, c_user) values ("
                    str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
                    str = str & "'" & lblid.InnerText & "', "
                    str = str & "'" & dts("soal") & "', "
                    str = str & "'" & dts("jawaban") & "', "
                    str = str & "'" & dts("jenis_soal") & "', "
                    str = str & "'" & dts("tipe_soal") & "', "
                    str = str & "'" & dts("skor") & "', "
                    str = str & "'" & newFileName & "', "
                    str = str & "'" & dts("nama_file") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "') "
                    salah.er_hasil = Mod_Utama.exec_sql(str)

                    strg = "select * from el_tbl_jawaban where id_soal = '" & dts("id_soal") & "'"
                    Me.salah = Mod_Utama.isi_data(dtjwb, strg, "id_jawaban", waktu_query)
                    For Each dtj As DataRow In dtjwb.Rows
                        If dtj("tipe_jawaban") = "gambar" Then
                            Dim sourceFilePath1 As String = Server.MapPath("~/gambar/" & dtj("file_gambar"))
                            Dim destinationFolder1 As String = Server.MapPath("~/gambar/")
                            Dim fileName1 As String = Path.GetFileNameWithoutExtension(sourceFilePath1)
                            Dim fileExtension1 As String = Path.GetExtension(sourceFilePath1)
                            Dim newFileName1 As String = fileName1 & lblid.InnerText & fileExtension1
                            Dim destinationFilePath1 As String = Path.Combine(destinationFolder1, newFileName1)

                            ' Melakukan copy file gambar
                            Try
                                File.Copy(sourceFilePath1, destinationFilePath1, True)
                                ' Jika file tujuan sudah ada, Anda dapat menggunakan parameter ketiga pada File.Copy untuk menindih file yang sudah ada
                                ' Jika parameter ketiga diatur ke False, maka File.Copy akan menghasilkan exception jika file tujuan sudah ada
                            Catch ex As Exception
                                Mod_Utama.tampil_error(Me, "Terjadi Kesalahan ketika Copy Image Silahkan Hubungi MIS Via Email")
                                Return
                            End Try

                            str = "INSERT INTO el_tbl_jawaban ("
                            str = str & "id_jawaban, id_soal, opsi , jawaban, tipe_jawaban, file_gambar, nama_file, "
                            str = str & "c_date, c_user) VALUES ("
                            str = str & "(select isnull(max(id_jawaban),0)+1 from el_tbl_jawaban),"
                            str = str & "(select isnull(max(id_soal),0) from el_tbl_soal),"
                            str = str & "'" & dtj("opsi") & "', "
                            str = str & "'" & dtj("jawaban") & "', "
                            str = str & "'" & dtj("tipe_jawaban") & "', "
                            str = str & "'" & newFileName1 & "', "
                            str = str & "'" & dtj("nama_file") & "', "
                            str = str & "getdate(), "
                            str = str & "'" & dr_user("nama") & "') "
                            salah.er_hasil = Mod_Utama.exec_sql(str)
                        Else
                            str = "INSERT INTO el_tbl_jawaban ("
                            str = str & "id_jawaban, id_soal, opsi , jawaban, tipe_jawaban,file_gambar,nama_file, "
                            str = str & "c_date, c_user) VALUES ("
                            str = str & "(select isnull(max(id_jawaban),0)+1 from el_tbl_jawaban),"
                            str = str & "(select isnull(max(id_soal),0) from el_tbl_soal),"
                            str = str & "'" & dtj("opsi") & "', "
                            str = str & "'" & dtj("jawaban") & "', "
                            str = str & "'" & dtj("tipe_jawaban") & "', "
                            str = str & "'" & dtj("file_gambar") & "', "
                            str = str & "'" & dtj("nama_file") & "', "
                            str = str & "getdate(), "
                            str = str & "'" & dr_user("nama") & "') "
                            salah.er_hasil = Mod_Utama.exec_sql(str)
                        End If

                    Next
                Else

                    str = "insert into el_tbl_soal ("
                    str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, tipe_soal, skor, file_gambar, nama_file, "
                    str = str & "c_date, c_user) values ("
                    str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
                    str = str & "'" & lblid.InnerText & "', "
                    str = str & "'" & dts("soal") & "', "
                    str = str & "'" & dts("jawaban") & "', "
                    str = str & "'" & dts("jenis_soal") & "', "
                    str = str & "'" & dts("tipe_soal") & "', "
                    str = str & "'" & dts("skor") & "', "
                    str = str & "'" & dts("file_gambar") & "', "
                    str = str & "'" & dts("nama_file") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "') "
                    salah.er_hasil = Mod_Utama.exec_sql(str)

                    strg = "select * from el_tbl_jawaban where id_soal = '" & dts("id_soal") & "'"
                    Me.salah = Mod_Utama.isi_data(dtjwb, strg, "id_jawaban", waktu_query)
                    For Each dtj As DataRow In dtjwb.Rows
                        If dtj("tipe_jawaban") = "gambar" Then
                            Dim sourceFilePath1 As String = Server.MapPath("~/gambar/" & dtj("file_gambar"))
                            Dim destinationFolder1 As String = Server.MapPath("~/gambar/")
                            Dim fileName1 As String = Path.GetFileNameWithoutExtension(sourceFilePath1)
                            Dim fileExtension1 As String = Path.GetExtension(sourceFilePath1)
                            Dim newFileName1 As String = fileName1 & lblid.InnerText & fileExtension1
                            Dim destinationFilePath1 As String = Path.Combine(destinationFolder1, newFileName1)

                            ' Melakukan copy file gambar
                            Try
                                File.Copy(sourceFilePath1, destinationFilePath1, True)
                                ' Jika file tujuan sudah ada, Anda dapat menggunakan parameter ketiga pada File.Copy untuk menindih file yang sudah ada
                                ' Jika parameter ketiga diatur ke False, maka File.Copy akan menghasilkan exception jika file tujuan sudah ada
                            Catch ex As Exception
                                Mod_Utama.tampil_error(Me, "Terjadi Kesalahan ketika Copy Image Silahkan Hubungi MIS Via Email")
                                Return
                            End Try

                            str = "INSERT INTO el_tbl_jawaban ("
                            str = str & "id_jawaban, id_soal, opsi , jawaban, tipe_jawaban, file_gambar, nama_file, "
                            str = str & "c_date, c_user) VALUES ("
                            str = str & "(select isnull(max(id_jawaban),0)+1 from el_tbl_jawaban),"
                            str = str & "(select isnull(max(id_soal),0) from el_tbl_soal),"
                            str = str & "'" & dtj("opsi") & "', "
                            str = str & "'" & dtj("jawaban") & "', "
                            str = str & "'" & dtj("tipe_jawaban") & "', "
                            str = str & "'" & newFileName1 & "', "
                            str = str & "'" & dtj("nama_file") & "', "
                            str = str & "getdate(), "
                            str = str & "'" & dr_user("nama") & "') "
                            salah.er_hasil = Mod_Utama.exec_sql(str)
                        Else
                            str = "INSERT INTO el_tbl_jawaban ("
                            str = str & "id_jawaban, id_soal, opsi , jawaban, tipe_jawaban,file_gambar,nama_file, "
                            str = str & "c_date, c_user) VALUES ("
                            str = str & "(select isnull(max(id_jawaban),0)+1 from el_tbl_jawaban),"
                            str = str & "(select isnull(max(id_soal),0) from el_tbl_soal),"
                            str = str & "'" & dtj("opsi") & "', "
                            str = str & "'" & dtj("jawaban") & "', "
                            str = str & "'" & dtj("tipe_jawaban") & "', "
                            str = str & "'" & dtj("file_gambar") & "', "
                            str = str & "'" & dtj("nama_file") & "', "
                            str = str & "getdate(), "
                            str = str & "'" & dr_user("nama") & "') "
                            salah.er_hasil = Mod_Utama.exec_sql(str)
                        End If
                    Next
                End If

            Else
                If dts("tipe_soal") = "gambar" Then

                    Dim sourceFilePath As String = Server.MapPath("~/gambar/" & dts("file_gambar"))
                    Dim destinationFolder As String = Server.MapPath("~/gambar/")
                    Dim fileName As String = Path.GetFileNameWithoutExtension(sourceFilePath)
                    Dim fileExtension As String = Path.GetExtension(sourceFilePath)
                    Dim newFileName As String = fileName & lblid.InnerText & fileExtension
                    Dim destinationFilePath As String = Path.Combine(destinationFolder, newFileName)

                    ' Melakukan copy file gambar
                    Try
                        File.Copy(sourceFilePath, destinationFilePath, True)
                        ' Jika file tujuan sudah ada, Anda dapat menggunakan parameter ketiga pada File.Copy untuk menindih file yang sudah ada
                        ' Jika parameter ketiga diatur ke False, maka File.Copy akan menghasilkan exception jika file tujuan sudah ada
                    Catch ex As Exception
                        Mod_Utama.tampil_error(Me, "Terjadi Kesalahan ketika Copy Image Silahkan Hubungi MIS Via Email")
                        Return
                    End Try

                    str = "insert into el_tbl_soal ("
                    str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, tipe_soal, skor, file_gambar, nama_file, "
                    str = str & "c_date, c_user) values ("
                    str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
                    str = str & "'" & lblid.InnerText & "', "
                    str = str & "'" & dts("soal") & "', "
                    str = str & "'" & dts("jawaban") & "', "
                    str = str & "'" & dts("jenis_soal") & "', "
                    str = str & "'" & dts("tipe_soal") & "', "
                    str = str & "'" & dts("skor") & "', "
                    str = str & "'" & newFileName & "', "
                    str = str & "'" & dts("nama_file") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "') "
                    salah.er_hasil = Mod_Utama.exec_sql(str)
                Else
                    str = "insert into el_tbl_soal ("
                    str = str & "id_soal, id_paket , soal, jawaban, jenis_soal, tipe_soal, skor, file_gambar, nama_file, "
                    str = str & "c_date, c_user) values ("
                    str = str & "(select isnull(max(id_soal),0)+1 from el_tbl_soal),"
                    str = str & "'" & lblid.InnerText & "', "
                    str = str & "'" & dts("soal") & "', "
                    str = str & "'" & dts("jawaban") & "', "
                    str = str & "'" & dts("jenis") & "', "
                    str = str & "'" & dts("tipe") & "', "
                    str = str & "'" & dts("skor") & "', "
                    str = str & "'" & dts("file_gambar") & "', "
                    str = str & "'" & dts("nama_file") & "', "
                    str = str & "getdate(), "
                    str = str & "'" & dr_user("nama") & "') "
                    salah.er_hasil = Mod_Utama.exec_sql(str)
                End If

            End If

        Next
        Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        popupControl.ShowOnPageLoad = False
    End Sub

    Private Sub BindRepeater()
        Dim strg As String = "SELECT * FROM el_tbl_paket WHERE id_dept = '" & Me.idrec & "' ORDER BY jenis_test DESC,id_paket ASC"
        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        dt1.Columns.Add("CombinedField", GetType(String))
        For Each row As DataRow In dt1.Rows
            row("CombinedField") = "Paket Soal: " & row("paket_soal") & " ,Judul Training: " & row("judul_training") & " ,Jenis Test: " & row("jenis_test") ' Ganti "Field1" dan "Field2" dengan nama kolom yang sesuai
        Next

        radioList.DataSource = dt1
        radioList.DataTextField = "CombinedField" ' Ganti "RadioText" dengan nama kolom yang sesuai
        radioList.DataValueField = "id_paket" ' Ganti "RadioValue" dengan nama kolom yang sesuai
        radioList.DataBind()



    End Sub

End Class