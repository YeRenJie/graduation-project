<%@ Page Title="" Language="C#" MasterPageFile="~/myadmin/site/layout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="UI.myadmin.site._default" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .table { width: 100%; line-height: 30px; }
        .textRight { width: 200px; font-weight: bold; }
        .textLeft { }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpNav" runat="server">
    <li class="ind"><a href="/myadmin/site/default.aspx"><i class="ico-home"></i>首页</a></li>
    <li><a href="/myadmin/user/userInfo.aspx"><i class=""></i>用户资料</a></li>
    <li><a href="/myadmin/notes/note.aspx"><i class=""></i>收支管理</a></li>
    <li><a href="/myadmin/notes/cost.aspx"><i class=""></i>消费统计</a></li>
    <li><a href="/myadmin/feedback/feedback.aspx"><i class=""></i>在线留言</a></li>
    <li><a href="/myadmin/webInfo/about.aspx"><i class=""></i>关于我们</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpPosition" runat="server">
    <i></i>欢迎您来到<b>YeMoney</b>管理系统。
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="server">
    <div class="set">
        <table class="table">
            <tr>
                <td class="textRight">最近登陆IP：</td>
                <td class="textLeft">
                    <%=model.ULastIP%>
                </td>
            </tr>
            <tr>
                <td class="textRight">最近登陆时间：</td>
                <td class="textLeft">
                    <%=model.ULastTime%>
                </td>
            </tr>
            <tr>
                <td class="textRight">服务器名称：</td>
                <td class="textLeft">
                    <%=Server.MachineName%>
                </td>
            </tr>
            <tr>
                <td class="textRight">服务器IP：</td>
                <td class="textLeft">
                    <%
                        if (Request.ServerVariables["LOCAL_ADDR"] == "::1")
                        {
                            Response.Write("127.0.0.1");
                        }
                        else
                        {
                            Response.Write(Request.ServerVariables["LOCAL_ADDR"]);
                        }
                    %>
                </td>
            </tr>
            <tr>
                <td class="textRight">NET框架版本：</td>
                <td class="textLeft">
                    <%=Environment.Version.ToString()%>
                </td>
            </tr>
            <tr>
                <td class="textRight">服务器操作系统：</td>
                <td class="textLeft">
                    <%=Environment.OSVersion.ToString()%>
                </td>
            </tr>
            <tr>
                <td class="textRight">IIS环境：</td>
                <td class="textLeft">
                    <%=Request.ServerVariables["SERVER_SOFTWARE"]%>
                </td>
            </tr>
            <tr>
                <td class="textRight">服务器端口：</td>
                <td class="textLeft">
                    <%=Request.ServerVariables["SERVER_PORT"]%>
                </td>
            </tr>
            <tr>
                <td class="textRight">虚拟目录绝对路径：</td>
                <td class="textLeft">
                    <%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%>
                </td>
            </tr>
            <tr>
                <td class="textRight">脚本超时时间：</td>
                <td class="textLeft">
                    <%=Server.ScriptTimeout%> 秒</td>
            </tr>
            <tr>
                <td class="textRight">服务器CPU数量：</td>
                <td class="textLeft">
                    <%=Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS")%>个
                </td>
            </tr>
            <tr>
                <td class="textRight">CPU类型</td>
                <td class="textLeft">
                    <%=Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")%></td>
            </tr>
            <tr>
                <td class="textRight">HTTPS支持：</td>
                <td class="textLeft">
                    <%=Request.ServerVariables["HTTPS"]%>
                </td>
            </tr>
            <tr>
                <td class="textRight">seesion总数：</td>
                <td class="textLeft">
                    <%=Session.Keys.Count.ToString()%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
