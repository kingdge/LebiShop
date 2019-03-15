(function ($) {
    $.fn.hoverDelay = function (options) {
        var defaults = {
            hoverDuring: 800,
            outDuring: 800,
            hoverEvent: function () {
                $.noop();
            },
            outEvent: function () {
                $.noop();
            }
        };
        var sets = $.extend(defaults, options || {});
        var hoverTimer, outTimer;
        return $(this).each(function () {
            $(this).hover(function () {
                clearTimeout(outTimer);
                hoverTimer = setTimeout(sets.hoverEvent, sets.hoverDuring);
            }, function () {
                clearTimeout(hoverTimer);
                outTimer = setTimeout(sets.outEvent, sets.outDuring);
            });
        });
    }
})(jQuery);
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
    $("#service .layout").hover(function(){
        $(this).find(".adminfacepop").toggle(200);
    })
    if (GetCookie("sidebar") == "hide") {
        $("#sidebar").hide();
        window.status = 0;
        $("#switchPoint").html('<img src="' + AdminImagePath + '/vertical/right.png">');
        $("#sidebar").hide();
        $("#sideplus a").attr("class", "cur");
    };
    $(".calendar,.input-calendar").datepicker({dateFormat:"yy-mm-dd"});
    $(window).resize(function () {
        var sitebar_w = $("#sidebar").width();
        if (GetCookie("sidebar") == "hide") { sitebar_w = 0; }
        var _body_main_w = $(window).width() - sitebar_w - 17;
        var _body_main_form_w = $(window).width() - sitebar_w - 35;
        if (_body_main_w > 1200) { var _body_content_h = $(window).height() - $("#body_head").height() - $("#body_foot").height(); }
        var height_body_top = $("#body_top").height();
        if (height_body_top == undefined) { height_body_top = 0; }
        var height_body_path = $("#body_path").height();
        if (height_body_path == undefined) { height_body_path = 0; }
        if (height_body_path == 0) { height_body_path = -30; }
        var height_mainbody_top = $(".mainbody_top").height();
        if (height_mainbody_top == undefined) { height_mainbody_top = 0; }
        var height_body_bottom = $("#body_bottom").height();
        if (height_body_bottom == undefined) { height_body_bottom = 0; }
        var _body_main_h = $(window).height() - $("#body_head").height() - $("#body_foot").height() - height_body_bottom - height_body_top - height_body_path - height_mainbody_top - 62;
        if (height_mainbody_top > 0) { _body_main_h -= 10; }
        $("#body_content,.navmenu").attr("style", "height:" + ($(window).height() - $("#body_head").height()) + "px");
        $("#body_main").attr("style", "height:" + _body_main_h + "px");
    }).resize();
    var ul = $(".navmenu #shopmenu");
    var li = $(".navmenu #shopmenu li");
    $(li).each(function (i) {
        var _this = this;
        $(_this).hoverDelay({
            hoverEvent: function () {
                ul.find("li").removeClass("current");
                $(_this).addClass("current");
                var menua = ul.find("li").find("a")[i];
                var reg = new RegExp("(^|&)pid=([^&]*)(&|$)");
                var r = menua.search.substr(1).match(reg);
                var menuid = unescape(r[2]);
                //var menuid = $(this).attr("menucode");
                $.ajax({
                    type: 'POST',
                    url: AdminPath + "/ajax/ajax_admin.aspx?__Action=GetMenu&id=" + menuid,
                    data: '',
                    success: function (res) {
                        $("#sidebar .menubar").html(res);
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
                                    $(this).attr('src', '' + AdminImagePath + '/2015/minus.png');
                                }
                                else {
                                    $(leftMeunUL[i]).hide();
                                    menucookie = menucookie + "|" + mid + "|";
                                    SetCookie("menus", menucookie, 1);
                                    $(this).attr('src', '' + AdminImagePath + '/2015/plus.png');
                                }
                            });
                        });
                    }
                });
            }});
    });
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
                $(this).attr('src', '' + AdminImagePath + '/2015/minus.png');
            }
            else {
                $(leftMeunUL[i]).hide();
                menucookie = menucookie + "|" + mid + "|";
                SetCookie("menus", menucookie, 1);
                $(this).attr('src', '' + AdminImagePath + '/2015/plus.png');
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