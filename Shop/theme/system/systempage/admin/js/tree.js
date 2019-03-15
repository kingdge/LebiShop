var showTypeids = GetCookie("showTypeids");
function ShowTree(ids, id) {
    if (showTypeids == '' || showTypeids == undefined)
        showTypeids = ',';
    //if (showTypeids.indexOf(',' + id + ',') < 0) {
    //    showTypeids = showTypeids + id + ',';
    //    SetCookie("showTypeids", showTypeids, 1);
    //}
    var src = $("#img" + id + "").attr("src");
    if (src.indexOf("plus.gif") == -1) {
        //打开时-执行关闭动作
        $("#img" + id + "").attr("src", AdminImagePath + "/plus.gif");
        var arr = ids.split(',');
        for (var i in arr) {
            //DoHide(arr[i]);
            $('#tr' + arr[i]).hide();
        }
        showTypeids = showTypeids.replace(',' + id + ',', ',');
        SetCookie("showTypeids", showTypeids, 1);
    }
    else {
        showTypeids = showTypeids + id + ',';
        SetCookie("showTypeids", showTypeids, 1);
        $("#img" + id + "").attr("src", AdminImagePath + "/minus.gif");
        $("tr[name='tr" + id + "']").each(function () {
            $(this).show();
        })
    }
}
//引用页面需重新function searchproduct(id)


var showUserLevelids = GetCookie("showUserLevelids");
function ShowTreeUser(id) {
    if (showUserLevelids == '' || showUserLevelids == undefined)
        showUserLevelids = ',';
    var src = $("#imgu" + id + "").attr("src");
    if (src.indexOf("plus.gif") == -1) {
        //打开时-执行关闭动作
        $("#imgu" + id + "").attr("src", AdminImagePath + "/plus.gif");
        $("tr[name='truser" + id + "']").each(function () {
            $(this).hide();
        })
        showUserLevelids = showUserLevelids.replace(',' + id + ',', ',');
        SetCookie("showUserLevelids", showUserLevelids, 1);
    }
    else {
        showUserLevelids = showUserLevelids + id + ',';
        SetCookie("showUserLevelids", showUserLevelids, 1);
        $("#imgu" + id + "").attr("src", AdminImagePath + "/minus.gif");
        $("tr[name='truser" + id + "']").each(function () {
            $(this).show();
        })
    }
}