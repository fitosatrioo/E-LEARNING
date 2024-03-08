<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_materi.aspx.vb" Inherits="e_learning.adm_materi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center">Menu Materi</h3>
    <br />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id" FieldName="id_materi" ReadOnly="true"> 
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="judul Training" FieldName="judul_materi" Width="280px"> </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataHyperLinkColumn Caption="Upload Materi" Width="200px" ReadOnly="true">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Text='<%#String.Concat(Eval("image"), " Materi") %>' NavigateUrl='<%#String.Format("~/el_adm_file_materi.aspx?idrec={0}", Eval("id_materi")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>
            
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
