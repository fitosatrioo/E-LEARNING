<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_log.aspx.vb" Inherits="e_learning.el_adm_log" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center">Aktivitas Pengguna</h3>
    <br />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Aktivitas" FieldName="history" Width="200px">

            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tanggal Akses" FieldName="c_date" Width="150px">

            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nickname" Width="150px">

            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Sumber" FieldName="sumber" Width="150px">

            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
