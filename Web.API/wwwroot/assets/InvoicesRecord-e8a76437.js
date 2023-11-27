import{d as $,g as a,h as O,r as s,i as E,c as p,b as t,w as n,j as k,F as K,L as U,o as f,k as w,a as i,t as v,y as W,S as G,q as H,M as J}from"./index-8a7c54d3.js";import{_ as Q}from"./AddButton.vue_vue_type_script_setup_true_lang-1faad7b7.js";import{i as X}from"./invoice-a6313178.js";import{_ as Z}from"./DeleteButton.vue_vue_type_script_setup_true_lang-33d37ec2.js";import{c as ee}from"./customers-69672a1b.js";const te={key:0},oe={class:"p-paginator-pages"},ae=["onSubmit"],ne={class:"grid p-fluid mt-1"},ie={class:"field col-12 md:col-6 lg:col-4"},le={class:"table-header flex flex-column md:flex-row justiify-content-between"},se={class:"p-input-icon-left p-float-label"},ce=i("i",{class:"fa-solid fa-magnifying-glass"},null,-1),re=i("label",{for:"search",style:{"font-weight":"lighter"}}," اسم العميل ",-1),ue={class:"field col-12 md:col-6 lg:col-4"},de={class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},me={key:0,style:{"font-size":"18px","font-weight":"bold",color:"#888"}},pe={key:1,style:{"font-size":"18px","font-weight":"bold",color:"#888"}},fe={key:0},we=$({__name:"InvoicesRecord",setup(ve){const r=a(!1),u=a(),C=a([]),P=a(),d=a(1),h=a(1),T=a(10),l=a(0),y=a(),b=a([]),N=a(null),S=a({global:{value:null,matchMode:U.CONTAINS}});O(async()=>{_(),D()});async function D(){await ee.get(1,10,"").then(function(e){b.value=e.data.content}).catch(function(e){console.log(e)}).finally(()=>{r.value=!1})}const L=e=>{setTimeout(()=>{e.query.trim().length?y.value=b.value.filter(m=>m.name.toLowerCase().startsWith(e.query.toLowerCase())):y.value=[...b.value]},250)};function _(){r.value=!0,u.value!=null&&X.get(h.value,T.value,u.value.id).then(function(e){C.value=e.data.content,P.value=e.data.content,d.value=e.data.totalPages,l.value=e.data.currentPage}).catch(function(e){console.log(e)}).finally(function(){r.value=!1})}const V=e=>e?"مدفوعه":"غير مدفوعه";function A(e){return J(e).format("YYYY/MM/DD | hh:mm A")}const M=()=>{l.value<d.value&&(l.value+=1,h.value+=1,r.value=!0,_())},R=()=>{l.value>1&&(l.value-=1,h.value-=1,r.value=!0,_())},x=async()=>{u.value&&_()};return(e,m)=>{const z=s("RouterView"),g=s("Button"),F=s("AutoComplete"),c=s("Column"),B=s("RouterLink"),I=s("Toast"),Y=s("DataTable"),j=s("Card"),q=E("tooltip");return f(),p(K,null,[t(z),e.$route.path==="/invoices"?(f(),p("div",te,[t(j,null,{title:n(()=>[w(" سجل الفواتير "),t(Q,{"name-button":"إنشاء فاتورة","rout-name":"/invoices/addInvoice"})]),content:n(()=>[t(Y,{value:C.value,dataKey:"id",globalFilterFields:["customerName","visitReason"],paginator:!0,rows:10,filters:S.value,pageLinkSize:d.value,currentPage:l.value-1,paginatorTemplate:"  ",rowsPerPageOptions:[5,10,25],currentPageReportTemplate:"  عرض {first} الى {last} من {totalRecords} عميل",responsiveLayout:"scroll"},{paginatorstart:n(()=>[t(g,{icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:l.value===1,onClick:R},null,8,["disabled"]),i("span",oe," الصفحة "+v(l.value)+" من "+v(d.value),1)]),paginatorend:n(()=>[t(g,{icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:l.value===d.value,onClick:M},null,8,["disabled"])]),header:n(()=>[i("form",{onSubmit:W(x,["prevent"])},[i("div",ne,[i("div",ie,[i("div",le,[i("span",se,[ce,t(F,{modelValue:u.value,"onUpdate:modelValue":m[0]||(m[0]=o=>u.value=o),optionLabel:"name",suggestions:y.value,onComplete:L},null,8,["modelValue","suggestions"]),re])])]),i("div",ue,[t(g,{label:"بحث",onClick:x})])])],40,ae)]),empty:n(()=>[i("div",de,[N.value==0?(f(),p("p",me," اسم العميل مطلوب ")):(f(),p("p",pe," لايوجد بيانات "))])]),default:n(()=>[t(c,{field:"customerName",header:"اسم العميل",style:{"min-width":"8rem"}}),t(c,{field:"date",header:"التاريخ",style:{"min-width":"12rem"}},{body:n(o=>[w(v(A(o.data.date)),1)]),_:1}),t(c,{field:"description",header:"وصف",style:{"min-width":"8rem"}}),t(c,{field:"totalAmount",header:"السعر",style:{"min-width":"8rem",direction:"revert"}},{body:n(o=>[w(v(o.data.totalAmount)+" د.ل ",1)]),_:1}),t(c,{field:"isPaid",header:"الحاله",style:{"min-width":"8rem"}},{body:n(o=>[i("span",{style:G({color:o.data.isPaid?"green":"red"})},v(V(o.data.isPaid)),5)]),_:1}),t(c,{style:{"min-width":"8rem"}},{body:n(o=>[o.data.status!==5?(f(),p("span",fe,[t(Z,{name:o.data.id,id:o.data.id,type:"Invoices"},null,8,["name","id"])])):k("",!0),t(B,{to:"/invoices/invoicesDetails/"+o.data.id,style:{"text-decoration":"none"}},{default:n(()=>[H(t(g,{icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:""},null,512),[[q,{value:"التفاصيل",fitContent:!0}]])]),_:2},1032,["to"])]),_:1}),t(I,{position:"bottom-left"})]),_:1},8,["value","filters","pageLinkSize","currentPage"])]),_:1})])):k("",!0)],64)}}});export{we as default};