$(document).ready(
function()
{
 $("#shopcategory dl").click(
 function()
 {
		var tid = $("#shopcategory dl").index(this);
		$("#shopcategory dl").removeClass("current");
		$("#shopcategory dl:eq("+tid+")").addClass("current");
 }
 );
});