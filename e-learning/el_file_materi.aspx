<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_file_materi.aspx.vb" Inherits="e_learning.el_file_materi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <asp:Label ID="lblData" runat="server" Text="" Visible="true"></asp:Label>
            <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                       <div class="card mb-4 mt-3">
      <div class="card-body">
        <h5 class="card-title" id="lbljudul" runat="server"></h5>
              </ItemTemplate>   

                <div class="col-xs-6" id="su57" runat="server">
                   
                </div>
                
        
      </div>
    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <div class="col-xs-6" >
                    <div>
                        <div style='max-width: 600px; margin: 0 auto; margin-top: 50px;' class='card text-center'>
                            <div class="card-body">
                                <label runat="server" id="lblbc" style="color: green;" >Telah Dibaca</label>
                                <button type="button" class="btn btn-success" runat="server" id="btn_log" onserverclick="btn_log_ServerClick">Tandai Telah Selesai Baca</button>
                            </div>
                        </div>
                    </div>
                    
                </div>
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click"/>
        <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click"/>
        <asp:LinkButton runat="server" ID="linkpost" CssClass="btn btn-primary"  Text="Post Test" OnClick="linkpost_Click"></asp:LinkButton>
    </div>
   <%--<div class="container mt-4">
              <center>
                   <h1 style="">Pelajari Materi </h1>
                   
              </center>
   
   
               <asp:Repeater ID="Repeater1" runat="server">
                           <ItemTemplate>
    <div class="card mb-4 mt-3">
      <div class="card-body">
        <h5 class="card-title"><%#Eval("nama_materi") %></h5>
              </ItemTemplate>
                      </asp:Repeater>     
   

                <div class="col-xs-6" id="su56" runat="server">
                   
                </div>
                <div class="col-xs-6" >
                    <div>
                        <div style='max-width: 600px; margin: 0 auto; margin-top: 50px;' class='card text-center'>
                            <div class="card-body">
                                <label runat="server" id="lblbc" style="color: green;" >Telah Dibaca</label>
                                <button type="button" class="btn btn-success" runat="server" id="btn_log" onserverclick="btn_log_ServerClick">Tandai Telah Selesai Baca</button>
                            </div>
                        </div>
                    </div>
                    
                </div>
        
      </div>
    </div>--%>
    
</asp:Content>
