<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="e_learning.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .body{
            margin-top:20px;
            width:auto;

        }
    </style>
    <div class="body">
         <iframe src="file/Presentasi_elearning.pdf" runat="server" width="1050px" height="500px"></iframe>
    </div>
</asp:Content>
