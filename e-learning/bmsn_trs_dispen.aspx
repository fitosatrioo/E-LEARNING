<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="bmsn_trs_dispen.aspx.vb" MasterPageFile="~/Site1.Master" Inherits="EBMS.bmsn_trs_dispen" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="alert alert-info">
        <span>Section Dispen harus sama dengan Section pada BSO yang akan diajukan.
        </span>
    </div>
    <br />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Theme="MaterialCompact">
        <Columns>
            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True" ShowEditButton="True" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataHyperLinkColumn Caption="ID" FieldName="id_dispen" ReadOnly="True" VisibleIndex="1" Width="80px">
                <PropertiesHyperLinkEdit NavigateUrlFormatString="~\bmsn_trs_dispen_dtl.aspx?id_dispen={0}" TextFormatString="Dtl. {0}">
                </PropertiesHyperLinkEdit>
                <Settings FilterMode="DisplayText" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>
            <dx:GridViewDataTextColumn Caption="No Memo" FieldName="no_memo" ReadOnly="True" VisibleIndex="2" Width="150px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Memo" FieldName="tgl" ReadOnly="True" VisibleIndex="3" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Pemohon" FieldName="pemohon" ReadOnly="true" VisibleIndex="4" Width="90px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Section" FieldName="id_section" VisibleIndex="5" Width="120px">
                <PropertiesComboBox>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Departement" FieldName="id_dept" ReadOnly="true" VisibleIndex="6" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Tanggal Selesai Dispensasi" FieldName="tgl_selesai" ReadOnly="true" VisibleIndex="7" Width="100px">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesDateEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataMemoColumn Caption="Keterangan" FieldName="ket" VisibleIndex="8" Width="150px">
            </dx:GridViewDataMemoColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="MGR" VisibleIndex="1" Width="60px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_mgr" Text="MGR">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="FIN" VisibleIndex="1" Width="80px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bt_fin" Text="FIN">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataHyperLinkColumn Caption="Files" FieldName="id_dispen" VisibleIndex="9" Width="100px" ReadOnly="True">
                <EditFormSettings Visible="False" />
                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/trs_files.aspx?service=DBSO&idrec={0}" TextField="jml_files" TextFormatString="{0} Files">
                </PropertiesHyperLinkEdit>
            </dx:GridViewDataHyperLinkColumn>
        </Columns>
        <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grid_detail" runat="server" OnBeforePerformDataSelect="grid_detail_BeforePerformDataSelect" AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="id_dispen_dtl" VisibleIndex="0" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="No BSO" FieldName="no_bso" VisibleIndex="1" Width="140px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="Tanggal Expired" FieldName="tgl_awal" ReadOnly="true" VisibleIndex="2" Width="100px">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="Tanggal Dispensasi" FieldName="tgl_baru" ReadOnly="true" VisibleIndex="3" Width="100px">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataMemoColumn Caption="Alasan" FieldName="alasan" VisibleIndex="4" Width="150px">
                        </dx:GridViewDataMemoColumn>
                    </Columns>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <Templates>
            <PreviewRow>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <font color="black"><strong>APPROVAL : MGR</strong>(<%#GetTgl(Container, "staf_date", "staf_user")%>)</font>
                        </td>
                        <td style="text-align: right">
                            <font color="black"><strong>APPROVAL : CC</strong>(<%#GetTgl(Container, "fin_date", "fin_user")%>)</font>
                        </td>
                    </tr>
                </table>
            </PreviewRow>
        </Templates>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
