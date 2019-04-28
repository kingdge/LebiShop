// JavaScript Document
var SaPload = {
	
	last:'_obj',
	jsReday:function (){
		return true;
	},
	Trace:function (t){
		if (window.console && window.console.log)  
			window.console.log('SaPload : Flash Trace => '+ t);   
	},
	JsonData:function (t){
		if (window.console && window.console.log)  
			window.console.log('SaPload : Flash json data => '+ t);   
	},
	PicData:function (t){
		if (window.console && window.console.log)  
			window.console.log(t);   
	},
	JsonDataEdit:function (prop, val) {
		// 如果 val 被忽略
		if(typeof val === "undefined") {
			// 删除属性
			delete str1[prop];
		}
		else {
			// 添加 或 修改
			str1[prop] = val;
		}
	},
	setArgs:function (o,t){
		var swf;
		var ishtml=false;
		movieName=o+'_obj';
		if (window.document[movieName]) {  
			swf = window.document[movieName];
		}
		if (navigator.appName.indexOf("Microsoft Internet") == -1) {
			if (document.embeds && document.embeds[movieName])  
			swf = document.embeds[movieName];
		}else {  
			swf = document.getElementById(movieName);
		} 
		if(swf == undefined){
			ishtml=true;
			swf =document.getElementById(movieName);
		};
		if(ishtml){
			var old_str = swf.opts.sa_args;
			var old_obj = eval('(' + old_str + ')');
			var new_obj = eval('(' + t + ')');
			for(var item in new_obj){  
				if(typeof new_obj[item]  === 'string'){   
				  old_obj[item]=new_obj[item];
				  SaPload.Trace(' 临时添加参数：' + item + '=' + new_obj[item]);
				  SaPload.JsonData('{"msg":"临时添加参数：' + item + '=' + new_obj[item]+'","sid":"' + swf.opts.sa_id +'","action":"addArgs"}');
				}
			}  
			swf.opts.sa_args = JSON.stringify(old_obj);
		}else{
			swf.setArgs(t);
		}
		
	},
	createNew: function(option){
		var sapload = {};
		//工具区：对象扩展
		sapload.extend = function(target,source){
			for (var p in source) {
				if (source.hasOwnProperty(p)) {
					target[p] = source[p];
				}
			}
			return target;
		};
		//工具区：获得swf对象通过id
		sapload.fobj = function(o) { 
			movieName=o;
			if (window.document[movieName]) {  
				return window.document[movieName];
			}
			if (navigator.appName.indexOf("Microsoft Internet") == -1) {
				if (document.embeds && document.embeds[movieName])  
				return document.embeds[movieName];
			}else {  
				return document.getElementById(movieName);
			}        
		}
		//工具区：获得dom对象通过id
		sapload.get = function (myHeader){
			var x=document.getElementById(myHeader)
			return x;	
		}
		//工具区：内置swfobject函数
		sapload.swf = function(){var D="undefined",r="object",S="Shockwave Flash",W="ShockwaveFlash.ShockwaveFlash",q="application/x-shockwave-flash",R="SWFObjectExprInst",x="onreadystatechange",O=window,j=document,t=navigator,T=false,U=[h],o=[],N=[],I=[],l,Q,E,B,J=false,a=false,n,G,m=true,M=function(){var aa=typeof j.getElementById!=D&&typeof j.getElementsByTagName!=D&&typeof j.createElement!=D,ah=t.userAgent.toLowerCase(),Y=t.platform.toLowerCase(),ae=Y?/win/.test(Y):/win/.test(ah),ac=Y?/mac/.test(Y):/mac/.test(ah),af=/webkit/.test(ah)?parseFloat(ah.replace(/^.*webkit\/(\d+(\.\d+)?).*$/,"$1")):false,X=!+"\v1",ag=[0,0,0],ab=null;if(typeof t.plugins!=D&&typeof t.plugins[S]==r){ab=t.plugins[S].description;if(ab&&!(typeof t.mimeTypes!=D&&t.mimeTypes[q]&&!t.mimeTypes[q].enabledPlugin)){T=true;X=false;ab=ab.replace(/^.*\s+(\S+\s+\S+$)/,"$1");ag[0]=parseInt(ab.replace(/^(.*)\..*$/,"$1"),10);ag[1]=parseInt(ab.replace(/^.*\.(.*)\s.*$/,"$1"),10);ag[2]=/[a-zA-Z]/.test(ab)?parseInt(ab.replace(/^.*[a-zA-Z]+(.*)$/,"$1"),10):0}}else{if(typeof O.ActiveXObject!=D){try{var ad=new ActiveXObject(W);if(ad){ab=ad.GetVariable("$version");if(ab){X=true;ab=ab.split(" ")[1].split(",");ag=[parseInt(ab[0],10),parseInt(ab[1],10),parseInt(ab[2],10)]}}}catch(Z){}}}return{w3:aa,pv:ag,wk:af,ie:X,win:ae,mac:ac}}(),k=function(){if(!M.w3){return}if((typeof j.readyState!=D&&j.readyState=="complete")||(typeof j.readyState==D&&(j.getElementsByTagName("body")[0]||j.body))){f()}if(!J){if(typeof j.addEventListener!=D){j.addEventListener("DOMContentLoaded",f,false)}if(M.ie&&M.win){j.attachEvent(x,function(){if(j.readyState=="complete"){j.detachEvent(x,arguments.callee);f()}});if(O==top){(function(){if(J){return}try{j.documentElement.doScroll("left")}catch(X){setTimeout(arguments.callee,0);return}f()})()}}if(M.wk){(function(){if(J){return}if(!/loaded|complete/.test(j.readyState)){setTimeout(arguments.callee,0);return}f()})()}s(f)}}();function f(){if(J){return}try{var Z=j.getElementsByTagName("body")[0].appendChild(C("span"));Z.parentNode.removeChild(Z)}catch(aa){return}J=true;var X=U.length;for(var Y=0;Y<X;Y++){U[Y]()}}function K(X){if(J){X()}else{U[U.length]=X}}function s(Y){if(typeof O.addEventListener!=D){O.addEventListener("load",Y,false)}else{if(typeof j.addEventListener!=D){j.addEventListener("load",Y,false)}else{if(typeof O.attachEvent!=D){i(O,"onload",Y)}else{if(typeof O.onload=="function"){var X=O.onload;O.onload=function(){X();Y()}}else{O.onload=Y}}}}}function h(){if(T){V()}else{H()}}function V(){var X=j.getElementsByTagName("body")[0];var aa=C(r);aa.setAttribute("type",q);var Z=X.appendChild(aa);if(Z){var Y=0;(function(){if(typeof Z.GetVariable!=D){var ab=Z.GetVariable("$version");if(ab){ab=ab.split(" ")[1].split(",");M.pv=[parseInt(ab[0],10),parseInt(ab[1],10),parseInt(ab[2],10)]}}else{if(Y<10){Y++;setTimeout(arguments.callee,10);return}}X.removeChild(aa);Z=null;H()})()}else{H()}}function H(){var ag=o.length;if(ag>0){for(var af=0;af<ag;af++){var Y=o[af].id;var ab=o[af].callbackFn;var aa={success:false,id:Y};if(M.pv[0]>0){var ae=c(Y);if(ae){if(F(o[af].swfVersion)&&!(M.wk&&M.wk<312)){w(Y,true);if(ab){aa.success=true;aa.ref=z(Y);ab(aa)}}else{if(o[af].expressInstall&&A()){var ai={};ai.data=o[af].expressInstall;ai.width=ae.getAttribute("width")||"0";ai.height=ae.getAttribute("height")||"0";if(ae.getAttribute("class")){ai.styleclass=ae.getAttribute("class")}if(ae.getAttribute("align")){ai.align=ae.getAttribute("align")}var ah={};var X=ae.getElementsByTagName("param");var ac=X.length;for(var ad=0;ad<ac;ad++){if(X[ad].getAttribute("name").toLowerCase()!="movie"){ah[X[ad].getAttribute("name")]=X[ad].getAttribute("value")}}P(ai,ah,Y,ab)}else{p(ae);if(ab){ab(aa)}}}}}else{w(Y,true);if(ab){var Z=z(Y);if(Z&&typeof Z.SetVariable!=D){aa.success=true;aa.ref=Z}ab(aa)}}}}}function z(aa){var X=null;var Y=c(aa);if(Y&&Y.nodeName=="OBJECT"){if(typeof Y.SetVariable!=D){X=Y}else{var Z=Y.getElementsByTagName(r)[0];if(Z){X=Z}}}return X}function A(){return !a&&F("6.0.65")&&(M.win||M.mac)&&!(M.wk&&M.wk<312)}function P(aa,ab,X,Z){a=true;E=Z||null;B={success:false,id:X};var ae=c(X);if(ae){if(ae.nodeName=="OBJECT"){l=g(ae);Q=null}else{l=ae;Q=X}aa.id=R;if(typeof aa.width==D||(!/%$/.test(aa.width)&&parseInt(aa.width,10)<310)){aa.width="310"}if(typeof aa.height==D||(!/%$/.test(aa.height)&&parseInt(aa.height,10)<137)){aa.height="137"}j.title=j.title.slice(0,47)+" - Flash Player Installation";var ad=M.ie&&M.win?"ActiveX":"PlugIn",ac="MMredirectURL="+O.location.toString().replace(/&/g,"%26")+"&MMplayerType="+ad+"&MMdoctitle="+j.title;if(typeof ab.flashvars!=D){ab.flashvars+="&"+ac}else{ab.flashvars=ac}if(M.ie&&M.win&&ae.readyState!=4){var Y=C("div");X+="SWFObjectNew";Y.setAttribute("id",X);ae.parentNode.insertBefore(Y,ae);ae.style.display="none";(function(){if(ae.readyState==4){ae.parentNode.removeChild(ae)}else{setTimeout(arguments.callee,10)}})()}u(aa,ab,X)}}function p(Y){if(M.ie&&M.win&&Y.readyState!=4){var X=C("div");Y.parentNode.insertBefore(X,Y);X.parentNode.replaceChild(g(Y),X);Y.style.display="none";(function(){if(Y.readyState==4){Y.parentNode.removeChild(Y)}else{setTimeout(arguments.callee,10)}})()}else{Y.parentNode.replaceChild(g(Y),Y)}}function g(ab){var aa=C("div");if(M.win&&M.ie){aa.innerHTML=ab.innerHTML}else{var Y=ab.getElementsByTagName(r)[0];if(Y){var ad=Y.childNodes;if(ad){var X=ad.length;for(var Z=0;Z<X;Z++){if(!(ad[Z].nodeType==1&&ad[Z].nodeName=="PARAM")&&!(ad[Z].nodeType==8)){aa.appendChild(ad[Z].cloneNode(true))}}}}}return aa}function u(ai,ag,Y){var X,aa=c(Y);if(M.wk&&M.wk<312){return X}if(aa){if(typeof ai.id==D){ai.id=Y}if(M.ie&&M.win){var ah="";for(var ae in ai){if(ai[ae]!=Object.prototype[ae]){if(ae.toLowerCase()=="data"){ag.movie=ai[ae]}else{if(ae.toLowerCase()=="styleclass"){ah+=' class="'+ai[ae]+'"'}else{if(ae.toLowerCase()!="classid"){ah+=" "+ae+'="'+ai[ae]+'"'}}}}}var af="";for(var ad in ag){if(ag[ad]!=Object.prototype[ad]){af+='<param name="'+ad+'" value="'+ag[ad]+'" />'}}aa.outerHTML='<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"'+ah+">"+af+"</object>";N[N.length]=ai.id;X=c(ai.id)}else{var Z=C(r);Z.setAttribute("type",q);for(var ac in ai){if(ai[ac]!=Object.prototype[ac]){if(ac.toLowerCase()=="styleclass"){Z.setAttribute("class",ai[ac])}else{if(ac.toLowerCase()!="classid"){Z.setAttribute(ac,ai[ac])}}}}for(var ab in ag){if(ag[ab]!=Object.prototype[ab]&&ab.toLowerCase()!="movie"){e(Z,ab,ag[ab])}}aa.parentNode.replaceChild(Z,aa);X=Z}}return X}function e(Z,X,Y){var aa=C("param");aa.setAttribute("name",X);aa.setAttribute("value",Y);Z.appendChild(aa)}function y(Y){var X=c(Y);if(X&&X.nodeName=="OBJECT"){if(M.ie&&M.win){X.style.display="none";(function(){if(X.readyState==4){b(Y)}else{setTimeout(arguments.callee,10)}})()}else{X.parentNode.removeChild(X)}}}function b(Z){var Y=c(Z);if(Y){for(var X in Y){if(typeof Y[X]=="function"){Y[X]=null}}Y.parentNode.removeChild(Y)}}function c(Z){var X=null;try{X=j.getElementById(Z)}catch(Y){}return X}function C(X){return j.createElement(X)}function i(Z,X,Y){Z.attachEvent(X,Y);I[I.length]=[Z,X,Y]}function F(Z){var Y=M.pv,X=Z.split(".");X[0]=parseInt(X[0],10);X[1]=parseInt(X[1],10)||0;X[2]=parseInt(X[2],10)||0;return(Y[0]>X[0]||(Y[0]==X[0]&&Y[1]>X[1])||(Y[0]==X[0]&&Y[1]==X[1]&&Y[2]>=X[2]))?true:false}function v(ac,Y,ad,ab){if(M.ie&&M.mac){return}var aa=j.getElementsByTagName("head")[0];if(!aa){return}var X=(ad&&typeof ad=="string")?ad:"screen";if(ab){n=null;G=null}if(!n||G!=X){var Z=C("style");Z.setAttribute("type","text/css");Z.setAttribute("media",X);n=aa.appendChild(Z);if(M.ie&&M.win&&typeof j.styleSheets!=D&&j.styleSheets.length>0){n=j.styleSheets[j.styleSheets.length-1]}G=X}if(M.ie&&M.win){if(n&&typeof n.addRule==r){n.addRule(ac,Y)}}else{if(n&&typeof j.createTextNode!=D){n.appendChild(j.createTextNode(ac+" {"+Y+"}"))}}}function w(Z,X){if(!m){return}var Y=X?"visible":"hidden";if(J&&c(Z)){c(Z).style.visibility=Y}else{v("#"+Z,"visibility:"+Y)}}function L(Y){var Z=/[\\\"<>\.;]/;var X=Z.exec(Y)!=null;return X&&typeof encodeURIComponent!=D?encodeURIComponent(Y):Y}var d=function(){if(M.ie&&M.win){window.attachEvent("onunload",function(){var ac=I.length;for(var ab=0;ab<ac;ab++){I[ab][0].detachEvent(I[ab][1],I[ab][2])}var Z=N.length;for(var aa=0;aa<Z;aa++){y(N[aa])}for(var Y in M){M[Y]=null}M=null;for(var X in swfobject){swfobject[X]=null}swfobject=null})}}();return{registerObject:function(ab,X,aa,Z){if(M.w3&&ab&&X){var Y={};Y.id=ab;Y.swfVersion=X;Y.expressInstall=aa;Y.callbackFn=Z;o[o.length]=Y;w(ab,false)}else{if(Z){Z({success:false,id:ab})}}},getObjectById:function(X){if(M.w3){return z(X)}},embedSWF:function(ab,ah,ae,ag,Y,aa,Z,ad,af,ac){var X={success:false,id:ah};if(M.w3&&!(M.wk&&M.wk<312)&&ab&&ah&&ae&&ag&&Y){w(ah,false);K(function(){ae+="";ag+="";var aj={};if(af&&typeof af===r){for(var al in af){aj[al]=af[al]}}aj.data=ab;aj.width=ae;aj.height=ag;var am={};if(ad&&typeof ad===r){for(var ak in ad){am[ak]=ad[ak]}}if(Z&&typeof Z===r){for(var ai in Z){if(typeof am.flashvars!=D){am.flashvars+="&"+ai+"="+Z[ai]}else{am.flashvars=ai+"="+Z[ai]}}}if(F(Y)){var an=u(aj,am,ah);if(aj.id==ah){w(ah,true)}X.success=true;X.ref=an}else{if(aa&&A()){aj.data=aa;P(aj,am,ah,ac);return}else{w(ah,true)}}if(ac){ac(X)}})}else{if(ac){ac(X)}}},switchOffAutoHideShow:function(){m=false},ua:M,getFlashPlayerVersion:function(){return{major:M.pv[0],minor:M.pv[1],release:M.pv[2]}},hasFlashPlayerVersion:F,createSWF:function(Z,Y,X){if(M.w3){return u(Z,Y,X)}else{return undefined}},showExpressInstall:function(Z,aa,X,Y){if(M.w3&&A()){P(Z,aa,X,Y)}},removeSWF:function(X){if(M.w3){y(X)}},createCSS:function(aa,Z,Y,X){if(M.w3){v(aa,Z,Y,X)}},addDomLoadEvent:K,addLoadEvent:s,getQueryParamValue:function(aa){var Z=j.location.search||j.location.hash;if(Z){if(/\?/.test(Z)){Z=Z.split("?")[1]}if(aa==null){return L(Z)}var Y=Z.split("&");for(var X=0;X<Y.length;X++){if(Y[X].substring(0,Y[X].indexOf("="))==aa){return L(Y[X].substring((Y[X].indexOf("=")+1)))}}}return""},expressInstallCallback:function(){if(a){var X=c(R);if(X&&l){X.parentNode.replaceChild(l,X);if(Q){w(Q,true);if(M.ie&&M.win){l.style.display="block"}}if(E){E(B)}}a=false}}}}();
		var opts = {
				"sa_path": "SaPload/",  
				"sa_id": "SaPload_v3",   
				"sa_box_id": "SaPload", 
				"sa_width": "500", 
				"sa_height": "40",
				"sa_session_id": "",    
				"sa_args": '{"a1":"a1"}', 
				"sa_model": "base",   
				"sa_upload_type": '{"图片文件":"*.jpg;*.jpeg;*.gif;*.png"}',    
				"sa_upload_max_num": "5",    
				"sa_upload_max_size": "2",    
				"sa_upload_url": "../../ajax/imageuploadmuti.aspx",
				"sa_upload_field": "uploadfile",   
				"sa_upload_debug": "true",  
				"sa_method": "POST",  
				"sa_skin": "default",  
				"sa_lang": "default"
			};
			
		//加载配置文件
		opts = sapload.extend(opts,option);
		if(opts.sa_model=='html5'){
			SaPload.last='_obj';
		}else{
			SaPload.last='_obj';
		}
		opts.sa_id = opts.sa_box_id+SaPload.last;
		//找到亲生父亲
		var father = sapload.get(opts.sa_box_id);
		var so;//声明flash对象
		var lang;//声明lang对象
		var skin;//声明skin对象
		var filesArr=[];
		var bar_lv=0;
		//实例化函数
		sapload.init = function(){
			sapload.insert();
		}
		//实例化flash对象
		sapload.initswfobj = function(){
			so = sapload.fobj(opts.sa_id);
		}
		//预留函数
		sapload.showfobje = function(){
			if(so){
				alert(so);
			}else{
				setTimeout(sapload.showfobje,500);
			}
		}
		//插入flash
		sapload.insert = function(){
			if(opts.sa_model=='html5'){
				sapload.getjson(opts.sa_path+'SaPload.Config.js');
			}
			if(opts.sa_model=='base'){
				var div = document.createElement('div');
				div.id=opts.sa_id;
				father.appendChild(div);
				//参数设置
				var flashvarsObj = {
					sa_id: opts.sa_id,    
					sa_session_id: opts.sa_session_id,    
					sa_model: opts.sa_model,    
					sa_upload_type: encodeURI(opts.sa_upload_type),    
					sa_upload_max_num: opts.sa_upload_max_num,    
					sa_upload_max_size: opts.sa_upload_max_size,    
					sa_upload_url: opts.sa_upload_url,    
					sa_upload_field: opts.sa_upload_field,   
					sa_method: opts.sa_method,   
					sa_args: encodeURI(opts.sa_args),   
					sa_upload_debug: opts.sa_upload_debug,   
					sa_lang: opts.sa_lang,
					sa_skin: opts.sa_skin  
				};
				var parObj = {
					wmode: "transparent"
				};
				var attObj = null
				sapload.swf.embedSWF(opts.sa_path+"SaPload.swf", opts.sa_id, opts.sa_width, opts.sa_height, "11.0.0",opts.sa_path+"expressInstall.swf", flashvarsObj, parObj, attObj);
				setTimeout(sapload.initswfobj,1);
			}
		}
		//动态加载js
		sapload.getjson=function (u){
			var oXmlHttp;
			if ( window.XMLHttpRequest ) // Gecko
				oXmlHttp =  new XMLHttpRequest() ;
			else if ( window.ActiveXObject ) // IE
				oXmlHttp =  new ActiveXObject("MsXml2.XmlHttp") ; 
			oXmlHttp.open('GET', u, true);
			oXmlHttp.send(null); 
			oXmlHttp.onreadystatechange = function() 
			{
				if ( oXmlHttp.readyState == 4 )
				{
					if ( oXmlHttp.status == 200 || oXmlHttp.status == 304 )
					{
						var rs =oXmlHttp.responseText;
						var ro = eval('(' + rs + ')');
						sapload.init_html5(ro);
					}
				}
			}
		}
		sapload.RGBToHex=function (strRGB,alpha){
			var reg = /^#([0-9a-fA-f]{3}|[0-9a-fA-f]{6})$/;  
			var sColor = strRGB.toLowerCase();  
			if(sColor && reg.test(sColor)){  
				if(sColor.length === 4){  
					var sColorNew = "#";  
					for(var i=1; i<4; i+=1){  
						sColorNew += sColor.slice(i,i+1).concat(sColor.slice(i,i+1));     
					}  
					sColor = sColorNew;  
				}  
				//处理六位的颜色值  
				var sColorChange = [];  
				for(var i=1; i<7; i+=2){  
					sColorChange.push(parseInt("0x"+sColor.slice(i,i+2)));    
				}  
				return "rgba(" + sColorChange.join(",") + ","+alpha+")";  
			}else{  
				return sColor;    
			}
		}
		//html5 model
		sapload.init_html5=function (u){
			var div = document.createElement('div');
			div.id=opts.sa_id;
			var msg = document.createElement('div');
			msg.id=opts.sa_id+'_msg';
			var fil = document.createElement('div');
			fil.id=opts.sa_id+'_fil';
			var upl = document.createElement('div');
			upl.id=opts.sa_id+'_upl';
			
			lang = u.lang[opts.sa_lang];
			skin = u.skin[opts.sa_skin];
			var pdar = skin.box_padding.split(' ');
			var mrar = skin.box_margin.split(' ');
			
			skin.paddding_left=pdar[3]*2+'px';
			skin.paddding_top=pdar[0]+'px';
			skin.paddding_bottom=pdar[2]+'px';
			skin.paddding_right=pdar[1]*2+'px';
			skin.margin_left=mrar[3]+'px';
			skin.margin_top=mrar[0]+'px';
			skin.margin_bottom=mrar[2]+'px';
			skin.margin_right=mrar[1]+'px';
			
			msg.className=fil.className=upl.className='button';
			msg.style.transition=fil.style.transition=fil.style.transition='all .6s ease-in-out';
			msg.style.border=fil.style.border=upl.style.border='1px '+skin.border_color+' solid';
			msg.style.borderRadius=fil.style.borderRadius=upl.style.borderRadius=(skin.box_radius-3)+'px';
			msg.style.position=fil.style.position=upl.style.position='relative';
			msg.style.height=fil.style.height=upl.style.height=skin.box_height+'px';
			msg.style.display=fil.style.display=upl.style.display='inline-block';
			msg.style.padding=fil.style.padding=upl.style.padding=skin.paddding_top+' '+skin.paddding_right+' '+skin.paddding_bottom+' '+skin.paddding_right;
			msg.style.margin=fil.style.margin=upl.style.margin=skin.margin_top+' '+skin.margin_left+' '+skin.margin_bottom+' '+skin.margin_right
			msg.style.lineHeight=fil.style.lineHeight=upl.style.lineHeight=skin.box_height+'px';
			msg.style.cursor=fil.style.cursor=upl.style.cursor='pointer';
			msg.style.overflow=fil.style.overflow=upl.style.overflow='hidden';
			msg.style.background=fil.style.background=upl.style.background=sapload.RGBToHex(skin.box_color,skin.box_alpha);
			//div.style='position:relative;overflow:hidden;border:1px '+skin.border_color+' solid;  height:'+skin.box_height+'px; display:inline-block;padding:'+skin.paddding_top+' '+skin.paddding_right+' '+skin.paddding_bottom+' '+skin.paddding_right+' '+';border-radius:'+(skin.box_radius-3)+'px; padding-left:1em;padding-right:1em; margin:'+skin.margin_top+' '+skin.margin_left+' '+skin.margin_bottom+' '+skin.margin_right+' '+';background:'+sapload.RGBToHex(skin.box_color,skin.box_alpha)+'; overflow:hidden; line-height:'+skin.box_height+'px;';
			fil.innerHTML=lang.msg1;
			so=document.createElement('input');
			so.type='file';
			so.multiple='true';
			//so.style='font-size: 999px; opacity: 0; position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; cursor:pointer;';
			so.style.opacity=0;
			so.style.position='absolute';
			so.style.width='100%';
			so.style.height='100%';
			so.style.top='0';
			so.style.left='0';
			so.style.fontSize='999px';
			so.style.cursor='pointer';
			fil.appendChild(so);
			div.appendChild(msg);
			msg.style.display='none';
			div.appendChild(fil);
			so.addEventListener("change", function(e) { sapload.funGetFiles(e); }, false);
			div.appendChild(upl);
			upl.style.display='none';
			div.opts = opts;
			father.appendChild(div);
		}
		//html5 model
		sapload.funGetFiles=function (e){
			filesArr=[];
			// 获取文件列表对象
			var files = e.target.files || e.dataTransfer.files;
			//继续添加文件
			for (var i = 0, f; f = files[i]; i++) {
			  if (f.size > opts.sa_upload_max_size * 1024 * 1024) {
				SaPload.Trace('文件名称：' + f.name + '为：' + Number(f.size / (1024 * 1024)).toFixed(2) + ' M，大于允许上传的' + opts.sa_upload_max_size + ' M');   
				SaPload.JsonData('{"name":"'+f.name+'","size":"'+Number(f.size/(1024*1024)).toFixed(2)+' M","status":"false","sid":"'+opts.sa_id+'","action":"fileList","sa_upload_url":"'+opts.sa_upload_url+'"}');   
			  }else{
				f.index = i;
				filesArr.push(f);
				SaPload.Trace('文件名称：' + f.name + '，标识：' + i);   
				SaPload.JsonData('{"name":"'+f.name+'","size":"'+Number(f.size/(1024*1024)).toFixed(2)+' M","status":"true","id":"'+i+'","sid":"'+opts.sa_id+'","action":"fileList","sa_upload_url":"'+opts.sa_upload_url+'"}'); 
			  }
			}
			SaPload.Trace('用户选择文件：' + filesArr.length);   
			if(filesArr.length>0){
				var msg = sapload.get(opts.sa_id+'_msg');
				var fil = sapload.get(opts.sa_id+'_fil');
				var bar = document.createElement('div');
				
				bar.id=opts.sa_id+'_bar'
				bar.style.width='0';
				bar.style.position='absolute';
				bar.style.background=sapload.RGBToHex(skin.bar_color,skin.bar_alpha);
				bar.style.height='100%';
				bar.style.top='0';
				bar.style.left='0';
				
				var upl = sapload.get(opts.sa_id+'_upl');
				so=document.createElement('input');
				so.type='file';
				so.multiple='true';
				//so.style='font-size: 999px; opacity: 0; position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; cursor:pointer;';
				so.style.opacity=0;
				so.style.position='absolute';
				so.style.width='100%';
				so.style.height='100%';
				so.style.top='0';
				so.style.left='0';
				so.style.fontSize='999px';
				so.style.cursor='pointer';
				msg.style.display='inline-block';
				fil.innerHTML=String(lang.msg5);
				upl.innerHTML=String(lang.msg3);
				upl.style.display='inline-block';
				fil.appendChild(so);
				
				so.addEventListener("change", function(e) { sapload.funGetFiles(e); }, false);
				upl.addEventListener("click", function(e) { sapload.funUploadFile(e,0); }, false);
				msg.innerHTML=String(lang.msg2).split("#").join(filesArr.length);
				msg.appendChild(bar);
			}
		}
		sapload.funUploadFile=function (e,i){
			var msg = sapload.get(opts.sa_id+'_msg');
			var fil = sapload.get(opts.sa_id+'_fil');
			var upl = sapload.get(opts.sa_id+'_upl');
			fil.style.display='none';
			upl.style.display='none';
			var f = filesArr[i];
			var xhr = new XMLHttpRequest();
			xhr.upload.addEventListener("progress", function(e) {
						sapload.onProgress(f, e.loaded, e.total,i);
					}, false);
			xhr.onreadystatechange = function(e) {
						if (xhr.readyState == 4) {
							if (xhr.status == 200) {
								sapload.onSuccess(f, xhr.responseText,i+1);
							} 
						}
					};
			var fd = new FormData();
			fd.append(opts.sa_upload_field, f);
			fd.append('timeStamp', new Date().getTime());
			var objs = sapload.get(opts.sa_id);
			var person = eval('(' + objs.opts.sa_args + ')');
			for(var item in person){  
				if(typeof person[item]  === 'string'){   
				  fd.append(item, person[item]);
				}
			}  
			// 开始上传
			xhr.open("POST", opts.sa_upload_url);
			xhr.send(fd);
			msg.innerHTML=lang.msg7.split("#").join(i+1);
			var bar = document.createElement('div');
			bar.id=opts.sa_id+'_bar'
			bar.style.width='0';
			bar.style.position='absolute';
			bar.style.background=sapload.RGBToHex(skin.bar_color,skin.bar_alpha);
			bar.style.height='100%';
			bar.style.top='0';
			bar.style.left='0';
			msg.appendChild(bar);	
			SaPload.Trace('开始上传第：'+(i+1)+'个文件，文件标识为：'+i);   
			SaPload.JsonData('{"msg":"upload '+i+'","sid":"'+opts.sa_id+'","action":"msg"}');  	
		}
		sapload.onSuccess=function (e,t,i){
			bar_lv=0;
			var bar = sapload.get(opts.sa_id+'_bar');
			bar.style.width='0';
			SaPload.Trace((i-1)+' 上传结束：'+t);    
			SaPload.JsonData('{"msg":"'+t+'","id":"'+(i-1)+'","sid":"'+opts.sa_id+'","action":"back"}');  
			SaPload.PicData(t);
			if (i<filesArr.length) {
				sapload.funUploadFile('',i);
			}else{
				var msg = sapload.get(opts.sa_id+'_msg');
				var fil = sapload.get(opts.sa_id+'_fil');
				fil.style.display='inline-block';
				fil.innerHTML=lang.msg9;
				fil.appendChild(so);
				msg.innerHTML=lang.msg8.split("#").join(filesArr.length);
				SaPload.Trace('上传动作全部结束');   
				SaPload.JsonData('{"msg":"upload finish","sid":"'+opts.sa_id+'","action":"msg"}');  	
			}
		}
		sapload.onProgress=function (e,a,b,i){
			var bar = sapload.get(opts.sa_id+'_bar');
			var percentComplete = (a / b).toFixed(2);
			if(percentComplete>bar_lv){
				bar_lv=percentComplete;
			}
			bar.style.width=parseInt(bar_lv*100)+'%';
			SaPload.Trace(i + ' 上传进行中' + bar_lv);
			SaPload.JsonData('{"v":"'+bar_lv+'","id":"'+i+'","sid":"'+opts.sa_id+'","action":"progress"}'); 
		}
		//初始化实例
		sapload.init();
		return sapload;
	}
};