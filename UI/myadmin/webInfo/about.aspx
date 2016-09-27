<%@ Page Title="" Language="C#" MasterPageFile="~/myadmin/site/layout.Master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="UI.myadmin.webInfo.about" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpNav" runat="server">
    <li><a href="/myadmin/site/default.aspx"><i class="ico-home"></i>首页</a></li>
    <li><a href="/myadmin/user/userInfo.aspx"><i class=""></i>用户资料</a></li>
    <li><a href="/myadmin/notes/note.aspx"><i class=""></i>收支管理</a></li>
    <li><a href="/myadmin/notes/cost.aspx"><i class=""></i>消费统计</a></li>
    <li><a href="/myadmin/feedback/feedback.aspx"><i class=""></i>在线留言</a></li>
    <li class="ind"><a href="/myadmin/webInfo/about.aspx"><i class=""></i>关于我们</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpPosition" runat="server">
    首页&gt;关于我
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="server">
    <div class="webInfo">
        <p><b>系统名称：</b>：支收管理系统</p>
        <p><b>开发者：</b>叶仁杰</p>
        <p><b>邮箱：</b>cnrj@qq.com</p>
        <p><b>座右铭：</b>现在可以不优秀，但要一直努力。</p>
        <br />
        <p><b>预计下一版本功能：</b></p>
        <p>00.【新增】数据可导出Excel功能</p>
        <p>01.【新增】收入管理功能，支持圆形统计图</p>
        <p>03.【新增】多用户管理功能，可能有角色管理，权限管理（看工作之外的时间是否充足来决定）。</p>
        <p>04.【其他】开放在线留言邮件发送功能，支持163，google,qq,等邮箱格式。</p>
        <br />    
        <p><b> 支收管理系统 V1.0</b>&nbsp;2015.04.23</p>
        <p>00.【程序】程序语言采用：asp.net，数据库为：SqlServer 2012</p>
        <p>01.【前台】本系统前台采用 HTML+CSS/CSS3+Jquery/javascript+ajax编写，界面扁平化设计，基本无刷新操作数据</p>
        <p>02.【功能】实现消费统计，消费查询，收入管理等功能</p>
        <p>03.【功能】可以根据各种条件查询数据，其中包括（关键字，消费地点，时间，时间段等等）</p>
        <p>04.【功能】用户信息管理功能，可实现用户信息修改，图片支持无刷新自动上传</p>
        <p>05.【功能】在线留言功能,并支持邮箱发送，未开放。在线留言支持字数统计。</p>
        <p>06.【功能】<b>收支管理可双击修改记录。</b></p>
        <p>07.【功能】消费直方图功能，可以通过直方图快速了解消费情况</p>
        <p>08.【功能】可根据时间查询某年某月消费直方图</p>
        <p>09.【功能】左边导航栏固定功能</p>
        <p>10.【功能】保持搜索信息</p>
    </div>
</asp:Content>
