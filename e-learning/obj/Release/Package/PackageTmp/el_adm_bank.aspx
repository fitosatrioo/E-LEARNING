<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_bank.aspx.vb" Inherits="e_learning.el_adm_bank" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center">Bank Soal</h3>
    <br />
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_paket" FieldName="id_paket" Visible="false">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Paket Soal" FieldName="paket_soal" Width="180px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul Training" FieldName="judul_training" Width="180px">

            </dx:GridViewDataTextColumn>

             <dx:GridViewDataSpinEditColumn Caption="Waktu" FieldName="waktu" Width="100px">
                 <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                 <EditFormSettings Caption="Jumlah Waktu (Menit):"></EditFormSettings>
             </dx:GridViewDataSpinEditColumn>


            <dx:GridViewDataSpinEditColumn Caption="Nilai Minimal" FieldName="kkm" Width="100px">
                 <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                 <EditFormSettings Caption="Nilai Minimal:"></EditFormSettings>
            </dx:GridViewDataSpinEditColumn>


            <dx:GridViewDataComboBoxColumn  Caption="Jenis Test" FieldName="jenis_test" Width="180px">
                <PropertiesComboBox>
                    <Items>
                    <dx:ListEditItem Text="Post Test" Value="POST TEST" />
                    <dx:ListEditItem Text="Pre Test" Value="PRE TEST" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Soal" ReadOnly="true" Width="100px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5" ReadOnly="true" runat="server" Text='<%#String.Concat(Eval("soal"), " Soal") %>' NavigateUrl='<%#String.Format("~/el_adm_soal.aspx?idrec={0}", Eval("id_paket")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="70px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="70px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
   
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
