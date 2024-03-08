Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Reflection.MethodBase
Public Class uc_menu
    Inherits System.Web.UI.UserControl
    Dim dr_user As DataRow
    Dim str As String
    Dim strg As String
    Dim salah As er_custom
    Dim waktu_query As New Stopwatch
    Dim waktu_page As New Stopwatch
    Dim dt As New DataTable
    Dim str_menu As String = ",2,"
    Dim dr As DataRow
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dr_user = Session("dr_user")

        'If CStr(dr_user("level")).Contains("1") = True Then
        '    Me.divuser.Visible = False
        'ElseIf CStr(dr_user("level")).Contains("2") = True Then
        '    Me.divuser.Visible = False
        'ElseIf CStr(dr_user("level")).Contains("3") = True Then
        '    Me.divuser.Visible = False
        'Else
        '    Response.Redirect("pilihan_materi.aspx")
        'End If

        'If CStr(dr_user("role")).Contains("Staff") = False Then
        '    Me.divuser.Visible = False
        'End If


    End Sub

    Private Sub uc_menu_Init(sender As Object, e As EventArgs) Handles Me.Init

    End Sub
    Private Sub isi_data()


    End Sub
End Class