<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_jawaban.aspx.vb" Inherits="e_learning.adm_jawaban" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
        <br />
    <h3 class="text-center">Bank Soal</h3>
    <br />
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
    Tambah Data
    </button>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Templates>
            <EditForm>

                <iframe style="width: 100%; height: calc(100vh - 80px); border: none;" runat="server" id="apage" src='<%#String.Format("~/el_adm_edit_jawaban.aspx?idrec={0}&id={1}", Eval("id_jawaban"), Eval("id")) %>'></iframe>
                <dx:ASPxGridViewTemplateReplacement runat="server" ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormCancelButton" />
            </EditForm>
            
        </Templates>
        <Columns>
             <dx:GridViewCommandColumn VisibleIndex="0" Width="80px"  ShowDeleteButton="true" ShowEditButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id" FieldName="id_jawaban" ReadOnly="true"> 
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="idsoal" FieldName="id_soal" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <%--<dx:GridViewDataHyperLinkColumn Caption="Edit" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='Edit' NavigateUrl='<%#String.Format("~/el_edit_jawaban.aspx?idrec={0}", Eval("id_jawaban")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>--%>

            <dx:GridViewDataTextColumn Caption="Opsi" FieldName="opsi" Width="50px"> 

            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Jawaban" FieldName="jawaban" Width="150px"> 

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataImageColumn Caption="Image" FieldName="file_gambar" VisibleIndex="1" Width="200px"> 
                <PropertiesImage ImageHeight="100px" ImageUrlFormatString="gambar/{0}"></PropertiesImage>
            </dx:GridViewDataImageColumn>

             <dx:GridViewDataTextColumn Caption="Tipe Jawaban" FieldName="tipe_jawaban" Width="150px"> 

            </dx:GridViewDataTextColumn>

             <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

        </Columns>
    </dx:ASPxGridView>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">Insert Data</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <label for="select1">Pilih Tipe Jawaban:</label>
            <select class="form-control" id="cb_tipe" runat="server">
              <option value="text">Text</option>
              <option value="gambar">Gambar</option>
            </select>
          </div>
          <div class="form-group" id="inputopsi">
            <label for="numberInput">Opsi</label>
            <input type="text" class="form-control" runat="server" id="txopsi"/>
          </div>
          <div class="form-group" id="inputjawaban" >
            <label for="textarea2">Jawaban:</label>
            <textarea class="form-control" id="txtjawaban" rows="3" runat="server"></textarea>
          </div>
          <div class="form-group" id="inputfile" style="display: none;">
            <label for="fileInput">File Input</label>
            <input type="file" class="form-control-file" runat="server" id="txfile"/>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary" id="btnsave" runat="server" onserverclick="btnsave_ServerClick" >Save changes</button>
        </div>
      </div>
    </div>
  </div>

  <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
  <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
            <script>
                $(document).ready(function () {
                    $('#<%= cb_tipe.ClientID %>').change(function () {
                        if ($(this).val() === "text") {
                            $('#inputopsi').show();
                            $('#inputjawaban').show();
                            $('#inputfile').hide();
                        } else if ($(this).val() === "gambar") {
                            $('#inputopsi').show();
                            $('#inputjawaban').hide();
                            $('#inputfile').show();
                        } 
                    });
                });

            </script>
    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
<%--<div class="panel-body">
            <div class="form-group">
            <div class="col-sm-3">
                <input type="text" class="form-control"  runat="server" id="tx_opsi" placeholder="Opsi..."/>
            </div>
            <div class="col-sm-3">
                <textarea class="styled" runat="server" id="tx_jawaban" placeholder="Jawaban..."> </textarea>
            </div>
            <div class="col-sm-3">
                <input type="file" class="form-control"  runat="server" id="tx_gambar" />
            </div>
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary" value="Upload & Save image" runat="server" id="bt_img" onserverclick="bt_img_ServerClick" />
            </div>
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary" value="Upload & Save" runat="server" id="bt_sv" onserverclick="bt_sv_ServerClick" />
            </div>
        </div>
    </div>--%>