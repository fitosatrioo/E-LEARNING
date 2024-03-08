Public Class register1
    Inherits System.Web.UI.Page
    Dim str As String
    Dim salah As er_custom

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Unnamed_ServerClick(sender As Object, e As EventArgs)

    End Sub

    Protected Sub bt_login_ServerClick(sender As Object, e As EventArgs)
        If tx_pass.Value <> tx_pass2.Value Then
            Me.lb_hasil.InnerText = "password tidak sama"
        Else
            str = "INSERT INTO tbl_login ("
            str = str & "id, username, password, "
            str = str & "nama, email,level,departement,jabatan,nik,no_telp ) VALUES ("
            str = str & "(select isnull(max(id),0)+1 from tbl_login),"
            str = str & "'" & Me.tx_user.Value & "', "
            str = str & "'" & Me.tx_pass2.Value & "', "
            str = str & "'" & Me.tx_nm.Value & "', "
            str = str & "'" & Me.tx_em.Value & "', "
            str = str & "'" & Me.cb_level.Value & "', "
            str = str & "'" & Me.cb_dept.Value & "', "
            str = str & "'" & Me.tx_jabatan.Value & "', "
            str = str & "'" & Me.tx_nik.Value & "', "
            str = str & "'" & Me.tx_telp.Value & "') "
            salah.er_hasil = Mod_Utama.exec_sql(str)

            If salah.er_hasil <> "" Then
                Me.lb_hasil.InnerText = "Gagal Register
"
            Else
                Response.Redirect("login.aspx")
            End If

        End If
    End Sub
End Class