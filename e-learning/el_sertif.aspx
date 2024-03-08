<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="el_sertif.aspx.vb" Inherits="e_learning.el_sertif" %>
<!DOCTYPE html>

<head>
  <meta charset="UTF-8">
  <title>Sertifikat Pelatihan Agung Logistics</title>
  <style>
    .image {
      max-width: 100%;
      max-height: 100%;
      position: absolute;
    }

    .text {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 18px;
      font-weight: bold;
      font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
      padding-bottom: 230px;
      

    }

    .text2 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 50px;
      font-weight: bold;
      font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
      padding-bottom: 170px; 

    }

    .text3 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 18px;
      font-weight: bold;
      font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
      padding-bottom: 80px;

    }

    .text4 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 28px;
      font-weight: bold;
      font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
      padding-bottom: 30px;

    }

    .text5 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: red;
      font-size: 20px;
      font-weight: bold;
      font-style: italic;
      font-family: 'Franklin Gothic';
      padding-top: 20px;
   
    }

    .text6 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 15px;
     
      font-family: 'Franklin Gothic';
      padding-top: 70px;

    }

    .text7 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: darkblue;
      font-size: 15px;
    
      font-family: 'Franklin Gothic';
      padding-top: 110px;

    }
    .text8 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: black;
      font-size: 20px;
      font-weight: bold;
      font-family: Georgia, 'Times New Roman', Times, serif;
      padding-top: 350px;

    }
    .text9 {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      color: black;
      font-size: 20px;
      font-family: Georgia, 'Times New Roman', Times, serif;
      padding-top: 440px;

    }

     .text10 {
      position: absolute;
      top: 50%;
      transform: translate(-50%, -50%);
      color: black;
      font-size: 20px;
      font-family: Georgia, 'Times New Roman', Times, serif;
      padding-top: 420px;
      margin-right: 350px;
      

    }

    body,
    html {
      height: 100%;
      margin: 0;
      display: flex;
      align-items: center;
      justify-content: center;
      
    }
  </style>
</head>

<body>

  <img src="img/sertif.png" alt="Gambar" class="image">
  <div class="text">
    Diberikan kepada:
  </div>
  <div class="text2" id="nama" runat="server" >
    Username
  </div>
  <div class="text3">
    Atas Partisipasinya Sebagai

  </div>
  <div class="text4">
    PESERTA


  </div>
  <div class="text5" runat="server" id="judul" >
     “Judul Training”
    



  </div>
    <div class="text6" runat="server" id="tgl">
      yang diselenggarakan pada tanggal 11 Maret 2005



    </div>
    <div class="text8">
        Eka Kurniawan

    </div>
    
    <div class="text9">
        HR & GA Manager

    </div>
    <div class="text10" runat="server" id="lblid">
      


        IT001

    </div>
   

 <script>
     window.print();
 </script>

</body>




