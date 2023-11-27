import{d as S,g as y,s as I,v as M,u as T,x as j,r as _,c as m,b as t,w as v,o as u,a as e,j as B,z as N,A as q,B as G,D as O,h as F,E as z,i as H,n as R,l as k,F as x,G as J,H as D,q as K,m as Q,t as W,I as X}from"./index-8a7c54d3.js";import{u as Y,a as V}from"./admin-84180614.js";import{v as Z}from"./validations-45222b88.js";import{_ as ee}from"./BackButton.vue_vue_type_script_setup_true_lang-3994efeb.js";const se={class:"flex align-items-center justify-content-between"},te=e("span",null," البيانات الشخصية ",-1),ne={key:0,class:"mb-5"},le=e("div",{class:"warning-message"},[e("div",{class:"warning-message-icon"}),e("div",{class:"warning-message-text"}," هذا المستخدم مقفل لا يمكن التعديل عليه ")],-1),oe=[le],ae={key:1},ie={class:"grid p-fluid"},de={class:"field col-12 md:col-6 lg:col-4"},ce={class:"p-float-label"},ue={class:"field col-12 md:col-6 lg:col-4"},re={class:"p-float-label"},me={class:"field col-12 md:col-6 lg:col-4"},_e={class:"p-float-label"},pe={class:"field col-12 md:col-6 lg:col-4"},ve={class:"p-float-label"},fe={class:"grid p-fluid"},he={class:"field col-12 md:col-6 lg:col-4"},ge={class:""},be=e("label",{for:"displayName"},"اسم المستخدم",-1),ye={class:"field col-12 md:col-6 lg:col-4"},$e={class:""},ke=e("label",{for:"email"},"البريد الإلكتروني",-1),we=S({__name:"InfoAdmin",props:{admin:{}},setup(C){const d=C,p=y(!1),w=I({id:d.admin.id,displayName:d.admin.displayName,email:d.admin.email,permissions:d.admin.permissions,isActive:d.admin.isActive,createdOn:d.admin.createdOn}),o=M(()=>({displayName:{required:N.withMessage("الحقل مطلوب",q),validateText:N.withMessage(", حروف عربيه او انجليزيه فقط",Z),minLength:N.withMessage("يجب أن يحتوي على الأقل 3 أحرف",G(3))}}));return T(),j(o,w),(r,a)=>{const h=_("Divider"),f=_("Skeleton"),i=_("InputText"),$=_("Toast"),A=_("Card");return u(),m("div",null,[t(A,null,{title:v(()=>[e("div",se,[te,t(ee,{style:{}})]),t(h)]),content:v(()=>[r.admin.isActive==!1?(u(),m("div",ne,oe)):B("",!0),p.value?(u(),m("div",ae,[e("div",ie,[e("div",de,[e("span",ce,[t(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",ue,[e("span",re,[t(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",me,[e("span",_e,[t(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",pe,[e("span",ve,[t(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])])])])):B("",!0),e("div",null,[e("div",fe,[e("div",he,[e("span",ge,[be,t(i,{id:"displayName",nameid:"displayName",disabled:!0,modelValue:r.admin.displayName,"onUpdate:modelValue":a[0]||(a[0]=b=>r.admin.displayName=b)},null,8,["modelValue"])])]),e("div",ye,[e("span",$e,[ke,t(i,{id:"email",name:"email",modelValue:r.admin.email,"onUpdate:modelValue":a[1]||(a[1]=b=>r.admin.email=b),disabled:!0},null,8,["modelValue"])])])]),t($,{position:"bottom-right"})])]),_:1})])}}}),Ae=e("i",{class:"fa-solid fa-key ml-2"},null,-1),Ne=e("span",null,"الصلاحيات",-1),xe={class:"flex align-items-center justify-content-between"},Ve=e("h3",null,"الصلاحيات:",-1),Be={class:"table-responsive"},Ce={class:"table"},De=e("thead",null,[e("tr",null,[e("th",null,"اسم الصلاحية"),e("th",null,"لديه اذن")])],-1),Se={key:0},Ie=["disabled"],Me=e("i",{class:"fa-regular fa-circle-check",style:{color:"#2cbd0f"}},null,-1),Te=[Me],Ee={key:1},Pe=["disabled"],Le=e("i",{class:"fa-regular fa-circle-xmark",style:{color:"red"}},null,-1),Ue=[Le],je=e("td",null,null,-1),qe=e("i",{class:"fa-solid fa-bars ml-2"},null,-1),Ge=e("span",null,"سجل الحركات",-1),Oe={class:"flex items-center gap-3 mt-5"},Je=S({__name:"ProfileAdmin",setup(C){const d=O(),p=T(),w=Y(),o=y(!1),r=y(!1),a=y([]),h=y([]),f=M(()=>d&&d.params&&d.params.id?X(d.params.id):null),i=I({id:"",displayName:"",email:"",permissions:0,isActive:!0,createdOn:""});function $(){V.getById(f.value).then(function(s){i.id=s.data.content.id,i.displayName=s.data.content.displayName,i.email=s.data.content.email,i.isActive=s.data.content.isActive,i.permissions=s.data.content.permissions}).catch(function(s){console.log(s)})}F(async()=>{$(),A()});function A(){V.permissions().then(function(s){h.value=s.data.content})}const b=(s,l)=>{for(let n=0;n<s.length;n++){const g=s[n];if((l&g)===g)return!0}return!1};z(o,()=>{o.value&&(a.value=[],h.value.forEach(s=>{(i.permissions&s.id)===s.id&&a.value.push(s)}))});const E=()=>{var l;r.value=!0;let s=0;a.value.some(n=>n.name==="Super Admin")?s=((l=a.value.find(n=>n.name==="Super Admin"))==null?void 0:l.id)||0:s=a.value.reduce((n,g)=>n+g.id,0),V.edit(f.value,s).then(function(n){p.add({severity:"success",summary:"رسالة نجاح",detail:n.data.msg,life:3e3}),o.value=!1,$()}).catch(function(n){console.log(n),p.add({severity:"error",summary:"رسالة فشل",detail:n.response.data.msg||"هنالك مشكلة في الوصول",life:3e3})}).finally(()=>{r.value=!1})};return(s,l)=>{const n=_("Button"),g=_("card"),P=_("MultiSelect"),L=_("Dialog"),U=H("tooltip");return u(),m(x,null,[(u(),R(we,{admin:i,key:i.id,onGetAdmins:k(w).getAdmins},null,8,["admin","onGetAdmins"])),t(g,{class:"shadow-2 p-3 mt-3 border-round-2xl"},{content:v(()=>[t(k(J),{class:"tabview-custom",ref:"tabview4"},{default:v(()=>[t(k(D),null,{header:v(()=>[Ae,Ne]),default:v(()=>[e("div",xe,[Ve,K(t(n,{onClick:l[0]||(l[0]=c=>o.value=!o.value),icon:" fa-solid fa-pen",label:"تعديل",text:"",rounded:"",class:"p-button-primary p-button-text"},null,512),[[U,{value:"تعديل صلاحيات المستخدم",fitContent:!0},void 0,{top:!0}]])]),e("div",Be,[e("table",Ce,[De,e("tbody",null,[(u(!0),m(x,null,Q(h.value,c=>(u(),m("tr",{key:c.id},[c.name!=="None"&&c.name!=="Super Admin"?(u(),m(x,{key:0},[e("td",null,W(c.name),1),b([c.id],i.permissions)?(u(),m("td",Se,[e("button",{disabled:o.value,style:{background:"none",border:"none",cursor:"pointer"}},Te,8,Ie)])):(u(),m("td",Ee,[e("button",{disabled:o.value,style:{background:"none",border:"none",cursor:"pointer"}},Ue,8,Pe)]))],64)):B("",!0)]))),128)),je])])])]),_:1}),t(k(D),null,{header:v(()=>[qe,Ge]),_:1})]),_:1},512)]),_:1}),t(L,{modal:!0,visible:o.value,"onUpdate:visible":l[3]||(l[3]=c=>o.value=c),style:{width:"50rem"},header:"تعديل صلاحيات المستخدم",breakpoints:{"600px":"100vw"}},{default:v(()=>[e("div",null,[t(P,{modelValue:a.value,"onUpdate:modelValue":l[1]||(l[1]=c=>a.value=c),display:"chip",optionValue:"id",options:h.value,optionLabel:"name",placeholder:"اختر الصلاحيات"},null,8,["modelValue","options"])]),e("div",Oe,[t(n,{class:"p-button-primry",label:"تعديل",type:"submit",onClick:E,loading:r.value},null,8,["loading"]),t(n,{onClick:l[2]||(l[2]=c=>o.value=!1),label:"الغاء",class:"p-button-danger"})])]),_:1},8,["visible"])],64)}}});export{Je as default};