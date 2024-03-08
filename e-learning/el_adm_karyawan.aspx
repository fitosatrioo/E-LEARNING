<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_karyawan.aspx.vb" Inherits="e_learning.adm_elarning" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center">Manajemen Karyawan</h3>
    <br />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id" FieldName="id" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataSpinEditColumn Caption="NIK" FieldName="nik" Width="100px">
                <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                <EditFormSettings Caption="NIK:"></EditFormSettings>
            </dx:GridViewDataSpinEditColumn>


            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" Width="100px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Username" FieldName="username" Width="100px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Password" FieldName="password" Width="100px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Email" FieldName="email" Width="100px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jabatan" FieldName="jabatan" Width="100px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataSpinEditColumn Caption="No Telp" FieldName="no_telp" Width="100px">
                <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                <EditFormSettings Caption="No Telp:"></EditFormSettings>
            </dx:GridViewDataSpinEditColumn>



            <dx:GridViewDataComboBoxColumn Caption="Departement" FieldName="departement" Width="100px">

                <PropertiesComboBox>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Level" FieldName="level" Width="100px">
                <PropertiesComboBox>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
