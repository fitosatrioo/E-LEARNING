<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_nilai.aspx.vb" Inherits="e_learning.el_adm_nilai" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px"  ShowEditButton="true"  >
                <HeaderStyle CssClass="edit-button-header"></HeaderStyle>
            <CellStyle CssClass="edit-button-cell"></CellStyle>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_nilai" FieldName="id_nilai" ReadOnly="true">
            </dx:GridViewDataTextColumn>

             <dx:GridViewDataTextColumn Caption="Nama" FieldName="nama" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Soal" FieldName="soal" Width="200px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Kunci Jawaban" FieldName="jawaban" Width="200px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jawaban User" FieldName="jawaban_user" Width="200px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jenis Soal" FieldName="jenis_soal" Width="50px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Skor" FieldName="skor" Width="50px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn Caption="Approve" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="app_sta">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
    
   
</asp:Content>
