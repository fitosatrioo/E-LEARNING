<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_pilihan_post.aspx.vb" Inherits="e_learning.el_pilihan_post" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
              
   <div class="card text-center mt-4">
  <div class="card-header" style="background-color: white">
        
    
      <h2 class="text-center" style="color: #47bb94">Daftar Quiz</h2>

  </div>
     <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
               <ItemTemplate>
                  <div id="su27" runat="server">
                          <div class="card-body text-center  "style="background-color: #47bb94">
                               <h4 class="card-title" style="color: white">POST TEST</h4>
                                <p class="card-text" style="color: white"><%#Eval("judul_training") %> ( Paket <%#Eval("paket_soal") %> ) (Manajemen Quiz <%#Eval("judul") %>)</p>
                                 <a href="el_exam.aspx?id_paket=<%#Eval("id_paket") %>&id_mquiz=<%#Eval("id_mquiz") %>" class="btn btn-warning" onclick="return confirm('Ingin Mulai Quiz?');">Mulai Quiz</a>
                          </div>
                         
                             <asp:Label ID="lblpre" visible="false" runat="server" Text='<%#Eval("jumlah_materi") %>'></asp:Label>
                             <asp:Label ID="lblmpre" visible ="false" runat="server" Text='<%#Eval("post") %>'></asp:Label>
                             <asp:Label ID="lblpost" visible ="false" runat="server" Text='<%#Eval("materi_dibaca") %>'></asp:Label>
                            
                        
                              <div id="su37" runat="server">
                               <div class="card-body text-center  "style="background-color: #47bb94">
                                       <h4 class="card-title" style="color: white">POST TEST</h4>
                                       <p class="card-text" style="color: white"><%#Eval("judul_training") %> ( Paket <%#Eval("paket_soal") %> ) (Manajemen Quiz <%#Eval("judul") %>)</p>
                                       <p class="card-text">  <b>Baca Materi Terlebih Dahulu!</b> </p>
                                 
                              </div>
                                 
                              <div id="su57" runat="server">
                               <div class="card-body text-center  "style="background-color: #47bb94">
                                       <h4 class="card-title" style="color: white">POST TEST</h4>
                                       <p class="card-text" style="color: white"><%#Eval("judul_training") %> ( Paket <%#Eval("paket_soal") %> ) (Manajemen Quiz <%#Eval("judul") %>)</p>
                                       <p class="card-text"><b>Ujian Telah Selesai</b> </p>
                                       <p class="card-text" style="color: white">Silahkan Isi Link Dibawah Ini</p>
                                       <a class="btn btn-default" style="background-color:cyan" target="_blank" href="<%#Eval("link") %>" style="color: white">Evaluasi Training</a>
                                       
                              </div>
                                  </div>
                                   <br />

                             

          </ItemTemplate>
           </asp:Repeater>
       <label id="lbllink" runat="server" visible="false"></label>
       </div>
        </div>
    

</asp:Content>
