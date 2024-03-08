<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_soal.aspx.vb" Inherits="e_learning.adm_soal" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc1:uc_header runat="server" ID="uc_header" />
    <br />
    <h3 class="text-center" id="lbljudul" runat="server">Bank Soal</h3>
    <br />
   
  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
    Tambah Data
  </button>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" OnRowCommand="ASPxGridView1_RowCommand1" >
        <Templates>
            <EditForm>

                <iframe style="width: 100%; height: calc(100vh - 80px); border: none;" runat="server" id="apage" src='<%#String.Format("~/el_adm_edit_soal.aspx?idrec={0}&id={1}", Eval("id_soal"), Eval("id")) %>'></iframe>
                <dx:ASPxGridViewTemplateReplacement runat="server" ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormCancelButton" />
            </EditForm>
            
        </Templates>
<%--        <SettingsPopup>
        <EditForm AllowResize="True"  >
        </EditForm>
    </SettingsPopup>
    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>--%>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowDeleteButton="true" ShowEditButton="true">
              
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="skor" FieldName="skor" Width="50px">
                
            </dx:GridViewDataTextColumn>
           
            <%--<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="edit" Width="50px">
                <CustomButtons>
                  <dx:GridViewCommandColumnCustomButton ID="CustomButton"  Text="Open Modal" Visibility="AllDataRows" >
                     
                  </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>--%>
            <%--<dx:GridViewDataHyperLinkColumn Caption="Edit" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='Edit' NavigateUrl='<%#String.Format("~/el_edit_soal.aspx?idrec={0}", Eval("id_soal")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>--%>

<%--            <dx:GridViewCommandColumn Caption="Edit"  Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app" Text="Edit">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>--%>

             <dx:GridViewDataTextColumn Caption="id_soal" FieldName="id_soal" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="idsoal" FieldName="id_soal" ReadOnly="true">
            </dx:GridViewDataTextColumn>

