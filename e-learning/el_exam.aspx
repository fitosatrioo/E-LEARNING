<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_exam.aspx.vb" Inherits="e_learning.el_exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top: 30px">
<input type="text" id="apage"  runat="server" ClientIDMode="Static"  style="display: none;"/>
        <div class="card">
            <div style="background-color: #7ce68a" class="card-header">
                <div class="text-center">
                     
                    <asp:Label Text="" ID="jenis" runat="server" />
                    <asp:Label ID="lbltest" Text="fdsafdsa" Visible="false" runat="server"></asp:Label>
                </div>

                <p class="text-center ">Waktu tersisa: <span id="timer"></span></p>

            </div>

            <form>
                <div class="card-body">
                    <div class="col-md-6">
                        <div class="well" style="background-color: white">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                            <div class="form-group" style="margin-top: 50px">
                                <div class="row">
                                     <div class="col-xs-6" >
                                        <asp:Label ID="lblData" runat="server" Text="" Visible="false"></asp:Label>
                                            <br />
                                        
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:Label runat="server" ID="lblno"></asp:Label>
                                                        <div id="su57" runat="server" >

                                                        </div>
                                                </ContentTemplate>
                                                 <Triggers>
                            <asp:PostBackTrigger ControlID="btnNext" />
                            <asp:PostBackTrigger ControlID="btnPrev" />
                            <asp:PostBackTrigger ControlID="test" />
                        </Triggers>
                                            </asp:UpdatePanel>

                                        <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click"/>
                                        <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click"/>
                                         <asp:LinkButton ID="test" CssClass="btn btn-success" runat="server" OnClientClick="return confirm('Ingin Submit Jawaban Quiz?');" visible="false" >Selesai</asp:LinkButton>
                                        <asp:LinkButton ID="btnselesai"  CssClass="btn btn-success" runat="server" OnClick="btnselesai_Click"   style="display: none;"  >Selesai</asp:LinkButton>
                                        <asp:PlaceHolder ID="phButtons" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                                </ContentTemplate>
                                 <Triggers>
                            <asp:PostBackTrigger ControlID="btnNext" />
                            <asp:PostBackTrigger ControlID="btnPrev" />
                            <asp:PostBackTrigger ControlID="test" />
                        </Triggers>
                                </asp:UpdatePanel>
            </form>
            <%--<button type="submit" class="btn btn-primary" id="btnselesai" runat="server" onserverclick="btnselesai_ServerClick1">Selesai</button>--%>
        </div>


    </div>
    </div>
               

                       
            </div> 
              
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
    </main>
      
      </div>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <script>
        var countdownInterval;
        var startTime;
        var countdownInProgress = false;
        
        function startCountdown() {
            if (countdownInProgress) {
                return;
            }

            var minutesInput = document.getElementById('apage');
            var minutes = parseInt(minutesInput.value);

            if (isNaN(minutes) || minutes <= 0) {
                alert('Masukkan jumlah menit yang valid!');
                return;
            }

            startTime = new Date().getTime() + minutes * 60000;

            // Disable input
            minutesInput.disabled = true;

            countdownInProgress = true;
            countdownInterval = setInterval(updateCountdown, 1000);
        }

        function updateCountdown() {
            var now = new Date().getTime();
            var timeRemaining = startTime - now;

            if (timeRemaining <= 0) {
                clearInterval(countdownInterval);
                countdownInProgress = false;

                // Enable input
                alert("Waktu ujian habis!");
                var linkButton = document.getElementById('<%= btnselesai.ClientID %>');
                if (linkButton) {
                    linkButton.click();
                }

            //    document.getElementById('btnselesai').click();
            } else {
                var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);

                document.getElementById('timer').innerHTML = minutes + 'm ' + seconds + 's';
            }
        }

        window.addEventListener('DOMContentLoaded', function () {
            var minutesInput = document.getElementById('apage');
            var storedMinutes = sessionStorage.getItem('countdownMinutes');

            if (storedMinutes) {
                minutesInput.value = storedMinutes;

                var storedStartTime = sessionStorage.getItem('countdownStartTime');
                if (storedStartTime) {
                    startTime = parseInt(storedStartTime);

                    var timeRemaining = startTime - new Date().getTime();
                    if (timeRemaining > 0) {
                        countdownInProgress = true;
                        countdownInterval = setInterval(updateCountdown, 1000);
                        minutesInput.disabled = true;
                    } else {
                        sessionStorage.removeItem('countdownMinutes');
                        sessionStorage.removeItem('countdownStartTime');
                    }
                }
            }
            startCountdown();
        });

        window.addEventListener('beforeunload', function () {
            var minutesInput = document.getElementById('apage');
            sessionStorage.setItem('countdownMinutes', minutesInput.value);
            sessionStorage.setItem('countdownStartTime', startTime);
        });

    </script>




</asp:Content>
