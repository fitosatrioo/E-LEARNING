<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_edit_file_materi.aspx.vb" Inherits="e_learning.el_edit_file_materi" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    <div class="container">
        <h1>Form Materi</h1>
        
            <div class="form-group">
                <label for="judulMateri">Judul Materi</label>
                <input type="text" class="form-control" id="txjudul" placeholder="Masukkan judul materi" runat="server" required="required"/>
            </div>
            
            <div class="form-group">
                <label for="pilihan">Pilihan</label>
                <select class="form-control" id="cb_pilihan" runat="server">
                     <option value="pdf">PDF</option>
                      <option value="ppt">PPT</option>
                      <option value="link">Link</option>
                      <option value="link_youtube">Link Youtube</option>
                </select>
            </div>
            
            <div class="form-group" id="linkContainer"  >
                <label for="link">Masukkan Link</label>
                <input type="text" class="form-control" id="txlink" placeholder="Masukkan link" runat="server"/>
            </div>
            
            <div class="form-group" id="fileContainer"  >
                <label for="file">Pilih File</label>
                <input type="file" class="form-control-file" id="txfile" runat="server"/>
                <div class="form-group">
                    <label>Nama File Sebelumnya : <asp:Label runat="server" ID="lb_file"></asp:Label></label>
                </div>
            </div>
        <input type="hidden" id="txtid" runat="server"/>
        <input type="hidden" id="txtnamafile" runat="server"/>
            
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="Unnamed_ServerClick"    >Submit</button>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.min.js"></script>
    <script>
       $(document).ready(function () {
            $('#<%= cb_pilihan.ClientID %>').change(function () {
                if ($(this).val() === "pdf") {
                    $('#linkContainer').hide();
                    $('#fileContainer').show();
                } else if ($(this).val() === "ppt") {
                    $('#linkContainer').hide();
                    $('#fileContainer').show();
                } else if ($(this).val() === "link") {
                    $('#linkContainer').show();
                    $('#fileContainer').hide();
                } else if ($(this).val() === "link_youtube") {
                    $('#linkContainer').show();
                    $('#fileContainer').hide();
                } else {
                    $('#linkContainer').hide();
                    $('#fileContainer').hide();
                }
            });
        });
    </script>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
