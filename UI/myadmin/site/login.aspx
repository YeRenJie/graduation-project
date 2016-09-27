<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="UI.myadmin.site.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="/myadmin/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/myadmin/css/common.css" />
    <script type="text/javascript" src="/myadmin/scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/myadmin/scripts/common.js"></script>
    <script type="text/javascript" src="/myadmin/scripts/msgBox.js"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="/myadmin/scripts/IE6PNG.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('div, img, background');
    </script>
    <![endif]-->
</head>
<body style="background: #f1f1f1;">
    <div class="logo">
        <a href="/myadmin/site/default.aspx" title="">
            <img src="../images/logo2.png" alt="" title="" /></a>
    </div>
    <div id="login" class="minheight">
        <div class="loginBar">
            <form id="form1" runat="server">
                <div class="inputDiv">
                    <span>用户名</span>
                    <input type="text" name="txtName" id="txtName" class="txt" placeholder="" />
                </div>
                <div class="inputDiv">
                    <span>密码</span>
                    <input type="password" name="txtPwd" id="txtPwd" class="txt" placeholder="" />
                </div>
                <div class="submit">
                    <span class="isAutoLogin">
                        <input type="checkbox" value="1" name="isAutoLogin" id="ck" checked="checked" />
                        记住我的登录信息</span><a id="loginIn">登陆</a>&nbsp;<a id="reg" >注册</a>
                </div>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        doResize();
        $(window).resize(function () { doResize(); });
        function doResize() { $(".logo").css({ "margin-top": Math.floor(($(window).height() - 400) / 2) + "px" }); }
    </script>
    <script type="text/javascript">
        var msgBox = null;
        $(function () {
            msgBox = new MsgBox({ imghref: "/myadmin/images/" });
            $("#reg").bind("click", function () {
                window.location = "/myadmin/site/reg.aspx";
            });
            $("#loginIn").bind("click", function () {
                if ($("#txtName").val() == "") {
                    msgBox.showMsgInfo("用户名不能为空！");
                    $("#txtName").focus();
                    return;
                }
                else if ($("#txtPwd").val() == "") {
                    msgBox.showMsgInfo("密码不能为空！");
                    $("#txtPwd").focus();
                    return;
                }
                else {
                    $.ajax({
                        url: "/myadmin/check/checkLogin.ashx",
                        data: $("#form1").serialize(),
                        type: "post",
                        dataType: "html",
                        timeout: 10000,
                        beforeSend: function (xhr) {
                            msgBox.showMsgWait("验证中");
                        },
                        success: function (data) {
                            if (data == "usererr") {
                                msgBox.showMsgErr("用户名不存在！");
                            }
                            else if (data == "down") {
                                msgBox.showMsgInfo("该用户已经禁用！");
                            }
                            else if (data == "pwderr") {
                                msgBox.showMsgErr("密码错误！");
                            }
                            else if (data == "ok") {
                                msgBox.showMsgOk("登陆成功！跳转中...", function () {
                                    window.location = "/myadmin/site/default.aspx";
                                });
                            }
                            else {
                                msgBox.showMsgInfo("系统繁忙，请稍后再试！");
                            }
                        },
                        error: function (xhr, txtstatus, errMsg) {
                            msgBox.showMsgErr("服务器错误，请联系管理员。");
                        },
                        complete: function (xhr, txtstatus) {
                            //msgBox.hidBox();
                        }
                    });
                }
            });
        })
    </script>
    <script type="text/javascript">
        document.onkeydown = function (evt) {
            var evt = window.event ? window.event : evt;
            if (evt.keyCode == 13) {
                $("#loginIn").click();
            }
        }
    </script>
</body>
</html>
