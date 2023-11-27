import{u as U,v as V}from"./visits-66c9d696.js";import{_ as H}from"./BackButton.vue_vue_type_script_setup_true_lang-2c968fd6.js";import{_ as K}from"./CompanionsDataTable.vue_vue_type_script_setup_true_lang-fc95d8a6.js";import{a as D,f as z}from"./formatTime-62e57159.js";import{d as R,g as x,h as N,x as I,r as _,i as J,c as S,b as y,w as C,l as P,Z as A,o as w,k as L,q as F,a as f,p as $,e as Z,_ as G,E as X,F as B,m as Q,j as Y,n as k}from"./index-434e95c2.js";var q={exports:{}};(function(T,M){(function(a,n){T.exports=n()})(window,function(){return function(b){var a={};function n(i){if(a[i])return a[i].exports;var c=a[i]={i,l:!1,exports:{}};return b[i].call(c.exports,c,c.exports,n),c.l=!0,c.exports}return n.m=b,n.c=a,n.d=function(i,c,o){n.o(i,c)||Object.defineProperty(i,c,{enumerable:!0,get:o})},n.r=function(i){typeof Symbol<"u"&&Symbol.toStringTag&&Object.defineProperty(i,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(i,"__esModule",{value:!0})},n.t=function(i,c){if(c&1&&(i=n(i)),c&8||c&4&&typeof i=="object"&&i&&i.__esModule)return i;var o=Object.create(null);if(n.r(o),Object.defineProperty(o,"default",{enumerable:!0,value:i}),c&2&&typeof i!="string")for(var u in i)n.d(o,u,(function(s){return i[s]}).bind(null,u));return o},n.n=function(i){var c=i&&i.__esModule?function(){return i.default}:function(){return i};return n.d(c,"a",c),c},n.o=function(i,c){return Object.prototype.hasOwnProperty.call(i,c)},n.p="",n(n.s=0)}({"./src/index.js":function(b,a,n){n.r(a),n("./src/sass/index.scss");var i=n("./src/js/init.js"),c=i.default.init;typeof window<"u"&&(window.printJS=c),a.default=c},"./src/js/browser.js":function(b,a,n){n.r(a);var i={isFirefox:function(){return typeof InstallTrigger<"u"},isIE:function(){return navigator.userAgent.indexOf("MSIE")!==-1||!!document.documentMode},isEdge:function(){return!i.isIE()&&!!window.StyleMedia},isChrome:function(){var o=arguments.length>0&&arguments[0]!==void 0?arguments[0]:window;return!!o.chrome},isSafari:function(){return Object.prototype.toString.call(window.HTMLElement).indexOf("Constructor")>0||navigator.userAgent.toLowerCase().indexOf("safari")!==-1},isIOSChrome:function(){return navigator.userAgent.toLowerCase().indexOf("crios")!==-1}};a.default=i},"./src/js/functions.js":function(b,a,n){n.r(a),n.d(a,"addWrapper",function(){return u}),n.d(a,"capitalizePrint",function(){return s}),n.d(a,"collectStyles",function(){return r}),n.d(a,"addHeader",function(){return t}),n.d(a,"cleanUp",function(){return h}),n.d(a,"isRawHTML",function(){return v});var i=n("./src/js/modal.js"),c=n("./src/js/browser.js");function o(e){"@babel/helpers - typeof";return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?o=function(m){return typeof m}:o=function(m){return m&&typeof Symbol=="function"&&m.constructor===Symbol&&m!==Symbol.prototype?"symbol":typeof m},o(e)}function u(e,d){var m="font-family:"+d.font+" !important; font-size: "+d.font_size+" !important; width:100%;";return'<div style="'+m+'">'+e+"</div>"}function s(e){return e.charAt(0).toUpperCase()+e.slice(1)}function r(e,d){for(var m=document.defaultView||window,g="",p=m.getComputedStyle(e,""),j=0;j<p.length;j++)(d.targetStyles.indexOf("*")!==-1||d.targetStyle.indexOf(p[j])!==-1||l(d.targetStyles,p[j]))&&p.getPropertyValue(p[j])&&(g+=p[j]+":"+p.getPropertyValue(p[j])+";");return g+="max-width: "+d.maxWidth+"px !important; font-size: "+d.font_size+" !important;",g}function l(e,d){for(var m=0;m<e.length;m++)if(o(d)==="object"&&d.indexOf(e[m])!==-1)return!0;return!1}function t(e,d){var m=document.createElement("div");if(v(d.header))m.innerHTML=d.header;else{var g=document.createElement("h1"),p=document.createTextNode(d.header);g.appendChild(p),g.setAttribute("style",d.headerStyle),m.appendChild(g)}e.insertBefore(m,e.childNodes[0])}function h(e){e.showModal&&i.default.close(),e.onLoadingEnd&&e.onLoadingEnd(),(e.showModal||e.onLoadingStart)&&window.URL.revokeObjectURL(e.printable);var d="mouseover";(c.default.isChrome()||c.default.isFirefox())&&(d="focus");var m=function g(){window.removeEventListener(d,g),e.onPrintDialogClose();var p=document.getElementById(e.frameId);p&&p.remove()};window.addEventListener(d,m)}function v(e){var d=new RegExp("<([A-Za-z][A-Za-z0-9]*)\\b[^>]*>(.*?)</\\1>");return d.test(e)}},"./src/js/html.js":function(b,a,n){n.r(a);var i=n("./src/js/functions.js"),c=n("./src/js/print.js");function o(r){"@babel/helpers - typeof";return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?o=function(t){return typeof t}:o=function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},o(r)}a.default={print:function(l,t){var h=s(l.printable)?l.printable:document.getElementById(l.printable);if(!h){window.console.error("Invalid HTML element id: "+l.printable);return}l.printableElement=u(h,l),l.header&&Object(i.addHeader)(l.printableElement,l),c.default.send(l,t)}};function u(r,l){for(var t=r.cloneNode(),h=Array.prototype.slice.call(r.childNodes),v=0;v<h.length;v++)if(l.ignoreElements.indexOf(h[v].id)===-1){var e=u(h[v],l);t.appendChild(e)}switch(l.scanStyles&&r.nodeType===1&&t.setAttribute("style",Object(i.collectStyles)(r,l)),r.tagName){case"SELECT":t.value=r.value;break;case"CANVAS":t.getContext("2d").drawImage(r,0,0);break}return t}function s(r){return o(r)==="object"&&r&&(r instanceof HTMLElement||r.nodeType===1)}},"./src/js/image.js":function(b,a,n){n.r(a);var i=n("./src/js/functions.js"),c=n("./src/js/print.js"),o=n("./src/js/browser.js");a.default={print:function(s,r){s.printable.constructor!==Array&&(s.printable=[s.printable]),s.printableElement=document.createElement("div"),s.printable.forEach(function(l){var t=document.createElement("img");if(t.setAttribute("style",s.imageStyle),t.src=l,o.default.isFirefox()){var h=t.src;t.src=h}var v=document.createElement("div");v.appendChild(t),s.printableElement.appendChild(v)}),s.header&&Object(i.addHeader)(s.printableElement,s),c.default.send(s,r)}}},"./src/js/init.js":function(b,a,n){n.r(a);var i=n("./src/js/browser.js"),c=n("./src/js/modal.js"),o=n("./src/js/pdf.js"),u=n("./src/js/html.js"),s=n("./src/js/raw-html.js"),r=n("./src/js/image.js"),l=n("./src/js/json.js");function t(v){"@babel/helpers - typeof";return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?t=function(d){return typeof d}:t=function(d){return d&&typeof Symbol=="function"&&d.constructor===Symbol&&d!==Symbol.prototype?"symbol":typeof d},t(v)}var h=["pdf","html","image","json","raw-html"];a.default={init:function(){var e={printable:null,fallbackPrintable:null,type:"pdf",header:null,headerStyle:"font-weight: 300;",maxWidth:800,properties:null,gridHeaderStyle:"font-weight: bold; padding: 5px; border: 1px solid #dddddd;",gridStyle:"border: 1px solid lightgray; margin-bottom: -1px;",showModal:!1,onError:function(W){throw W},onLoadingStart:null,onLoadingEnd:null,onPrintDialogClose:function(){},onIncompatibleBrowser:function(){},modalMessage:"Retrieving Document...",frameId:"printJS",printableElement:null,documentTitle:"Document",targetStyle:["clear","display","width","min-width","height","min-height","max-height"],targetStyles:["border","box","break","text-decoration"],ignoreElements:[],repeatTableHeader:!0,css:null,style:null,scanStyles:!0,base64:!1,onPdfOpen:null,font:"TimesNewRoman",font_size:"12pt",honorMarginPadding:!0,honorColor:!1,imageStyle:"max-width: 100%;"},d=arguments[0];if(d===void 0)throw new Error("printJS expects at least 1 attribute.");switch(t(d)){case"string":e.printable=encodeURI(d),e.fallbackPrintable=e.printable,e.type=arguments[1]||e.type;break;case"object":e.printable=d.printable,e.fallbackPrintable=typeof d.fallbackPrintable<"u"?d.fallbackPrintable:e.printable,e.fallbackPrintable=e.base64?"data:application/pdf;base64,".concat(e.fallbackPrintable):e.fallbackPrintable;for(var m in e)m==="printable"||m==="fallbackPrintable"||(e[m]=typeof d[m]<"u"?d[m]:e[m]);break;default:throw new Error('Unexpected argument type! Expected "string" or "object", got '+t(d))}if(!e.printable)throw new Error("Missing printable information.");if(!e.type||typeof e.type!="string"||h.indexOf(e.type.toLowerCase())===-1)throw new Error("Invalid print type. Available types are: pdf, html, image and json.");e.showModal&&c.default.show(e),e.onLoadingStart&&e.onLoadingStart();var g=document.getElementById(e.frameId);g&&g.parentNode.removeChild(g);var p=document.createElement("iframe");switch(i.default.isFirefox()?p.setAttribute("style","width: 1px; height: 100px; position: fixed; left: 0; top: 0; opacity: 0; border-width: 0; margin: 0; padding: 0"):p.setAttribute("style","visibility: hidden; height: 0; width: 0; position: absolute; border: 0"),p.setAttribute("id",e.frameId),e.type!=="pdf"&&(p.srcdoc="<html><head><title>"+e.documentTitle+"</title>",e.css&&(Array.isArray(e.css)||(e.css=[e.css]),e.css.forEach(function(O){p.srcdoc+='<link rel="stylesheet" href="'+O+'">'})),p.srcdoc+="</head><body></body></html>"),e.type){case"pdf":if(i.default.isIE())try{console.info("Print.js doesn't support PDF printing in Internet Explorer.");var j=window.open(e.fallbackPrintable,"_blank");j.focus(),e.onIncompatibleBrowser()}catch(O){e.onError(O)}finally{e.showModal&&c.default.close(),e.onLoadingEnd&&e.onLoadingEnd()}else o.default.print(e,p);break;case"image":r.default.print(e,p);break;case"html":u.default.print(e,p);break;case"raw-html":s.default.print(e,p);break;case"json":l.default.print(e,p);break}}}},"./src/js/json.js":function(b,a,n){n.r(a);var i=n("./src/js/functions.js"),c=n("./src/js/print.js");function o(s){"@babel/helpers - typeof";return typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?o=function(l){return typeof l}:o=function(l){return l&&typeof Symbol=="function"&&l.constructor===Symbol&&l!==Symbol.prototype?"symbol":typeof l},o(s)}a.default={print:function(r,l){if(o(r.printable)!=="object")throw new Error("Invalid javascript data object (JSON).");if(typeof r.repeatTableHeader!="boolean")throw new Error("Invalid value for repeatTableHeader attribute (JSON).");if(!r.properties||!Array.isArray(r.properties))throw new Error("Invalid properties array for your JSON data.");r.properties=r.properties.map(function(t){return{field:o(t)==="object"?t.field:t,displayName:o(t)==="object"?t.displayName:t,columnSize:o(t)==="object"&&t.columnSize?t.columnSize+";":100/r.properties.length+"%;"}}),r.printableElement=document.createElement("div"),r.header&&Object(i.addHeader)(r.printableElement,r),r.printableElement.innerHTML+=u(r),c.default.send(r,l)}};function u(s){var r=s.printable,l=s.properties,t='<table style="border-collapse: collapse; width: 100%;">';s.repeatTableHeader&&(t+="<thead>"),t+="<tr>";for(var h=0;h<l.length;h++)t+='<th style="width:'+l[h].columnSize+";"+s.gridHeaderStyle+'">'+Object(i.capitalizePrint)(l[h].displayName)+"</th>";t+="</tr>",s.repeatTableHeader&&(t+="</thead>"),t+="<tbody>";for(var v=0;v<r.length;v++){t+="<tr>";for(var e=0;e<l.length;e++){var d=r[v],m=l[e].field.split(".");if(m.length>1)for(var g=0;g<m.length;g++)d=d[m[g]];else d=d[l[e].field];t+='<td style="width:'+l[e].columnSize+s.gridStyle+'">'+d+"</td>"}t+="</tr>"}return t+="</tbody></table>",t}},"./src/js/modal.js":function(b,a,n){n.r(a);var i={show:function(o){var u="font-family:sans-serif; display:table; text-align:center; font-weight:300; font-size:30px; left:0; top:0;position:fixed; z-index: 9990;color: #0460B5; width: 100%; height: 100%; background-color:rgba(255,255,255,.9);transition: opacity .3s ease;",s=document.createElement("div");s.setAttribute("style",u),s.setAttribute("id","printJS-Modal");var r=document.createElement("div");r.setAttribute("style","display:table-cell; vertical-align:middle; padding-bottom:100px;");var l=document.createElement("div");l.setAttribute("class","printClose"),l.setAttribute("id","printClose"),r.appendChild(l);var t=document.createElement("span");t.setAttribute("class","printSpinner"),r.appendChild(t);var h=document.createTextNode(o.modalMessage);r.appendChild(h),s.appendChild(r),document.getElementsByTagName("body")[0].appendChild(s),document.getElementById("printClose").addEventListener("click",function(){i.close()})},close:function(){var o=document.getElementById("printJS-Modal");o&&o.parentNode.removeChild(o)}};a.default=i},"./src/js/pdf.js":function(b,a,n){n.r(a);var i=n("./src/js/print.js"),c=n("./src/js/functions.js");a.default={print:function(s,r){if(s.base64){var l=Uint8Array.from(atob(s.printable),function(h){return h.charCodeAt(0)});o(s,r,l);return}s.printable=/^(blob|http|\/\/)/i.test(s.printable)?s.printable:window.location.origin+(s.printable.charAt(0)!=="/"?"/"+s.printable:s.printable);var t=new window.XMLHttpRequest;t.responseType="arraybuffer",t.addEventListener("error",function(){Object(c.cleanUp)(s),s.onError(t.statusText,t)}),t.addEventListener("load",function(){if([200,201].indexOf(t.status)===-1){Object(c.cleanUp)(s),s.onError(t.statusText,t);return}o(s,r,t.response)}),t.open("GET",s.printable,!0),t.send()}};function o(u,s,r){var l=new window.Blob([r],{type:"application/pdf"});l=window.URL.createObjectURL(l),s.setAttribute("src",l),i.default.send(u,s)}},"./src/js/print.js":function(b,a,n){n.r(a);var i=n("./src/js/browser.js"),c=n("./src/js/functions.js"),o={send:function(t,h){document.getElementsByTagName("body")[0].appendChild(h);var v=document.getElementById(t.frameId);v.onload=function(){if(t.type==="pdf"){i.default.isFirefox()?setTimeout(function(){return u(v,t)},1e3):u(v,t);return}var e=v.contentWindow||v.contentDocument;if(e.document&&(e=e.document),e.body.appendChild(t.printableElement),t.type!=="pdf"&&t.style){var d=document.createElement("style");d.innerHTML=t.style,e.head.appendChild(d)}var m=e.getElementsByTagName("img");m.length>0?s(Array.from(m)).then(function(){return u(v,t)}):u(v,t)}}};function u(l,t){try{if(l.focus(),i.default.isEdge()||i.default.isIE())try{l.contentWindow.document.execCommand("print",!1,null)}catch{l.contentWindow.print()}else l.contentWindow.print()}catch(h){t.onError(h)}finally{i.default.isFirefox()&&(l.style.visibility="hidden",l.style.left="-1px"),Object(c.cleanUp)(t)}}function s(l){var t=l.map(function(h){if(h.src&&h.src!==window.location.href)return r(h)});return Promise.all(t)}function r(l){return new Promise(function(t){var h=function v(){!l||typeof l.naturalWidth>"u"||l.naturalWidth===0||!l.complete?setTimeout(v,500):t()};h()})}a.default=o},"./src/js/raw-html.js":function(b,a,n){n.r(a);var i=n("./src/js/print.js");a.default={print:function(o,u){o.printableElement=document.createElement("div"),o.printableElement.setAttribute("style","width:100%"),o.printableElement.innerHTML=o.printable,i.default.send(o,u)}}},"./src/sass/index.scss":function(b,a,n){},0:function(b,a,n){b.exports=n("./src/index.js")}}).default})})(q);const E=T=>($("data-v-d9930bed"),T=T(),Z(),T),ee={id:"printJS-form"},te={key:0},ne={class:"grid p-fluid"},oe={class:"field col-12 md:col-6 lg:col-4"},ie={class:"p-float-label"},le={class:"field col-12 md:col-6 lg:col-4"},re={class:"p-float-label"},se={class:"field col-12 md:col-6 lg:col-4"},de={class:"p-float-label"},ae={class:"field col-12 md:col-6 lg:col-4"},ce={class:"p-float-label"},fe={class:"field col-12 md:col-6 lg:col-4"},ue={class:"p-float-label"},pe={class:"field col-12 md:col-6 lg:col-4"},me={key:1},he={class:"grid p-fluid my-5"},ve={class:"field col-12 md:col-6 lg:col-4"},ye={class:""},be=E(()=>f("label",{for:"startTime"}," تاريخ بداية الزيارة المتوقع",-1)),ge={class:"field col-12 md:col-6 lg:col-4"},Ee={class:""},_e=E(()=>f("label",{for:"stopDate"},"تاريخ انتهاء الزيارة المتوقع",-1)),we={class:"grid p-fluid"},je={class:"field col-12 md:col-6 lg:col-4"},Se={class:""},Te=E(()=>f("label",{for:"customerName"},"العميل",-1)),Me={class:"field col-12 md:col-6 lg:col-4"},Pe={class:""},Oe=E(()=>f("label",{for:"customerName"},"الاشتراك",-1)),Ce={class:"field col-12 md:col-6 lg:col-4"},xe={class:""},De=E(()=>f("label",{for:"companionName"}," مدة الزيارة ",-1)),Ie={class:"field col-12 md:col-6 lg:col-4"},Le={class:""},Ae=E(()=>f("label",{for:"visitPrice"}," السعر ",-1)),Be={class:"field col-12 md:col-6 lg:col-4"},Ue={class:""},Re=E(()=>f("label",{style:{color:"black",top:"-0.75rem","font-size":"12px"},for:"visitType"},"سبب الزيارة ",-1)),Ne={class:"field col-8 md:col-3 lg:col-4"},We={class:""},Ve=E(()=>f("label",{for:"notes"}," الملاحظات ",-1)),He=E(()=>f("span",{style:{"font-size":"20px","font-weight":"bold","margin-right":"0.5rem"},for:"representives"}," المخوليين: ",-1)),Ke=E(()=>f("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[f("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1)),ze=E(()=>f("span",{style:{top:"-0.75rem","font-size":"20px","font-weight":"bold","margin-right":"0.5rem"},for:"representives"}," المرافقين: ",-1)),Je={key:0,style:{"font-style":"italic",color:"#999","margin-top":"0.5rem"}},Fe={key:1},$e=E(()=>f("div",{class:"confirmation-content"},[f("i",{class:"pi pi-exclamation-triangle mr-3",style:{"font-size":"2rem",color:"red"}}),f("span",null,[L("ادخل تاريخ البداية والنهاية لإنهاء الزيارة "),f("b"),L(" ؟")])],-1)),Ze=R({__name:"VisitDetails",props:{visit:{}},setup(T){const M=U(),b=T,a=x(!0),n=x(!1);let i=x(!1);x([]),N(async()=>{}),I({get:()=>D(b.visit.startTime),set:o=>{b.visit.startTime=o}}),I({get:()=>D(b.visit.endTime),set:o=>{b.visit.endTime=o}});function c(){i.value=!0}return(o,u)=>{const s=_("Button"),r=_("Skeleton"),l=_("InputText"),t=_("Dropdown"),h=_("Textarea"),v=_("Column"),e=_("DataTable"),d=_("Card"),m=_("Dialog"),g=J("tooltip");return w(),S("div",ee,[y(d,null,{title:C(()=>[L(" تفاصيل الزيارة "),F(y(s,{id:"printButton",icon:"fa-solid fa-print",text:"",rounded:"",type:"button",onclick:`printJS({ printable: 'printJS-form', type: 'html', header: 'إدارة مركز البيانات',\r
    documentTitle:'مركز البيانات',headerStyle: 'font-weight:500', ignoreElements:['backButton', 'printButton'], maxWidth:'1000', targetStyles: ['*'],\r
   })`},null,512),[[g,{value:"طباعة",fitContent:!0}]]),y(H,{id:"backButton",style:{float:"left"}})]),content:C(()=>[P(M).loading?(w(),S("div",te,[f("div",ne,[f("div",oe,[f("span",ie,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),f("div",le,[f("span",re,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),f("div",se,[f("span",de,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),f("div",ae,[f("span",ce,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),f("div",fe,[f("span",ue,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),f("div",pe,[y(r,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])])])):(w(),S("div",me,[y(s,{onClick:c,label:"إنهاء الزيارة"}),f("div",he,[f("div",ve,[f("span",ye,[be,y(l,{inputId:"startTime",value:o.visit.expectedStartTime?P(D)(o.visit.expectedStartTime):"الزيارة لم تبدأ",disabled:a.value},null,8,["value","disabled"])])]),f("div",ge,[f("span",Ee,[_e,y(l,{inputId:"stopDate",value:o.visit.expectedEndTime?P(D)(o.visit.expectedEndTime):"الزيارة غير منتهية",disabled:a.value},null,8,["value","disabled"])])])]),f("div",we,[f("div",je,[f("span",Se,[Te,y(l,{modelValue:o.visit.customer,"onUpdate:modelValue":u[0]||(u[0]=p=>o.visit.customer=p),optionLabel:"customerName",disabled:!0},null,8,["modelValue"])])]),f("div",Me,[f("span",Pe,[Oe,y(l,{modelValue:o.visit.service,"onUpdate:modelValue":u[1]||(u[1]=p=>o.visit.service=p),optionLabel:"customerName",disabled:!0},null,8,["modelValue"])])]),f("div",Ce,[f("span",xe,[De,y(l,{id:"companionName",value:o.visit.totalTime?P(z)(o.visit.totalTime):"الزيارة غير منتهة",readonly:!0,disabled:!0},null,8,["value"])])]),f("div",Ie,[f("span",Le,[Ae,y(l,{id:"visitPrice",value:o.visit.visitPrice+" دينار",readonly:!0,disabled:!0},null,8,["value"])])]),f("div",Be,[f("span",Ue,[Re,y(t,{id:"visitType",modelValue:o.visit.visitType,"onUpdate:modelValue":u[2]||(u[2]=p=>o.visit.visitType=p),options:P(M).visitReasons,optionValue:"id",optionLabel:"name",disabled:!0},null,8,["modelValue","options"])])]),f("div",Ne,[f("span",We,[Ve,y(h,{id:"notes",modelValue:o.visit.notes,"onUpdate:modelValue":u[3]||(u[3]=p=>o.visit.notes=p),disabled:!0},null,8,["modelValue"])])])]),He,y(e,{dataKey:"identityNo",ref:"dt",value:o.visit.representatives},{empty:C(()=>[Ke]),default:C(()=>[y(v,{field:"firstName",header:"الاسم الاول "}),y(v,{field:"lastName",header:"الاسم التاني "}),y(v,{field:"identityType",header:"نوع الاثبات"}),y(v,{field:"identityNo",header:"رقم الاثبات"})]),_:1},8,["value"]),ze,b.visit.companions.length==0?(w(),S("div",Je," لايوجد مرافقين في هذه الزيارة ")):(w(),S("div",Fe,[y(K,{"comp-list":o.visit.companions,editable:a.value},null,8,["comp-list","editable"])]))]))]),_:1}),y(m,{visible:P(i),"onUpdate:visible":u[5]||(u[5]=p=>A(i)?i.value=p:i=p),style:{width:"450px"},header:"تأكيد",modal:!0},{footer:C(()=>[y(s,{label:"نعم",icon:"pi pi-check",text:"",loading:n.value},null,8,["loading"]),y(s,{label:"لا",icon:"pi pi-times",text:"",onClick:u[4]||(u[4]=p=>A(i)?i.value=!1:i=!1)})]),default:C(()=>[$e]),_:1},8,["visible"])])}}});const Ge=G(Ze,[["__scopeId","data-v-d9930bed"]]),Xe={key:0,class:"border-round border-1 surface-border p-4 surface-card"},Qe={class:"grid p-fluid"},Ye={class:"field col-12 md:col-6 lg:col-4"},ke={class:"p-float-label"},qe={class:"flex justify-content-between mt-3"},et={key:1},rt=R({__name:"VisitDetailsView",setup(T){const M=U(),b=X(),a=I(()=>b&&b.params&&b.params.id?b.params.id:null),n=x();return N(async()=>{M.loading=!0,V.getById(a.value).then(i=>{n.value=i.data.content}).catch(i=>{console.log(i)}).finally(()=>{M.loading=!1})}),(i,c)=>{const o=_("Skeleton");return w(),S(B,null,[P(M).loading?(w(),S("div",Xe,[f("div",Qe,[(w(),S(B,null,Q(9,u=>f("div",Ye,[f("span",ke,[y(o,{width:"100%",height:"2rem"})])])),64))]),y(o,{width:"100%",height:"100px"}),f("div",qe,[y(o,{width:"100%",height:"2rem"})])])):Y("",!0),n.value?(w(),k(Ge,{key:2,visit:n.value},null,8,["visit"])):(w(),S("div",et))],64)}}});export{rt as default};