<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="coba2.aspx.vb" Inherits="e_learning.coba2" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div runat="server" id="content">
        <img src="img/sertif.png" alt="Gambar" class="image">
  <div class="text">
    Diberikan kepada:
  </div>
  <div class="text2" id="nama" runat="server" >
    Username
  </div>
  <div class="text3">
    Atas Partisipasinya Sebagai

  </div>
  <div class="text4">
    PESERTA


  </div>
  <div class="text5" runat="server" id="judul" >
     “Judul Training”
    



  </div>
    <div class="text6" runat="server" id="tgl">
      yang diselenggarakan pada tanggal 11 Maret 2005



    </div>
    <div class="text8">
        Eka Kurniawan

    </div>
    
    <div class="text9">
        HR & GA Manager

    </div>
    <%--<div class="text10" runat="server" id="lblid">
      




    </div>
   --%>
    </div>
  

</asp:Content>