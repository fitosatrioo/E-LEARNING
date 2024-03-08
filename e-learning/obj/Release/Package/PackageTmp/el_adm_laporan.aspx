<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_laporan.aspx.vb" Inherits="e_learning.el_adm_laporan" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" OnCustomColumnDisplayText="ASPxGridView1_CustomColumnDisplayText">
        <Columns>
            <dx:GridViewDataTextColumn Caption="id_hasil" FieldName="id_hasil" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jabatan" FieldName="jabatan" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nilai Pre Test" FieldName="nilai_pre" Width="75px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Kategori Pre Test"  Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tanggal Pelaksanaan Pre Test" FieldName="c_date" Width="125px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nilai Post Test" FieldName="nilai_post" Width="85px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Kategori Post Test" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tanggal Pelaksanaan Post Test" FieldName="u_date" Width="125px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Penyerapan Materi"  Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Kategori Penyerapan Materi"  Width="100px" >

            </dx:GridViewDataTextColumn>

        </Columns>
    </dx:ASPxGridView>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
