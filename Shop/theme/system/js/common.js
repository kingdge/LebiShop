var LangMenu=new Array();
function hidemenu(depth){
	var i=0;
	if(depth){
		if (depth>0){
		i=depth;
		}
	}
	while(LangMenu[i]!=null && LangMenu[i]!=''){
			if (LangMenu[i].style.display=='block'){
			    LangMenu[i].style.display='none';
		    }
			LangMenu[i]='';
			i++;
	}
}
function getsubmenu(menu){
	for(var i=0;i<menu.childNodes.length;i++){
		if(menu.childNodes[i].nodeName.toLowerCase()=="div"){
			var obj=menu.childNodes[i];
			break;
		}
	}
	return obj;
}
function showmenu(pobj,depth){
	var obj=getsubmenu(pobj);
	if (obj){
		if (LangMenu[depth] && LangMenu[depth]!=''){
		if (LangMenu[depth].style.display=='block' || LangMenu[depth].style.display==''){
			LangMenu[depth].style.display='none';
		}
		LangMenu[depth]='';
		}
		if (depth >0){
			if (obj.parentNode.offsetWidth>0){
			obj.style.left= obj.parentNode.offsetWidth+'px';
			}else{
			obj.style.left='0px';
			}
			obj.style.top='0px';
			}
			obj.style.display ='block';
			LangMenu[depth]=obj;
		}
	}
document.onclick=hidemenu;
function showhint(iconid, str)
{
	var imgUrl='../images/hint.gif';
	if (iconid != 0)
	{
		imgUrl = '../images/warning.gif';
	}
	document.write('<div style="background:url(' + imgUrl + ') no-repeat 20px 10px;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin-bottom:10px; padding:10px 10px 10px 56px; text-align: left; font-size: 12px;">');
	document.write(str + '</div><div style="clear:both;"></div>');
}

function showloadinghint(divid, str)
{
	if (divid=='')
	{
		divid='PostInfo';
	}
	document.write('<div id="' + divid + ' " style="display:none;position:relative;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin:auto;padding:10px" width="90%"  ><img border="0" src="../images/ajax_loading.gif" /> ' + str + '</div>');
}

function isie()
{
   if(navigator.userAgent.toLowerCase().indexOf('msie') != -1)
   {
       return true;
   }
   else
   {
       return false;
   }
}  

//显示提示层
function showhintinfo(obj, objleftoffset,objtopoffset, title, info , objheight, showtype ,objtopfirefoxoffset)
{
   
   var p = getposition(obj);
   
   if((showtype==null)||(showtype =="")) 
   {
       showtype =="up";
   }
   document.getElementById('hintiframe'+showtype).style.height= objheight + "px";
   document.getElementById('hintinfo'+showtype).innerHTML = info;
   document.getElementById('hintdiv'+showtype).style.display='block';
   
   if(objtopfirefoxoffset != null && objtopfirefoxoffset !=0 && !isie())
   {
        document.getElementById('hintdiv'+showtype).style.top=p['y']+parseInt(objtopfirefoxoffset)+"px";
   }
   else
   {
        if(objtopoffset == 0)
        { 
			if(showtype=="up")
			{
				 document.getElementById('hintdiv'+showtype).style.top=p['y']-document.getElementById('hintinfo'+showtype).offsetHeight-40+"px";
			}
			else
			{
				 document.getElementById('hintdiv'+showtype).style.top=p['y']+obj.offsetHeight+5+"px";
			}
        }
        else
        {
			document.getElementById('hintdiv'+showtype).style.top=p['y']+objtopoffset+"px";
        }
   }
   document.getElementById('hintdiv'+showtype).style.left=p['x']+objleftoffset+"px";
}

//隐藏提示层
function hidehintinfo()
{
    document.getElementById('hintdivup').style.display='none';
    document.getElementById('hintdivdown').style.display='none';
}

//得到字符串长度
function getLen( str) 
{
   var totallength=0;
   
   for (var i=0;i<str.length;i++)
   {
     var intCode=str.charCodeAt(i);   
     if (intCode>=0&&intCode<=128)
     {
        totallength=totallength+1; //非中文单个字符长度加 1
	 }
     else
     {
        totallength=totallength+2; //中文字符长度则加 2
     }
   } 
   return totallength;
}   

function getposition(obj)
{
	var r = new Array();
	r['x'] = obj.offsetLeft;
	r['y'] = obj.offsetTop;
	while(obj = obj.offsetParent)
	{
		r['x'] += obj.offsetLeft;
		r['y'] += obj.offsetTop;
	}
	return r;
}
document.write('<span id="hintdivup" style="display:none; position:absolute; z-index:500;">');
document.write('<div style="position:absolute; left:-65px; visibility: visible; width: 271px;z-index:501;">');
document.write('<p><img src="images/commandbg.gif" /></p>');
document.write('<div class="messagetext"><span id="hintinfoup" ></span></div>');
document.write('<p><img src="images/commandbg2.gif" /></p>');
document.write('</div>');
document.write('<iframe id="hintiframeup" style="position:absolute; left:-65px; z-index:100;width:266px;scrolling:no;opacity: 0; filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" frameborder="0"></iframe>');
document.write('</span>');
document.write('<span id="hintdivdown" style="display:none; position:absolute;z-index:500;">');
document.write('<div style="position:absolute; visibility: visible; width: 271px;z-index:501;">');
document.write('<p><img src="images/commandbg3.gif" /></p>');
document.write('<div class="messagetext"><span id="hintinfoup" ></span></div>');
document.write('<p><img src="images/commandbg4.gif" /></p>');
document.write('</div>');
document.write('<iframe id="hintiframedown" style="position:absolute; left:-65px; z-index:100;width:266px;scrolling:no;opacity: 0; filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" frameborder="0"></iframe>');
document.write('</span>');
document.write('<span id="showTips" style="display:none; position:absolute; z-index:500;">');
document.write('<div style="position:absolute; left:-65px; visibility: visible; width: 271px;z-index:501;">');
document.write('<p><img src="images/commandbg.gif" /></p>');
document.write('<div class="messagetext"><span id="hintinfoup" ></span></div>');
document.write('<p><img src="images/commandbg2.gif" /></p>');
document.write('</div>');
document.write('<iframe id="showTipsiframe" style="position:absolute; left:-65px; z-index:100;width:266px;scrolling:no;opacity: 0; filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);" frameborder="0"></iframe>');
document.write('</span>')