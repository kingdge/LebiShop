/*
颜色选取器 兼容FF 修改自深度颜色选取器
2010-08-12 by 56770.Kingdge
Copyright www.56770.com
*/
document.write('<DIV id=colorpanel style="Z-INDEX: 2; LEFT: 0px; POSITION: absolute; TOP: 0px ;display:none;"></DIV>');
document.write('<style>#colorpaneldiv{width:253px !important;width:253px;position:absolute;font:line-height:18px;z-index:100;}.Tips_colorpaneldiv{POSITION: absolute}</style>');
var ocolorpanel = document.getElementById("colorpanel");
var colorxs = {};
colorxs.pos = (function () {
    var t = {};
    t.getX = function (obj) {
        var curleft = 0;
        if (obj.offsetParent) {
            while (obj.offsetParent) {
                curleft += obj.offsetLeft;
                obj = obj.offsetParent;
            }
            curleft += obj.offsetLeft;
        }
        else if (obj.x) {
            curleft += obj.x;
        }
        return curleft;
    };
    t.getY = function (obj) {
        var curtop = 0;
        if (obj.offsetParent) {
            while (obj.offsetParent) {
                curtop += obj.offsetTop;
                obj = obj.offsetParent;
            }
            curtop += obj.offsetTop;
        }
        else if (obj.y)
            curtop += obj.y;
        return curtop;
    };
    return t;
})();
function color_tooltip(c) {
    var _tooltips = document.getElementsByTagName('div');
    for (var i = 0; i < _tooltips.length; i++) {
        if (_tooltips[i].getAttribute('tooltip') == 'color') _tooltips[i].parentNode.removeChild(_tooltips[i]);
    }
    var _tooltip = document.createElement('div');
    _tooltip.innerHTML = '<div id="colorpaneldiv">' + c + '</div>';
    _tooltip.className = 'Tips_colorpaneldiv';
    _tooltip.setAttribute('tooltip', 'color');
    _tooltip.style.width = '200px';
    _tooltip.style.zIndex = '999';
    _tooltip.style.display = 'none';
    return document.body.appendChild(_tooltip);
}

//当前颜色块函数
function currentColor(colorStr) {
    document.getElementById("colorDis").style.backgroundColor = colorStr;
    document.getElementById("colorHexDis").value = colorStr.toUpperCase();    //toUpperCase()方法将颜色值大写
}

//点击选择颜色函数
function clickColor(colorStr, ssss) {
    var oColorPicker = document.getElementById(ssss);
    var iColorPicker = document.getElementById("s_" + ssss);
    iColorPicker.style.backgroundColor = colorStr;
    oColorPicker.value = colorStr;
    ocolorpanel.style.display = "none";
    document.getElementById("colorpaneldiv").style.display = "none";
}

//关闭颜色选择器函数
function doClose() {
    ocolorpanel.style.display = "none";
    document.getElementById("colorpaneldiv").style.display = "none";
}
function colorcd(dddd, ssss, ffff) {
    var offsetT = 5;
    var offsetL = -120;
    var baseColorHex = new Array('00', '33', '66', '99', 'CC', 'FF');  //256色的颜色是用00,33,66,99,cc,ff组成
    var SpColorHex = new Array('000000', '333333', '666666', '999999', 'cccccc', 'FFFFFF', 'FF0000', '00FF00', '0000FF', 'FFFF00', '00FFFF', 'FF00FF');
    var colorRGB = "";
    var sColorPopup;
    sColorPopup = '<table width=253 border="0" cellspacing="0" cellpadding="0" style="border:1px #000000 solid;border-bottom:none;border-collapse: collapse" bordercolor="000000" title="乐彼多语言网店系统 (深度)颜色选择器">'
			   + '<tr height=30><td colspan=21 bgcolor=#cccccc>'
			   + '<table width="100%" cellpadding="0" cellspacing="1" border="0" style="border-collapse: collapse">'
			   + '<tr><td onclick="doClose()" title="点击鼠标关闭"><input type="text" name="colorDis" id="colorDis" disabled style="border:solid 1px #000000;background-color:#ffff00;width:30px;"> <input type="text" id="colorHexDis" size="5" style="border:inset 1px;font-family:Arial;width:60px;" value="#000000"></td><td align="right" onclick="doClose()" title="点击鼠标关闭"><a href="http://www.56770.com" target="_blank"><span style="font-size:10px">56770 EShop ColorCop</span></a></td></tr></table></td></tr></table>'
			   + '<table width=253 border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#000000"style="cursor:hand;">'
    for (n = 0; n < 2; n++) {	//循环2块
        for (i = 0; i < 6; i++) {  //每块6行
            sColorPopup += "<tr    height=12>";
            for (j = 0 + 3 * n; j < 3 + 3 * n; j++) {
                for (k = 0; k < 6; k++) {
                    colorRGB = baseColorHex[j] + baseColorHex[k] + baseColorHex[i];
                    sColorPopup += "<td  width='12' onmouseover='currentColor(this.bgColor)' onclick=\"clickColor(this.bgColor,'" + ssss + "')\"  bgColor='#" + colorRGB + "' style='cursor:pointer;'  title='点击选择颜色" + colorRGB + "'></td>";
                }
            }
            sColorPopup += "</tr>";
        }
    }

    sColorPopup += "</table>";
    var tooltip = color_tooltip(sColorPopup);
    var o = document.getElementById("s_" + ssss + "")
    tooltip.style.top = offsetT ? (colorxs.pos.getY(o) + offsetT) + 'px' : (colorxs.pos.getY(o) - 80) + 'px';
    tooltip.style.left = offsetL ? (colorxs.pos.getX(o) + offsetL) + 'px' : (colorxs.pos.getX(o) - 80) + 'px';
    tooltip.style.visibility = 'visible'
    tooltip.style.display = 'block';
    tooltip.id = 'color_tooltip';
    return false
}