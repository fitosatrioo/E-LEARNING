<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site2.Master" CodeBehind="cobauser.aspx.vb" Inherits="e_learning.cobauser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <style>
    .countdown {
      text-align: center;
      font-size: 36px;
      margin-top: 50px;
    }

    .countdown input {
      margin-top: 10px;
    }
  </style>

  <div class="countdown">
    <div>
      <label for="minutes">Masukkan jumlah menit:</label>
          <input type="text" id="apage" runat="server"   ClientIDMode="Static" style="display: none;" />
    </div>
    <div>
   <asp:LinkButton ID="btnselesai"  CssClass="btn btn-success" runat="server" OnClick="btnselesai_Click" OnClientClick="return confirm('Ingin Submit Jawaban Quiz?');"  style="display: none;"  >Selesai</asp:LinkButton>
    </div>
    <div id="timer"></div>
  </div>

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
              alert("Waktu ujian habis!");
              // Enable input
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
