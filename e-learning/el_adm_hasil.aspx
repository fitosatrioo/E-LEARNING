<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_hasil.aspx.vb" Inherits="e_learning.el_adm_hasil" %>

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

            <dx:GridViewDataHyperLinkColumn Caption="PRE TEST" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#Eval("pre") %>' NavigateUrl='<%#String.Format("~/el_hasil_user.aspx?idrec={0}&id_mquiz={1}&sumber={2}", Eval("pre_test"), Eval("id_mquiz"), "pre") %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="POST TEST" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#Eval("post") %>' NavigateUrl='<%#String.Format("~/el_hasil_user.aspx?idrec={0}&id_mquiz={1}&sumber={2}", Eval("id_soal"), Eval("id_mquiz"), "post") %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Hasil Keseluruhan" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='Hasil' NavigateUrl='<%#String.Format("~/el_adm_laporan.aspx?idrec={0}&idlvl={1}&id_dept={2}", Eval("id_mquiz"), Eval("level"), Eval("id_dept")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Aktivitas Pengguna" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='Aktivitas Pengguna' NavigateUrl='<%#String.Format("~/el_adm_log.aspx?idrec={0}", Eval("id_mquiz")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akses" FieldName="tgl_akses" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akhir" FieldName="tgl_akhir" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

           
        </Columns>
    </dx:ASPxGridView>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
<%--<Columns>
            
            <dx:GridViewDataTextColumn Caption="id_paket" FieldName="id_paket" Visible="false">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Paket Soal" FieldName="paket_soal" Width="180px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul Training" FieldName="judul_training" Width="180px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jenis Test" FieldName="jenis_test" Width="180px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Soal"  Width="100px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5" Target="_blank" runat="server" Text='<%#String.Concat(Eval("soal"), " Soal") %>' NavigateUrl='<%#String.Format("~/el_adm_nilai.aspx?idrec={0}", Eval("id_paket")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" >
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" >
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="70px" FieldName="u_date">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="70px" FieldName="u_user">
                   <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
        </Columns>--%>