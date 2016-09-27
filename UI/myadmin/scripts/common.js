//限制Input输入长度
function testlength(obj, sizes, ids) {
    if (obj.value.length > sizes) {
        obj.value = obj.value.substr(0, sizes);
        alert('超出15字符限制,只为您保留15个字符');
    }
    else {
        textbox = document.getElementById(ids);
        textbox.value = textbox.value.substr(0, sizes);
    }
}

//设置框架的高度
function dyniframesize(down) {
    var pTar = null;
    if (document.getElementById) {
        pTar = document.getElementById(down);
    }
    else {
        eval('pTar = ' + down + ';');
    }
    if (pTar && !window.opera) {
        //begin resizing iframe 
        pTar.style.display = "block"
        if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
            //ns6 syntax 
            pTar.height = pTar.contentDocument.body.offsetHeight + 20;
            pTar.width = pTar.contentDocument.body.scrollWidth + 20;
        }
        else if (pTar.Document && pTar.Document.body.scrollHeight) {
            //ie5+ syntax 
            pTar.height = pTar.Document.body.scrollHeight;
            pTar.width = pTar.Document.body.scrollWidth;
        }
    }
}
/******************** 过滤 "."或者"#" *****************/
/*                                                   */
/*                                                   */
//自动添加"."或者"#"，默认添加".", n，m取值为1，2
function SubDotOrSharp(className, n) {
    var ref = ReturnResult(className, n);
    if (ref == "0") {
        return className;
    }
    else {
        if (ref.a == "1") {
            className = "." + className;
        }
        else if (ref.b == "2") {
            className = "#" + className;
        }
        return className;
    }
}
//实现过滤的内部判断
function ReturnResult(str, n) {
    var strNow = str.substring(0, 1);
    var flag;
    switch (strNow) {
        case ".":
            return "0";
            break;
        case "#":
            return "0";
            break;
        default:
            return { a: n, b: "2" }   //这里控制默认值
            break;
    }
}
/*                                                   */
/*                                                   */
/******************** 过滤 "."或者"#" *****************/