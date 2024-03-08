Imports System.Data.SqlClient
Imports DevExpress.Web
Imports System.Net.Mail
Imports DevExpress.Web.ASPxPivotGrid

Module Mod_Utama

    Public Structure er_custom
        Public er_str As String
        Public er_hasil As String
        Public er_menu As String
        Public er_page As String
        Public er_tquery As Stopwatch
        Public er_tpage As Stopwatch
        Public er_id As Int64
        Public er_waktu As String
        Public er_sql As Integer
    End Structure

    Public Structure user_autho
        Public lihat As String
        Public baru As String
        Public ubah As String
        Public hapus As String
        Public spv As String
        Public mgr As String
        Public fin As String
        Public coo As String
        Public oth As String
        Public staf As String
    End Structure

    ''---SERVER
    Public sql_str = "Data Source=DESKTOP-VIOR32L;Initial Catalog=elearning;Persist Security Info=True;User ID=sa;Password=123456"
    Public sql_cms_act = "Data Source=DESKTOP-13K4MQB\SQLEXPRESS;Initial Catalog=CMS_ACT;Persist Security Info=True;User ID=sa;Password=123456"

    '---LOCAL
    'Public sql_str = "Data Source=DESKTOP-13K4MQB\SQLEXPRESS;Initial Catalog=elearning;Persist Security Info=True;User ID=sa;Password=123456"
    'Public sql_cms_act = "Data Source=DESKTOP-13K4MQB\SQLEXPRESS;Initial Catalog=CMS_ACT;Persist Security Info=True;User ID=sa;Password=123456"

    Public rc_max As Integer = 1000
    Public rc_day As Integer = 7
    Public page_login As String = "http://agungcartrans.co.id:1025/"

    Dim str As String

    Public Function isi_data(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As er_custom
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)
        Dim swtime As Integer = sw.Elapsed.TotalMilliseconds

        dt.Clear()
        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            If Not sw Is Nothing Then sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.er_hasil = ex.Message
        Finally
            sql_cont.Close()
            If Not sw Is Nothing Then sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        HttpContext.Current.Session("time_query") &= dt.TableName & " <" & sw.Elapsed.TotalMilliseconds - swtime & ">" & Chr(13) & Chr(10)

        Return hasil
    End Function

    Public Function isi_data_cms(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As er_custom
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_cms_act)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)
        Dim swtime As Integer = sw.Elapsed.TotalMilliseconds

        dt.Clear()
        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            If Not sw Is Nothing Then sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.er_hasil = ex.Message
        Finally
            sql_cont.Close()
            If Not sw Is Nothing Then sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        HttpContext.Current.Session("time_query") &= dt.TableName & " <" & sw.Elapsed.TotalMilliseconds - swtime & ">" & Chr(13) & Chr(10)

        Return hasil
    End Function

    Public Function isi_data_notime(dt As DataTable, str As String, clmn_nm As String) As String
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        dt.Clear()
        Try
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            Return ex.ToString
        Finally
            sql_cont.Close()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        Return ""
    End Function

    Public Function isi_data_noclear(dt As DataTable, str As String, clmn_nm As String, sw As Stopwatch) As er_custom
        Dim sql_cont As New SqlConnection(Mod_Utama.sql_str)
        Dim sql_adpt As New SqlDataAdapter(str, sql_cont)

        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            sw.Start()
            sql_cont.Open()
            sql_adpt.Fill(dt)
        Catch ex As Exception
            hasil.er_hasil = ex.Message
        Finally
            sql_cont.Close()
            sw.Stop()
        End Try

        If Not clmn_nm = "" Then
            dt.PrimaryKey = New DataColumn() {dt.Columns(clmn_nm)}
        End If

        Return hasil
    End Function

    Public Function exec_sql(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As String
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        If Not druser Is Nothing Then
            Mod_Utama.log_user(druser, "TQA", "Exec : " & str, hal)
        End If

        Return ""
    End Function

    Public Function exec_cms_act(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As String
        Dim cont As New SqlConnection(Mod_Utama.sql_cms_act)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

    Public Function exec_sql_id(str As String, Optional ByVal druser As DataRow = Nothing, Optional ByVal hal As String = "") As er_custom
        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Dim hasil As New er_custom
        hasil.er_str = str
        hasil.er_id = 0

        Try
            cont.Open()
            hasil.er_id = cmnd.ExecuteScalar()
            hasil.er_hasil = ""
        Catch ex As Exception
            hasil.er_hasil = ex.ToString
            hasil.er_id = -1
            Return hasil
        Finally
            cont.Close()
        End Try

        If Not druser Is Nothing Then
            Mod_Utama.log_user(druser, "TQA", "Exec : " & str, hal)
        End If

        Return hasil
    End Function

    Public Function hasil_gambar(dr As DataRow) As String
        Try
            Dim bytes As Byte() = DirectCast(dr("photo"), Byte())
            Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            Return Convert.ToString("data:image/png;base64,") & base64String
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub tampil_error(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { theme: 'growl-error', header: 'Error !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_error", js, True)
    End Sub

    Public Sub tampil_sukses(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { theme: 'growl-success', header: 'Success !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_sukses", js, True)
    End Sub

    Public Sub tampil_pesan(hal As Page, pesan As String)
        Dim js As String = "$.jGrowl('" & pesan & "', { header: 'Notification !', life: 10000 });"
        hal.ClientScript.RegisterStartupScript(GetType(String), "tampil_pesan", js, True)
    End Sub

    Public Function master_waktu(sw_query As Stopwatch, sw_page As Stopwatch, hal As UserControl) As String
        Dim str As String = ""

        Try
            str = " Q: <font color='red'><b>"
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_query.Elapsed.TotalMinutes,
                                sw_query.Elapsed.Seconds,
                                 sw_query.Elapsed.Milliseconds)
            str = str & "</b></font> Sec "
            str = str & " | P: <font color='red'><b>"
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_page.Elapsed.TotalMinutes,
                                sw_page.Elapsed.Seconds,
                                 sw_page.Elapsed.Milliseconds)
            str = str & "</b></font> Sec "
            str = "2016. MIS ACTrans Company [" & str & "]"
        Catch ex As Exception
            str = "[ No Time ]"
        End Try

        Dim pagehand As Page = HttpContext.Current.Handler
        If pagehand Is Nothing Then GoTo SKIP
        If pagehand.IsPostBack = True Or pagehand.IsCallback = True Then GoTo SKIP
        If sw_query.Elapsed.Seconds > 4 Or sw_page.Elapsed.Seconds > 6 Then
            Dim strqry As String = ""
            Dim druser As DataRow = HttpContext.Current.Session("dr_user")
            strqry = "insert into log_query ("
            strqry = strqry & "waktu, web_nm, page_nm, query_scnd, page_scnd, "
            strqry = strqry & "c_user) "
            strqry = strqry & "values ("
            strqry = strqry & "getdate(), "
            strqry = strqry & "'TQA', "
            strqry = strqry & "'" & HttpContext.Current.Request.FilePath & "', "
            strqry = strqry & "'" & sw_query.Elapsed.Seconds & "', "
            strqry = strqry & "'" & sw_page.Elapsed.Seconds & "', "
            strqry = strqry & "'" & druser("nama") & "') "
            Dim hasil As String = exec_sql(strqry)
        End If

SKIP:

        Dim mpLabel As System.Web.UI.HtmlControls.HtmlGenericControl
        mpLabel = CType(hal.FindControl("lb_time"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If Not mpLabel Is Nothing Then
            mpLabel.InnerHtml = str
        End If

        HttpContext.Current.Session("waktu_query") = sw_query
        HttpContext.Current.Session("waktu_page") = sw_page

        Return str
    End Function

    Public Function str_waktu(sw_query As Stopwatch, sw_page As Stopwatch) As String
        Dim str As String = ""

        Try
            str = " Query Time : "
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_query.Elapsed.TotalMinutes,
                                sw_query.Elapsed.Seconds,
                                 sw_query.Elapsed.Milliseconds)
            str = str & " Seconds "
            str = str & " // Page Time : "
            str = str & String.Format("{0:00}:{1:00}.{2:000}", sw_page.Elapsed.TotalMinutes,
                                sw_page.Elapsed.Seconds,
                                 sw_page.Elapsed.Milliseconds)
            str = str & " Seconds "
            str = "[" & str & "]"
        Catch ex As Exception
            'str = "[ No Time ]"
            str = ex.ToString
        End Try

        Return str
    End Function

    Public Function send_mail(from_add As String, to_add As String, subject As String, body As String) As Boolean
        Dim mail As New System.Net.Mail.MailMessage
        Dim smtpClient As New System.Net.Mail.SmtpClient("mail.agungcartrans.co.id")

        Try
            mail.To.Add(to_add)
            mail.From = New Net.Mail.MailAddress(from_add)
            mail.Subject = subject
            mail.Body = body

            smtpClient.Send(mail)
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return False
        End Try

        Return True
    End Function

    Public Sub Atur_Grid(grid As ASPxGridView, Optional usecustomcallback As Boolean = False)
        If usecustomcallback = True Then
            grid.ClientSideEvents.CustomButtonClick = "function (s, e) { e.processOnServer = false; s.PerformCallback(e.buttonID+';'+s.GetRowKey(e.visibleIndex)); }"
            grid.ClientSideEvents.EndCallback = "function (s, e) { tampil_pesan(s.cpTitle, s.cpContent); delete s.cpTitle; }"
        End If

        grid.Settings.ShowGroupPanel = True
        grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center
        grid.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True
        grid.StylesEditors.ReadOnly.BackColor = Drawing.Color.LightPink
        grid.SettingsText.ConfirmDelete = "Yakin untuk hapus record ini ?"
        grid.SettingsBehavior.ConfirmDelete = True
        grid.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control
        grid.SettingsBehavior.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom

        grid.Settings.ShowHeaderFilterButton = True
        grid.Settings.ShowHeaderFilterBlankItems = True
        grid.SettingsPopup.HeaderFilter.Height = 300
        grid.SettingsPopup.HeaderFilter.Width = 300
        'grid.Styles.CommandColumn.Spacing = 6
        grid.SettingsBehavior.EnableCustomizationWindow = True

        grid.Settings.ShowFilterRow = True
        grid.SettingsPager.Position = PagerPosition.TopAndBottom
        grid.SettingsPager.PageSizeItemSettings.Visible = True

        AddHandler grid.HtmlRowPrepared, AddressOf Grid_HtmlRowPrepared

        Dim lebar As Integer = 0
        Dim clm As GridViewDataColumn
        For i = 0 To grid.Columns.Count - 1
            If grid.Columns(i).Visible = True Then
                lebar += grid.Columns(i).Width.Value
            End If

            Select Case grid.Columns(i).GetType
                Case GetType(GridViewCommandColumn)
                    Dim cmd As GridViewCommandColumn = grid.Columns(i)
                    'cmd.ClearFilterButton.Visible = True
                    GoTo SKIP
                Case GetType(GridViewBandColumn)
                    GoTo SKIP
            End Select

            Try
                clm = grid.Columns(i)
            Catch ex As Exception
                GoTo SKIP
            End Try

            clm = grid.Columns(i)
            If clm.GetType = GetType(GridViewDataHyperLinkColumn) Then
                clm.Settings.FilterMode = ColumnFilterMode.DisplayText
                clm.Settings.AutoFilterCondition = AutoFilterCondition.Contains
            Else
                clm.Settings.FilterMode = ColumnFilterMode.Value
            End If

            clm.Settings.HeaderFilterMode = HeaderFilterMode.CheckedList
            clm.Settings.AutoFilterCondition = AutoFilterCondition.Contains
            clm.Settings.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText
            clm.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True

SKIP:
        Next

        grid.Width = lebar
    End Sub

    Private Sub Grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Select Case e.RowType
            Case GridViewRowType.Preview
                e.Row.BackColor = HttpContext.Current.Session("warna")
            Case GridViewRowType.Data, GridViewRowType.Detail
                If Trim(e.Row.BackColor.ToString).Replace(" ", "") = "Color[Empty]" Then
                    HttpContext.Current.Session("warna") = System.Drawing.Color.White
                Else
                    HttpContext.Current.Session("warna") = e.Row.BackColor
                End If
        End Select
    End Sub

    Public Sub Atur_pivot(pivot As ASPxPivotGrid)
        pivot.OptionsPager.AllButton.Visible = True
        pivot.OptionsPager.ShowSeparators = True
        pivot.OptionsPager.PageSizeItemSettings.Visible = True
        pivot.OptionsPager.PageSizeItemSettings.Position = DevExpress.Web.PagerPageSizePosition.Right
        pivot.OptionsChartDataSource.MaxAllowedSeriesCount = 100
        pivot.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
        pivot.OptionsChartDataSource.CurrentPageOnly = False
        'pivot.OptionsPager.RenderMode = DevExpress.Web.ControlRenderMode.Lightweight

        For i = 0 To pivot.Fields.Count - 1
            Select Case pivot.Fields(i).DataType
                Case GetType(Decimal)
                    pivot.Fields(i).CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    pivot.Fields(i).CellFormat.FormatString = "#,###"
                Case GetType(Date)
                    pivot.Fields(i).ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    pivot.Fields(i).ValueFormat.FormatString = "yyyy-MM-dd"
            End Select
        Next
    End Sub

    Public Sub clear_pivot(pivot As ASPxPivotGrid)
        For i = 0 To pivot.Fields.Count - 1
            pivot.Fields(i).Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
        Next
    End Sub

    Public Function to_null(nilai) As String
        If nilai Is Nothing Then Return "NULL"
        If nilai Is DBNull.Value Then Return "NULL"
        If IsDBNull(nilai) Then Return "NULL"
        If nilai.ToString = "" Then Return "NULL"
        If nilai.ToString = "0" Then Return "NULL"

        Return nilai.ToString
    End Function

    Public Function log_user(druser As DataRow, web As String, ket As String, hal As String) As String
        Dim ket_str As String = Replace(ket, "'", "''")

        str = "insert into log_user ("
        str = str & "waktu, id_user, nm_user, web, module, ket) "
        str = str & "values ("
        str = str & "getdate(), "
        str = str & "'" & druser("id") & "', "
        str = str & "'" & druser("nama") & "', "
        str = str & "'" & web & "', "
        str = str & "'" & hal & "', "
        str = str & "'" & ket_str & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

    Public Sub insert_error(msg As String, strquery As String)
        Dim ket_str As String = Replace(strquery, "'", "''")

        str = "insert into opr_logerror ("
        str = str & "tgl, msg, str, hal) "
        str = str & "values ("
        str = str & "getdate(), "
        str = str & "'" & msg & "', "
        str = str & "'" & ket_str & "', "
        str = str & "'" & HttpContext.Current.ToString & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            MsgBox(str)
        Finally
            cont.Close()
        End Try
    End Sub

    Public Function dttbl_xml(dtt As DataTable) As String
        dtt.TableName = "teat"
        Dim xmlstr As New IO.StringWriter
        dtt.WriteXml(xmlstr)
        Return xmlstr.ToString
    End Function

    Public Function log_delete(sumbersql As String, tblnm As String, druser As DataRow) As String
        Dim method = New StackTrace().GetFrame(1).GetMethod().Name
        Dim str As String = ""
        Dim dtemp As New DataTable
        str = sumbersql
        Dim swload As New Stopwatch

        Mod_Utama.isi_data(dtemp, str, "", swload)
        Dim hasil As String = ""
        hasil = Mod_Utama.dttbl_xml(dtemp)

        str = "insert into log_delete "
        str = str & "(xml, table_nm, web, module, page, c_date, c_user) values ("
        str = str & "'" & hasil & "', "
        str = str & "'" & tblnm & "', "
        str = str & "'TQA', "
        str = str & "'" & method.ToString & "', "
        str = str & "'" & HttpContext.Current.CurrentHandler.ToString & "', "
        str = str & "getdate(), "
        str = str & "'" & druser("nama") & "') "

        Dim cont As New SqlConnection(Mod_Utama.sql_str)
        Dim cmnd As New SqlCommand(str, cont)

        Try
            cont.Open()
            cmnd.ExecuteScalar()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return ex.Message
        Finally
            cont.Close()
        End Try

        Return ""
    End Function

End Module
