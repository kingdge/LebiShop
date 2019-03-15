// JavaScript Document
<!--

//栏目分类的引用脚本
function startlist(overout_obj,e_type) {
	var the_node,theli_nodeobj,div_nodes;
	theli_nodeobj=overout_obj;
	if(document.getElementById(theli_nodeobj))
	{
		overout_obj=document.getElementById(theli_nodeobj);
	}
	if (overout_obj.nodeName.toLowerCase()=="li") {
		if (overout_obj.childNodes.length>2) {
			if(document.all) {
				the_node=overout_obj.childNodes.item(2);
			}
			else {
				the_node=overout_obj.childNodes.item(3);
			}
			if (e_type==1) {
				the_node.style.display="block";
			}
			if (e_type==0) {
				the_node.style.display="none";
			}
		}
	}
}

//改变对象className的值
function chg_classstyle(overout_obj,class_name,e_type) {
	var the_eventobj=eval(overout_obj);
	if (class_name) {
		if (e_type==1) {
			the_eventobj.className +=" " + class_name;
		}
		else {
			the_eventobj.className=the_eventobj.className.replace(" " + class_name,"");
		}
	}
}

//支持多浏览器的菜单脚本
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

//只改变本身的相关属性
function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }	
    obj.visibility=v; }
}

//改变本身的相关属性及其它对象的属性
function MM_showHideLayers_2() { //v6.0
  var i,p,v,obj,f_objstr,t_v;
  var args=MM_showHideLayers_2.arguments;
  for (i=0; i<(args.length-2); i++)
  {
	if ((obj=MM_findObj(args[i]))!=null) {
		v=args[i+2];
		f_objstr=args[i+1];
		t_v=args[i+3];

		if(t_v>1)
		{
			for(var j=1; j<=t_v; j++)
			{
				var other_obj=MM_findObj(f_objstr + j);
				if(other_obj)
				{
					if (other_obj.style) { other_obj.style.visibility='hidden'; }
				}
			}
		}
		if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }	
		obj.visibility=v;
	}
  }
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

//-->