import{d as Y,u as Z,g as n,s as ee,N as te,h as ae,r as l,i as se,c as f,b as s,w as i,j as T,F as C,L as R,o as c,k as L,a as o,m as N,n as V,t as B,O as oe,q as ne}from"./index-faac8ff5.js";import{u as le}from"./customers-7057e3d7.js";import{_ as ie}from"./AddButton.vue_vue_type_script_setup_true_lang-ecff473e.js";import{_ as re}from"./LockButton.vue_vue_type_style_index_0_lang-f4e32cf2.js";import{c as h}from"./customers-ff0d04eb.js";import{_ as de}from"./DeleteButton.vue_vue_type_script_setup_true_lang-b9b4737b.js";const ue={key:0},ce={key:0,class:"border-round border-1 surface-border p-4 surface-card"},me={class:"grid p-fluid"},fe={class:"field col-12 md:col-6 lg:col-4"},pe={class:"p-float-label"},_e={class:"flex justify-content-between mt-3"},ge={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},ve={class:"grid p-fluid mt-1"},he={class:"field col-12 md:col-6 lg:col-4"},ye={class:"table-header flex flex-column md:flex-row justiify-content-between"},be={class:"p-input-icon-left p-float-label"},we=o("i",{class:"fa-solid fa-magnifying-glass"},null,-1),Ce=o("label",{for:"search",style:{"font-weight":"lighter"}}," البحث ",-1),ke=o("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[o("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1),xe={key:0},Me=Y({__name:"CustomersRecord",setup(Se){const g=Z(),k=n(),m=n(!0),p=n(1),y=n(1),M=n(5),r=n(0),u=n("");le();const x=ee({global:{value:"",matchMode:R.CONTAINS},status:{value:"",matchMode:R.EQUALS}}),F=n([{field:"email",header:"البريد الإكتروني"},{field:"primaryPhone",header:"رقم الهاتف"},{field:"address",header:"العنوان"}]),P=n(F.value),j=n([{value:1,label:"نشط"},{value:2,label:"مقيد"}]),z=e=>{switch(D(e)){case"نشط":return"success";case"مقيد":return"danger"}},D=e=>{if(e=="1")return"نشط";if(e=="2")return"مقيد"},I=e=>{const a=j.value.find(_=>_.value===e);return a?a.label:""};te((e,a,_)=>{d(),_()}),ae(async()=>{d()});async function K(e){u.value=e,await d()}async function d(){console.log("getCustomers method called from child"),(u.value===void 0||u.value===null)&&(u.value=""),await h.get(y.value,M.value,u.value).then(function(e){k.value=e.data.content,p.value=e.data.totalPages,r.value=e.data.currentPage}).catch(function(e){console.log(e)}).finally(()=>{m.value=!1})}const U=async e=>{try{let a;e.status===1?(a=await h.block(e.id),e.status=2):e.status===2&&(a=await h.unblock(e.id),e.status=1),g.add({severity:"success",summary:"نجحة العملية",detail:a==null?void 0:a.data.msg,life:3e3})}catch(a){g.add({severity:"error",summary:"حدث خطأ",detail:a.response.data.msg||"حدث خطأ",life:3e3})}finally{d()}},$=e=>{m.value=!0,h.remove(e).then(a=>{g.add({severity:"success",summary:"تم الحذف",detail:a.data.msg,life:3e3}),d()}).catch(a=>{g.add({severity:"error",summary:"رسالة خطأ",detail:a.response.data.msg,life:3e3})}).finally(()=>{m.value=!1})},A=()=>{r.value<p.value&&(r.value+=1,y.value+=1,m.value=!0,d())},E=()=>{r.value>1&&(r.value-=1,y.value-=1,m.value=!0,d())},O=e=>{e.key==="Enter"&&K(u.value)};return(e,a)=>{const _=l("RouterView"),b=l("Skeleton"),w=l("Button"),G=l("InputText"),v=l("Column"),q=l("Tag"),Q=l("RouterLink"),H=l("Toast"),J=l("DataTable"),W=l("Card"),X=se("tooltip");return c(),f(C,null,[s(_),e.$route.path==="/customersRecord"?(c(),f("div",ue,[s(W,null,{title:i(()=>[L(" سجل العملاء "),s(ie,{onGetCustomers:d,"name-button":"اضافة عميل","rout-name":"/customersRecord/addCustomer"})]),content:i(()=>[o("div",null,[m.value?(c(),f("div",ce,[o("div",me,[(c(),f(C,null,N(9,t=>o("div",fe,[o("span",pe,[s(b,{width:"100%",height:"2rem"})])])),64))]),s(b,{width:"100%",height:"100px"}),o("div",_e,[s(b,{width:"100%",height:"2rem"})])])):(c(),V(J,{key:1,ref:"dt",value:k.value,dataKey:"id",paginator:!0,rows:10,filters:x,"onUpdate:filters":a[1]||(a[1]=t=>x=t),globalFilterFields:["name","status"],rowsPerPageOptions:[5,10,25],currentPageReportTemplate:"  عرض {first} الى {last} من {totalRecords} عميل",pageLinkSize:p.value,currentPage:r.value-1,paginatorTemplate:"  "},{paginatorstart:i(()=>[o("span",ge,[s(w,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:r.value===1,onClick:E},null,8,["disabled"]),L(" الصفحة "+B(r.value)+" من "+B(p.value)+" ",1),s(w,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:r.value===p.value,onClick:A},null,8,["disabled"])])]),header:i(()=>[o("div",ve,[o("div",he,[o("div",ye,[o("span",be,[we,s(G,{ref:"searchInput",modelValue:u.value,"onUpdate:modelValue":a[0]||(a[0]=t=>u.value=t),placeholder:"البحث",onKeydown:oe(O,["enter"])},null,8,["modelValue","onKeydown"]),Ce])])])])]),empty:i(()=>[ke]),default:i(()=>[s(v,{field:"name",header:"الإسم",style:{"min-width":"6rem"},class:"font-bold",frozen:""}),s(v,{field:"status",header:"  الحاله ",filterField:"status",style:{width:"6rem"},showFilterMenu:!1,filterMenuStyle:{width:"4rem"}},{body:i(({data:t})=>[s(q,{value:I(t.status),severity:z(t.status)},null,8,["value","severity"])]),filter:i(({filterModel:t,filterCallback:S})=>[]),_:1}),(c(!0),f(C,null,N(P.value,(t,S)=>(c(),V(v,{field:t.field,header:t.header,key:t.field+"_"+S,style:{"min-width":"6rem"}},null,8,["field","header"]))),128)),s(v,{style:{"min-width":"11rem"},header:"  الاجراءات "},{body:i(t=>[t.data.status!==2?(c(),f("span",xe,[s(de,{name:t.data.name,id:t.data.id,onSubmit:()=>$(t.data.id),type:"Customers"},null,8,["name","id","onSubmit"])])):T("",!0),s(Q,{to:"customersRecord/CustomerProfile/"+t.data.id},{default:i(()=>[ne(s(w,{icon:"fa-solid fa-user",severity:"info",text:"",rounded:"","aria-label":"Cancel"},null,512),[[X,{value:"البيانات الشخصية",fitContent:!0}]])]),_:2},1032,["to"]),s(re,{typeLock:"Customers",id:t.data.id,name:t.data.name,status:t.data.status,onGetdata:d,onSubmit:()=>U(t.data)},null,8,["id","name","status","onSubmit"])]),_:1}),s(H,{position:"bottom-left"})]),_:1},8,["value","filters","pageLinkSize","currentPage"]))])]),_:1})])):T("",!0)],64)}}});export{Me as default};
