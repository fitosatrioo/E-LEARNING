<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="el_exam.aspx.vb" Inherits="e_learning.el_exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top: 30px">

        <div class="card">
            <div style="background-color: #7ce68a" class="card-header">
                <div class="text-center">
                    <input type="number" runat="server" id="txwaktu" style="display: none" />
                    <asp:Label Text="" ID="jenis" runat="server" />
                    <asp:Label ID="lbltest" Text="fdsafdsa" Visible="false" runat="server"></asp:Label>
                </div>

                <p class="text-center ">Waktu tersisa: <span id="timer">60:00</span></p>

            </div>

            <form>
                <div class="card-body">
                    <div class="col-md-6">
                        <div class="well" style="background-color: white">

                            <div class="form-group" style="margin-top: 50px">
                                <div class="row">

                                    <div class="col-xs-6" runat="server" id="su57">
                                        <label for="exampleFormControlInput1"></label>

                                    </div>


                                </div>
                                <asp:LinkButton ID="test" CssClass="btn btn-primary" runat="server" OnClientClick="return confirm('Ingin Submit Jawaban Quiz?');"  >Selesai</asp:LinkButton>
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
    <script type="text/javascript">
        var timer;
        var minutes = document.getElementById('<%= txwaktu.ClientID %>').value;
        var seconds = 00;

        function startTimer() {
            timer = setInterval(updateTimer, 1000);
        }

        function updateTimer() {
            if (seconds === 0) {
                if (minutes === 0) {
                    clearInterval(timer);
                    timerExpired();
                    return;
                } else {
                    minutes--;
                    seconds = 59;
                }
            } else {
                seconds--;
            }

            document.getElementById('timer').innerText = formatTime(minutes, seconds);
        }

        function formatTime(minutes, seconds) {
            var hours = Math.floor(minutes / 60);
            var formattedHours = ('0' + hours).slice(-2);
            var formattedMinutes = ('0' + (minutes % 60)).slice(-2);
            var formattedSeconds = ('0' + seconds).slice(-2);
            return formattedHours + ':' + formattedMinutes + ':' + formattedSeconds;
        }

        function timerExpired() {
            alert("Waktu ujian habis!");

            // Menggunakan JavaScript untuk mengklik LinkButton
            var linkButton = document.getElementById('<%= test.ClientID %>');
            if (linkButton) {
                linkButton.click();
            }

            // Tambahkan kode untuk menyelesaikan ujian di sini
            // Misalnya, mengirimkan hasil ujian ke server
        }
    </script>

</asp:Content>
