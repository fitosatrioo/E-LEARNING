Imports System.Data.SqlClient
Public Class el_pilihan_materi
    Inherits System.Web.UI.Page
    Dim dr_user As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dr_user = Session("dr_user")
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
        Dim cmd As SqlCommand = New SqlCommand("SELECT * FROM tbl_departement ", conn)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)

        Dim dt1 As DataTable = New DataTable()

        sda.Fill(dt1)

        Repeater1.DataSource = dt1
        Repeater1.DataBind()
    End Sub

End Class