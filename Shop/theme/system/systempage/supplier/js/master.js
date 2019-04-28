function addtrbg(obj) {
    $("tr.list:odd", obj).css("background-color", "#F8F8F8").attr("odd", "1");
    $("tr.list", obj).hover(function () {
        $(this).css("background", "#e4e8ec");
    }, function () {
        if ($(this).attr('odd') == "1")
            $(this).css("background-color", "#F8F8F8");
        else
            $(this).css("background-color", "#FFFFFF");
    });
}
$(function () {
    $(window).resize(function () {
        var _body_main_w = $(window).width() - $("#sidebar").width() - 17;
        var _body_main_form_w = $(window).width() - $("#sidebar").width() - 35;
        if (_body_main_w > 1200)
            $("#body_main_form").attr("style", "min-width:" + _body_main_form_w + "px;width:" + _body_main_form_w + "px");
        var _body_content_h = $(window).height() - $("#body_head").height() - $("#body_foot").height() - 7;
        var _body_main_h = $(window).height() - $("#body_head").height() - $("#body_top").height() - $("#body_foot").height() - $("#body_path").height() - $("#body_bottom").height() - $(".mainbody_top").height() - 10;
        $("#body_content").height(_body_content_h);
        $("#sidebar").height(_body_content_h);
        $("#sidecontent").width(_body_main_w);
        $("#body_main").height(_body_main_h);
        $("#body_main").width(_body_main_w);
        $("#body_bottom").width(_body_main_w);
        //                $("#body_main_form").height(_body_content_h);
    }).resize();
    //添加自动背景色
    addtrbg(".table");
    addtrbg(".datalist");
    //添加自动背景色结束
    //定位菜单
    var leftMeunLI = $(".menubar").find("ul li");
    meunsIndex = 0;
    var isMatch = false;
    $(".menubar").find("a").each(function (i) {
        var href = $(this).attr("href").toLowerCase() + "&";
        if (requestPage + '&' == href || (requestPage + '&').indexOf(href) != -1) {
            isMatch = true;
            meunsIndex = i;
            return false;
        }
    });
    $(leftMeunLI).removeClass("current");
    if (isMatch) {
        if (meunsIndex == 0) {
            $(leftMeunLI[meunsIndex]).addClass("current first");
        } else {
            $(leftMeunLI[meunsIndex]).addClass("current");
        }
    }
    //定位菜单结束
    //收缩展开菜单
    var leftMeunUL = $(".menubar ul");
    var menucookie = GetCookie("menus");
    if (menucookie == null)
        menucookie = '';
    $(".menubar h2 img").each(function (i) {
        $(this).click(function () {
            var mid = $(this).attr('mid');
            if ($(leftMeunUL[i]).is(":hidden")) {
                $(leftMeunUL[i]).show();
                menucookie = menucookie.replace('|' + mid + '|', '');
                SetCookie("menus", menucookie, 1);
                $(this).attr('src', ''+ AdminImagePath +'/minus.gif');
            }
            else {
                $(leftMeunUL[i]).hide();
                menucookie = menucookie + "|" + mid + "|";
                SetCookie("menus", menucookie, 1);
                $(this).attr('src', ''+ AdminImagePath +'/plus.gif');
            }
        });
    });
    //收缩展开菜单结束
    var $dropmenu = $(".dropmenu"), $dropmenu_li = $dropmenu.find('li.menu_li'), $dropmenu_li_content = $dropmenu_li.find('.menu_li_content'), $droplang = $(".droplang"), $droplang_li = $droplang.find('li.lang_li'), $droplang_li_content = $droplang_li.find('.lang_li_content');
    $dropmenu_li.hover(function () {
        $dropmenu_li_content.stop(true, true).fadeIn(0);
    }, function () {
        $dropmenu_li_content.fadeOut(500, function () {
            $dropmenu_li_content.css("display", "none");
        });
    });
    $droplang_li.hover(function () {
        $droplang_li_content.stop(true, true).fadeIn(0);
    }, function () {
        $droplang_li_content.fadeOut(500, function () {
            $droplang_li_content.css("display", "none");
        });
    });
    $("a.noclick").click(function (event) {
        event.preventDefault();
    });
});
function SetLanguage(c, n) {
    SetCookie("AdminLanguage", c, 1);
    SetCookie("AdminLanguageName", n, 1);
    window.location.reload();
}