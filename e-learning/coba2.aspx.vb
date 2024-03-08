Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Imports System.Data.SqlClient

Public Class coba2
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Dim str As String
    Dim strg, strg2 As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt, dt2, dt3, dt4 As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Dim idrec As Int64
    Dim cb As GridViewDataComboBoxColumn

    Private dataTable As New DataTable()
    Private currentPage As Integer = 0
    Private rowsPerPage As Integer = 3
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
            tampildata()
        End If
    End Sub

    Private Sub coba2_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Me.waktu_page.Stop()
    End Sub

    Private Sub coba2_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Me.waktu_page.Start()

    End Sub

    Protected Sub btnsv_ServerClick(sender As Object, e As EventArgs)


    End Sub
    Private Sub coba2_Init(sender As Object, e As EventArgs) Handles Me.Init
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

        btnNext.Enabled = (rowIndex < rowCount - 1)
    End Sub



    Protected Sub btnPrev_Click(sender As Object, e As EventArgs)
        Dim rowIndex As Integer = CType(ViewState("RowIndex"), Integer)

        If rowIndex > 0 Then
            rowIndex -= 1
            ViewState("RowIndex") = rowIndex
            ShowData()
            tampildata()
        End If
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
            btnNext.Enabled = False
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

    Private Sub GenerateButtons(ByVal rowCount As Integer)
        'phButtons.Controls.Clear()

        'For i As Integer = 0 To rowCount - 1
        '    Dim button As New Button()
        '    button.ID = "btnRow" & i.ToString()
        '    button.Text = (i + 1).ToString()
        '    button.CssClass = "rowButton"
        '    AddHandler button.Click, AddressOf RowButton_Click
        '    phButtons.Controls.Add(button)
        '    phButtons.Controls.Add(New LiteralControl("&nbsp;"))
        'Next
    End Sub

    Protected Sub RowButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Dim rowIndex As Integer = Int32.Parse(button.ID.Replace("btnRow", ""))

        ViewState("RowIndex") = rowIndex
        ShowData()
        tampildata()
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
            'strg = "select count(id_log) as log from el_tbl_log where id_mquiz='" + Request.QueryString("id_mquiz") + "' and history = '" & dtr("nama_materi") & "' and c_user = '" & dr_user("id") & "'"

            'Me.salah = Mod_Utama.isi_data(dt3, strg, "id_log", waktu_query)

            'For Each dtr1 As DataRow In dt3.Rows
            '    If dtr1("log") <> 0 Then
            '        Me.btn_log.Visible = False
            '    Else
            '        Me.lblbc.Visible = False
            '    End If

            'Next

        Next


        Me.su57.InnerHtml = str
    End Sub


End Class
'For Each dts As DataRow In dt3.Rows
'If dts("tipe_jawaban") = "text" Then
'str = str & "<div>"
'str = str & ""
'str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'"

'' Periksa jika valuenya sama dengan yang ada dalam database
'If dts("opsi") = nilai_dari_database Then
'str = str & " checked" ' Tambahkan atribut checked
'End If

'str = str & "> <label for='exampleFormControlInput1'>" & dts("opsi") & ". " & dts("jawaban") & "</label>"
'str = str & "</div>"
'Else
'str = str & "<div>"
'str = str & ""
'str = str & "<input id='id_soal' name='id_soal" & dts("id_soal") & "' type='radio' for='radio' value='" & dts("opsi") & "'"

'' Periksa jika valuenya sama dengan yang ada dalam database
'If dts("opsi") = nilai_dari_database Then
'str = str & " checked" ' Tambahkan atribut checked
'End If

'str = str & "> <label for='exampleFormControlInput1'><img src='gambar/" & dts("file_gambar") & "' alt='Gambar' style='max-width: 100px; max-height: 100px; width: auto; height: auto;'></label>"
'str = str & "</div>"
'End If
'Next
