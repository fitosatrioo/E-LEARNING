Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Public Class el_exam
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
    Dim dr As DataRow
    Dim idrec As Int64
    Private dataTable As New DataTable()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id_paket") Is Nothing Then
            Response.Redirect("pilihan_quiz.aspx")
        End If

        If Request.QueryString("id_mquiz") Is Nothing Then
            Response.Redirect("pilihan_quiz.aspx")
        End If
        If Not IsPostBack Then
            BindData()
            ShowData()
            tampildata()

        End If

    End Sub

    Private Sub el_exam_Init(sender As Object, e As EventArgs) Handles Me.Init

        dr_user = Session("dr_user")
        Me.insertawal()
        Try
            insert()
        Catch ex As Exception

        End Try
        Me.BindData()
        Me.waktu()


    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)
        Dim rowCount As Integer = DataTable.Rows.Count

        If rowIndex < rowCount - 1 Then
            rowIndex += 1
            ViewState("RowIndex") = rowIndex
            insert()
            ShowData()
            tampildata()
        End If
        If rowIndex = rowCount - 1 Then
            btnNext.Enabled = False
            test.Visible = True
        End If
        If rowCount = 1 Then
            insert()
            ShowData()
            tampildata()
        End If

    End Sub

    Protected Sub btnPrev_Click(sender As Object, e As EventArgs)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)
        Dim rowCount As Integer = dataTable.Rows.Count
        If rowIndex > 0 Then
            rowIndex -= 1
            ViewState("RowIndex") = rowIndex
            insert()
            ShowData()
            tampildata()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "startTimer", "startTimer();", True)
        End If
        If rowCount = 1 Then
            insert()
            ShowData()
            tampildata()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "startTimer", "startTimer();", True)
        End If
        btnNext.Enabled = True
        test.Visible = False

    End Sub

    Private Sub BindData()
        strg2 = "select *, ROW_NUMBER() OVER (ORDER BY id_soal) AS nomr_urutan, "
        strg2 = strg2 & "(select jawaban_user from el_tbl_nilai where id_hasil IN (SELECT el_tbl_hasil.id_hasil FROM el_tbl_hasil WHERE id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "' ) and id_soal = el_tbl_soal.id_soal) as jwb"
        strg2 = strg2 & " from el_tbl_soal where id_paket = '" & Request.QueryString("id_paket") & "' order by newid()"
        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg2, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        dataTable = New DataTable() ' Menggunakan variabel dataTable yang tepat

        sda.Fill(dataTable)
        dataTable.Columns.Add("nomor_urutan", GetType(Integer))

        ' Mengisi nilai nomor berurutan pada setiap baris kolom baru
        For i As Integer = 0 To dataTable.Rows.Count - 1
            dataTable.Rows(i)("nomor_urutan") = i + 1
        Next
        ViewState("Data") = dataTable
        ViewState("RowIndex") = 0

        If dataTable.Rows.Count > 0 Then ' Menggunakan dataTable.Rows.Count
            ViewState("Data") = dataTable
            ViewState("RowIndex") = 0
            ShowData()
        Else
            lblData.Text = "No data available."

        End If
        GenerateButtons(dataTable.Rows.Count) ' Menggunakan dataTable.Rows.Count

    End Sub

    Private Sub ShowData()
        Dim dataTable As DataTable = CType(ViewState("Data"), DataTable)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)

        If rowIndex < dataTable.Rows.Count Then
            Dim row As DataRow = dataTable.Rows(rowIndex)
            lblData.Text = row("id_soal").ToString()
            lblno.Text = row("nomor_urutan").ToString()

        Else
            lblData.Text = "No more data."
            btnNext.Enabled = False
            test.Visible = True

        End If

    End Sub

    Private Sub GenerateButtons(ByVal rowCount As Integer)
        phButtons.Controls.Clear()
        Dim dataTable As DataTable = CType(ViewState("Data"), DataTable)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)

        For i As Integer = 0 To rowCount - 1
            Dim button As New Button()
            button.ID = "btnRow" & i.ToString()
            button.Text = (i + 1).ToString()

            ' Dapatkan nilai jwb (jawaban_user) dari dataTable
            Dim jwb As String = dataTable.Rows(i)("jwb").ToString()

            ' Periksa apakah jwb tidak null atau kosong
            If Not String.IsNullOrEmpty(jwb) Then
                button.CssClass = "btn-success" ' Ganti dengan kelas CSS yang sesuai dengan warna hijau
            Else
                button.CssClass = "rowButton"
            End If

            AddHandler button.Click, AddressOf RowButton_Click

            phButtons.Controls.Add(button)
            phButtons.Controls.Add(New LiteralControl("&nbsp;"))
        Next
    End Sub

    Protected Sub RowButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim rowIndex As Integer = Int32.Parse(button.ID.Replace("btnRow", ""))

        ViewState("RowIndex") = rowIndex
        insert()
        ShowData()
        tampildata()


    End Sub
    Private Sub waktu()
        Dim dtwkt As New DataTable
        strg = "select waktu from el_tbl_paket where id_paket = '" & Request.QueryString("id_paket") & "'"
        Me.salah = Mod_Utama.isi_data(dtwkt, strg, "id_paket", waktu_query)
        For Each dtw As DataRow In dtwkt.Rows
            Me.apage.Value = dtw("waktu")
        Next
    End Sub



    Protected Sub btnselesai_Click(sender As Object, e As EventArgs)
        Dim dtm As New DataTable
        strg = "select * from el_tbl_manajemen_quiz where id_mquiz = '" & Request.QueryString("id_mquiz") & "'"
        Me.salah = Mod_Utama.isi_data(dtm, strg, "id_paket", waktu_query)
        For Each dt10 As DataRow In dtm.Rows
            If dt10("pre_test") = "" Then
                Dim dthasil As New DataTable
                strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                For Each dth As DataRow In dthasil.Rows
                    If dth("jml") = 0 Then
                        Dim random As New Random()
                        Dim kode As Integer = random.Next(100, 999)
                        Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                        Dim dt5 As New DataTable
                        strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_post,u_date,urutan) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate(),(select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') )"
                        Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                    End If
                Next
                strg = "select *,(select id_hasil from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "') as id_hasil"
                strg = " from el_tbl_soal  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)
                For Each dtr As DataRow In dt.Rows
                    Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                    conn.Open()
                    Dim dt6, dt7 As New DataTable
                    Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr("id_hasil") & "',@tx_skor)"
                    Dim cmd As New SqlCommand(query, conn)
                    If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                        cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                    End If
                    cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                    cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                    cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Next
            Else
                Dim random As New Random()
                Dim kode As Integer = random.Next(100, 999)
                Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                Dim dt5 As New DataTable
                strg = "select * from el_tbl_paket where id_paket = '" & Request.QueryString("id_paket") & "'"
                Me.salah = Mod_Utama.isi_data(dt5, strg, "id_paket", waktu_query)

                For Each dtr As DataRow In dt5.Rows
                    If dtr("jenis_test") = "PRE TEST" Then
                        Dim dthasil As New DataTable
                        strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                        Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                        For Each dth As DataRow In dthasil.Rows
                            If dth("jml") = 0 Then

                                strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_pre,c_date) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate() )"
                                Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                            End If
                        Next
                    ElseIf dtr("jenis_test") = "POST TEST" Then
                        strg = "update el_tbl_hasil set id_post = '" & Request.QueryString("id_paket") & "',"
                        strg = strg & "u_date = getdate(),"
                        strg = strg & "urutan = (select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') "
                        strg = strg & "where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                        Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                    End If
                Next



                strg2 = "select * from el_tbl_soal  WHERE id_soal='" + lblData.Text + "'"

                Me.salah = Mod_Utama.isi_data(dt, strg2, "id_soal", waktu_query)
                If salah.er_hasil <> "" Then

                    Exit Sub
                End If
                For Each dtr As DataRow In dt.Rows
                    Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                    conn.Open()
                    Dim dt6, dt7, dtup, dtup2 As New DataTable
                    strg = "select * from el_tbl_paket  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                    Me.salah = Mod_Utama.isi_data(dt6, strg, "id_soal", waktu_query)
                    For Each dtr1 As DataRow In dt6.Rows
                        If dtr1("jenis_test") = "PRE TEST" Then
                            strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                            For Each dtr2 As DataRow In dt7.Rows
                                strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                For Each dtr3 As DataRow In dtup.Rows
                                    If dtr3("jmlh") = 0 Then

                                        Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                        Dim cmd As New SqlCommand(query, conn)
                                        If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                            cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                        Else
                                            cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                        End If
                                        cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                        cmd.ExecuteNonQuery()
                                        conn.Close()

                                    Else
                                        strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                        Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                        For Each dtr4 As DataRow In dtup2.Rows
                                            Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()
                                        Next
                                    End If

                                Next

                            Next
                        ElseIf dtr1("jenis_test") = "POST TEST" Then
                            strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                            For Each dtr2 As DataRow In dt7.Rows
                                strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                For Each dtr3 As DataRow In dtup.Rows
                                    If dtr3("jmlh") = 0 Then

                                        Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                        Dim cmd As New SqlCommand(query, conn)
                                        If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                            cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                        Else
                                            cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                        End If
                                        cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                        cmd.ExecuteNonQuery()
                                        conn.Close()

                                    Else
                                        strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                        Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                        For Each dtr4 As DataRow In dtup2.Rows
                                            Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()
                                        Next
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next

            End If
        Next

        strg = "UPDATE el_tbl_nilai set app_sta = 1 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user = el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)

        strg = "UPDATE el_tbl_nilai set app_sta = 0 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user <> el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)



        Dim dt2, dt3 As New DataTable

        strg = "select * from el_tbl_paket where id_paket='" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt2, strg, "id_isimt", waktu_query)
        If salah.er_hasil <> "" Then

            Exit Sub
        End If

        For Each dtr As DataRow In dt2.Rows

            strg = "INSERT INTO el_tbl_log ("
            strg = strg & "id_log, history,id_history, id_mquiz, sumber, "
            strg = strg & " c_date, c_user) VALUES ("
            strg = strg & "(select isnull(max(id_log),0)+1 from el_tbl_log),"
            strg = strg & "'" & dtr("paket_soal") & "_" & dtr("judul_training") & "', "
            strg = strg & "'" & dtr("id_paket") & "', "
            strg = strg & "'" & Request.QueryString("id_mquiz") & "', "
            strg = strg & "'Test', "
            strg = strg & " getdate(), "
            strg = strg & "'" & dr_user("id") & "') "
            salah.er_hasil = Mod_Utama.exec_sql(strg, dr_user, "AUTH USER")
        Next
        Response.Redirect("el_pilihan_quiz.aspx")
    End Sub

    Private Sub tampildata()
        Dim dttampil As New DataTable
        strg = "SELECT count(id_nilai) as id from el_tbl_nilai inner join el_tbl_hasil on el_tbl_hasil.id_hasil = el_tbl_nilai.id_hasil where el_tbl_nilai.id_soal = '" & lblData.Text & "' and el_tbl_nilai.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "'"

        Me.salah = Mod_Utama.isi_data(dttampil, strg, "id_nilai", waktu_query)

        For Each dtam As DataRow In dttampil.Rows
            If dtam("id") = "0" Then
                strg = "select * from el_tbl_soal where id_soal = " & Me.lblData.Text & ""

                Me.salah = Mod_Utama.isi_data(dt2, strg, "id_soal", waktu_query)
                For Each dtr As DataRow In dt2.Rows
                    'Me.txwaktu.Value = dtr("waktu")

                    strg2 = "select * from el_tbl_jawaban where id_soal = " & dtr("id_soal") & ""

                    Me.salah = Mod_Utama.isi_data(dt3, strg2, "id_jawaban", waktu_query)
                    If salah.er_hasil <> "" Then

                        Exit Sub
                    End If
                    If dtr("jenis_soal") = "pg" Then
                        If dtr("tipe_soal") = "gambar" Then
                            str = str & "<div>"
                            str = str & "<img class='img-fluid' src='gambar/" & dtr("file_gambar") & "' style='width: 300px; '  />"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        ElseIf dtr("tipe_soal") = "text" Then
                            str = str & "<div>"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        ElseIf dtr("tipe_soal") = "link" Then
                            str = str & "<div>"
                            str = str & "<div style=' position: relative; padding-bottom: 40.25%; height: 0; overflow: hidden;'>"
                            str = str & "<iframe style='width: 450px; height: 200px; position: absolute;' src='https://www.youtube.com/embed/" & dtr("nama_file") & "' frameborder='0' allowfullscreen></iframe>"
                            str = str & "</div>"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        End If
                    Else
                        If dtr("tipe_soal") = "gambar" Then
                            str = str & "<div>"
                            str = str & "<img class='img-fluid' src='gambar/" & dtr("file_gambar") & "' width='300px'  />"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<div style='padding-bottom: 40.25%;'>"
                            str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
                            str = str & "</div>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        ElseIf dtr("tipe_soal") = "text" Then
                            str = str & "<div>"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<div style='padding-bottom: 40.25%;'>"
                            str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
                            str = str & "</div>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        ElseIf dtr("tipe_soal") = "link" Then
                            str = str & "<div>"
                            str = str & "<div style=' position: relative; padding-bottom: 40.25%; height: 0; overflow: hidden;'>"
                            str = str & "<iframe style='width: 450px; height: 200px; position: absolute;' src='https://www.youtube.com/embed/" & dtr("file_gambar") & "' frameborder='0' allowfullscreen></iframe>"
                            str = str & "</div>"
                            str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                            str = str & "<div style='padding-bottom: 40.25%;'>"
                            str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
                            str = str & "</div>"
                            str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                            str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                            str = str & "</div>"
                        End If
                    End If


                    str = str & "<input name='tx_paket" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_paket") & "'>"


                    For Each dts As DataRow In dt3.Rows
                        If dts("tipe_jawaban") = "text" Then
                            str = str & "<div>"
                            str = str & ""
                            str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'> <label for='exampleFormControlInput1'>" & dts("opsi") & ". " & dts("jawaban") & "</label>"
                            str = str & "</div>"
                        Else
                            str = str & "<div>"
                            str = str & ""
                            str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'> <label for='exampleFormControlInput1'><img src='gambar/" & dts("file_gambar") & "' alt='Gambar' style='max-width: 100px; max-height: 100px; width: auto; height: auto;'></label>"
                            str = str & "</div>"
                        End If


                    Next
                Next

            Else
                Dim dtnilai As New DataTable
                strg = "SELECT * from el_tbl_nilai inner join el_tbl_hasil on el_tbl_hasil.id_hasil = el_tbl_nilai.id_hasil where el_tbl_nilai.id_soal = '" & lblData.Text & "' and el_tbl_nilai.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "'"

                Me.salah = Mod_Utama.isi_data(dtnilai, strg, "id_nilai", waktu_query)

                For Each dtn As DataRow In dtnilai.Rows
                    strg = "select * from el_tbl_soal where id_soal = " & Me.lblData.Text & ""

                    Me.salah = Mod_Utama.isi_data(dt2, strg, "id_soal", waktu_query)
                    For Each dtr As DataRow In dt2.Rows
                        'Me.txwaktu.Value = dtr("waktu")
                        If dtr("jenis_soal") = "pg" Then
                            If dtr("tipe_soal") = "gambar" Then
                                str = str & "<div>"
                                str = str & "<img class='img-fluid' src='gambar/" & dtr("file_gambar") & "' style='width: 300px; '  />"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            ElseIf dtr("tipe_soal") = "text" Then
                                str = str & "<div>"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            ElseIf dtr("tipe_soal") = "link" Then
                                str = str & "<div>"
                                str = str & "<div style=' position: relative; padding-bottom: 40.25%; height: 0; overflow: hidden;'>"
                                str = str & "<iframe style='width: 450px; height: 200px; position: absolute;' src='https://www.youtube.com/embed/" & dtr("nama_file") & "' frameborder='0' allowfullscreen></iframe>"
                                str = str & "</div>"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            End If
                        Else
                            If dtr("tipe_soal") = "gambar" Then
                                str = str & "<div>"
                                str = str & "<img class='img-fluid' src='gambar/" & dtr("file_gambar") & "' width='300px'  />"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<div style='padding-bottom: 40.25%;'>"
                                str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'>" & dtn("jawaban_user") & "</textarea>"
                                str = str & "</div>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            ElseIf dtr("tipe_soal") = "text" Then
                                str = str & "<div>"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<div style='padding-bottom: 40.25%;'>"
                                str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'>" & dtn("jawaban_user") & "</textarea>"
                                str = str & "</div>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            ElseIf dtr("tipe_soal") = "link" Then
                                str = str & "<div>"
                                str = str & "<div style=' position: relative; padding-bottom: 40.25%; height: 0; overflow: hidden;'>"
                                str = str & "<iframe style='width: 450px; height: 200px; position: absolute;' src='https://www.youtube.com/embed/" & dtr("file_gambar") & "' frameborder='0' allowfullscreen></iframe>"
                                str = str & "</div>"
                                str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
                                str = str & "<div style='padding-bottom: 40.25%;'>"
                                str = str & "<textarea class='form-control' rows='4' style='width: 100%; height: 180px; resize: none; outline: none; box-sizing: border-box;' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'>" & dtn("jawaban_user") & "</textarea>"
                                str = str & "</div>"
                                str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
                                str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
                                str = str & "</div>"
                            End If
                        End If


                        str = str & "<input name='tx_paket" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_paket") & "'>"

                        strg2 = "select * from el_tbl_jawaban where id_soal = " & dtr("id_soal") & ""

                        Me.salah = Mod_Utama.isi_data(dt3, strg2, "id_jawaban", waktu_query)
                        For Each dts As DataRow In dt3.Rows
                            If dts("tipe_jawaban") = "text" Then
                                str = str & "<div>"
                                str = str & ""
                                str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'"

                                ' Periksa jika valuenya sama dengan yang ada dalam database
                                If dts("opsi") = If(dtn("jawaban_user") Is DBNull.Value, "", dtn("jawaban_user")) Then
                                    str = str & " checked" ' Tambahkan atribut checked
                                End If

                                str = str & "> <label for='exampleFormControlInput1'>" & dts("opsi") & ". " & dts("jawaban") & "</label>"
                                str = str & "</div>"
                            Else
                                str = str & "<div>"
                                str = str & ""
                                str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'"

                                ' Periksa jika valuenya sama dengan yang ada dalam database
                                If dts("opsi") = If(dtn("jawaban_user") Is DBNull.Value, "", dtn("jawaban_user")) Then
                                    str = str & " checked" ' Tambahkan atribut checked
                                End If

                                str = str & "> <label for='exampleFormControlInput1'><img src='gambar/" & dts("file_gambar") & "' alt='Gambar' style='max-width: 100px; max-height: 100px; width: auto; height: auto;'></label>"
                                str = str & "</div>"
                            End If
                        Next

                    Next

                Next
            End If
        Next



        Me.su57.InnerHtml = str
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

    Private Sub insertawal()
        Dim dthasil As New DataTable
        strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
        Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

        For Each dth As DataRow In dthasil.Rows
            If dth("jml") = 0 Then
                Dim random As New Random()
                Dim kode As Integer = random.Next(100, 999)
                Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                Dim dt5 As New DataTable
                strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_pre,c_date) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate() )"
                Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
            End If
        Next
    End Sub
    Private Sub test_Click(sender As Object, e As EventArgs) Handles test.Click
        Dim dtm As New DataTable
        strg = "select * from el_tbl_manajemen_quiz where id_mquiz = '" & Request.QueryString("id_mquiz") & "'"
        Me.salah = Mod_Utama.isi_data(dtm, strg, "id_paket", waktu_query)
        For Each dt10 As DataRow In dtm.Rows
            If dt10("pre_test") = "" Then
                Dim dthasil As New DataTable
                strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                For Each dth As DataRow In dthasil.Rows
                    If dth("jml") = 0 Then
                        Dim random As New Random()
                        Dim kode As Integer = random.Next(100, 999)
                        Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                        Dim dt5 As New DataTable
                        strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_post,u_date,urutan) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate(),(select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') )"
                        Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                    End If
                Next
                strg = "select *,(select id_hasil from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "') as id_hasil"
                strg = " from el_tbl_soal  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)
                For Each dtr As DataRow In dt.Rows
                    Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                    conn.Open()
                    Dim dt6, dt7 As New DataTable
                    Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr("id_hasil") & "',@tx_skor)"
                    Dim cmd As New SqlCommand(query, conn)
                    If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                        cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                    End If
                    cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                    cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                    cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                    cmd.ExecuteNonQuery()
                    conn.Close()
                Next
            Else
                Dim random As New Random()
                Dim kode As Integer = random.Next(100, 999)
                Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                Dim dt5 As New DataTable
                strg = "select * from el_tbl_paket where id_paket = '" & Request.QueryString("id_paket") & "'"
                Me.salah = Mod_Utama.isi_data(dt5, strg, "id_paket", waktu_query)

                For Each dtr As DataRow In dt5.Rows
                    If dtr("jenis_test") = "PRE TEST" Then
                        Dim dthasil As New DataTable
                        strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                        Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                        For Each dth As DataRow In dthasil.Rows
                            If dth("jml") = 0 Then

                                strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_pre,c_date) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate() )"
                                Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                            End If
                        Next
                    ElseIf dtr("jenis_test") = "POST TEST" Then
                        strg = "update el_tbl_hasil set id_post = '" & Request.QueryString("id_paket") & "',"
                        strg = strg & "u_date = getdate(),"
                        strg = strg & "urutan = (select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') "
                        strg = strg & "where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                        Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                    End If
                Next



                strg2 = "select * from el_tbl_soal  WHERE id_soal='" + lblData.Text + "'"

                Me.salah = Mod_Utama.isi_data(dt, strg2, "id_soal", waktu_query)
                If salah.er_hasil <> "" Then

                    Exit Sub
                End If
                For Each dtr As DataRow In dt.Rows
                    Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                    conn.Open()
                    Dim dt6, dt7, dtup, dtup2 As New DataTable
                    strg = "select * from el_tbl_paket  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                    Me.salah = Mod_Utama.isi_data(dt6, strg, "id_soal", waktu_query)
                    For Each dtr1 As DataRow In dt6.Rows
                        If dtr1("jenis_test") = "PRE TEST" Then
                            strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                            For Each dtr2 As DataRow In dt7.Rows
                                strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                For Each dtr3 As DataRow In dtup.Rows
                                    If dtr3("jmlh") = 0 Then

                                        Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                        Dim cmd As New SqlCommand(query, conn)
                                        If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                            cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                        Else
                                            cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                        End If
                                        cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                        cmd.ExecuteNonQuery()
                                        conn.Close()

                                    Else
                                        strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                        Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                        For Each dtr4 As DataRow In dtup2.Rows
                                            Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()
                                        Next
                                    End If

                                Next

                            Next
                        ElseIf dtr1("jenis_test") = "POST TEST" Then
                            strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                            For Each dtr2 As DataRow In dt7.Rows
                                strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                For Each dtr3 As DataRow In dtup.Rows
                                    If dtr3("jmlh") = 0 Then

                                        Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                        Dim cmd As New SqlCommand(query, conn)
                                        If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                            cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                        Else
                                            cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                        End If
                                        cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                        cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                        cmd.ExecuteNonQuery()
                                        conn.Close()

                                    Else
                                        strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                        Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                        For Each dtr4 As DataRow In dtup2.Rows
                                            Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()
                                        Next
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next

            End If
        Next

        strg = "UPDATE el_tbl_nilai set app_sta = 1 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user = el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)

        strg = "UPDATE el_tbl_nilai set app_sta = 0 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user <> el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)



        Dim dt2, dt3 As New DataTable

        strg = "select * from el_tbl_paket where id_paket='" + Request.QueryString("id_paket") + "'"

        Me.salah = Mod_Utama.isi_data(dt2, strg, "id_isimt", waktu_query)
        If salah.er_hasil <> "" Then

            Exit Sub
        End If

        For Each dtr As DataRow In dt2.Rows

            strg = "INSERT INTO el_tbl_log ("
            strg = strg & "id_log, history,id_history, id_mquiz, sumber, "
            strg = strg & " c_date, c_user) VALUES ("
            strg = strg & "(select isnull(max(id_log),0)+1 from el_tbl_log),"
            strg = strg & "'" & dtr("paket_soal") & "_" & dtr("judul_training") & "', "
            strg = strg & "'" & dtr("id_paket") & "', "
            strg = strg & "'" & Request.QueryString("id_mquiz") & "', "
            strg = strg & "'Test', "
            strg = strg & " getdate(), "
            strg = strg & "'" & dr_user("id") & "') "
            salah.er_hasil = Mod_Utama.exec_sql(strg, dr_user, "AUTH USER")
        Next
        Response.Redirect("clearsession.aspx")
    End Sub

    Private Sub insert()
        Try
            Dim dtm As New DataTable
            strg = "select * from el_tbl_manajemen_quiz where id_mquiz = '" & Request.QueryString("id_mquiz") & "'"
            Me.salah = Mod_Utama.isi_data(dtm, strg, "id_paket", waktu_query)
            For Each dt10 As DataRow In dtm.Rows
                If dt10("pre_test") = "" Then
                    Dim dthasil As New DataTable
                    strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                    Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                    For Each dth As DataRow In dthasil.Rows
                        If dth("jml") = 0 Then
                            Dim random As New Random()
                            Dim kode As Integer = random.Next(100, 999)
                            Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                            Dim dt5 As New DataTable
                            strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_post,u_date,urutan) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate(),(select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') )"
                            Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                        End If
                    Next
                    strg = "select *,(select id_hasil from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "') as id_hasil"
                    strg = strg & " from el_tbl_soal  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                    Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)
                    For Each dtr As DataRow In dt.Rows
                        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                        conn.Open()
                        Dim dt6, dt7, dtup, dtup2 As New DataTable
                        strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                        Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                        For Each dtr3 As DataRow In dtup.Rows

                            If dtr3("jmlh") = 0 Then
                                Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr("id_hasil") & "',@tx_skor)"
                                Dim cmd As New SqlCommand(query, conn)
                                If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                    cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                Else
                                    cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                End If
                                cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                cmd.ExecuteNonQuery()
                                conn.Close()

                            Else

                                strg = "select * from el_tbl_nilai where id_hasil = '" & dtr("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                For Each dtr4 As DataRow In dtup2.Rows
                                    Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                    Dim cmd As New SqlCommand(query, conn)
                                    If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                        cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                    Else
                                        cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                    End If
                                    cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                    cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                    cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                    cmd.ExecuteNonQuery()
                                    conn.Close()
                                Next
                            End If

                        Next
                    Next
                Else
                    Dim random As New Random()
                    Dim kode As Integer = random.Next(100, 999)
                    Dim idhasil As String = dr_user("id") & Request.QueryString("id_mquiz") & Request.QueryString("id_paket") & kode.ToString
                    Dim dt5 As New DataTable
                    strg = "select * from el_tbl_paket where id_paket = '" & Request.QueryString("id_paket") & "'"
                    Me.salah = Mod_Utama.isi_data(dt5, strg, "id_paket", waktu_query)

                    For Each dtr As DataRow In dt5.Rows
                        If dtr("jenis_test") = "PRE TEST" Then
                            Dim dthasil As New DataTable
                            strg = "select count(id_hasil) as jml from el_tbl_hasil where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user = '" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dthasil, strg, "id_hasil", waktu_query)

                            For Each dth As DataRow In dthasil.Rows
                                If dth("jml") = 0 Then

                                    strg = "INSERT INTO el_tbl_hasil (id_hasil, id_user, id_mquiz,id_pre,c_date) VALUES ('" & idhasil & "', '" & dr_user("id") & "', '" & Request.QueryString("id_mquiz") & "','" & Request.QueryString("id_paket") & "',getdate() )"
                                    Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                                End If
                            Next
                        ElseIf dtr("jenis_test") = "POST TEST" Then
                            strg = "update el_tbl_hasil set id_post = '" & Request.QueryString("id_paket") & "',"
                            strg = strg & "u_date = getdate(),"
                            strg = strg & "urutan = (select isnull(max(urutan),0)+1 from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Request.QueryString("id_mquiz") & "' and el_tbl_paket.jenis_test = 'POST TEST') "
                            strg = strg & "where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                            Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)
                        End If
                    Next



                    strg2 = "select * from el_tbl_soal  WHERE id_soal='" + lblData.Text + "'"

                    Me.salah = Mod_Utama.isi_data(dt, strg2, "id_soal", waktu_query)
                    If salah.er_hasil <> "" Then

                        Exit Sub
                    End If
                    For Each dtr As DataRow In dt.Rows
                        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                        conn.Open()
                        Dim dt6, dt7, dtup, dtup2 As New DataTable
                        strg = "select * from el_tbl_paket  WHERE id_paket='" + Request.QueryString("id_paket") + "'"
                        Me.salah = Mod_Utama.isi_data(dt6, strg, "id_soal", waktu_query)
                        For Each dtr1 As DataRow In dt6.Rows
                            If dtr1("jenis_test") = "PRE TEST" Then
                                strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                                For Each dtr2 As DataRow In dt7.Rows
                                    strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                    Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                    For Each dtr3 As DataRow In dtup.Rows
                                        If dtr3("jmlh") = 0 Then

                                            Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()

                                        Else
                                            strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                            Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                            For Each dtr4 As DataRow In dtup2.Rows
                                                Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                                Dim cmd As New SqlCommand(query, conn)
                                                If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                    cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                                Else
                                                    cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                                End If
                                                cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                                cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                                cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                                cmd.ExecuteNonQuery()
                                                conn.Close()
                                            Next
                                        End If

                                    Next

                                Next
                            ElseIf dtr1("jenis_test") = "POST TEST" Then
                                strg = "select * from el_tbl_hasil  where id_mquiz = '" & Request.QueryString("id_mquiz") & "' and id_user='" & dr_user("id") & "'"
                                Me.salah = Mod_Utama.isi_data(dt7, strg, "id_soal", waktu_query)
                                For Each dtr2 As DataRow In dt7.Rows
                                    strg = "select count(id_nilai) as jmlh from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                    Me.salah = Mod_Utama.isi_data(dtup, strg, "id_nilai", waktu_query)
                                    For Each dtr3 As DataRow In dtup.Rows
                                        If dtr3("jmlh") = 0 Then

                                            Dim query As String = "INSERT INTO el_tbl_nilai (id_nilai, id_soal, jawaban_user,id_user,id_paket,id_hasil,skor) VALUES ((SELECT ISNULL(MAX(id_nilai), 0) + 1 FROM el_tbl_nilai), @tx_soal, @id_soal," & dr_user("id") & ",@tx_paket,'" & dtr2("id_hasil") & "',@tx_skor)"
                                            Dim cmd As New SqlCommand(query, conn)
                                            If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                            Else
                                                cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                            End If
                                            cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                            cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                            cmd.ExecuteNonQuery()
                                            conn.Close()

                                        Else
                                            strg = "select * from el_tbl_nilai where id_hasil = '" & dtr2("id_hasil") & "' and id_soal = '" & lblData.Text & "' and id_user = '" & dr_user("id") & "'"
                                            Me.salah = Mod_Utama.isi_data(dtup2, strg, "id_nilai", waktu_query)
                                            For Each dtr4 As DataRow In dtup2.Rows
                                                Dim query As String = "UPDATE el_tbl_nilai SET id_soal = @tx_soal, jawaban_user = @id_soal, id_paket = @tx_paket, skor = @tx_skor WHERE id_nilai = '" & dtr4("id_nilai") & "'"
                                                Dim cmd As New SqlCommand(query, conn)
                                                If String.IsNullOrEmpty(Request.Form("id_soal" & dtr("id_soal") & "")) Then
                                                    cmd.Parameters.AddWithValue("@id_soal", DBNull.Value)
                                                Else
                                                    cmd.Parameters.AddWithValue("@id_soal", Request.Form("id_soal" & dtr("id_soal") & ""))
                                                End If
                                                cmd.Parameters.AddWithValue("@tx_soal", Request.Form("tx_soal" & dtr("id_soal") & ""))

                                                cmd.Parameters.AddWithValue("@tx_paket", Request.Form("tx_paket" & dtr("id_soal") & ""))

                                                cmd.Parameters.AddWithValue("@tx_skor", Request.Form("tx_skor" & dtr("id_soal") & ""))
                                                cmd.ExecuteNonQuery()
                                                conn.Close()
                                            Next
                                        End If
                                    Next
                                Next
                            End If
                        Next
                    Next

                End If
            Next

            strg = "UPDATE el_tbl_nilai set app_sta = 1 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user = el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)

            strg = "UPDATE el_tbl_nilai set app_sta = 0 FROM el_tbl_nilai INNER JOIN el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.jawaban_user <> el_tbl_soal.jawaban and el_tbl_soal.jenis_soal = 'pg' and el_tbl_soal.id_paket = '" + Request.QueryString("id_paket") + "'"

            Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)



        Catch ex As Exception
        End Try

    End Sub
