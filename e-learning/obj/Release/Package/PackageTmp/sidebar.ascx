<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="sidebar.ascx.vb" Inherits="e_learning.sidebar" %>

<div id="layoutSidenav">
      <div id="layoutSidenav_nav">
   <nav  style='background-image: url("img/bg2.png')" class="sb-sidenav accordion" id="sidenavAccordion">
          <div class="sb-sidenav-menu">
              <hr  />
            <div class="nav">
                 </a>
                <hr />
              <a style="color: white" class="nav-link" href="el_pilihan_quiz.aspx">
              
                PRE TEST
                    <div class="sb-nav-link-icon"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-patch-question-fill" viewBox="0 0 16 16">
  <path d="M5.933.87a2.89 2.89 0 0 1 4.134 0l.622.638.89-.011a2.89 2.89 0 0 1 2.924 2.924l-.01.89.636.622a2.89 2.89 0 0 1 0 4.134l-.637.622.011.89a2.89 2.89 0 0 1-2.924 2.924l-.89-.01-.622.636a2.89 2.89 0 0 1-4.134 0l-.622-.637-.89.011a2.89 2.89 0 0 1-2.924-2.924l.01-.89-.636-.622a2.89 2.89 0 0 1 0-4.134l.637-.622-.011-.89a2.89 2.89 0 0 1 2.924-2.924l.89.01.622-.636zM7.002 11a1 1 0 1 0 2 0 1 1 0 0 0-2 0zm1.602-2.027c.04-.534.198-.815.846-1.26.674-.475 1.05-1.09 1.05-1.986 0-1.325-.92-2.227-2.262-2.227-1.02 0-1.792.492-2.1 1.29A1.71 1.71 0 0 0 6 5.48c0 .393.203.64.545.64.272 0 .455-.147.564-.51.158-.592.525-.915 1.074-.915.61 0 1.03.446 1.03 1.084 0 .563-.208.885-.822 1.325-.619.433-.926.914-.926 1.64v.111c0 .428.208.745.585.745.336 0 .504-.24.554-.627z"/>
</svg></div>
              </a>
                <hr />
              <!-- <div class="sb-sidenav-menu-heading">Core</div> -->
            <a style="color: white" class="nav-link active" href="el_pilihan_materi2.aspx">
               
                Daftar Materi
                 <div style="float: right" class="sb-nav-link-icon"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp;<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-book-half" viewBox="0 0 16 16">
  <path d="M8.5 2.687c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783z"/>
</svg></div>
              </a>
                <hr />
              <a style="color: white" class="nav-link" href="el_pilihan_post.aspx">
              
               POST TEST
                    <div class="sb-nav-link-icon"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-patch-question-fill" viewBox="0 0 16 16">
  <path d="M5.933.87a2.89 2.89 0 0 1 4.134 0l.622.638.89-.011a2.89 2.89 0 0 1 2.924 2.924l-.01.89.636.622a2.89 2.89 0 0 1 0 4.134l-.637.622.011.89a2.89 2.89 0 0 1-2.924 2.924l-.89-.01-.622.636a2.89 2.89 0 0 1-4.134 0l-.622-.637-.89.011a2.89 2.89 0 0 1-2.924-2.924l.01-.89-.636-.622a2.89 2.89 0 0 1 0-4.134l.637-.622-.011-.89a2.89 2.89 0 0 1 2.924-2.924l.89.01.622-.636zM7.002 11a1 1 0 1 0 2 0 1 1 0 0 0-2 0zm1.602-2.027c.04-.534.198-.815.846-1.26.674-.475 1.05-1.09 1.05-1.986 0-1.325-.92-2.227-2.262-2.227-1.02 0-1.792.492-2.1 1.29A1.71 1.71 0 0 0 6 5.48c0 .393.203.64.545.64.272 0 .455-.147.564-.51.158-.592.525-.915 1.074-.915.61 0 1.03.446 1.03 1.084 0 .563-.208.885-.822 1.325-.619.433-.926.914-.926 1.64v.111c0 .428.208.745.585.745.336 0 .504-.24.554-.627z"/>
</svg></div>
              </a>
                <hr />
              <a style="color: white" class="nav-link" href="el_pilihan_nilai.aspx">
             
                Nilai / Hasil
                     <div class="sb-nav-link-icon">
                         &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; <svg width="18" height="18" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <mask id="mask0_152_363" style="mask-type:luminance" maskUnits="userSpaceOnUse" x="3" y="0" width="26" height="32">
                        <path d="M6.66683 29.3332H25.3335C25.6871 29.3332 26.0263 29.1927 26.2763 28.9426C26.5264 28.6926 26.6668 28.3535 26.6668 27.9998V9.33317H20.0002V2.6665H6.66683C6.31321 2.6665 5.97407 2.80698 5.72402 3.05703C5.47397 3.30708 5.3335 3.64622 5.3335 3.99984V27.9998C5.3335 28.3535 5.47397 28.6926 5.72402 28.9426C5.97407 29.1927 6.31321 29.3332 6.66683 29.3332Z" fill="white" stroke="white" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
                        <path d="M20 2.6665L26.6667 9.33317" stroke="white" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
                        <path d="M11.3335 19.333L15.3335 22.6663L21.3335 15.333" stroke="black" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/></mask><g mask="url(#mask0_152_363)">
                        <path d="M0 0H32V32H0V0Z" fill="white"/></g>
                        </svg>

                </div>
              </a>
                <hr />

                       
            
            </div>
          </div>
        
        </nav>
      </div>
