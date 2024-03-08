<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_pilihan_quiz.aspx.vb" Inherits="e_learning.el_pilihan_quiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container mt-3">
              
   <div class="card text-center mt-4">
  <div class="card-header" style="background-color: white">
        
    
      <h2 class="text-center" style="color: #47bb94">Daftar Quiz</h2>

  </div>
       <div runat="server" id="mig29">
           <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
              <ItemTemplate>
                  
                      <div id="su25" runat="server">
                          <div class="card-body text-center  "style="background-color: #47bb94">
                               <h4 class="card-title" style="color: white">PRE TEST</h4>
                                <p class="card-text" style="color: white"><%#Eval("judul_training") %> ( Paket <%#Eval("paket_soal") %> )</p>
                                 <a href="el_exam.aspx?id_paket=<%#Eval("id_paket") %>&id_mquiz=<%#Eval("id_mquiz") %>" class="btn btn-warning" onclick="return confirm('Ingin Mulai Quiz?');">Mulai Quiz</a>
                          </div>
                         
                             
                             <asp:Label Visible ="false" ID="lblpre" runat="server" Text='<%#Eval("pre") %>'></asp:Label>


                           <div id="su35" runat="server">
                              <div class="card-body text-center "style="background-color: #47bb94">
                                       <h4 class="card-title" style="color: white">PRE TEST</h4>
                                       <p class="card-text"> <b>Pre Test Telah Selesai</b> </p>
                                 
                              </div>
                              <div class="card-footer text-muted" style="background-color: white">
                             
                              </div>
                          </div>
                         
                      </div>
                 
                   

          </ItemTemplate>

                       </asp:Repeater>



          
        
   <%--    <div class="card-body mt-3">
    <h4 class="card-title">Pemrograman Dasar</h4>
    <p class="card-text">PRE TEST (120 Menit)</p>
   
    <a href="el_exam2.aspx" class="btn btn-warning">Mulai Quiz</a>
  </div>
        <div class="card-footer text-muted">
    
  </div>

        <div class="card-body mt-3">
    <h4 class="card-title">Belum Tersedia</h4>
   
   
    <a href="el_exam2.aspx" class="btn btn-info">Mulai Quiz</a>
  </div>
        <div class="card-footer text-muted">
    
  </div>--%>
  
</div>
    </div>
</asp:Content>
