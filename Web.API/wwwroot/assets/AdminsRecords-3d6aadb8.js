import{u as z,a as y}from"./admin-ba3a33ea.js";import{_ as M}from"./AddButton.vue_vue_type_script_setup_true_lang-f3c8c711.js";import{d as q,u as E,g as f,h as G,r,i as K,c as m,b as t,w as o,j as v,F as b,o as d,k,l as s,a as l,m as O,n as w,t as A,q as U}from"./index-e1df4f23.js";import{_ as H}from"./LockButton.vue_vue_type_style_index_0_lang-0edeee2a.js";import{_ as I}from"./DeleteButton.vue_vue_type_script_setup_true_lang-02aa35f2.js";const J={key:0},Q={key:0,class:"border-round border-1 surface-border p-4 surface-card"},W={class:"grid p-fluid"},X={class:"field col-12 md:col-6 lg:col-4"},Y={class:"p-float-label"},Z={class:"flex justify-content-between mt-3"},ee={class:"p-paginator-pages",style:{display:"flex","justify-content":"center","align-items":"center","margin-top":"1rem"}},te=l("div",{class:"no-data-message",style:{height:"100px",display:"flex","flex-direction":"column","align-items":"center","justify-content":"center",padding:"20px","background-color":"#f9f9f9","border-radius":"5px","box-shadow":"0 2px 4px rgba(0, 0, 0, 0.1)"}},[l("p",{style:{"font-size":"18px","font-weight":"bold",color:"#888"}}," لا يوجد بيانات ")],-1),ae={key:0},le=q({__name:"AdminsRecords",setup(ne){const e=z(),c=E();f();const u=f(!1);f(!1),G(()=>{e.getAdmins()}),f([{value:!0,label:"نشط"},{value:!1,label:"غير نشط"}]);const h=n=>{if(n==!0)return"نشط";if(n==!1)return"غير نشط"},x=n=>{switch(h(n)){case"نشط":return"success";case"غير نشط":return"danger"}},S=n=>h(n),P=()=>{e.currentPage<e.totalPages&&(e.currentPage+=1,e.pageNumber+=1,e.loading=!0,e.getAdmins())},R=()=>{e.currentPage>1&&(e.currentPage-=1,e.pageNumber-=1,e.loading=!0,e.getAdmins())},T=n=>{u.value=!0,y.remove(n).then(i=>{c.add({severity:"success",summary:"تم الحذف",detail:i.data.msg,life:3e3}),e.getAdmins()}).catch(i=>{c.add({severity:"error",summary:"رسالة خطأ",detail:i.response.data.msg,life:3e3})}).finally(()=>{u.value=!1})};function C(n){u.value=!0,y.block(n).then(i=>{c.add({severity:"success",summary:"رسالة تأكيد",detail:i.data.msg,life:3e3}),e.getAdmins()}).catch(i=>{c.add({severity:"error",summary:"رسالة خطأ",detail:i.response.data.msg,life:3e3})}).finally(()=>{u.value=!1})}function N(n){u.value=!0,y.unblock(n).then(i=>{c.add({severity:"success",summary:"رسالة تأكيد",detail:i.data.msg,life:3e3}),e.getAdmins()}).catch(i=>{c.add({severity:"error",summary:"رسالة خطأ",detail:i.response.data.msg,life:3e3})}).finally(()=>{u.value=!1})}return(n,i)=>{const B=r("RouterView"),D=r("Divider"),p=r("Skeleton"),_=r("Button"),g=r("Column"),L=r("Tag"),V=r("RouterLink"),j=r("DataTable"),F=r("card"),$=K("tooltip");return d(),m(b,null,[t(B),n.$route.path==="/AdminsRecord"?(d(),m("div",J,[t(F,null,{title:o(()=>[k(" سجل المستخدمين "),t(M,{"name-button":"اضافة مستخدم","rout-name":"/AdminsRecord/AddAdmin"}),t(D)]),content:o(()=>[s(e).loading?(d(),m("div",Q,[l("div",W,[(d(),m(b,null,O(9,a=>l("div",X,[l("span",Y,[t(p,{width:"100%",height:"2rem"})])])),64))]),t(p,{width:"100%",height:"100px"}),l("div",Z,[t(p,{width:"100%",height:"2rem"})])])):(d(),w(j,{key:1,ref:"dt",value:s(e).userData,dataKey:"id",paginator:!0,rows:5,paginatorTemplate:"",rowsPerPageOptions:[5,10,25],currentPageReportTemplate:"عرض {first} الى {last} من {totalRecords} عميل",responsiveLayout:"scroll",pageLinkSize:s(e).totalPages,currentPage:s(e).currentPage-1},{paginatorstart:o(()=>[l("span",ee,[t(_,{style:{"margin-left":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-right",class:"p-button-rounded p-button-primary p-paginator-element",disabled:s(e).currentPage===1,onClick:R},null,8,["disabled"]),k(" الصفحة "+A(s(e).currentPage)+" من "+A(s(e).totalPages)+" ",1),t(_,{style:{"margin-right":"1rem",height:"2rem",width:"2rem"},icon:"pi pi-angle-left",class:"p-button-rounded p-button-primary p-paginator-element",disabled:s(e).currentPage===s(e).totalPages,onClick:P},null,8,["disabled"])])]),empty:o(()=>[te]),default:o(()=>[t(g,{field:"displayName",header:"اسم المستخدم",style:{"min-width":"10rem"},class:"font-bold"}),t(g,{field:"isActive",header:"  الحاله ",filterField:"status",style:{width:"2rem"},showFilterMenu:!1,filterMenuStyle:{width:"4rem"}},{body:o(({data:a})=>[t(L,{value:S(a.isActive),severity:x(a.isActive)},null,8,["value","severity"])]),_:1}),t(g,{field:"email",header:"البريد الإلكتروني",style:{"min-width":"10rem"}}),t(g,{style:{"min-width":"13rem"}},{body:o(a=>[a.data.status!==5?(d(),m("span",ae,[t(I,{name:a.data.displayName,id:a.data.id,onSubmit:()=>T(a.data.id),type:"User"},null,8,["name","id","onSubmit"])])):v("",!0),(d(),w(V,{key:a.data.id,to:"/AdminsRecord/AdminsProfile/"+a.data.id,style:{"text-decoration":"none"}},{default:o(()=>[U(t(_,{icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:""},null,512),[[$,{value:"التفاصيل",fitContent:!0}]])]),_:2},1032,["to"])),t(H,{typeLock:"Admins",id:a.data.id,name:a.data.id,status:a.data.isActive,onGetdata:i[0]||(i[0]=()=>s(e).getAdmins()),onSubmit:()=>a.data.isActive?N(a.data.id):C(a.data.id)},null,8,["id","name","status","onSubmit"])]),_:1})]),_:1},8,["value","pageLinkSize","currentPage"]))]),_:1})])):v("",!0)],64)}}});export{le as default};
