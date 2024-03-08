<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_manajemen_quiz.aspx.vb" Inherits="e_learning.el_adm_manajemen_quiz" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h3 class="text-center">Manajemen Quiz</h3>
    <br />
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_mquiz" FieldName="id_mquiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul" FieldName="judul" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Pre Test" FieldName="pre_test" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Materi" FieldName="id_materi" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Post Test" FieldName="id_soal" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akses" FieldName="tgl_akses" Width="100px">
                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akhir" FieldName="tgl_akhir" Width="100px">

            </dx:GridViewDataDateColumn>

            <dx:GridViewDataComboBoxColumn Caption="Level" FieldName="level" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <%--<dx:GridViewDataHyperLinkColumn Caption="Sertifikat" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='<%#String.Concat(Eval("sertif"), " sertif") %>' NavigateUrl='<%#String.Format("~/el_adm_sertifikat.aspx?idrec={0}", Eval("id_mquiz")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>--%>

            <dx:GridViewDataTextColumn Caption="u_user" FieldName="c_date" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="u_date" FieldName="c_user" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn Caption="Approve" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>

            <dx:GridViewCommandColumn Caption="Approve Otomatis" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app2">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
