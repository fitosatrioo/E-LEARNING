<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_menu.ascx.vb" Inherits="e_learning.uc_menu" %>


<div class="sidebar-content" runat="server" id="divadmin">
    <ul class="navigation yamm-fw">
        
        <li runat="server" id="karyawan" ><a href="el_adm_karyawan.aspx"  ><span>Manajemen Karyawan</span> <i class="icon-user"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_autho_grp"><a href="About.aspx">- Group Manajemen</a></li>
                <li runat="server" id="a_autho_user"><a href="Contact.aspx">- User Manajemen</a></li>
            </ul>--%>
        </li>
        <li runat="server" id="dept"><a href="el_adm_dept.aspx"  ><span> Manajemen Departement</span> <i class="icon-briefcase"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_act_defect"><a href="#">- Import Defect</a></li>
                <li runat="server" id="a_act_cc"><a href="#">- Sertifikasi TAM</a></li>
                <li runat="server" id="a_act_gatepass"><a href="#">- Gate Pass</a></li>
            </ul>--%>
        </li>
        <li runat="server" id="mn_materi"><a href="el_dept.aspx?idrec=1" ><span>Menu Materi</span> <i class="icon-book"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_lap_def"><a href="materi.aspx">- Materi</a></li>
                <li runat="server" id="a_lap_gate"><a href="adm_dept.aspx">- Admin Materi</a></li>
                <li runat="server" id="a_lap_inout"><a href="#">- Laporan CCR IN/OUT</a></li>
            </ul>--%>
        </li>

         <li runat="server" id="bnk_soal"><a href="el_dept.aspx?idrec=2"><span>Bank Soal</span> <i class="icon-list"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_help_userguide"><a href="#">- User Guide</a></li>
                <li runat="server" id="a_help_flow"><a href="#">- Flow Chart</a></li>
                <li runat="server" id="a_help_uat"><a href="#">- U.A.T</a></li>
            </ul>--%>
        </li>

        <li runat="server" id="mn_quiz"><a href="el_dept.aspx?idrec=3"><span>Manajemen Quiz</span> <i class="icon-list"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_help_userguide"><a href="#">- User Guide</a></li>
                <li runat="server" id="a_help_flow"><a href="#">- Flow Chart</a></li>
                <li runat="server" id="a_help_uat"><a href="#">- U.A.T</a></li>
            </ul>--%>
        </li>

        
        <li runat="server" id="nilai"><a href="el_dept.aspx?idrec=4"><span>Hasil Test</span> <i class="icon-list"></i></a>
            <%--<ul style="background-color: transparent">
                <li runat="server" id="a_help_userguide"><a href="#">- User Guide</a></li>
                <li runat="server" id="a_help_flow"><a href="#">- Flow Chart</a></li>
                <li runat="server" id="a_help_uat"><a href="#">- U.A.T</a></li>
            </ul>--%>
        </li>
        
    </ul>
</div>
