<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_pilihan_materi2.aspx.vb" Inherits="e_learning.el_pilihan_materi2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
    <h1 style="color:#47bb94">E-Learning <span style="color:darkblue">Agung<span style="color: deepskyblue">logistics</span></span> </h1>
              
    <p>Silakan pilih materi yang ingin Anda pelajari:</p>

 
    <!-- Materi List -->
    <div class="row" runat="server" id="su35">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
       <ItemTemplate>
      <div class="col-md-6" runat="server" id="iyh">
        <div class="card mb-4">
          <div class="card-body">
            <h5 class="card-title"><asp:Label runat="server" ID="Label1" Text='<%#Eval("judul") %>'></asp:Label></h5>
            <p class="card-text">materi kali ini yaitu tentang </p><asp:Label runat="server" ID="lbljudul" Text='<%#Eval("judul_materi") %>'></asp:Label><p class="card-text">silahkan dibaca dan dipelajari ya!</p>
              <div id="su25" runat="server">
                <a href="el_pilihan_materi1.aspx?id_materi=<%#Eval("id_materi") %>&id_mquiz1=<%#Eval("id_mquiz") %>"  class="btn btn-primary">Baca Materi</a>
              </div>
              <div id="su37" runat="server">
                   <p class="card-text"><b>Kerjakan Pre Test Terlebih Dahulu</b> </p>
                  
              </div>
          </div>
        </div>
      </div>
           <asp:Label runat="server" Visible="false" ID="lblpre" Text='<%#Eval("pre") %>'></asp:Label>
           <asp:Label runat="server" Visible="false" ID="Lblmpre" Text='<%#Eval("pre_test") %>'></asp:Label>
         </ItemTemplate>
   </asp:Repeater>

    </div>
        <div class="row" runat="server" id="su57" visible="false">
        
      <div class="col-md-6" runat="server" id="iyh">
        <div class="card mb-4">
          <div class="card-body">
            <h5 class="card-title"><asp:Label runat="server" Text='Kerjakan Pre Test Terlebih Dahulu'></asp:Label></h5>
            
          </div>
        </div>
      </div>

         <

    </div>
  </div>
              </div>
</asp:Content>
