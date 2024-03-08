<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_hasil_user.aspx.vb" Inherits="e_learning.el_hasil" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewDataTextColumn Caption="id_mquiz" FieldName="id_mquiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul" FieldName="judul" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama Ujian" FieldName="paket" Width="100px" >

            </dx:GridViewDataTextColumn>

             <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataDateColumn  Caption="Tanggal Mengerjakan" FieldName="c_date" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="Skor" FieldName="skor" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Jawaban" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='Lihat jawaban' NavigateUrl='<%#String.Format("~/el_adm_nilai.aspx?idhasil={0}&sumber={1}", Eval("id_hasil"), Eval("jenis")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewCommandColumn Caption="Approve" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>
            
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
