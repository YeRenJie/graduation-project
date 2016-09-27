<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg.aspx.cs" Inherits="UI.myadmin.site.reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <script src="/myadmin/scripts/jquery.form.js"></script>
        <link rel="stylesheet" type="text/css" href="/myadmin/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/myadmin/css/common.css" />
    <script type="text/javascript" src="/myadmin/scripts/jquery.min.js"></script>
    <script type="text/javascript" src="/myadmin/scripts/common.js"></script>
    <script type="text/javascript" src="/myadmin/scripts/msgBox.js"></script>

</head>
<body>
     <form id="myForm" runat="server" enctype="multipart/form-data">        
         <div class="userInfo">
             <div class="uDiv">
                 <div class="uDiv"><span style="color:#0094ff;font-size:23px;">新用户注册</span></div>
                 <div class="uDiv"><span>登录名：</span><input type="text" name="uLoginName" id="uLoginName" class="ur" value="" /><label>* 请填写您的登录名。必填</label></div>
                 <div class="uDiv"><span>密  码：</span><input type="text" name="uLoginPwd" id="uLoginPwd" class="ur" value="" /><label>* 请填写您的密码。必填</label></div>
                 <div class="uDiv"><span>确认密码：</span><input type="text" name="uLoginPwds" id="uLoginPwds" class="ur" value="" /><label>* 请再次填写您的密码。必填</label></div>
                 <span>用户头像：</span><img src="<%="/upload/user/thumb/thumb_"+model.UPic %>" alt="添加头像" title="添加头像" width="100" style="border: 1px solid #ccc; padding: 3px; background: #e5e5e5; cursor: pointer" id="usImg" />
                 <input type="file" onchange="validate()" name="upImg" class="upImg" id="upImg" />
                 <input type="hidden" value="<%=model.UPic %>" name="imgsrc" id="imgsrc" />
                 <label>* 点击添加头像。运行上传最大大小为2M，支持png,jpg,jpeg,gif格式</label>
             </div>
             <div class="uDiv"><span>用户昵称：</span><input type="text" name="uNick" id="uNick" class="ur" value="" /><label>* 请填写您的昵称，1到16个字之间。必填</label></div>
             <div class="uDiv"><span>真实姓名：</span><input type="text" name="uRealName" id="uRealName" class="ur" value="" /><label>* 请填写您的真实姓名，2到16个字之间。必填</label></div>
             <div class="uDiv">
                 <span>性别：</span>
                 <select name="uSex">
                     <%=select %>
                 </select><label> * 请选择您的性别。</label>
             </div>
             <div class="uDiv"><span>电子邮箱：</span><input type="text" name="uEmail" id="uEmail" class="ur" value="" /><label>* 请填写您的电子邮箱。必填</label></div>
             <div class="uDiv"><span>联系手机：</span><input type="text" name="uPhone" id="uPhone" class="ur" value="" /><label>* 请填写您的联系手机。可选</label></div>
             <div class="uDiv"><span>QQ：</span><input type="text" name="uQQ" id="uQQ" class="ur" value="" /><label>* 请填写您的QQ账号。必填</label></div>
             <div class="uDiv"><span>联系地址：</span><input type="text" name="uAddress" id="uAddress" class="ur" value="" /><label>* 请填写您的联系地址。可选，少于255个字</label></div>
             <div class="iDiv"><a title="注册" id="tj">注册</a>&nbsp; <a title="返回" id="fh">返回</a></div>  <div class="iDiv"></div>
         </div>
           </form>
    <script type="text/javascript">
        doResize();
        $(window).resize(function () { doResize(); });
        function doResize() { $(".logo").css({ "margin-top": Math.floor(($(window).height() - 400) / 2) + "px" }); }
    </script>
        <script type="text/javascript">
            function validate() {
                if ($("#upImg").val() == "") {
                    alert("请选择您要上传的图片");
                    return;
                }
                $("#myForm").ajaxSubmit({
                    url: "/myadmin/check/upload.ashx",
                    type: "post",
                    beforeSubmit: function () {
                        $("#usImg").attr("src", "/upload/user/up.jpg");
                    },
                    dataType: "json",
                    data: "upImg=" + $('#upImg').val(),
                    success: function (data) {
                        if (data.ResultMsg == "big") {
                            alert("您选择的图片超出限制的2M，请重新选择！");
                        }
                        else if (data.ResultMsg == "nosupport") {
                            alert("您选择的图片格式不对。");
                        }
                        else if (data.ResultMsg == "ok") {
                            $("#imgsrc").val(data.ImgSrc);
                            $("#usImg").attr("src", "/upload/user/thumb/thumb_" + data.ImgSrc);
                        }
                    },
                    error: function (xhr, txtStatus, errMsg) {
                        alert(errMsg);
                    }
                });
            }
    </script>
    <script type="text/javascript">
        var msgBox = null;
        var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        $(function () {
            msgBox = new MsgBox({ imghref: "/myadmin/images/" });
            $("#fh").bind("click", function () {
                msgBox.showMsgOk("返回！跳转中...", function () {
                    window.location = "/myadmin/site/default.aspx";
                });
            })
            $("#tj").bind("click", function () {
                if ($("#uLoginName").val() == "") {
                    msgBox.showMsgInfoSide("uNick", "登录名不能为空", 800);
                    $("#uLoginName").focus();
                    return;
                }
                else if ($("#uLoginPwd").val() == "") {
                    msgBox.showMsgInfoSide("uLoginPwd", "密码不能为空", 800);
                    $("#uLoginPwd").focus();
                    return;
                }
                else if ("" == $("#uLoginPwds").val()) {
                    msgBox.showMsgInfoSide("uLoginPwds", "再次密码不能为空", 800);
                    $("#uLoginPwds").focus();
                    return;
                }
                else if ($("#uLoginPwd").val() != $("#uLoginPwds").val()) {
                    msgBox.showMsgInfoSide("uLoginPwds", "两次密码不一致", 800);
                    $("#uLoginPwds").focus();
                    return;
                }
                else if ($("#uNick").val() == "") {
                    msgBox.showMsgInfoSide("uNick", "用户昵称不能为空", 800);
                    $("#uNick").focus();
                    return;
                }
                else if ($("#uNick").val().length < 1 || $("#uNick").val().length > 16) {
                    msgBox.showMsgInfoSide("uNick", "用户昵称不正确", 800);
                    $("#uNick").focus();
                    return;
                }
                else if ($("#uRealName").val() == "") {
                    msgBox.showMsgInfoSide("uRealName", "真实名称不能为空", 800);
                    $("#uRealName").focus();
                    return;
                }
                else if ($("#uRealName").val().length < 2 || $("#uRealName").val().length > 16) {
                    msgBox.showMsgInfoSide("uRealName", "真实名称不正确", 800);
                    $("#uRealName").focus();
                    return;
                }
                else if ($("#uEmail").val() == "") {
                    msgBox.showMsgInfoSide("uEmail", "电子邮箱不能为空。", 800);
                    $("#uEmail").focus();
                    return;
                }
                else if (!reg.test($("#uEmail").val())) {
                    msgBox.showMsgInfoSide("uEmail", "电子邮箱格式不正确。", 800);
                    $("#uEmail").focus();
                    return;
                }
                else if ($("#uPhone").val() == "") {
                    msgBox.showMsgInfoSide("uPhone", "联系手机不能为空", 800);
                    $("#uPhone").focus();
                    return;
                }
                else if (!$("#uPhone").val().match(/^1[3|4|5|8][0-9]\d{4,8}$/)) {
                    msgBox.showMsgInfoSide("uPhone", "联系手机不正确。", 800);
                    $("#uPhone").focus();
                    return;
                }
                else if ($("#uQQ").val() == "") {
                    msgBox.showMsgInfoSide("uQQ", "QQ不能为空。", 800);
                    $("#uQQ").focus();
                    return;
                }
                else if (!$("#uQQ").val().match(/^[1-9]\d{4,10}$/)) {
                    msgBox.showMsgInfoSide("uQQ", "QQ格式不正确。", 800);
                    $("#uQQ").focus();
                    return;
                }
                else if ($("#uAddress").val() == "") {
                    msgBox.showMsgInfoSide("uAddress", "联系地址不能为空", 800);
                    $("#uAddress").focus();
                    return;
                }
                else if ($("#uAddress").val().length > 255) {
                    msgBox.showMsgInfoSide("uAddress", "联系地址不能超过255个字符", 800);
                    $("#uAddress").focus();
                    return;
                }
                else {
                    $.ajax({
                        url: "/myadmin/check/reg.ashx",
                        data: $("#myForm").serialize(),
                        type: "post",
                        dataType: "html",
                        timeout: 10000,
                        beforeSend: function (xhr) {
                            msgBox.showMsgWait("验证中");
                        },
                        success: function (data) {
                            if (data == "usererr") {
                                msgBox.showMsgErr("用户名已存在！");
                            }
                            else if (data == "no") {
                                msgBox.showMsgInfo("注册失败！");
                            }           
                            else if (data == "ok") {
                                msgBox.showMsgOk("注册成功！跳转中...", function () {
                                    window.location = "/myadmin/site/login.aspx";
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
        });
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
