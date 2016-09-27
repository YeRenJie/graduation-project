<%@ Page Title="" Language="C#" MasterPageFile="~/myadmin/site/layout.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="UI.myadmin.feedback.feedback" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/myadmin/scripts/txtIntime.js"></script>
    <script type="text/javascript">
        var msgBox = null;
        var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        $(function () {
            msgBox = new MsgBox({ imghref: "/myadmin/images/" });
            $("body").textIntime({
                maxLength: 300,
                divSize: "#limit",
                divInput: "#txtMsg"
            });
            $("#tj").click(function () {
                if ($("#txtThemb").val() == "") {
                    msgBox.showMsgInfoSide("txtThemb", "留言主题不能为空。", 800);
                    $("#txtThemb").focus();
                    return;
                }
                else if ($("#txtThemb").val().length > 60) {
                    msgBox.showMsgInfoSide("txtThemb", "留言主题不能超过60个字。", 800);
                    $("#txtThemb").focus();
                    return;
                }
                else if ($("#txtEmail").val() == "") {
                    msgBox.showMsgInfoSide("txtEmail", "电子邮箱不能为空。", 800);
                    $("#txtEmail").focus();
                    return;
                }
                else if (!reg.test($("#txtEmail").val())) {
                    msgBox.showMsgInfoSide("txtEmail", "电子邮箱格式不正确。", 800);
                    $("#txtEmail").focus();
                    return;
                }
                else if ($("#txtMsg").val() == "") {
                    msgBox.showMsgInfoSide("txtMsg", "留言内容不能为空。", 800);
                    $("#txtMsg").focus();
                    return;
                }
                else {
                    $.ajax({
                        url: "/myadmin/feedback/feedback.aspx",
                        type: "post",
                        dataType: "html",
                        data: $("#myForm").serialize(),
                        beforeSend: function (xhr) {
                            msgBox.showMsgWait("留言中...");
                        },
                        success: function (data) {
                            switch (data) {
                                case "500":
                                    msgBox.showMsgInfo("系统繁忙，请稍后再试");
                                    break;
                                case "ok":
                                    msgBox.showMsgOk("留言成功，即将刷新", function () {
                                        $("#m").show();
                                        window.location.reload();
                                    });
                                    break;
                                default:
                                    break;
                            }
                        },
                        error: function (xhr, txtStatus, errMsg) {
                            msgBox.showMsgErr("服务器错误，请联系管理员");
                        },
                        complete: function (xhr, txtstatus) {

                        }
                    });
                }
            })
        })
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpNav" runat="server">
    <li><a href="/myadmin/site/default.aspx"><i class="ico-home"></i>首页</a></li>
    <li><a href="/myadmin/user/userInfo.aspx"><i class=""></i>用户资料</a></li>
    <li><a href="/myadmin/notes/note.aspx"><i class=""></i>收支管理</a></li>
    <li><a href="/myadmin/notes/cost.aspx"><i class=""></i>消费统计</a></li>
    <li class="ind"><a href="/myadmin/feedback/feedback.aspx"><i class=""></i>在线留言</a></li>
    <li><a href="/myadmin/webInfo/about.aspx"><i class=""></i>关于我们</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpPosition" runat="server">
    首页&gt;在线留言
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="server">
    <div class="flist clear">
        <p>你好,如果你有任何投诉或建议，可以在这里留言通知我们。</p>
        <div class="flists">
            <asp:Repeater ID="rp" runat="server">
                <ItemTemplate>
                    <div class="item minheight">
                        <div class="fPic">
                            <img src="/upload/user/<%#GetUser(Eval("fUID").ToString()).UPic %>" alt="" title="" width="40" height="40" />
                        </div>
                        <div class="fInfo">
                            <span class="floor"><%#GetFloor(Eval("fID").ToString()).ToString() %>F</span><p><b><%#GetUser(Eval("fUID").ToString()).UNick %></b> 来自 IP:<%#Eval("fIP") %>&nbsp;&nbsp;&nbsp; <%#Eval("fTime") %></p>
                            <p>主题：<b><%#Eval("fThemb") %></b></p>
                            <div class="fMsg">内容：<%#Eval("fMsg") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="fPage"><span>当前导航页 <b><%=cp %></b> / <b><%=pageCount %></b>，共 <b><%=totalSize %></b> 条记录 </span>&nbsp;&nbsp;&nbsp;&nbsp;<a href="?page=1" title="首页">首页</a><a href="<%=prevHtml %>" title="上一页">上一页</a><a href="<%=nextHtml %>" title="下一页">下一页</a><a href="<%="?page="+pageCount %>" title="尾页">尾页</a></div>
    </div>
    <div class="msgBar clear">
        <div class="iDiv"><span>留言主题：</span><input type="text" class="io" name="txtThemb" id="txtThemb" /><label>* 请填写你这次留言的主题，或标题，少于60个字。必填</label></div>
        <div class="iDiv"><span>电子邮箱：</span><input type="text" class="io" name="txtEmail" id="txtEmail" value="<%=eMail %>" /><label>* 请填写你的电子邮箱，方便我们回复您，必填。</label></div>
        <div class="iDiv"><span>留言内容：</span><textarea name="txtMsg" id="txtMsg"></textarea><label>* 请填写你的留言内容，少于300个字符，必填。</label></div>
        <div class="iDiv">
            <span style="visibility: hidden;">字符统计：</span>您还可以输入<b id="limit">300</b> 字
        </div>
        <br />
        <div class="iDiv"><span style="visibility: hidden;">留言内容：</span><a title="提交" id="tj">提交</a> <b id="m" style="color: #f00; display: none;">留言成功</b></div>
    </div>
    <%--<script type="text/javascript">
        document.onkeydown = function (evt) {
            var evt = window.event ? window.event : evt;
            if (evt.keyCode == 13) {
                $("#tj").click();
            }
        }
    </script>--%>
</asp:Content>
