<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_level.aspx.vb" Inherits="e_learning.el_adm_level" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" id="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" Width="80px">
                
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="id" FieldName="id" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama Level" FieldName="nama_level" Width="200px">

            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <p>Pastikan Level All Memiliki ID = 1</p>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
