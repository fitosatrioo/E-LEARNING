Imports System.Data.SqlClient
Public Class file_materi
    Inherits System.Web.UI.Page
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim salah As er_custom
    Dim str As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            'Me.isi_data()
            Me.BindRepeater()
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

        Repeater1.DataSource = dt1
        Repeater1.DataBind()
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub

End Class