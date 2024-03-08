<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="register.aspx.vb" Inherits="e_learning.register1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <link href="/css/londinium-theme.css" type="text/css" rel="stylesheet" />
    <link href="/css/styles.min.css" type="text/css" rel="stylesheet" />
    <link href="/css/icons.min.css" type="text/css" rel="stylesheet" />
    <link href="fonts/font_google.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse" style="background-color:deepskyblue" role="navigation">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toogle="collapse" data-target=".navbar-right"><span class="sr-only">Toogle navbar</span><i class="icon-grid3"></i></button>

                <a class="navbar-brand"  href="#">
                    <%--<img src="img/.." height="30" />--%>
                    <label>E-Learning &nbsp; &nbsp;</label>
                </a>
            </div>
        </div>
        <div class="container" style="margin-top: 30px">
            <div class="row">
            <div class="col-md-3 text-center">
              
            </div>
                <div class="col-md-6">
                     <div class="well" style="background-color:white">
                  <h1 class="text-center" style="color:deepskyblue; margin-top: 20px">REGISTER</h1>
                        <div class="form-group" style="margin-top: 50px">
                            <div class="row">
                                <div class="col-xs-6">
                                     <label for="exampleFormControlInput1">Nama</label>
                                     <input type="text" class="form-control" runat="server" id="tx_nm" placeholder="Masukkan Nama" />
                                </div>
                                 <div class="col-xs-6">
                                     <label for="exampleFormControlInput1">NIK</label>
                                     <input type="text" class="form-control" runat="server" id="tx_nik" placeholder="Masukkan NIK" />
                                </div>
                            </div>
                          
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">Username</label>
                                    <input type="text" class="form-control" runat="server" id="tx_user" placeholder="Masukkan Username" />
                                </div>

                                <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">Email</label>
                                    <input type="text" class="form-control" runat="server" id="tx_em" placeholder="Masukkan Email" />
                                </div>

                              
                            </div>
                            
                        </div>

                        <div class="form-group">
                            <div class="row">

                                  <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">Level</label>
                                   <%--  <input type="text" class="form-control" runat="server" id="tx_dept" placeholder="Masukkan Dept" />--%>
                                 <select name="select" class="form-control" runat="server" id="cb_level">
                                     <option selected>Pilih Level</option>
                                      <option value="ALL">ALL</option>
                                     <option value="STAFF">STAFF</option>
                                     <option value="SUPERVISOR">SUPERVISOR</option>
                                     <option value="HEAD MANAGER">HEAD MANAGER</option>
                                     <option value="MANAGER">MANAGER</option>
                                     <option value="GENERAL MANAGER">GENERAL MANAGER</option>
                                </select>
                                    
                                </div>

                                  <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">Departement</label>
                                   <%--  <input type="text" class="form-control" runat="server" id="tx_dept" placeholder="Masukkan Dept" />--%>
                                 <select name="select" class="form-control" runat="server" id="cb_dept">
                                     <option selected>Pilih Departement</option>
                                      <option value="ALL">ALL</option>
                                     <option value="IT">IT</option>
                                     <option value="MIS">MIS</option>
                                     <option value="HRGA">HRGA</option>
                                </select>
                                    
                                </div>

                               
                            </div>
                            
                        </div>

                         <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">No. Telp</label>
                                    <input type="text" class="form-control" runat="server" id="tx_telp" placeholder="Masukkan No. Telp" />
                                </div>

                                 <div class="col-xs-6">
                                    <label for="exampleFormControlInput1">Jabatan</label>
                                    <input type="text" class="form-control" runat="server" id="tx_jabatan" placeholder="Masukkan Jabatan" />
                                </div>

                              
                            </div>
                            
                        </div>

                         


                          <div class="form-group">
                              <div class="row">
                                  <div class="col-xs-6">
                                      <label for="exampleFormControlInput1">Password</label>
                                      <input type="password" class="form-control" runat="server" clientIDMode="static" id="tx_pass" placeholder="Masukkan Password" />
                                  </div>

                                  <div class="col-xs-6">
                                       <label for="exampleFormControlInput1">Confrim Password</label>
                                       <input type="password" class="form-control" runat="server" clientIDMode="static" id="tx_pass2" placeholder="Confirm Password" />
                                  </div>
                              </div>
                         
                        </div>

                        

                        <br />

                        <table style="width: 100%">
                            <tr>
                                <td><button type="button" class="btn btn-primary" style="background-color:deepskyblue; width:100%" runat="server" id="bt_login" onserverclick="bt_login_ServerClick">Register</button></td>
                            </tr>
                        </table>
 
                           <h5 runat="server" id="lb_hasil" style="color: #ff0000"></h5>
            </div>
                </div>
            </div>
           
            
           
        </div>
       <%-- <h1 style="text-align:center">Register</h1>
        <div class="container">
     <div class="form-group">
        <label for="exampleFormControlInput1">Nama</label>
        <input type="text" class="form-control" runat="server" id="tx_nm" placeholder="input Nama" />
    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">Nik</label>
        <input type="text" class="form-control" runat="server" id="tx_nik" placeholder="input Nama" />
    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">E-mail</label>
        <input type="text" class="form-control" runat="server" id="tx_em" placeholder="input e-mail" />
    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">Username</label>
        <input type="text" class="form-control" runat="server" id="tx_user" placeholder="input username" />
    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">level</label>
        <dx:ASPxComboBox ID="cb_level" runat="server" >
              <Items>
                  <dx:ListEditItem Text="Staff" Value="Staff" Selected="false" />
                  <dx:ListEditItem Text="Manager" Value="Manager" Selected="false" />
                  <dx:ListEditItem Text="Supervisor" Value="Supervisor" Selected="false" />
              </Items>
        </dx:ASPxComboBox>
    <div class="form-group">
        <label for="exampleFormControlInput1">Departement</label>
        <dx:ASPxComboBox ID="cb_dept" runat="server" >
              <Items>
                  <dx:ListEditItem Text="IT" Value="img" Selected="false" />
                  <dx:ListEditItem Text="Management" Value="img" Selected="false" />
                  <dx:ListEditItem Text="Marketing" Value="img" Selected="false" />
              </Items>
        </dx:ASPxComboBox>
    </div>         
    <div class="form-group">
        <label for="exampleFormControlInput1">Jabatan</label>
        <input type="text" class="form-control" runat="server" id="tx_jabatan" placeholder="input username" />
    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">No Telp</label>
        <input type="text" class="form-control" runat="server" id="tx_telp" placeholder="input username" />
    </div>
     <div class="form-group">
        <label for="exampleFormControlInput1">Password</label>
        <input type="password" class="form-control" runat="server" id="tx_pass" placeholder="input password" />

    </div>
    <div class="form-group">
        <label for="exampleFormControlInput1">Masukan Kembali Password</label>
        <input type="password" class="form-control" runat="server" id="tx_pass2" placeholder="input password" />
  
    </div>
            <button type="button" class="btn btn-primary" runat="server"  onserverclick="Unnamed_ServerClick">Login</button>
            <h5 runat="server" id="lb_hasil" style="color:#FF0000"></h5>
   </div>--%>
    </form>
</body>
</html>
