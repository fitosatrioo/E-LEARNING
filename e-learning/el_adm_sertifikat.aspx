<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_sertifikat.aspx.vb" Inherits="e_learning.el_adm_sertifikat" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
          <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
            Tambah Data
          </button>
 
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
             <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_soal" FieldName="id_sertif" ReadOnly="true">
            </dx:GridViewDataTextColumn>
              <%--<dx:GridViewDataHyperLinkColumn Caption="Edit" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='Edit' NavigateUrl='<%#String.Format("~/el_edit_file_materi.aspx?idrec={0}", Eval("id_isimt")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
              </dx:GridViewDataHyperLinkColumn>--%>
              <dx:GridViewDataTextColumn Caption="Judul Sertifikat" FieldName="nama_materi" Width="150px">
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn Caption="File" FieldName="nama_file" Width="150px">
              </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="User" Width="70px" FieldName="id_user" ReadOnly="true">
              </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

          <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="myModalLabel">Upload Sertifikat</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">

                  <div class="form-group">
                    <label for="hiddenText">Judul Materi</label>
                    <input type="text" class="form-control" runat="server" id="tx_ket" placeholder="Judul Sertifikat" />
                  </div>
 
                  <div class="form-group">
                    <label for="selectType">Select Type</label>
                    <select class="form-control" id="cmb_doc"  runat="server">

                      <option value="none" selected="selected">Choose...</option>
                      <option value="1">PDF</option>
                      <option value="2">PPT</option>
                      <option value="3">Link</option>
                      <option value="4">Link Youtube</option>
                    </select>
                  </div>

                  <div class="form-group"  >
                    <label for="hiddenFile" class="d-none">Hidden File</label>
                    <input type="file" class="form-control-file" runat="server" id="tx_fileimg" />  
                  </div>
                </div>
                <div class="modal-footer">
                    <div class="form-group">
                       <button type="button" class="btn btn-primary" id="bt_img" runat="server" onserverclick="bt_img_ServerClick1">Save changes</button>
                      </div>
                </div>
              </div>
            </div>
          </div>

          <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
          <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
            
</asp:Content>
