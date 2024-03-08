<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="coba2.aspx.vb" Inherits="e_learning.coba2" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div class="container mt-4">
        <asp:Label ID="lblData" runat="server" Text="" Visible="true"></asp:Label>
            <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                       <div class="card mb-4 mt-3">
      <div class="card-body">
        <h5 class="card-title" id="lbljudul" runat="server"></h5>
              </ItemTemplate>   

                <div class="col-xs-6" id="su57" runat="server">
                   
                </div>
                <div class="col-xs-6" >
                    <div>
                        <div style='max-width: 600px; margin: 0 auto; margin-top: 50px;' class='card text-center'>
                            <div class="card-body">
                                <label runat="server" id="lblbc" style="color: green;" >Telah Dibaca</label>
                                <button type="button" class="btn btn-success" runat="server" id="btn_log">Tandai Telah Selesai Baca</button>
                            </div>
                        </div>
                    </div>
                    
                </div>
        
      </div>
    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click"/>
        <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click"/>
    </div>
</asp:Content>