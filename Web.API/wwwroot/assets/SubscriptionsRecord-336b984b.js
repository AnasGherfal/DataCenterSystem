import{d as Q,u as W,g as s,N as X,h as Y,V as w,E as Z,r as l,i as ee,c as p,b as a,w as o,j as M,F as C,L as te,o as r,k as S,a as u,m as z,n as N,t as h,l as U,U as ae,q as ne,M as R}from"./index-e13382f1.js";import{_ as oe}from"./AddButton.vue_vue_type_script_setup_true_lang-268854f3.js";import{_ as ie}from"./LockButton.vue_vue_type_style_index_0_lang-ae93ffaf.js";import{_ as se}from"./DeleteButton.vue_vue_type_script_setup_true_lang-55843619.js";import{u as le}from"./shared-75a1ab43.js";const re={key:0},de={key:0,class:"border-round border-1 surface-border p-4 surface-card"},ue={class:"grid p-fluid"},ce={class:"field col-12 md:col-6 lg:col-4"},me={class:"p-float-label"},pe={class:"flex justify-content-between mt-3"},fe={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},ge=u("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[u("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1),he={key:0},ke=Q({__name:"SubscriptionsRecord",setup(ve){const v=W(),y=s(!0),T=s([]),f=s(1),_=s(1),$=s(10),i=s(0),V=le(),k=s({global:{value:null,matchMode:te.CONTAINS}}),j=s([{field:"serviceName",header:"الباقه"}]),O=s(j.value);X((t,n,b)=>{d(),b()}),Y(()=>{d()});function d(){w.getPages(_.value,$.value).then(function(t){T.value=t.data.content,f.value=t.data.totalPages,i.value=t.data.currentPage}).catch(function(t){console.log(t)}).finally(()=>{y.value=!1})}const L=t=>R(t).format("yy/M/D  hh:mm a"),F=t=>{const n=R(),m=R(t).diff(n,"days");return m<=0?{label:"منتهي",color:"red"}:m<=30?{label:"قريباً من الانتهاء",color:"orange"}:{label:"صالح",color:"green"}};Z(k,t=>{i.value=1,_.value=1,d()});const A=()=>{i.value<f.value&&(i.value+=1,_.value+=1,y.value=!0,d())},E=()=>{i.value>1&&(i.value-=1,_.value-=1,y.value=!0,d())},P=async t=>{try{let n;t.status==1&&(n=await w.block(t.id)),t.status==2&&(n=await w.unblock(t.id)),v.add({severity:"success",summary:"نجحة العملية",detail:n==null?void 0:n.data.msg,life:3e3}),d()}catch(n){v.add({severity:"error",summary:"حدث خطأ",detail:n.response.data.msg||"حدث خطأ",life:3e3})}},q=t=>{w.remove(t).then(n=>{v.add({severity:"success",summary:"تم الحذف",detail:n.data.msg,life:3e3}),d()}).catch(n=>{v.add({severity:"error",summary:"رسالة خطأ",detail:n.response.data.msg,life:3e3})})};return(t,n)=>{const b=l("RouterView"),m=l("Skeleton"),x=l("Button"),c=l("Column"),B=l("Tag"),G=l("Dropdown"),I=l("RouterLink"),K=l("DataTable"),H=l("Card"),J=ee("tooltip");return r(),p(C,null,[a(b),t.$route.path==="/subscriptionsRecord"?(r(),p("div",re,[a(H,null,{title:o(()=>[S(" سجل الاشتراكات "),a(oe,{"name-button":"إضافة اشتراك","rout-name":"/subscriptionsRecord/addSubsciptions"})]),content:o(()=>[y.value?(r(),p("div",de,[u("div",ue,[(r(),p(C,null,z(9,e=>u("div",ce,[u("span",me,[a(m,{width:"100%",height:"2rem"})])])),64))]),a(m,{width:"100%",height:"100px"}),u("div",pe,[a(m,{width:"100%",height:"2rem"})])])):(r(),N(K,{key:1,ref:"dt",value:T.value,dataKey:"id",paginator:!0,rows:10,filters:k.value,"onUpdate:filters":n[0]||(n[0]=e=>k.value=e),globalFilterFields:["serviceName","customerName"],rowsPerPageOptions:[5,10,25],pageLinkSize:f.value,currentPage:i.value-1,paginatorTemplate:"  "},{paginatorstart:o(()=>[u("span",fe,[a(x,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:i.value===1,onClick:E},null,8,["disabled"]),S(" الصفحة "+h(i.value)+" من "+h(f.value)+" ",1),a(x,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:i.value===f.value,onClick:A},null,8,["disabled"])])]),empty:o(()=>[ge]),default:o(()=>[a(c,{field:"customerName",header:"اسم العميل",style:{"min-width":"6rem"},class:"font-bold"}),a(c,{field:"status",header:"  الحاله ",filterField:"status",style:{width:"6rem"},showFilterMenu:!1,filterMenuStyle:{width:"12rem"}},{body:o(({data:e})=>[a(B,{value:U(V).getSelectedStatusLabel(e.status),severity:U(V).getSeverity(e.status)},null,8,["value","severity"])]),filter:o(({filterModel:e,filterCallback:D})=>[a(G,{modelValue:e.value,"onUpdate:modelValue":g=>e.value=g,onChange:g=>D(),options:["warning",""],placeholder:"Select One",class:"p-column-filter",style:{"min-width":"12rem"},showClear:!0},{option:o(g=>[a(B,{value:g.option,severity:g.option},null,8,["value","severity"])]),_:2},1032,["modelValue","onUpdate:modelValue","onChange"])]),_:1}),(r(!0),p(C,null,z(O.value,(e,D)=>(r(),N(c,{field:e.field,header:e.header,key:e.field+"_"+D,style:{"min-width":"3rem"}},null,8,["field","header"]))),128)),a(c,{field:"daysRemaining",header:"الصلاحية",style:{"min-width":"4rem"}},{body:o(({data:e})=>[u("span",{style:ae({color:F(e.endDate).color})},h(F(e.endDate).label),5)]),_:1}),a(c,{field:"startDate",header:"تاريخ بداية الاشتراك",dataType:"date",style:{"min-width":"11rem"}},{body:o(({data:e})=>[S(h(L(e.startDate)),1)]),_:1}),a(c,{field:"endDate",header:"تاريخ نهاية الاشتراك",dataType:"date",style:{"min-width":"11rem"}},{body:o(({data:e})=>[S(h(L(e.endDate)),1)]),_:1}),a(c,{style:{"min-width":"11rem"}},{body:o(e=>[e.data.status!==5?(r(),p("span",he,[a(se,{name:e.data.id,id:e.data.id,onSubmit:()=>q(e.data.id),type:"Subscription"},null,8,["name","id","onSubmit"])])):M("",!0),a(ie,{typeLock:"Subscriptions",id:e.data.id,name:e.data.id,status:e.data.status,onSubmit:()=>P(e.data),onGetdata:d},null,8,["id","name","status","onSubmit"]),(r(),N(I,{key:e.data.id,to:"/subscriptionsRecord/SubscriptionsDetaView/"+e.data.id,style:{"text-decoration":"none"}},{default:o(()=>[ne(a(x,{icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:""},null,512),[[J,{value:"التفاصيل",fitContent:!0}]])]),_:2},1032,["to"]))]),_:1})]),_:1},8,["value","filters","pageLinkSize","currentPage"]))]),_:1})])):M("",!0)],64)}}});export{ke as default};