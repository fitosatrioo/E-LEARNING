<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="el_dept.aspx.vb" Inherits="e_learning.dept" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/uc_header.ascx" TagPrefix="uc1" TagName="uc_header" %>
<%@ Register Src="~/uc_footer.ascx" TagPrefix="uc1" TagName="uc_footer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc_header runat="server" ID="uc_header" />
    
    <div id="div1" runat="server">
        <br />
    <h3 class="text-center">Menu Materi</h3>
    <br />
        <dx:ASPxGridView ID="ASPxGridView1"  runat="server">
        <Columns>
            <dx:GridViewDataTextColumn Caption="id_ruang" FieldName="id_dept" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="ID Departement" FieldName="id_dept" Width="180px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Nama Departement" FieldName="dept_name" Width="280px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Training"  Width="100px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink4"  runat="server" Text='<%#String.Concat(Eval("jumlah"), " Training") %>' NavigateUrl='<%#String.Format("~/el_adm_materi.aspx?idrec={0}", Eval("id_dept")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="70px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="70px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
        </Columns>
             <SettingsDetail ShowDetailRow="True" />
        <Templates> 
            <DetailRow>
                <dx:ASPxGridView  ID="ASPxGridViewDetail1" OnBeforePerformDataSelect="ASPxGridViewDetail1_BeforePerformDataSelect" runat="server" KeyFieldName="id_materi">
                    <Columns>
            <dx:GridViewDataTextColumn Caption="id" FieldName="id_materi" ReadOnly="true"> 
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="judul Training" FieldName="judul_materi" Width="280px"> </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataHyperLinkColumn Caption="Upload Materi" Width="200px" ReadOnly="true">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#String.Concat(Eval("image"), " Materi") %>' NavigateUrl='<%#String.Format("~/el_adm_file_materi.aspx?idrec={0}", Eval("id_materi")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>
        </Columns>
                    <SettingsDetail ShowDetailRow="True" />
                  <Templates>
                      <DetailRow>
                          <dx:ASPxGridView ID="ASPxGridViewDetail12" runat="server" OnBeforePerformDataSelect="ASPxGridViewDetail12_BeforePerformDataSelect1">
                              <Columns>
                                  <dx:GridViewDataTextColumn Caption="id_soal" FieldName="id_isimt" ReadOnly="true">
            </dx:GridViewDataTextColumn>
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
                      </DetailRow>
                  </Templates>
                </dx:ASPxGridView>
                </DetailRow>
        </Templates>

    </dx:ASPxGridView>
    </div>
    

    <div id="div2" runat="server">
        <br />
    <h3 class="text-center">Bank Soal</h3>
    <br />
        <dx:ASPxGridView ID="ASPxGridView2" runat="server">
                <Columns>
            <dx:GridViewDataTextColumn Caption="id_ruang" FieldName="id_dept" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Departement" FieldName="dept_name" Width="280px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Paket Soal" ReadOnly="true" Width="300px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5" runat="server" Text='<%#String.Concat(Eval("quiz"), " Paket") %>' NavigateUrl='<%#String.Format("~/el_adm_bank.aspx?idrec={0}", Eval("id_dept")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="150px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="150px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="200px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="150px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
        </Columns>
            <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="ASPxGridViewDetail2" runat="server" OnBeforePerformDataSelect="ASPxGridViewDetail2_BeforePerformDataSelect">
                    <Columns>
                         <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" ShowNewButtonInHeader="False" ShowEditButton="False" ShowDeleteButton="False"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id_paket" FieldName="id_paket" Visible="false">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Paket Soal" FieldName="paket_soal" Width="180px" >
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul Training" FieldName="judul_training" Width="180px">
            </dx:GridViewDataTextColumn>

             <dx:GridViewDataSpinEditColumn Caption="Waktu" FieldName="waktu" Width="100px">
                 <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                 <EditFormSettings Caption="Jumlah Waktu (Menit):"></EditFormSettings>
             </dx:GridViewDataSpinEditColumn>


            <dx:GridViewDataSpinEditColumn Caption="Nilai Minimal" FieldName="kkm" Width="100px">
                 <PropertiesSpinEdit DisplayFormatString="N0"></PropertiesSpinEdit>
                 <EditFormSettings Caption="Nilai Minimal:"></EditFormSettings>
            </dx:GridViewDataSpinEditColumn>


            <dx:GridViewDataComboBoxColumn  Caption="Jenis Test" FieldName="jenis_test" Width="180px">
                <PropertiesComboBox>
                    <Items>
                    <dx:ListEditItem Text="Post Test" Value="POST TEST" />
                    <dx:ListEditItem Text="Pre Test" Value="PRE TEST" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Soal"  Width="100px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5" ReadOnly="true" runat="server" Text='<%#String.Concat(Eval("soal"), " Soal") %>' NavigateUrl='<%#String.Format("~/el_adm_soal.aspx?idrec={0}", Eval("id_paket")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="70px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="70px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="70px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="70px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                </DetailRow>
        </Templates>
    </dx:ASPxGridView>
    </div>

    

    <div id="div3" runat="server">
        <br />
    <h3 class="text-center">Manajemen Quiz</h3>
    <br />
        <dx:ASPxGridView ID="ASPxGridView3" runat="server">
         <Columns>
            <dx:GridViewDataTextColumn Caption="id_ruang" FieldName="id_dept" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Departement" FieldName="dept_name" Width="380px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Quiz"  Width="300px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5"  runat="server" Text='<%#String.Concat(Eval("quiz"), " Quiz") %>' NavigateUrl='<%#String.Format("~/el_adm_manajemen_quiz.aspx?idrec={0}", Eval("id_dept"))%>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="120px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="120px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="120px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="120px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
        </Columns>
            <SettingsDetail ShowDetailRow="True" />
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="ASPxGridViewDetail3" runat="server" OnBeforePerformDataSelect="ASPxGridViewDetail3_BeforePerformDataSelect" OnCustomButtonInitialize="ASPxGridViewDetail3_CustomButtonInitialize" OnCustomButtonCallback="ASPxGridViewDetail3_CustomButtonCallback">
                        <Columns>
            <dx:GridViewDataTextColumn Caption="id_mquiz" FieldName="id_mquiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

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

            <dx:GridViewDataDateColumn Caption="Tanggal Akses" FieldName="tgl_akses" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akhir" FieldName="tgl_akhir" Width="100px" ReadOnly="true">
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

            <dx:GridViewDataTextColumn Caption="u_user" FieldName="c_date" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="u_date" FieldName="c_user" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewCommandColumn Caption="Approve" ButtonType="Image" Width="80px" ShowClearFilterButton="true">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="bt_app">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
           </dx:GridViewCommandColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
    </dx:ASPxGridView>
    </div>

    

    <div id="div4" runat="server">
        <br />
    <h3 class="text-center">Hasil Test</h3>
    <br />
        <dx:ASPxGridView ID="ASPxGridView4" runat="server" >
        <Columns>
            <dx:GridViewDataTextColumn Caption="id_ruang" FieldName="id_dept" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Departement" FieldName="dept_name" Width="380px">

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Quiz"  Width="350px">
              <DataItemTemplate>
               <dx:ASPxHyperLink ID="aspxhyperlink5"  runat="server" Text='<%#String.Concat(Eval("quiz"), " Quiz") %>' NavigateUrl='<%#String.Format("~/el_adm_hasil.aspx?idrec={0}", Eval("id_dept")) %>' >

               </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataTextColumn Caption="create date" Width="100px" FieldName="c_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="create user" Width="100px" FieldName="c_user" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update date" Width="100px" FieldName="u_date" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="update user" Width="100px" FieldName="u_user" ReadOnly="true">
                </dx:GridViewDataTextColumn>
        </Columns>
            <SettingsDetail ShowDetailRow="True" />
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="ASPxGridView5" runat="server" OnBeforePerformDataSelect="ASPxGridView5_BeforePerformDataSelect">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="id_mquiz" FieldName="id_mquiz" ReadOnly="true">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Judul" FieldName="judul" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataHyperLinkColumn Caption="PRE TEST" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#Eval("pre") %>' NavigateUrl='<%#String.Format("~/el_hasil_user.aspx?idrec={0}&id_mquiz={1}&sumber={2}", Eval("pre_test"), Eval("id_mquiz"), "pre") %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="POST TEST" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='<%#Eval("post") %>' NavigateUrl='<%#String.Format("~/el_hasil_user.aspx?idrec={0}&id_mquiz={1}&sumber={2}", Eval("id_soal"), Eval("id_mquiz"), "post") %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Hasil Keseluruhan" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='Hasil' NavigateUrl='<%#String.Format("~/el_adm_laporan.aspx?idrec={0}&idlvl={1}", Eval("id_mquiz"), Eval("level")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataHyperLinkColumn Caption="Aktivitas Pengguna" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink runat="server"  Text='Aktivitas Pengguna' NavigateUrl='<%#String.Format("~/el_adm_log.aspx?idrec={0}", Eval("id_mquiz")) %>'>
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akses" FieldName="tgl_akses" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataDateColumn Caption="Tanggal Akhir" FieldName="tgl_akhir" Width="100px" ReadOnly="true">
            </dx:GridViewDataDateColumn>

            <dx:GridViewDataTextColumn Caption="u_user" FieldName="c_date" Width="100px" >

            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="u_date" FieldName="c_user" Width="100px" >

            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
    </dx:ASPxGridView>
    </div>

    

    <uc1:uc_footer runat="server" ID="uc_footer" />
</asp:Content>
