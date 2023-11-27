import{d as se,u as ae,g as n,h as ne,G as R,r as l,i as ie,c as S,b as s,w as i,j as le,F as Y,M as re,o as y,k as g,a,m as ce,n as de,t as _,l as m,N as A,q as ue,W as me}from"./index-5abe41cf.js";import{_ as pe}from"./AddButton.vue_vue_type_script_setup_true_lang-70d99390.js";import{u as fe,v as B}from"./visits-20400e3d.js";import{f as ve}from"./formatTime-36425ba4.js";import{_ as ge}from"./DeleteButton.vue_vue_type_script_setup_true_lang-91ed38cd.js";import{c as _e}from"./customers-e1aa762f.js";const he={key:0},ye={key:0,class:"border-round border-1 surface-border p-4 surface-card"},be={class:"grid p-fluid"},we={class:"field col-12 md:col-6 lg:col-4"},xe={class:"p-float-label"},Se={class:"flex justify-content-between mt-3"},Ce={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},Te={class:"grid p-fluid mt-1"},Me={class:"field col-12 md:col-6 lg:col-4"},ke={class:"table-header flex flex-column md:flex-row justiify-content-between"},Ve={class:"p-input-icon-left p-float-label"},Ne=a("i",{class:"fa-solid fa-magnifying-glass"},null,-1),Re=a("label",{for:"customerName"},"العملاء",-1),Le={class:"field col-12 md:col-6 lg:col-4"},De={class:"p-float-label"},ze=a("label",{for:"customerName"},"اشتراكات",-1),Pe={class:"field col-12 md:col-6 lg:col-4"},Ye=a("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[a("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1),He=se({__name:"VisitsRecord",setup(Ae){const L=ae(),p=n(!1),C=n(),D=n(),F=n(""),z=n();let c="";const T=n();let f="";const b=fe(),h=n(1),w=n(1),j=n(10),r=n(0),M=n({global:{value:null,matchMode:re.CONTAINS}}),k=n();ne(async()=>{v(c,f),U()}),R(T,async t=>{f=t[0].id});async function v(t,o){try{const d=await B.get(w.value,j.value,t,o);D.value=d.data.content,h.value=d.data.totalPages,r.value=d.data.currentPage}catch(d){console.log(d)}finally{b.loading=!1}}const I=t=>{setTimeout(()=>{t.query.trim().length?k.value=C.value.filter(o=>o.name.toLowerCase().startsWith(t.query.toLowerCase())):k.value=[...C.value]},250)};R(M,t=>{r.value=1,w.value=1,v(c,f)});const $=()=>{r.value<h.value&&(r.value+=1,w.value+=1,p.value=!0,v(c,f))},E=()=>{r.value>1&&(r.value-=1,w.value-=1,p.value=!0,v(c,f))};async function H(t){try{const o=await B.remove(t);L.add({severity:"success",summary:"رسالة نجاح",detail:`${o.data.msg}`,life:3e3}),v(c,f)}catch(o){L.add({severity:"error",summary:"رسالة تحذير",detail:o.response.data.msg,life:3e3})}}async function U(){await _e.get(1,50,F.value).then(function(t){C.value=t.data.content}).catch(function(t){console.log(t)}).finally(()=>{p.value=!1})}const q=t=>{me.get(1,50,t).then(function(o){z.value=o.data.content}).catch(function(o){console.log(o)})},V=n();R(V,async t=>{if(c=t.id,t&&c!==void 0)try{p.value=!0,await q(c),p.value=!1}catch(o){console.error("Error fetching representatives:",o)}});const W=n([{value:"Not Started",label:"لم تبدأ"},{value:"In Progress",label:"بدأت"},{value:"Completed",label:" منتهية"}]),G=t=>{if(t=="not Started")return"لم تبدأ";if(t=="In Progress")return"بدأت";if(t=="Completed")return" منتهية"},K=t=>{const o=W.value.find(d=>d.value===t);return o?o.label:""},O=t=>{switch(G(t)){case"بدأت":return"success";case"لم تبدأ":return"warning";case" منتهية":return"danger";default:return""}};return(t,o)=>{const d=l("RouterView"),N=l("Skeleton"),x=l("Button"),J=l("AutoComplete"),Q=l("MultiSelect"),u=l("Column"),P=l("Tag"),X=l("RouterLink"),Z=l("Toast"),ee=l("DataTable"),te=l("Card"),oe=ie("tooltip");return y(),S(Y,null,[s(d),t.$route.path==="/VisitsRecords"?(y(),S("div",he,[s(te,null,{title:i(()=>[g(" سجل الزيارات "),s(pe,{"name-button":"إنشاء زياره","rout-name":"/visitsRecords/createVisit"})]),content:i(()=>[p.value?(y(),S("div",ye,[a("div",be,[(y(),S(Y,null,ce(9,e=>a("div",we,[a("span",xe,[s(N,{width:"100%",height:"2rem"})])])),64))]),s(N,{width:"100%",height:"100px"}),a("div",Se,[s(N,{width:"100%",height:"2rem"})])])):(y(),de(ee,{key:1,value:D.value,dataKey:"id",ref:"dt",globalFilterFields:["customerName","visitReason"],paginator:!0,rows:10,filters:M.value,"onUpdate:filters":o[3]||(o[3]=e=>M.value=e),pageLinkSize:h.value,currentPage:r.value-1,paginatorTemplate:"  "},{paginatorstart:i(()=>[a("span",Ce,[s(x,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:r.value===1,onClick:E},null,8,["disabled"]),g(" الصفحة "+_(r.value)+" من "+_(h.value)+" ",1),s(x,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:r.value===h.value,onClick:$},null,8,["disabled"])])]),header:i(()=>[a("div",Te,[a("div",Me,[a("div",ke,[a("span",Ve,[Ne,s(J,{modelValue:V.value,"onUpdate:modelValue":o[0]||(o[0]=e=>V.value=e),optionLabel:"name",suggestions:k.value,onComplete:I},null,8,["modelValue","suggestions"]),Re])])]),a("div",Le,[a("span",De,[s(Q,{modelValue:T.value,"onUpdate:modelValue":o[1]||(o[1]=e=>T.value=e),options:z.value,optionLabel:"serviceName",emptyMessage:"هاذا العميل ليس لديه اشتراكات",placeholder:" اختر اشتراك",selectionLimit:1,loading:p.value},null,8,["modelValue","options","loading"]),ze])]),a("div",Pe,[s(x,{label:"بحث",onClick:o[2]||(o[2]=e=>v(m(c),m(f)))})])])]),empty:i(()=>[Ye]),default:i(()=>[s(u,{field:"customerName",header:"اسم العميل ",style:{"min-width":"8rem"},class:"font-bold",frozen:""}),s(u,{field:"visitType",header:"سبب الزياره",style:{"min-width":"8rem"},frozen:""},{body:i(({data:e})=>[s(P,{severity:"info",value:m(b).visitReasons&&m(b).visitReasons[e.visitType-1]?m(b).visitReasons[e.visitType-1].name:""},null,8,["value"])]),_:1}),s(u,{field:"totalMinutes",header:"مدة الزيارة",style:{"min-width":"8rem"}},{body:i(({data:e})=>[g(_(e!=null&&e.totalMinutes?m(ve)(e.totalMinutes):"- "),1)]),_:1}),s(u,{field:"price",header:"السعر",style:{"min-width":"8rem",direction:"revert"}},{body:i(e=>[g(_(e.data.price)+" د.ل ",1)]),_:1}),s(u,{field:"visitStatus",header:"الحاله",style:{"min-width":"1rem"}},{body:i(({data:e})=>[s(P,{value:K(e.visitStatus),severity:O(e.visitStatus)},null,8,["value","severity"])]),_:1}),s(u,{field:"expectedStartTime",header:" التاريخ بداية الزياره",style:{"min-width":"8rem"},class:"font-bold",frozen:""},{body:i(({data:e})=>[g(_(m(A)(e.expectedStartTime).format("YYYY-MM-DD HH:MM")),1)]),_:1}),s(u,{field:"expectedEndTime",header:"تاريخ انتهاء الزياره",style:{"min-width":"8rem"},class:"font-bold",frozen:""},{body:i(({data:e})=>[g(_(m(A)(e.expectedEndTime).format("YYYY-MM-DD HH:MM")),1)]),_:1}),s(u,{style:{"min-width":"11rem","text-align-last":"start"}},{body:i(e=>[s(ge,{name:e.data.customerName,id:e.data.id,onSubmit:()=>H(e.data.id),type:"User"},null,8,["name","id","onSubmit"]),s(X,{to:"/visitsRecords/visitDetails/"+e.data.id,style:{"text-decoration":"none"}},{default:i(()=>[ue(s(x,{icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:""},null,512),[[oe,{value:"التفاصيل",fitContent:!0}]])]),_:2},1032,["to"])]),_:1}),s(Z,{position:"bottom-left"})]),_:1},8,["value","filters","pageLinkSize","currentPage"]))]),_:1})])):le("",!0)],64)}}});export{He as default};
