Imports System.Data.SqlClient
Public Class el_file_materi
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dtj As New DataTable
    Dim dt2 As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Dim id_mquiz As Int64
    Private dataTable As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindData()
            ShowData()
            tampildata()

        End If
        'Me.isi_data()
    End Sub

    Private Sub BindRepeater()
        'Dim id_dept As String = Session("id_user")
        Dim id_dept As String = "1"


        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM el_tbl_isi_materi WHERE id_isimt = ' " + Request.QueryString("id_materi") + " ' ", conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        'Repeater1.DataSource = dt1
        'Repeater1.DataBind()
    End Sub

    Private Sub el_file_materi_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            idrec = Request.QueryString("idrec")
        Catch ex As Exception
            Response.Redirect("materi.aspx")
        End Try
        dr_user = Session("dr_user")
        Me.BindData()
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)
        Dim rowCount As Integer = dataTable.Rows.Count

        If rowIndex < rowCount - 1 Then
            rowIndex += 1
            ViewState("RowIndex") = rowIndex
            ShowData()
            tampildata()
        End If
        If rowIndex = rowCount - 1 Then
            btnNext.Enabled = False
        End If
        If rowCount = 1 Then
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
            ShowData()
            tampildata()
        End If
        If rowCount = 1 Then
            ShowData()
            tampildata()
        End If
        btnNext.Enabled = True
    End Sub

    Private Sub BindData()
        strg2 = "select * from el_tbl_isi_materi where id_materi = '1'"
        Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
        Dim cmd As SqlCommand = New SqlCommand(strg2, conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        dataTable = New DataTable() ' Menggunakan variabel dataTable yang tepat

        sda.Fill(dataTable)

        ViewState("Data") = dataTable
        ViewState("RowIndex") = 0

        If dataTable.Rows.Count > 0 Then ' Menggunakan dataTable.Rows.Count
            ViewState("Data") = dataTable
            ViewState("RowIndex") = 0
            ShowData()
        Else
            lblData.Text = "No data available."

        End If
    End Sub

    Private Sub ShowData()
        Dim dataTable As DataTable = CType(ViewState("Data"), DataTable)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)

        If rowIndex < dataTable.Rows.Count Then
            Dim row As DataRow = dataTable.Rows(rowIndex)
            lblData.Text = row("id_isimt").ToString()

        Else
            lblData.Text = "No more data."
            btnNext.Enabled = False

        End If

    End Sub



    Private Sub tampildata()
        strg = "select * from el_tbl_isi_materi where id_isimt='" + lblData.Text + "'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_isimt", waktu_query)
        If salah.er_hasil <> "" Then

            Exit Sub
        End If

        For Each dtr As DataRow In dt.Rows

            Me.lbljudul.InnerText = dtr("nama_materi")

            If dtr("tipe") = "pdf" Then
                str = str & "<div>"
                str = str & "<div style='max-width: 1200px; margin: 0 auto; margin-top: 50px;' width='100%' class='card text-center'>"
                str = str & "<div class='card-body'>"
                str = str & "<h5 class='card-title'>" & dtr("nama_materi") & "</h5>"
                str = str & " <p Class='card-text'> " & dtr("keterangan") & "</p>"
                str = str & "<div>"
                str = str & "<embed src='file/" & dtr("nama_file") & "' type='application/pdf' width='100%' height='600px'>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"
            ElseIf dtr("tipe") = "ppt" Then
                str = str & "<div>"
                str = str & "<div style='max-width: 600px; margin: 0 auto; margin-top: 50px;' class='card text-center'>"
                str = str & "<div class='card-body'>"
                str = str & "<h5 class='card-title'>" & dtr("nama_materi") & "</h5>"
                str = str & " <p Class='card-text'> " & dtr("keterangan") & "</p>"
                str = str & "<p class='card-text'>Silahkan Download Materi PPT Dibawah Ini!</p>"
                str = str & "<a style='background-color: orange; color: white' href ='file/" & dtr("nama_file") & "' class='btn btn-default'  >Download File PPT</a>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"
            ElseIf dtr("tipe") = "link" Then
                str = str & "<div>"
                str = str & "<div style='max-width: 600px; margin: 0 auto; margin-top: 50px;' class='card text-center'>"
                str = str & "<div class='card-body'>"
                str = str & "<h5 class='card-title'>" & dtr("nama_materi") & "</h5>"
                str = str & " <p Class='card-text'> " & dtr("keterangan") & "</p>"
                str = str & "<p class='card-text'>Silahkan Akses Link Drive Ini Untuk Membaca Materi!</p>"
                str = str & "<a href ='" & dtr("nama_file") & "' Target='_blank'  >Klik Disini Untuk Memasuki Artikel Materi</a>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"
            ElseIf dtr("tipe") = "link_youtube" Then
                str = str & "<div>"
                str = str & "<div style='max-width: 1000px; margin: 0 auto; margin-top: 50px;' class='card text-center'>"
                str = str & "<div class='card-body'>"
                str = str & "<h5 class='card-title'>" & dtr("nama_materi") & "</h5>"
                str = str & " <p Class='card-text'> " & dtr("keterangan") & "</p>"
                str = str & "<div>"
                str = str & "<iframe style='width: 100%; height: 600px' src='https://www.youtube.com/embed/" & dtr("nama_file") & "' frameborder='0' allowfullscreen></iframe>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"
                str = str & "</div>"

            End If

            Dim dt3 As New DataTable
            strg = "select count(id_log) as log from el_tbl_log where id_mquiz='" + Request.QueryString("id_mquiz") + "' and history = '" & dtr("nama_materi") & "' and c_user = '" & dr_user("id") & "'"

            Me.salah = Mod_Utama.isi_data(dt3, strg, "id_log", waktu_query)

            For Each dtr1 As DataRow In dt3.Rows
                If dtr1("log") <> 0 Then
                    Me.btn_log.Visible = False
                    Me.lblbc.Visible = True
                Else
                    Me.btn_log.Visible = True
                    Me.lblbc.Visible = False

                End If

            Next

        Next


        Me.su57.InnerHtml = str
    End Sub

    Protected Sub btn_log_ServerClick(sender As Object, e As EventArgs)
        Dim dt2, dt3 As New DataTable

        strg = "select * from el_tbl_isi_materi where id_isimt='" + lblData.Text + "'"

        Me.salah = Mod_Utama.isi_data(dt2, strg, "id_isimt", waktu_query)
        If salah.er_hasil <> "" Then

            Exit Sub
        End If

        For Each dtr As DataRow In dt2.Rows

            strg = "select count(id_log) as log from el_tbl_log where id_mquiz='" + Request.QueryString("id_mquiz") + "' and history = '" & dtr("id_isimt") & "' and c_user = '" & dr_user("nama") & "'"

            Me.salah = Mod_Utama.isi_data(dt3, strg, "id_log", waktu_query)

            For Each dtr1 As DataRow In dt3.Rows
                If dtr1("log") = 0 Then
                    str = "INSERT INTO el_tbl_log ("
                    str = str & "id_log, history,id_history, id_mquiz, sumber, "
                    str = str & " c_date, c_user) VALUES ("
                    str = str & "(select isnull(max(id_log),0)+1 from el_tbl_log),"
                    str = str & "'" & dtr("nama_materi") & "', "
                    str = str & "'" & dtr("id_isimt") & "', "
                    str = str & "'" & Request.QueryString("id_mquiz") & "', "
                    str = str & "'Materi', "
                    str = str & " getdate(), "
                    str = str & "'" & dr_user("id") & "') "
                    salah.er_hasil = Mod_Utama.exec_sql(str, dr_user, "AUTH USER")

                End If

            Next



        Next
        Me.btn_log.Visible = False
        Me.lblbc.Visible = True
    End Sub

    Protected Sub linkpost_Click(sender As Object, e As EventArgs)
        Response.Redirect("el_pilihan_post.aspx")
        'Dim dtm As New DataTable
        'strg = "select * from el_tbl_manajemen_quiz where id_mquiz '" & Request.QueryString("id_mquiz") & "'"
        'Me.salah = Mod_Utama.isi_data(dtm, strg, "id_mquiz", waktu_query)
        'For Each dtp As DataRow In dtm.Rows
        '    Response.Redirect("el_exam.aspx?")
        'Next
    End Sub





End Class