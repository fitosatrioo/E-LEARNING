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
        <th>Lihat Nilai</th>
        
      </tr>
    </thead>
    <tbody>
     <asp:Repeater ID="Repeater3" runat="server">
                                     <ItemTemplate>
                                             <tr class="text-center">

                                                  <td>
                                                     <asp:Label runat="server" Text='<%#Eval("judul") %>'></asp:Label>
                                                  

                                                     <%--<asp:TextBox ID="txtNama2" runat="server" Text='<%# Eval("NamaSekolah") %>' Visible="false"></asp:TextBox>--%>
                                                 </td>
                                                 <td>
                                                    <a class="btn btn-info" href="el_nilai.aspx?id_mquiz=<%#Eval("id_mquiz") %>&id_post=<%#Eval("id_soal") %>&id_pre=<%#Eval("pre_test") %>">Pilih Nilai</a>
                                                 
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
