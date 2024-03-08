<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
  CodeBehind="el_adm_file_materi.aspx.vb" Inherits="e_learning.adm_file_materi" %>

  <%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

    <%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
      <%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <br />
          <uc1:uc_header runat="server" ID="uc_header" />
          <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
            Tambah Data
          </button>
 
          <dx:ASPxGridView ID="ASPxGridView1" runat="server">
              <Templates>
            <EditForm>

                <iframe style="width: 100%; height: calc(100vh - 80px); border: none;" runat="server" id="apage" src='<%#String.Format("~/el_adm_edit_materi.aspx?idrec={0}&id={1}", Eval("id_isimt"), Eval("id")) %>'></iframe>
                <dx:ASPxGridViewTemplateReplacement runat="server" ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormCancelButton" />
            </EditForm>
            
        </Templates>
            <Columns>
                <dx:GridViewDataTextColumn Caption="id_soal" FieldName="id_isimt" ReadOnly="true">
            </dx:GridViewDataTextColumn>
              <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowDeleteButton="true" ShowEditButton="true">
              </dx:GridViewCommandColumn>
              <%--<dx:GridViewDataHyperLinkColumn Caption="Edit" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='Edit' NavigateUrl='<%#String.Format("~/el_edit_file_materi.aspx?idrec={0}", Eval("id_isimt")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
              </dx:GridViewDataHyperLinkColumn>--%>
              <dx:GridViewDataTextColumn Caption="Judul Materi" FieldName="nama_materi" Width="150px">
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn Caption="Materi" FieldName="nama_file" Width="150px">
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" ReadOnly="true">
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" ReadOnly="true">
              </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Tipe" Width="70px" FieldName="tipe" ReadOnly="true">
              </dx:GridViewDataTextColumn>
            </Columns>
          </dx:ASPxGridView>

          <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="myModalLabel">Insert Data</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">

                  <div class="form-group">
                    <label for="hiddenText">Judul Materi</label>
                    <input type="text" class="form-control" runat="server" id="tx_ket" placeholder="Judul Materi" />
                  </div>
                  <div class="form-group">
                    <label for="txtsoal">Keterangan:</label>
                    <textarea class="form-control" id="txtket" rows="3" runat="server"></textarea>
                  </div>

                  <div class="form-group">
                    <label for="selectType">Select Type</label>
                    <select class="form-control" id="cmb_doc"  runat="server">
                      <option value="none" selected="selected">Choose...</option>
                      <option value="pdf">PDF</option>
                      <option value="ppt">PPT</option>
                      <option value="link">Link</option>
                      <option value="link_youtube">Link Youtube</option>
                    </select>
                  </div>
                    <div class="form-group" id="hiddenText" style="display:none;">
                    <label for="hiddenText" class="d-none">LINK:</label>
                    <input type="text" class="form-control" runat="server" id="tx_link" placeholder="Link" autocomplete="off"/>
                  </div>
                  <div class="form-group" id="hiddenFile" style="display:none;">
                    <label for="hiddenFile" class="d-none">FILE:</label>
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
            <script>
                $(document).ready(function () {
                    $('#<%= cmb_doc.ClientID %>').change(function () {
                        if ($(this).val() === "pdf") {
                            $('#hiddenText').hide();
                            $('#hiddenFile').show();
                        } else if ($(this).val() === "ppt") {
                            $('#hiddenText').hide();
                            $('#hiddenFile').show();
                        } else if ($(this).val() === "link") {
                            $('#hiddenText').show();
                            $('#hiddenFile').hide();
                        } else if ($(this).val() === "link_youtube") {
                            $('#hiddenText').show();
                            $('#hiddenFile').hide();
                        } else {
                            $('#hiddenText').hide();
                            $('#hiddenFile').hide();
                        }
                     });
                });

            </script>
          <%--<script type="text/javascript">


            function toggleFields() {
                var selectedValue = document.getElementById('#<%= cmb_doc.ClientID %>').value;
              var hiddenTextbox = document.getElementById("hiddenText");
              var hiddenFilebox = document.getElementById("hiddenFile");

              if (selectedValue === "file") {
                hiddenFilebox.style.display = "block";
                hiddenTextbox.style.display = "none";
              } else if (selectedValue === "link") {
                hiddenTextbox.style.display = "block";
                hiddenFilebox.style.display = "none";
              } else if (selectedValue === "none"){
                foodTextbox.style.display = "none";
                drinkTextbox.style.display = "none";
              }
            }


          </script>--%>
        <uc1:uc_footer runat="server" ID="uc_footer" />
        </asp:Content>