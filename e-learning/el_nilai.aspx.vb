Imports DevExpress.Web
Imports System.Reflection.MethodBase
Imports System.Drawing
Imports System.IO
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Public Class el_nilai
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
    Dim post_test As Int64
    Dim pre_test As String
    Dim id_mquiz As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id_mquiz") Is Nothing Then
            Response.Redirect("el_pilihan_nilai.aspx")
        End If
        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()
        End If
    End Sub

    Private Sub el_nilai_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            id_mquiz = Request.QueryString("id_mquiz")
            post_test = Request.QueryString("id_post")
            pre_test = Request.QueryString("id_pre")
            If String.IsNullOrEmpty(pre_test) Then
                pre_test = String.Empty
            End If

        Catch ex As Exception
            Response.Redirect("el_pilihan_materi2.aspx")
        End Try

        dr_user = Session("dr_user")
        Me.isi_data()
    End Sub

    Private Sub isi_data()
        strg = "select sum(skor) as nilai from el_tbl_nilai inner join  el_tbl_paket on el_tbl_nilai.id_paket = el_tbl_paket.id_paket inner join el_tbl_hasil on el_tbl_paket.id_paket = el_tbl_hasil.id_pre and el_tbl_hasil.id_hasil = el_tbl_nilai.id_hasil where el_tbl_nilai.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' and el_tbl_hasil.id_pre = '" & Me.pre_test & "' and el_tbl_nilai.app_sta = 1 and el_tbl_hasil.app = '1'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)

        For Each dtjr As DataRow In dt.Rows
            If dtjr("nilai") Is DBNull.Value Then
                Me.skor.Text = "0"
            Else

                Me.skor.Text = dtjr("nilai")
            End If
        Next

        strg = "select sum(skor) as nilai from el_tbl_nilai inner join  el_tbl_paket on el_tbl_nilai.id_paket = el_tbl_paket.id_paket inner join el_tbl_hasil on el_tbl_paket.id_paket = el_tbl_hasil.id_post and el_tbl_hasil.id_hasil = el_tbl_nilai.id_hasil where el_tbl_nilai.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' and el_tbl_hasil.id_post = '" & Me.post_test & "' and el_tbl_nilai.app_sta = 1 and el_tbl_hasil.app = '1'"

        Me.salah = Mod_Utama.isi_data(dt, strg, "id_soal", waktu_query)

        For Each dtjr As DataRow In dt.Rows
            If dtjr("nilai") Is DBNull.Value Then
                Me.pro.Text = "0"
            Else

                Me.pro.Text = dtjr("nilai")
            End If


        Next

        If Me.skor.Text Or Me.pro.Text > "1" Then

            Dim skor As Integer = Me.skor.Text
            Dim pro As Integer = Me.pro.Text

            Dim increase As Integer = pro - skor
            Dim hasil As Integer = (increase / skor) * 100
            Me.persen.Text = hasil & "%"

        Else
            Me.persen.Text = "0"
        End If






    End Sub



    Private Sub BindRepeater()
        'Dim id_dept As String = Session("id_user")
        Dim id_dept As String = "1"

        If Me.pre_test <> "" Then
            strg = "select * from el_tbl_hasil where id_user = '" & dr_user("id") & "' and id_mquiz = '" & Me.id_mquiz & "' and id_pre = '" & Me.pre_test & "' and app = '1'"
            Me.salah = Mod_Utama.isi_data(dt, strg, "id_hasil", waktu_query)

            For Each dtr As DataRow In dt.Rows
                Me.lbldate.InnerText = dtr("c_date")
                strg = "select * from el_tbl_nilai inner join el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.id_paket = '" & dtr("id_pre") & "' and el_tbl_nilai.id_user = '" & dr_user("id") & "'"
                Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
                Dim cmd As SqlCommand = New SqlCommand(strg, conn)
                Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dt1 As DataTable = New DataTable()
                sda.Fill(dt1)
                Repeater1.DataSource = dt1
                Repeater1.DataBind()
            Next
        End If

        strg2 = "select * from el_tbl_hasil where id_user = '" & dr_user("id") & "' and id_mquiz = '" & Me.id_mquiz & "' and id_post = '" & Me.post_test & "' and app = '1'"
        Me.salah = Mod_Utama.isi_data(dt, strg2, "id_hasil", waktu_query)
        For Each dtr2 As DataRow In dt.Rows
            strg2 = "select * from el_tbl_nilai inner join el_tbl_soal on el_tbl_nilai.id_soal = el_tbl_soal.id_soal where el_tbl_nilai.id_paket = '" & dtr2("id_post") & "' and el_tbl_nilai.id_user = '" & dr_user("id") & "'"
            Dim conn As SqlConnection = New SqlConnection(Mod_Utama.sql_str)
            Dim cmd2 As SqlCommand = New SqlCommand(strg2, conn)
            Dim sda2 As SqlDataAdapter = New SqlDataAdapter(cmd2)
            Dim dt2 As DataTable = New DataTable()
            sda2.Fill(dt2)
            Repeater2.DataSource = dt2
            Repeater2.DataBind()
        Next

        Me.lblname.InnerText = dr_user("nama")
        Me.lblmquiz.InnerText = Me.id_mquiz
        strg = "select judul from el_tbl_manajemen_quiz where id_mquiz = '" & Me.id_mquiz & "'"
        Me.salah = Mod_Utama.isi_data(dt, strg, "id_mquiz", waktu_query)
        For Each dtr As DataRow In dt.Rows
            Me.lbljudul.InnerText = dtr("judul")

        Next

        str = "select *, "
        str = str & "(select sum(skor) from el_tbl_nilai inner join el_tbl_hasil on el_tbl_nilai.id_hasil = el_tbl_hasil.id_hasil  inner join el_tbl_paket on el_tbl_paket.id_paket  = el_tbl_hasil.id_post and el_tbl_paket.id_paket = el_tbl_nilai.id_paket where el_tbl_nilai.app_sta = '1' and el_tbl_hasil.id_user = '" & dr_user("id") & "' and el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' and el_tbl_paket.jenis_test='post test') as skors "
        str = str & "from el_tbl_hasil inner join el_tbl_paket on el_tbl_hasil.id_post = el_tbl_paket.id_paket where el_tbl_hasil.id_mquiz = '" & Me.id_mquiz & "' and el_tbl_hasil.id_user = '" & dr_user("id") & "' and el_tbl_paket.jenis_test = 'POST TEST' and el_tbl_hasil.app = '1'"
        Me.salah = Mod_Utama.isi_data(dt, str, "id_hasil", waktu_query)
        For Each dtr As DataRow In dt.Rows

            If IsDBNull(dtr("skors")) Then
                str = "<p style='color: #297a00; font-weight: bold; margin-top: 50px'>Selamat Anda Lulus Training, Silahkan Download Sertifikat!</p>"
                str = str & "<a href='el_sertif.aspx?id_mquiz=" & Me.id_mquiz & "&id_user=" & dr_user("id") & "' class='btn btn-primary mt-4' Target='_blank'>Download Sertifikat</a>"
                Me.lnksertif.InnerHtml = str
            ElseIf dtr("skors") >= dtr("kkm") Then
                str = "<p style='color: #297a00; font-weight: bold; margin-top: 50px'>Selamat Anda Lulus Training, Silahkan Download Sertifikat!</p>"
                str = str & "<a href='el_sertif.aspx?id_mquiz=" & Me.id_mquiz & "&id_user=" & dr_user("id") & "' class='btn btn-primary mt-4' Target='_blank'>Download Sertifikat</a>"
                Me.lnksertif.InnerHtml = str

            Else
                Me.lnksertif.InnerHtml = "<p style='color: red'>Maaf Anda Tidak Lulus Training, Silahkan Coba Lagi!</p>"
            End If

        Next
        'strg = "SELECT jawaban_user, skor, app_sta, soal FROM el_tbl_nilai inner join el_tbl_soal ON el_tbl_nilai.id_soal = el_tbl_soal.id_soal INNER join el_tbl_paket on el_tbl_paket.id_paket = el_tbl_soal.id_paket WHERE el_tbl_paket.jenis_test = 'pre test' AND id_mquiz = ' " + Request.QueryString("id_mquiz") + "'"
        'strg2 = "SELECT jawaban_user, skor, app_sta, soal FROM el_tbl_nilai inner join el_tbl_soal ON el_tbl_nilai.id_soal = el_tbl_soal.id_soal INNER join el_tbl_paket on el_tbl_paket.id_paket = el_tbl_soal.id_paket WHERE el_tbl_paket.jenis_test = 'post test' AND id_mquiz = ' " + Request.QueryString("id_mquiz") + "'"

    End Sub

    Protected Function GetItemStyle(dataItem As Object) As String
        Dim style As String = ""

        ' Logika untuk menentukan gaya teks berdasarkan data item
        Dim itemValue As String = dataItem.ToString()

        If itemValue = "Correct" Then
            style = "color: green;"
        ElseIf itemValue = "Incorrect" Then
            style = "color: red;"
        End If

        Return style
    End Function
End Class