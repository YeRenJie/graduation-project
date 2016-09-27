<%@ Page Title="" Language="C#" MasterPageFile="~/myadmin/site/layout.Master" AutoEventWireup="true" CodeBehind="userinfo.aspx.cs" Inherits="UI.myadmin.site.userinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/myadmin/scripts/jquery.form.js"></script>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpNav" runat="server">
    <li><a href="/myadmin/site/default.aspx"><i class="ico-home"></i>首页</a></li>
    <li class="ind"><a href="/myadmin/user/userInfo.aspx"><i class=""></i>用户资料</a></li>
    <li><a href="/myadmin/notes/note.aspx"><i class=""></i>收支管理</a></li>
    <li><a href="/myadmin/notes/cost.aspx"><i class=""></i>消费统计</a></li>
    <li><a href="/myadmin/feedback/feedback.aspx"><i class=""></i>在线留言</a></li>
    <li><a href="/myadmin/webInfo/about.aspx"><i class=""></i>关于我们</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpPosition" runat="server">
    首页&gt;用户资料
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMain" runat="server">
    <div class="userInfo">
        <div class="uDiv">
            <span>用户头像：</span><img src="<%="/upload/user/thumb/thumb_"+model.UPic %>" alt="修改头像" title="修改头像" width="100" style="border: 1px solid #ccc; padding: 3px; background: #e5e5e5; cursor: pointer" id="usImg" />
            <input type="file" onchange="validate()" name="upImg" class="upImg" id="upImg" />
            <input type="hidden" value="<%=model.UPic %>" name="imgsrc" id="imgsrc" />
            <label>* 点击更换头像。运行上传最大大小为2M，支持png,jpg,jpeg,gif格式</label>
        </div>
        <div class="uDiv"><span>用户昵称：</span><input type="text" name="uNick" id="uNick" class="ur" value="<%=model.UNick %>" /><label>* 请填写您的昵称，1到16个字之间。必填</label></div>
        <div class="uDiv"><span>真实姓名：</span><input type="text" name="uRealName" id="uRealName" class="ur" value="<%=model.URealName %>" /><label>* 请填写您的真实姓名，2到16个字之间。必填</label></div>
        <div class="uDiv">
            <span>性别：</span>
            <select name="uSex">
                <%=select %>
            </select><label> * 请选择您的性别。</label>
        </div>
         <div class="uDiv"><span>原密码：</span><input type="text" name="yuLoginPwd" id="yuLoginPwd" class="ur" value="<%=model.ULoginPwd %>"  readonly="false"/></div>
         <div class="uDiv"><span>密  码：</span><input type="text" name="uLoginPwd" id="uLoginPwd" class="ur" value=""  /><label>* 请填写您的密码。必填</label></div>
         <div class="uDiv"><span>确认密码：</span><input type="text" name="uLoginPwds" id="uLoginPwds" class="ur" value="" /><label>* 请再次填写您的密码。必填</label></div>
        <div class="uDiv"><span>电子邮箱：</span><input type="text" name="uEmail" id="uEmail" class="ur" value="<%=model.UEmail %>" /><label>* 请填写您的电子邮箱。必填</label></div>
        <div class="uDiv"><span>联系手机：</span><input type="text" name="uPhone" id="uPhone" class="ur" value="<%=model.UPhone %>" /><label>* 请填写您的联系手机。可选</label></div>
        <div class="uDiv"><span>QQ：</span><input type="text" name="uQQ" id="uQQ" class="ur" value="<%=model.UQQ %>" /><label>* 请填写您的QQ账号。必填</label></div>
        <div class="uDiv"><span>联系地址：</span><input type="text" name="uAddress" id="uAddress" class="ur" value="<%=model.UAddress %>" /><label>* 请填写您的联系地址。可选，少于255个字</label></div>
        <div class="iDiv"><a title="提交" id="tj">更新资料</a></div>
    </div>
    <script type="text/javascript">
        var msgBox = null;
        var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
        $(function () {
            msgBox = new MsgBox({ imghref: "/myadmin/images/" });
            $("#tj").bind("click", function () {
                if ($("#uNick").val() == "") {
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
                }else if ($("#uLoginPwd").val() == "") {
                    msgBox.showMsgInfoSide("uLoginPwd", "密码不能为空", 800);
                    $("#uLoginPwd").focus();
                    return;
                }else if ("" == $("#uLoginPwds").val()) {
                    msgBox.showMsgInfoSide("uLoginPwds", "再次密码不能为空", 800);
                    $("#uLoginPwds").focus();
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
                    
                    $("#myForm").ajaxSubmit({
                        url: "/myadmin/user/userinfo.aspx",
                        type: "post",
                        beforeSubmit: function () {
                            msgBox.showMsgWait("更新中,请稍等");
                        },
                        dataType: "html",
                        data: $("#myForm").serialize(),
                        success: function (data) {
                            if (data == "busy") {
                                msgBox.showMsgErr("系统繁忙,请稍后再试");
                            }
                            else if (data == "ok") {
                                msgBox.showMsgOk("更新成功。", function () {
                                    window.location.reload();
                                });
                            }
                        },
                        error: function (xhr, txtStatus, errMsg) {
                            alert(errMsg);
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>
