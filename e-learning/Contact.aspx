<%@ Page Title="Contact" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="e_learning.Contact" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" id="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_ruang" FieldName="id_ruang" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="no_ruang" FieldName="no_ruang" Width="80px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="nama_ruang" FieldName="nama_ruang" Width="150px">

            </dx:GridViewDataTextColumn>

                </Columns>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" id="uc_footer" />
</asp:Content>