<%--            <dx:GridViewDataHyperLinkColumn Caption="File Image" FieldName="gambar" VisibleIndex="2" Width="150px">
                <PropertiesHyperLinkEdit NavigateUrlFormatString="gambar\{0}" Target="_blank" TextField="nm_gambar">
                </PropertiesHyperLinkEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataHyperLinkColumn>--%>

            <dx:GridViewDataTextColumn Caption="Soal" FieldName="soal" VisibleIndex="5" Width="300px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Jawaban" FieldName="jawaban" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Tipe_soal" FieldName="tipe_soal" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

             <dx:GridViewDataImageColumn Caption="Image" FieldName="file_gambar" VisibleIndex="2" Width="200px"> 
                <PropertiesImage ImageHeight="100px" ImageUrlFormatString="gambar/{0}"></PropertiesImage>
            </dx:GridViewDataImageColumn>

            <dx:GridViewDataTextColumn Caption="Jenis" FieldName="jenis_soal" VisibleIndex="5" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Opsi" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#String.Concat(Eval("opsi"), " Opsi") %>' NavigateUrl='<%#String.Format("~/el_adm_jawaban.aspx?idrec={0}", Eval("id_soal")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="inserted" FieldName="c_date" VisibleIndex="6" Width="100px">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="by" FieldName="c_user" VisibleIndex="7" Width="100px">
                <CellStyle HorizontalAlign="Center" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="ASPxGridViewDetail" runat="server" OnBeforePerformDataSelect="ASPxGridViewDetail_BeforePerformDataSelect">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="id" FieldName="id_jawaban" ReadOnly="true"> 
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="idsoal" FieldName="id_soal" ReadOnly="true">
            </dx:GridViewDataTextColumn>

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
            </DetailRow>
        </Templates>
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
<%--            <iframe src="el_edit_soal?idrec=1"></iframe>--%>
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
          <div class="form-group" id="inputfile" style="display: none;">
            <label for="fileInput">File Input</label>
            <input type="file" class="form-control-file" runat="server" id="txfile"/>
          </div>
          <div class="form-group" id="inputlink" style="display: none;">
            <label for="textInput">Link Input</label>
            <input type="text" class="form-control" runat="server" id="txlink"  />
          </div>
          <div class="form-group">
            <label for="numberInput">Skor</label>
            <input type="number" class="form-control" runat="server" id="txskor" />
          </div>

        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary" id="btnsave" runat="server" onserverclick="btnsave_ServerClick">Save changes</button>
        </div>
      </div>
    </div>
  </div>
  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
                            $('#<%= txlink.ClientID %>').prop('required', true);
                            $('#<%= txfile.ClientID %>').prop('required', false);
                        } else if ($(this).val() === "gambar") {
                            $('#inputlink').hide();
                            $('#inputfile').show();
                            $('#<%= txlink.ClientID %>').prop('required', false);
                            $('#<%= txfile.ClientID %>').prop('required', true);
                        } else {
                            $('#inputlink').hide();
                            $('#inputfile').hide();
                            $('#<%= txlink.ClientID %>').prop('required', false);
                            $('#<%= txfile.ClientID %>').prop('required"', false);
                        }
                    });

                    $('#<%= ASPxGridView1.ClientID %> #etipe').change(function () {
                        if ($(this).val() === "link") {
                            $('#<%= ASPxGridView1.ClientID %> #editinputlink').show();
                            $('#<%= ASPxGridView1.ClientID %> #editinputfile').hide();
                        } else if ($(this).val() === "gambar") {
                            $('#<%= ASPxGridView1.ClientID %> #editinputlink').hide();
                            $('#<%= ASPxGridView1.ClientID %> #editinputfile').show();
                        } else {
                            $('#<%= ASPxGridView1.ClientID %> #editinputlink').hide();
                            $('#<%= ASPxGridView1.ClientID %> #editinputfile').hide();
                        }   
                    });









                    $("#<%= ASPxGridView1.ClientID %> .dxgvCommandColumnItem_CustomButton").click(function () {
                        $("#button2").trigger("click");
                    });

                });

               

            </script>

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
<%--<div class="panel-body">
            <div class="form-group">
            <div class="col-sm-1">
                <label class="control-label" runat="server" id="lb_ttl">Buat Soal : </label>
            </div>
            <div class="col-sm-2">
                <textarea class="styled" runat="server" id="tx_soal" placeholder="Soal..."> </textarea>
            </div>
            <div class="col-sm-3">
                <input type="file" class="form-control"  runat="server" id="tx_gambar" />
            </div>
            <div>
                <dx:ASPxComboBox ID="cb_doc" runat="server" EnableTheming="false" Height="32px" Width="300px" SelectedIndex="0">
                    <ButtonStyle BackColor="Transparent">
                    </ButtonStyle>
                    <Paddings Padding="6px" />
                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <Items>
                        <dx:ListEditItem Text="PG" Value="img" Selected="false" />
                        <dx:ListEditItem Text="ESSAY" Value="img" Selected="false" />
                    </Items>
                </dx:ASPxComboBox>
            </div>
                <br />
                <br />
            <div class="col-sm-3">
                 <label class="control-label" runat="server" id="Label2">Jawaban : </label>
                <input type="text" class="form-control"  runat="server" id="txt_jawaban" />
            </div>
                <br />
                
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary" value="Upload & Save image" runat="server" id="bt_img" onserverclick="bt_img_ServerClick" />
            </div>
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary" value="Upload & Save" runat="server" id="bt_sv" onserverclick="bt_sv_ServerClick" />
            </div>
        </div>
    </div>--%>

<%--<div class="form-group">
            <label for="textarea1">soal</label>
            <textarea class="form-control" id="txtesoal" rows="3" runat="server"  ></textarea>
          </div>
          <div class="form-group">
            <label for="textarea2">Jawaban:</label>
            <textarea class="form-control" id="txtejawaban" rows="3" runat="server"></textarea>
          </div>
            <div class="form-group">
            <label for="select1">Pilih Jenis Soal:</label>
            <select class="form-control" id="cb_ejenis" runat="server">
              <option value="pg">PG</option>
              <option value="essay">Essay</option>
            </select>
          </div>
          <div class="form-group">
            <label for="select1">Pilih Tipe Soal:</label>
            <select class="form-control" id="cb_etipe">
              <option value="text">Text</option>
              <option value="gambar">Gambar</option>
              <option value="link">Link</option>
            </select>
          </div>
          <div class="form-group" id="editinputfile" style="display: none;">
            <label for="fileInput">File Input</label>
            <input type="file" class="form-control-file" runat="server" id="txefile"/>
          </div>
          <div class="form-group" id="editinputlink" style="display: none;">
            <label for="textInput">Link Input</label>
            <input type="text" class="form-control" runat="server" id="txelink"  />
          </div>
          <div class="form-group">
            <label for="numberInput">Skor</label>
            <input type="number" class="form-control" runat="server" id="txeskor" />
          </div>

        </div>
          <input type="hidden" runat="server" id="txeid" />
          <input type="hidden" runat="server" id="txenmfile"/>--%>