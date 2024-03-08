<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="el_adm_edit_materi.aspx.vb" Inherits="e_learning.el_adm_edit_materi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="../css/londinium-theme.css" type="text/css" rel="stylesheet" />
    <link href="../css/styles.min.css" type="text/css" rel="stylesheet" />
    <link href="../css/icons.min.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
        </div>
    </form>
</body>
</html>
