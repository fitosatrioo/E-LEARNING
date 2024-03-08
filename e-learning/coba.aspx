<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="coba.aspx.vb" Inherits="e_learning.coba" %>
    <!DOCTYPE html>
<html>
<head>
  <title>Countdown</title>
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
</head>
<body>

  <div class="countdown">
    <div>
      <label for="minutes">Masukkan jumlah menit:</label>
      <input runat="server" type="hidden" id="minutes" min="1" step="1">
    </div>
    <div>
      <button id="btnselesai" onclick="return confirm('Ingin Mulai Quiz?');">Mulai Countdown</button>
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

          var minutesInput = document.getElementById('minutes');
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
              document.getElementById('minutes').disabled = false;
              document.getElementById('btnselesai').click();
          } else {
              var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
              var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);

              document.getElementById('timer').innerHTML = minutes + 'm ' + seconds + 's';
          }
      }

      window.addEventListener('DOMContentLoaded', function () {
          var minutesInput = document.getElementById('minutes');
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
          var minutesInput = document.getElementById('minutes');
          sessionStorage.setItem('countdownMinutes', minutesInput.value);
          sessionStorage.setItem('countdownStartTime', startTime);
      });
  </script>
</body>
</html>