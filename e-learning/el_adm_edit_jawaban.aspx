<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="el_adm_edit_jawaban.aspx.vb" Inherits="e_learning.el_adm_edit_jawaban" %>

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
       <div class="form-group" >
            <label for="textInput">Opsi</label>
            <input type="text" class="form-control" runat="server" id="txopsi"/>
          </div>
          <div class="form-group">
            <label for="textarea2">Jawaban:</label>
            <textarea class="form-control" id="txtjawaban" rows="3" runat="server"></textarea>
          </div>
          <div class="form-group">
            <label for="select1">Pilih Tipe Jawaban:</label>
            <select class="form-control" id="cb_tipe" runat="server">
              <option value="text">Text</option>
              <option value="gambar">Gambar</option>
            </select>
          </div>
          <div class="form-group" id="inputfile" >
            <label for="fileInput">File Input</label>
            <input type="file" class="form-control-file" runat="server" id="txfile"/>
                  <label>Gambar Sebelumnya:</label>
              <div runat="server" id="gambar">
              </div>
          </div>
         <input type="hidden" class="form-control" runat="server" id="txgambar"/>
         <input type="hidden" class="form-control" runat="server" id="txid"/>
         <button type="submit" class="btn btn-primary" runat="server" id="btnedit" onserverclick="btnedit_ServerClick">Submit</button>
         </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
            <script>
                $(document).ready(function () {
                    $('#<%= cb_tipe.ClientID %>').change(function () {
                        if ($(this).val() === "text") {
                            $('#inputfile').hide();
                        } else {
                            $('#inputfile').show();
                        }
                    });
                });

            </script>
        </div>
    </form>
</body>
</html>
