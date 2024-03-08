<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="e_learning.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agung Logistics</title>
    <link rel="icon" href="img/fabicon.png" />
    <link href="/css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="/css/londinium-theme.css" type="text/css" rel="stylesheet" />
    <link href="/css/styles.min.css" type="text/css" rel="stylesheet" />
    <link href="/css/icons.min.css" type="text/css" rel="stylesheet" />
    <link href="fonts/font_google.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse" style='background-image: url("img/bg2.png')" role="navigation">
            <div class="navbar-header">
              
                  <div class="container-fluid">
     
           
                <a class="navbar-brand" href="#">
                   <%-- <img src="img/fabicon.png" height="30" />--%>
                    <label style="padding-top: 15px">E-Learning &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp  &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp</label>
                </a>
                
       <img data-target=".navbar-right" style="padding-bottom: 5px;" src="/img/Logo-agung.png" width="150" height="80" alt="test">
     
            </div>
        </div>

        <style type="text/css">
             body {
      background-image: url("/img/bg-login.jpg");
      background-size: cover;
      background-position: center;
      overflow-x: hidden;
    }

            .login-wrapper{
                top: 250px;

            }
        </style>

        <br />
        <br />
        <div class="row">
            
            <div class="col-md-11 text-center">
                <div class="login-wrapper">
                    <div style="width: 400px; height: 300px" class="well" style="background-color:white">
                        <div style="margin-top: 15px" class="thumbnail pt-4">
                            <h1 style="color:deepskyblue">LOGIN</h1>

                        </div>

                       <%-- <div class="form-group has-feedback has-feedback-no-label">
                            <select name="select" class="form-control" runat="server" id="cb_comp">
                                <option value="ACT">Agung Citra Transformasi</option>
                                <option value="ATR">Agung Transia Raya</option>
                                <option value="AR">Agung Raya</option>
                                <option value="AJL">Agung Jasa Logistik</option>
                                <option value="TAMA">Agung Tama Raya</option>
                            </select>
                        </div>--%>

                        <div class="form-group has-feedback has-feedback-no-label">
                            <input type="text" class="form-control" placeholder="Username" runat="server" id="tx_user" />
                            <i class="icon-user form-control-feedback"></i>
                        </div>

                        <div class="form-group has-feedback has-feedback-no-label">
                            <input type="password" class="form-control" placeholder="Password" onkeypress="return handleEnter('')" runat="server" id="tx_pass" />
                            <i class="icon-lock form-control-feedback"></i>
                        </div>

                        <div class="row form-actions">
                           
                            <div class="col-xs-12">
                                <button type="button" style="width:250px;"  class="btn btn-primary" runat="server" id="bt_login" onserverclick="bt_login_ServerClick">Sign in</button>
                            </div>
                            <br />
                             <div style="padding-top: 10px" class="col-xs-12">
                               <%--  <p>Belum memiliki akun? <a style=" color: deepskyblue; text-decoration: none;" target="_blank" href="register.aspx">Daftar di sini</a>.</p>--%>
                               <%-- <a href="register.aspx" class="text-primary" target="_blank"> Silahkan Register Terlebih Dahulu</a>--%>
                            </div>
                        </div>
                    </div>

                    <div class="alert alert-warning fade in block-inner" runat="server" id="pesan_div">
                        <button type="button" class="close" data-dismiss="alert" runat="server" onserverclick="Unnamed_ServerClick">X</button>
                        <i class="icon-warning"></i>
                        <label runat="server" id="pesan_lb">Warning &amp; default alert</label>
                    </div>
                </div> 
            </div>
            <div class="col-md-3 text-center">
            </div>
        </div>

    </form>

    <script src="js/jquery.min.js"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/plugins/charts/sparkline.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/uniform.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/select2.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/inputmask.js"></script>
    <script type="text/javascript" src="js/plugins/forms/autosize.js"></script>
    <script type="text/javascript" src="js/plugins/forms/inputlimit.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/listbox.js"></script>
    <script type="text/javascript" src="js/plugins/forms/multiselect.js"></script>
    <script type="text/javascript" src="js/plugins/forms/validate.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/tags.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/switch.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/uploader/plupload.full.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/uploader/plupload.queue.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/wysihtml5/wysihtml5.min.js"></script>
    <script type="text/javascript" src="js/plugins/forms/wysihtml5/toolbar.js"></script>
    <script type="text/javascript" src="js/plugins/interface/daterangepicker.js"></script>
    <script type="text/javascript" src="js/plugins/interface/fancybox.min.js"></script>
    <script type="text/javascript" src="js/plugins/interface/moment.js"></script>
    <script type="text/javascript" src="js/plugins/interface/jgrowl.min.js"></script>
    <script type="text/javascript" src="js/plugins/interface/datatables.min.js"></script>
    <script type="text/javascript" src="js/plugins/interface/colorpicker.js"></script>
    <script type="text/javascript" src="js/plugins/interface/fullcalendar.min.js"></script>
    <script type="text/javascript" src="js/plugins/interface/timepicker.min.js"></script>
    <script type="text/javascript" src="js/plugins/interface/collapsible.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/application.js"></script>
</body>
    <script>
    function handleEnter(obj, event) {
        var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
        if (keyCode == 13) {
            console.log("masuk callapd")
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "login.aspx/bt_login_ServerClick",
                dataType: "json",
                success: function (data) {
                    console.log("success")
                },
                error: function (textStatus, errorThrown) { console.log("Status: " + textStatus); console.log("Error: " + errorThrown); },
            })
            return false;
        }
        else {
            return true;
        }
    }
    function callapd() {
        console.log("masuk callapd")
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/isi_apd",
            dataType: "json",
            success: function (data) {
                console.log(data)
                $("#modalApd").modal('show');
                $("#bd_table").html(data.d)
                console.log("success")
            },
            error: function (textStatus, errorThrown) { console.log("Status: " + textStatus); console.log("Error: " + errorThrown); },
        })
    }
    </script>
</html>