End Class

'If dtr("jenis_soal") = "pg" Then
'    If dtr("tipe_soal") = "gambar" Then
'        str = str & "<div>"
'        str = str & "<img src='gambar/" & dtr("file_gambar") & "' width='200px'  />"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "</div>"
'    ElseIf dtr("tipe_soal") = "text" Then
'        str = str & "<div>"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "</div>"
'    ElseIf dtr("tipe_soal") = "link" Then
'        str = str & "<div>"
'        str = str & "<iframe style='width: 450px; height: 200px' src='https://www.youtube.com/embed/" & dtr("file_gambar") & "' frameborder='0' allowfullscreen></iframe>"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "</div>"
'    End If
'Else
'    If dtr("tipe_soal") = "gambar" Then
'        str = str & "<div>"
'        str = str & "<img src='gambar/" & dtr("file_gambar") & "' width='200px'  />"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "<textarea style='width: 450px; height: 180px' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
'        str = str & "</div>"
'    ElseIf dtr("tipe_soal") = "text" Then
'        str = str & "<div>"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<textarea style='width: 450px; height: 180px' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "</div>"
'    ElseIf dtr("tipe_soal") = "link" Then
'        str = str & "<div>"
'        str = str & "<iframe width='560' height='315' src='https://www.youtube.com/embed/" & dtr("file_gambar") & "' frameborder='0' allowfullscreen></iframe>"
'        str = str & "<label for='exampleFormControlInput1'>" & dtr("soal") & "</label>"
'        str = str & "<input name='tx_soal" & dtr("id_soal") & "' type='hidden' value='" & dtr("id_soal") & "'>"
'        str = str & "<input name='tx_skor" & dtr("id_soal") & "' type='hidden' value='" & dtr("skor") & "'>"
'        str = str & "<textarea style='width: 450px; height: 180px' name='id_soal" & dtr("id_soal") & "' placeholder='Jawabanmu'></textarea>"
'        str = str & "</div>"
'    End If
'End If