<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="clearsession.aspx.vb" Inherits="e_learning.clearsession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>

        window.addEventListener('DOMContentLoaded', function () {
           
            sessionStorage.clear();
        });
    </script>
</asp:Content>
