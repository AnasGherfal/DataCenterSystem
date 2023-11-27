import{L as B,d as z,g as n,v as L,h as F,r as s,n as y,w as l,M as w,o as u,k as f,b as a,c as x,a as o,F as O,m as Y,t as v,N as j}from"./index-37af3c3b.js";const k=B(),R={get:async function(d,r){return await k.get("/Audits",{params:{PageNumber:d,PageSize:r}})},getById:async function(d){return await k.get(`/Audits/${d}`)}},V={key:0,class:"border-round border-1 surface-border p-4 surface-card"},E={class:"grid p-fluid"},I={class:"field col-12 md:col-6 lg:col-4"},H={class:"p-float-label"},K={class:"flex justify-content-between mt-3"},Q={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},U=o("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[o("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1),q=z({__name:"AuditsRecord",setup(d){const r=n(),i=n(),c=n(1),p=n(1),P=n(5),e=n(0);L({global:{value:"",matchMode:w.CONTAINS},status:{value:"",matchMode:w.EQUALS}});function C(t){return j(t).format("YYYY/MM/DD | hh:mm A")}F(async()=>{g()});function g(){R.get(p.value,P.value).then(function(t){console.log(t),r.value=t.data.content,c.value=t.data.totalPages,e.value=t.data.currentPage,i.value=!1}).catch(function(t){console.log(t)})}const N=()=>{e.value<c.value&&(e.value+=1,p.value+=1,i.value=!0,g())},M=()=>{e.value>1&&(e.value-=1,p.value-=1,i.value=!0,g())};return(t,S)=>{const T=s("Divider"),m=s("Skeleton"),_=s("Button"),h=s("Column"),D=s("DataTable"),A=s("card");return u(),y(A,null,{title:l(()=>[f(" سجل الحركات "),a(T)]),content:l(()=>[i.value?(u(),x("div",V,[o("div",E,[(u(),x(O,null,Y(9,b=>o("div",I,[o("span",H,[a(m,{width:"100%",height:"2rem"})])])),64))]),a(m,{width:"100%",height:"100px"}),o("div",K,[a(m,{width:"100%",height:"2rem"})])])):(u(),y(D,{key:1,ref:"dt",value:r.value,dataKey:"id",paginator:!0,rows:10,rowsPerPageOptions:[5,10,25],currentPageReportTemplate:"  عرض {first} الى {last} من {totalRecords} عميل",pageLinkSize:c.value,currentPage:e.value-1,paginatorTemplate:"  "},{paginatorstart:l(()=>[o("span",Q,[a(_,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:e.value===1,onClick:M},null,8,["disabled"]),f(" الصفحة "+v(e.value)+" من "+v(c.value)+" ",1),a(_,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:e.value===c.value,onClick:N},null,8,["disabled"])])]),empty:l(()=>[U]),default:l(()=>[a(h,{field:"type",header:"الحركة",style:{width:"2rem"},class:"font-bold"}),a(h,{field:"userName",header:"تمت بواسطة",style:{width:"2rem"},class:"font-bold"}),a(h,{field:"occuredOn",header:"  حدث في ",style:{width:"4rem"},showFilterMenu:!1},{body:l(b=>[f(v(C(b.data.occuredOn)),1)]),_:1})]),_:1},8,["value","pageLinkSize","currentPage"]))]),_:1})}}});export{q as default};
