<%@ Page Title="" Language="C#" MasterPageFile="~/myadmin/site/layout.Master" AutoEventWireup="true" CodeBehind="cost.aspx.cs" Inherits="UI.myadmin.notes.cost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #cIncome { width: 80px; border: 1px solid #d9d9d9; border-radius: 3px; padding: 2px; font-size: 12px; }
    </style>
    <script type="text/javascript">
        var msgBox = null;
        $(function () {
            msgBox = new MsgBox({ imghref: "/myadmin/images/" });
            $("#cIncome").focus(function () {
                $("#addNote").show();
            });

            $("#addNote a").click(function () {
                if ($("#cIncome").val() == "") {
                    msgBox.showMsgInfoSide("cIncome", "总收入不能为空。", 800);
                    $("#cIncome").focus();
                    return;
                }
                else if (!$("#cIncome").val().match(/^(([1-9]\d*)|\d)(\.\d{1,2})?$/)) {
                    msgBox.showMsgInfoSide("cIncome", "总收入格式不正确", 800);
                    $("#cIncome").focus();
                    return;
                }
                else {
                    $.ajax({
                        url: "/myadmin/notes/cModify.ashx",
                        type: "post",
                        data: $("#myForm").serialize(),
                        dataType: "html",
                        timeout: 15000,
                        success: function (data) {
                            if (data == "busy") {
                                msgBox.showMsgErr("系统繁忙,请稍后再试");
                            }
                            else if (data == "ok") {
                                msgBox.showMsgOk("更新成功。", function () {
                                    //window.location.reload();
                                    window.location = "/myadmin/notes/cost.aspx";
                                });
                            }
                        },
                        error: function (xhr, txtStatus, errMsg) {
                            alert(errMsg);
                        },
                        complete: function (xhr) {

                        }
                    });
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpNav" runat="server">
    <li><a href="/myadmin/site/default.aspx"><i class="ico-home"></i>首页</a></li>
    <li><a href="/myadmin/user/userInfo.aspx"><i class=""></i>用户资料</a></li>
    <li><a href="/myadmin/notes/note.aspx"><i class=""></i>收支管理</a></li>
    <li class="ind"><a href="/myadmin/notes/cost.aspx"><i class=""></i>消费统计</a></li>
    <li><a href="/myadmin/feedback/feedback.aspx"><i class=""></i>在线留言</a></li>
    <li><a href="/myadmin/webInfo/about.aspx"><i class=""></i>关于我们</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpPosition" runat="server">
    首页&gt;消费统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="server">
    <div class="cost">
        <p>在这里，你可以查看你的总收支和总收入信息。</p>
        <p>
            您本月的总收入&nbsp;=&nbsp;<b><input type="text" name="cIncome" style="width: 65px; font-weight: bold; color: #444; font-size: 14px;" id="cIncome" value="<%=model.CMoney %>" /><input type="hidden" name="cID" value="<%=model.CID%>" />
                元</b>&nbsp;&nbsp;<span id="addNote" style="display: none;"><a title="提交">提交</a></span>
        </p>
        <p>您本月的总支出&nbsp;=&nbsp;<b><%=countPay %> 元</b><a href="/myadmin/notes/note.aspx?ts=<%=firstDate.ToString("yyyy-MM-dd")+"|"+DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") %>" title="详情" style="color: #f00"> 「详情」</a></p>
        <p>您本月的总余额&nbsp;=&nbsp;<b><%=model.CMoney-countPay %> 元</b></p>
        <hr style="border: 1px solid  #e5e5e5;" />
        <span>年份：<select name="nf" id="nf">
            <option value="2004">2004</option>
            <option value="2005">2005</option>
            <option value="2006">2006</option>
            <option value="2007">2007</option>
            <option value="2008">2008</option>
            <option value="2009">2009</option>
            <option value="2010">2010</option>
            <option value="2011">2011</option>
            <option value="2012">2012</option>
            <option value="2013">2013</option>
            <option value="2014">2014</option>
            <option value="2015">2015</option>
            <option value="2016">2016</option>
            <option value="2017">2017</option>
            <option value="2018">2018</option>
            <option value="2019">2019</option>
            <option value="2020">2020</option>
            <option value="2021">2021</option>
            <option value="2022">2022</option>
            <option value="2023">2023</option>
            <option value="2024">2024</option>
        </select>&nbsp;月份：<select name="yf" id="yf">
            <option value="01">1</option>
            <option value="02">2</option>
            <option value="03">3</option>
            <option value="04">4</option>
            <option value="05">5</option>
            <option value="06">6</option>
            <option value="07">7</option>
            <option value="08">8</option>
            <option value="09">9</option>
            <option value="10">10</option>
            <option value="11">11</option>
            <option value="12">12</option>
        </select>&nbsp;&nbsp;<span id="delNote" style="display: inline-block;"><a title="查看统计图">查看统计图</a><label></label></span></span>
        <script type="text/javascript">
            $("#delNote").bind("click", function () {
                var dt = $("#nf").val() + "-" + $("#yf").val() + "-" + "01";
                window.location = "/myadmin/notes/cost.aspx?dt=" + dt;
            });
        </script>
        <div class="section">
            <b id="max">200元</b>
            <b id="mid">100元</b>
            <b id="di">0元</b>
            <h2>
                <b id="ys"><%=string.IsNullOrEmpty(Request.QueryString["dt"])?DateTime.Now.Year:DateTime.Parse(Request.QueryString["dt"]).Year %></b>年<b id="ms"><%=string.IsNullOrEmpty(Request.QueryString["dt"])?DateTime.Now.Month:DateTime.Parse(Request.QueryString["dt"]).Month %></b>月每天消费统计柱状图表（停留直方体可显示当天总消费）</h2>
            <script type="text/javascript">
                var ys = $("#ys").text();
                var ms = $("#ms").text();
                $("#nf").find("option").each(function () {
                    if (ys == $(this).val()) {
                        $(this).attr("selected", "selected").siblings().removeAttr("selected");
                    }
                });
                $("#yf").find("option").each(function () {
                    if (ms == $(this).text()) {
                        $(this).attr("selected", "selected").siblings().removeAttr("selected");
                    }
                });
            </script>
            <ul class="timeline">
                <%
                    for (int i = 1; i <= monthDays; i++)
                    {
                        Response.Write("<li><a href='javascript:void(0);' title='" + dcs[i - 1] + "元'><span class='label'>" + i + "</span><span class='count' style='height: " + (int)((dcs[i - 1]) / 2) + "%'>" + dcs[i - 1] + "</span></a></li>");
                    }
                %>
            </ul>
        </div>
    </div>
</asp:Content>
