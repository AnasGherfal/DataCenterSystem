import{d as $,g as a,h as O,r as s,i as E,c as f,b as t,w as n,j as k,F as K,M as U,o as p,k as h,a as i,t as v,z as W,V as G,q as H,N as J}from"./index-e755b632.js";import{_ as Q}from"./AddButton.vue_vue_type_script_setup_true_lang-c676c0bf.js";import{i as X}from"./invoice-75bcb8aa.js";import{_ as Z}from"./DeleteButton.vue_vue_type_script_setup_true_lang-e1582fea.js";import{c as ee}from"./customers-c0fffc40.js";const te={key:0},oe={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},ae=["onSubmit"],ne={class:"grid p-fluid mt-1"},ie={class:"field col-12 md:col-6 lg:col-4"},le={class:"table-header flex flex-column md:flex-row justiify-content-between"},se={class:"p-input-icon-left p-float-label"},re=i("i",{class:"fa-solid fa-magnifying-glass"},null,-1),ce=i("label",{for:"search",style:{"font-weight":"lighter"}}," اسم العميل ",-1),ue={class:"field col-12 md:col-6 lg:col-4"},de={class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},me={key:0,style:{"font-size":"18px","font-weight":"bold",color:"#888"}},fe={key:1,style:{"font-size":"18px","font-weight":"bold",color:"#888"}},pe={key:0},we=$({__name:"InvoicesRecord",setup(ve){const c=a(!1),u=a(),x=a([]),P=a(),d=a(1),y=a(1),T=a(10),l=a(0),b=a(),w=a([]),N=a(null),V=a({global:{value:null,matchMode:U.CONTAINS}});O(async()=>{g(),D()});async function D(){await ee.get(1,10,"").then(function(e){w.value=e.data.content}).catch(function(e){console.log(e)}).finally(()=>{c.value=!1})}const S=e=>{setTimeout(()=>{e.query.trim().length?b.value=w.value.filter(m=>m.name.toLowerCase().startsWith(e.query.toLowerCase())):b.value=[...w.value]},250)};function g(){c.value=!0,u.value!=null&&X.get(y.value,T.value,u.value.id).then(function(e){x.value=e.data.content,P.value=e.data.content,d.value=e.data.totalPages,l.value=e.data.currentPage}).catch(function(e){console.log(e)}).finally(function(){c.value=!1})}const A=e=>e?"مدفوعه":"غير مدفوعه";function L(e){return J(e).format("YYYY/MM/DD | hh:mm A")}const M=()=>{l.value<d.value&&(l.value+=1,y.value+=1,c.value=!0,g())},R=()=>{l.value>1&&(l.value-=1,y.value-=1,c.value=!0,g())},C=async()=>{u.value&&g()};return(e,m)=>{const z=s("RouterView"),_=s("Button"),F=s("AutoComplete"),r=s("Column"),B=s("RouterLink"),I=s("Toast"),j=s("DataTable"),Y=s("Card"),q=E("tooltip");return p(),f(K,null,[t(z),e.$route.path==="/invoices"?(p(),f("div",te,[t(Y,null,{title:n(()=>[h(" سجل الفواتير "),t(Q,{"name-button":"إنشاء فاتورة","rout-name":"/invoices/addInvoice"})]),content:n(()=>[t(j,{value:x.value,dataKey:"id",globalFilterFields:["customerName","visitReason"],paginator:!0,rows:10,filters:V.value,pageLinkSize:d.value,currentPage:l.value-1,paginatorTemplate:"  ",rowsPerPageOptions:[5,10,25],currentPageReportTemplate:"  عرض {first} الى {last} من {totalRecords} عميل",responsiveLayout:"scroll"},{paginatorstart:n(()=>[i("span",oe,[t(_,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:l.value===1,onClick:R},null,8,["disabled"]),h(" الصفحة "+v(l.value)+" من "+v(d.value)+" ",1),t(_,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:l.value===d.value,onClick:M},null,8,["disabled"])])]),header:n(()=>[i("form",{onSubmit:W(C,["prevent"])},[i("div",ne,[i("div",ie,[i("div",le,[i("span",se,[re,t(F,{modelValue:u.value,"onUpdate:modelValue":m[0]||(m[0]=o=>u.value=o),optionLabel:"name",suggestions:b.value,onComplete:S},null,8,["modelValue","suggestions"]),ce])])]),i("div",ue,[t(_,{label:"بحث",onClick:C})])])],40,ae)]),empty:n(()=>[i("div",de,[N.value==0?(p(),f("p",me," اسم العميل مطلوب ")):(p(),f("p",fe," لايوجد بيانات "))])]),default:n(()=>[t(r,{field:"customerName",header:"اسم العميل",style:{"min-width":"8rem"}}),t(r,{field:"date",header:"التاريخ",style:{"min-width":"12rem"}},{body:n(o=>[h(v(L(o.data.date)),1)]),_:1}),t(r,{field:"description",header:"وصف",style:{"min-width":"8rem"}}),t(r,{field:"totalAmount",header:"السعر",style:{"min-width":"8rem",direction:"revert"}},{body:n(o=>[h(v(o.data.totalAmount)+" د.ل ",1)]),_:1}),t(r,{field:"isPaid",header:"الحاله",style:{"min-width":"8rem"}},{body:n(o=>[i("span",{style:G({color:o.data.isPaid?"green":"red"})},v(A(o.data.isPaid)),5)]),_:1}),t(r,{style:{"min-width":"8rem"}},{body:n(o=>[o.data.status!==5?(p(),f("span",pe,[t(Z,{name:o.data.id,id:o.data.id,type:"Invoices"},null,8,["name","id"])])):k("",!0),t(B,{to:"/invoices/invoicesDetails/"+o.data.id,style:{"text-decoration":"none"}},{default:n(()=>[H(t(_,{icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:""},null,512),[[q,{value:"التفاصيل",fitContent:!0}]])]),_:2},1032,["to"])]),_:1}),t(I,{position:"bottom-left"})]),_:1},8,["value","filters","pageLinkSize","currentPage"])]),_:1})])):k("",!0)],64)}}});export{we as default};
