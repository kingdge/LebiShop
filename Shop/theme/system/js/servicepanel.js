var startX = document.body.clientWidth-170;
var winWidth = 0;
function iecompattest(){
   return (document.compatMode && document.compatMode!='BackCompat')? document.documentElement : document.body
}
function staticbar(){
	if (window.innerWidth)
	winWidth = window.innerWidth;
	else if ((document.body) && (document.body.clientWidth))
	winWidth = document.body.clientWidth;
	if (document.documentElement && document.documentElement.clientWidth){winWidth = document.documentElement.clientWidth;}
	startX = winWidth-170;
	barheight=document.getElementById('divStayTopLeft').offsetHeight
	var ns = (navigator.appName.indexOf('Netscape') != -1) || window.opera;
	var d = document;
	function ml(id){
		var el=d.getElementById(id);
		el.style.visibility='visible'
		if(d.layers)el.style=el;
		el.sP=function(x,y){this.style.left=x+'px';this.style.top=y+'px';};
		el.x = startX;
		if (verticalpos=='fromtop')
		el.y = startY;
		else{
		el.y = ns ? pageYOffset + innerHeight : iecompattest().scrollTop + iecompattest().clientHeight;
		el.y -= startY;
		}
		return el;
	}
	window.stayTopLeft=function(){
		if (verticalpos=='fromtop'){
		var pY = ns ? pageYOffset : iecompattest().scrollTop;
		ftlObj.y += (pY + startY - ftlObj.y)/8;
		}
		else{
		var pY = ns ? pageYOffset + innerHeight - barheight: iecompattest().scrollTop + iecompattest().clientHeight - barheight;
		ftlObj.y += (pY - startY - ftlObj.y)/8;
		}
		ftlObj.sP(ftlObj.x, ftlObj.y);
		setTimeout('stayTopLeft()', 10);
	}
	ftlObj = ml('divStayTopLeft');
	stayTopLeft();
}
if(typeof(HTMLElement)!='undefined'){
  HTMLElement.prototype.contains=function (obj)
  {
	  while(obj!=null&&typeof(obj.tagName)!='undefind'){
　 　 if(obj==this) return true;
　　	　obj=obj.parentNode;
　	  }
	  return false;
  }
}
if (window.addEventListener){
window.addEventListener('load', staticbar, false)
}else if (window.attachEvent){
window.attachEvent('onload', staticbar)
}else if (document.getElementById){
window.onload=staticbar;}
window.onresize=staticbar;
function servicepannelOver(){
   document.getElementById('divMenu').style.display = 'none';
   document.getElementById('divOnline').style.display = 'block';
   document.getElementById('divStayTopLeft').style.width = '170px';
}
function servicepannelOut(){
   document.getElementById('divMenu').style.display = 'block';
   document.getElementById('divOnline').style.display = 'none';
}
if(typeof(HTMLElement)!='undefined') 
{
   HTMLElement.prototype.contains=function(obj)
   {
       while(obj!=null&&typeof(obj.tagName)!='undefind'){
   　　　 if(obj==this) return true;
   　　　 obj=obj.parentNode;
   　　}
          return false;
   };
}
function Showservicepannel(theEvent){
　 if (theEvent){
　    var browser=navigator.userAgent;
　	if (browser.indexOf('Firefox')>0){
　　     if (document.getElementById('divOnline').contains(theEvent.relatedTarget)) {
　　         return;
         }
      }
	    if (browser.indexOf('MSIE')>0 || browser.indexOf('Safari')>0 || browser.indexOf('.NET') > 0){ //IE Safari
          if (document.getElementById('divOnline').contains(event.toElement)) {
	          return;
          }
      }
   }
   document.getElementById('divMenu').style.display = 'block';
   document.getElementById('divOnline').style.display = 'none';
}