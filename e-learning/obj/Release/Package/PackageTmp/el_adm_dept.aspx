<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_dept.aspx.vb" Inherits="e_learning.adm_dept" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center">Manajemen Departement</h3>
    <br />

    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="ID Departement" FieldName="id_dept" ReadOnly="true" Width="180px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama Departement" FieldName="dept_name" Width="220px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="100px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="100px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="100px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="100px" FieldName="u_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <p>Pastikan Departement ALL Memiliki ID = 1</p>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
