<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_adm_manajemen_quiz.aspx.vb" Inherits="e_learning.el_adm_manajemen_quiz" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h3 class="text-center">Manajemen Quiz</h3>
    <br />
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
            Tambah Data
    </button>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="true" ShowEditButton="false" ShowDeleteButton="true"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_mquiz" FieldName="id_mquiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            

             <dx:GridViewDataColumn Width="100px">
                <DataItemTemplate>
                    <dx:ASPxButton runat="server" ID="btncopy"   CssClass="btn-default" OnClick="btncopy_Click" Text="Edit" ></dx:ASPxButton>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn Caption="Judul" FieldName="judul" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Pre Test" FieldName="pre_test" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Materi" FieldName="id_materi" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataComboBoxColumn Caption="Paket Post Test" FieldName="id_soal" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akses" FieldName="tgl_akses" Width="100px">
                
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akhir" FieldName="tgl_akhir" Width="100px">

            </dx:GridViewDataDateColumn>

            <dx:GridViewDataComboBoxColumn Caption="Level" FieldName="level" Width="100px" >
                <PropertiesComboBox>

                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <%--<dx:GridViewDataHyperLinkColumn Caption="Sertifikat" Width="50px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server" Target="_blank" Text='<%#String.Concat(Eval("sertif"), " sertif") %>' NavigateUrl='<%#String.Format("~/el_adm_sertifikat.aspx?idrec={0}", Eval("id_mquiz")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>--%>

            <dx:GridViewDataTextColumn Caption="u_user" FieldName="c_date" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="u_date" FieldName="c_user" Width="100px" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn Caption="Approve" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>

            <dx:GridViewCommandColumn Caption="Approve Otomatis" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app2">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
    <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
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
            <label runat="server" id="lblid"></label>
            <div class="form-group">
                
                    <label for="hiddenText">Judul latihan</label>
                    <input type="text" class="form-control" runat="server" id="txejdul" placeholder="Judul Latihan" />
                  </div>
                  <div class="form-group">
                    <label for="hiddenText">Tanggal Akses</label>
                      <span runat="server" id="lbakses"></span>
                    <asp:TextBox ID="txe_akses" CssClass="form-control" runat="server" TextMode="Date" ></asp:TextBox>
                  </div>
                    <div class="form-group">
                        <span runat="server" id="lbakhir"></span>
                    <label for="hiddenText">Tanggal Akhir</label>
                    <asp:TextBox ID="txe_akhir" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>

                  </div>

                  <div class="form-group">
                    <label for="selectType">Pre Test</label>
                      <asp:DropDownList ID="cmbe_pre" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                     <div class="form-group">
                    <label for="selectType">materi</label>
                      <asp:DropDownList ID="cmbe_materi" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                     <div class="form-group">
                    <label for="selectType">post test</label>
                      <asp:DropDownList ID="cmbe_post" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                    <div class="form-group">
                    <label for="selectType">Level</label>
                      <asp:DropDownList ID="cmbe_lvl" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmb_level_SelectedIndexChanged">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rbeOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbeOptions_SelectedIndexChanged"  >
                        <asp:ListItem Text="None" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Custom" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                        <div id="su37" runat="server" style="overflow-y: scroll; height: 50px; border: 1px solid black; border-radius: 5px;">

                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary" id="btnsave" runat="server" onserverclick="btnsave_ServerClick" >Save changes</button>
        </div>
      </div>
    </div>
  </div>

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
                    <label for="hiddenText">Judul latihan</label>
                    <input type="text" class="form-control" runat="server" id="tx_judul" placeholder="Judul Latihan" />
                  </div>
                  <div class="form-group">
                    <label for="hiddenText">Tanggal Akses</label>
                    <asp:TextBox ID="txakses" CssClass="form-control" runat="server" TextMode="Date" ></asp:TextBox>
                  </div>
                    <div class="form-group">
                    <label for="hiddenText">Tanggal Akhir</label>
                    <asp:TextBox ID="txakhir" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>

                  </div>

                  <div class="form-group">
                    <label for="selectType">Pre Test</label>
                      <asp:DropDownList ID="cmb_pre" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                     <div class="form-group">
                    <label for="selectType">materi</label>
                      <asp:DropDownList ID="cmb_materi" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                     <div class="form-group">
                    <label for="selectType">post test</label>
                      <asp:DropDownList ID="cmb_post" runat="server" CssClass="form-control">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
                    <div class="form-group">
                    <label for="selectType">Level</label>
                      <asp:DropDownList ID="cmb_level" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmb_level_SelectedIndexChanged">
                          <asp:ListItem>

                          </asp:ListItem>
                      </asp:DropDownList>
                  </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rbOptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbOptions_SelectedIndexChanged"  >
                        <asp:ListItem Text="None" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Custom" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                        <div id="su57" runat="server" style="overflow-y: scroll; height: 50px; border: 1px solid black; border-radius: 5px;">

                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <div class="form-group">
                       <button type="button" class="btn btn-primary" id="bt_img" runat="server" onserverclick="bt_img_ServerClick" >Save changes</button>
                      </div>
                </div>
              </div>
            </div>
          </div>
    <uc1:uc_footer runat="server" ID="uc_footer" />

    <script type="text/javascript">
        function openModal() {
            $('#EditModal').modal('show'); // Menggunakan selector ID untuk membuka modal
        }
    </script>




</asp:Content>
