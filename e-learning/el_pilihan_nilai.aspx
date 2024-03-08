<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_pilihan_nilai.aspx.vb" Inherits="e_learning.el_pilihan_nilai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">

    <h2 class="text-center">Daftar Nilai</h2>
               <div class="card text-center">
  
  <div class="card-body">
   

<div class="table-responsive">
  <table class="table table-bordered table-hover mt-3">
    <thead class="thead-white">
      <tr>
        <th>Quiz</th>
          <th>Pre Test</th>
          <th>Post Test</th>
        <th>Lihat Nilai</th>
        
      </tr>
    </thead>
    <tbody>
     <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                     <ItemTemplate>
                                             <tr class="text-center">

                                                  <td>
                                                     <asp:Label runat="server" Text='<%#Eval("judul") %>'></asp:Label>             
                                                 </td>
                                                  <td>
                                                     <asp:Label runat="server" ID="pre" Text='<%#Eval("pre") %>'></asp:Label>
                                                      <asp:Label runat="server" ID="tdk_pre" Text='<%#Eval("pre_test") %>' Visible="false"></asp:Label>
                                                      <asp:Label runat="server" ID="blm_pre" Text='Anda Belum Mengerjakan Pre Test!'></asp:Label>
                                                      <asp:Label runat="server" ID="tdk_pre2" Text='Tidak Ada PRE TEST'></asp:Label>
                                                 </td>
                                                  <td>
                                                     <asp:Label runat="server" ID="post" Text='<%#Eval("post") %>'></asp:Label> 
                                                      <asp:Label runat="server" ID="blm_post" Text='Anda Belum Mengerjakan Post Test!'></asp:Label>
                                                 </td>

                                                 <td>
                                                     <asp:Label runat="server" ID="acc" Text='<%#Eval("acc") %>' Visible="false"></asp:Label>
                                                     <asp:Label runat="server" ID="blm_acc" Text='Sedang Menunggu Nilai'></asp:Label>
                                                     <div id="btn_nilai" runat="server">
                                                          <a class="btn btn-info"  href="el_nilai.aspx?id_mquiz=<%#Eval("id_mquiz") %>&id_post=<%#Eval("id_soal") %>&id_pre=<%#Eval("pre_test") %>">Pilih Nilai</a>
                                                     </div>
                                                 
                                                </td>
                                    
                                             </tr>
                                     </ItemTemplate>
                                </asp:Repeater>
    </tbody>
  </table>
</div>
  </div>
    
  
</div>
  </div>
</asp:Content>
