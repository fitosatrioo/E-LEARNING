<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_quiz.aspx.vb" Inherits="e_learning.adm_quiz" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <uc1:uc_header runat="server" id="uc_header" />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_quiz" FieldName="id_quiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="nama_quiz" FieldName="nama_quiz" Width="180px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="id dept" FieldName="id_dept" Width="100px" Visible="false">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Daftar Soal"  Width="100px">
              <DataItemTemplate>
                  <dx:ASPxHyperLink ID="ASPxHyperLink1" Target="_blank" runat="server" Text='<%#String.Concat(Eval("jumlah"), " Soal") %>' NavigateUrl='<%#String.Format("~/adm_soal.aspx?idrec={0}", Eval("id_quiz")) %>'>
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
    <div runat="server" id="coba"></div>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
