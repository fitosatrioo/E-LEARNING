<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_footer.ascx.vb" Inherits="e_learning.uc_footer" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<br />
<br />
<br />
<br />
<br />
<div class="footer clearfix" style="bottom: 0px; position: fixed; width: 79%;">
    <div class="pull-left">
        &copy; <span runat="server" id="lb_time">2016. MIS ACTrans Company [ No Time ]</span>
        <span id="loadtime"></span>
        <a href="page_error.aspx" style='color: #f00'> [View Error] </a>
    </div>
    <div class="pull-right">
        <span id="lblSessionTime"></span>
    </div>
    <div style="height: 10px">
        <marquee scrollamount="6" behavior="alternate" direction="right">
            <span style="color: #FF0000">Selamat Datang!</span>
<%--            <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="( Click here for download CHROME )" avigateUrl="https://www.google.com/chrome/browser/desktop/index.html" Target="_blank">--%>
            </dx:ASPxHyperLink>
            </div>
    <div style="height: 10px"></div>
</div>
<script type="text/javascript">

    var sessionTimeout = "<%= Session.Timeout * 60%>";
    function DisplaySessionTimeout() {

        var minleft = Math.floor(sessionTimeout / 60);
        var secleft = Math.floor((sessionTimeout % 60));
        var sminlef = ("00" + (minleft).toString()).slice(-2) + ":";
        var ssecleft = ("00" + (secleft).toString()).slice(-2);

        document.getElementById("lblSessionTime").innerHTML = "[ S: <font color='red'><b>" + sminlef + ssecleft + "</b></font> Min ]";
        sessionTimeout = sessionTimeout - 1;

        if (sessionTimeout >= 0)
            window.setTimeout("DisplaySessionTimeout()", 1000);
        else {
            $.jGrowl('Session Anda telah berakhir, harap login kembali', { theme: 'growl-error', header: 'Error !', life: 10000 });
        }
    }

    var before_loadtime = new Date().getTime();
    function Pageloadtime() {
        var aftr_loadtime = new Date().getTime();
        pgloadtime = (aftr_loadtime - before_loadtime) / 1000
        document.getElementById("loadtime").innerHTML = "L: <font color='red'><b>" + pgloadtime + "</b></font> Sec";
    }

    function addLoadEvent(func) {
        var oldonload = window.onload;
        if (typeof window.onload != 'function') {
            window.onload = func
        } else {
            window.onload = function () {
                if (oldonload) {
                    oldonload()
                }
                func()
            }
        }
    }

    addLoadEvent(DisplaySessionTimeout);
    addLoadEvent(Pageloadtime);
</script>
