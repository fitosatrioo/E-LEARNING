<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="el_adm_edit_soal.aspx.vb" Inherits="e_learning.el_adm_edit_soal" %>

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
             <br />
    <h3 class="text-center">Bank Soal</h3>
    <br />
     <div class="container">
       <div class="form-group">
            <label for="textarea1">Soal:</label>
            <textarea class="form-control" id="txtsoal" rows="3" runat="server"></textarea>
          </div>
          <div class="form-group">
            <label for="textarea2">Jawaban:</label>
            <textarea class="form-control" id="txtjawaban" rows="3" runat="server"></textarea>
          </div>
            <div class="form-group">
            <label for="select1">Pilih Jenis Soal:</label>
            <select class="form-control" id="cb_jenis" runat="server">
              <option value="pg">PG</option>
              <option value="essay">Essay</option>
            </select>
          </div>
          <div class="form-group">
            <label for="select1">Pilih Tipe Soal:</label>
            <select class="form-control" id="cb_tipe" runat="server">
              <option value="text">Text</option>
              <option value="gambar">Gambar</option>
              <option value="link">Link</option>
            </select>
          </div>
          <div class="form-group" id="inputfile" >
            <label for="fileInput">File Input</label>
            <input type="file" class="form-control-file" runat="server" id="txfile"/>
                  <label>Gambar Sebelumnya:</label>
              <div runat="server" id="gambar">
              </div>
          </div>
          <div class="form-group" id="inputlink" >
            <label for="textInput">Link Input</label>
            <input type="text" class="form-control" runat="server" id="txlink"/>
          </div>

          <div class="form-group">
            <label for="numberInput">Skor</label>
            <input type="number" class="form-control" runat="server" id="txskor"/>
          </div>
         <input type="hidden" class="form-control" runat="server" id="txgambar"/>
         <input type="hidden" class="form-control" runat="server" id="txid"/>
         <button type="submit" class="btn btn-primary" runat="server" id="btnedit" onserverclick="btnedit_ServerClick"    >Submit</button>
         </div>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
            <script>
                $(document).ready(function () {
                    $('#<%= cb_tipe.ClientID %>').change(function () {
                        if ($(this).val() === "link") {
                            $('#inputlink').show();
                            $('#inputfile').hide();
                        } else if ($(this).val() === "gambar") {
                            $('#inputlink').hide();
                            $('#inputfile').show();
                        } else {
                            $('#inputlink').hide();
                            $('#inputfile').hide();
                        }
                    });
                });

            </script>
        </div>
    </form>
</body>
</html>
