import{d as B,g,s as D,v as I,u as T,x as q,r as _,c as u,b as s,w as v,o as m,a as e,j as V,z as M,A as G,B as O,D as F,h as R,E as z,i as H,n as J,l as w,F as N,G as K,H as C,q as Q,m as W,t as X,I as Y}from"./index-faac8ff5.js";import{u as Z,a as x}from"./admin-b76d02b0.js";import{v as ee}from"./validations-45222b88.js";import{_ as te}from"./BackButton.vue_vue_type_script_setup_true_lang-20365ff3.js";const se={class:"flex align-items-center justify-content-between"},ne=e("span",null," البيانات الشخصية ",-1),ae={key:0,class:"mb-5"},ie=e("div",{class:"warning-message"},[e("div",{class:"warning-message-icon"}),e("div",{class:"warning-message-text"}," هذا المستخدم مقفل لا يمكن التعديل عليه ")],-1),le=[ie],oe={key:1},de={class:"grid p-fluid"},ce={class:"field col-12 md:col-6 lg:col-4"},me={class:"p-float-label"},re={class:"field col-12 md:col-6 lg:col-4"},ue={class:"p-float-label"},_e={class:"field col-12 md:col-6 lg:col-4"},pe={class:"p-float-label"},ve={class:"field col-12 md:col-6 lg:col-4"},fe={class:"p-float-label"},he={class:"grid p-fluid"},ge={class:"field col-12 md:col-6 lg:col-4"},be={class:""},ye=e("label",{for:"displayName"},"اسم المستخدم",-1),$e={class:"field col-12 md:col-6 lg:col-4"},ke={class:""},we=e("label",{for:"email"},"البريد الإلكتروني",-1),Ae=B({__name:"InfoAdmin",props:{admin:{}},setup(S){const d=S,p=g(!1),A=D({id:d.admin.id,displayName:d.admin.displayName,email:d.admin.email,permissions:d.admin.permissions,isActive:d.admin.isActive,createdOn:d.admin.createdOn}),i=I(()=>({displayName:{required:M.withMessage("الحقل مطلوب",G),validateText:M.withMessage(", حروف عربيه او انجليزيه فقط",ee),minLength:M.withMessage("يجب أن يحتوي على الأقل 3 أحرف",O(3))}}));return T(),q(i,A),(r,l)=>{const y=_("Divider"),f=_("Skeleton"),o=_("InputText"),$=_("Toast"),k=_("Card");return m(),u("div",null,[s(k,null,{title:v(()=>[e("div",se,[ne,s(te,{style:{}})]),s(y)]),content:v(()=>[r.admin.isActive==!1?(m(),u("div",ae,le)):V("",!0),p.value?(m(),u("div",oe,[e("div",de,[e("div",ce,[e("span",me,[s(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",re,[e("span",ue,[s(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",_e,[e("span",pe,[s(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",ve,[e("span",fe,[s(f,{loading:p.value,width:"100%",height:"2rem"},null,8,["loading"])])])])])):V("",!0),e("div",null,[e("div",he,[e("div",ge,[e("span",be,[ye,s(o,{id:"displayName",nameid:"displayName",disabled:!0,modelValue:r.admin.displayName,"onUpdate:modelValue":l[0]||(l[0]=b=>r.admin.displayName=b)},null,8,["modelValue"])])]),e("div",$e,[e("span",ke,[we,s(o,{id:"email",name:"email",modelValue:r.admin.email,"onUpdate:modelValue":l[1]||(l[1]=b=>r.admin.email=b),disabled:!0},null,8,["modelValue"])])])]),s($,{position:"bottom-right"})])]),_:1})])}}}),Me=e("i",{class:"fa-solid fa-key ml-2"},null,-1),Ne=e("span",null,"الصلاحيات",-1),xe={class:"flex align-items-center justify-content-between"},Ve=e("h3",null,"الصلاحيات:",-1),Se={class:"table-responsive"},Ce={class:"table"},Be=e("thead",null,[e("tr",null,[e("th",null,"اسم الصلاحية"),e("th",null,"لديه اذن")])],-1),De={key:0},Ie=["disabled"],Te=e("i",{class:"fa-regular fa-circle-check",style:{color:"#2cbd0f"}},null,-1),Ee=[Te],Pe={key:1},Le=["disabled"],Ue=e("i",{class:"fa-regular fa-circle-xmark",style:{color:"red"}},null,-1),je=[Ue],qe=e("td",null,null,-1),Ge=e("i",{class:"fa-solid fa-bars ml-2"},null,-1),Oe=e("span",null,"سجل الحركات",-1),Fe={class:"flex items-center gap-3 mt-5"},Ke=B({__name:"ProfileAdmin",setup(S){const d=F(),p=T(),A=Z(),i=g(!1),r=g(!1),l=g([]),y=g([]),f=I(()=>d&&d.params&&d.params.id?Y(d.params.id):null),o=D({id:"",displayName:"",email:"",permissions:0,isActive:!0,createdOn:""}),$=g([{name:"Customer Management",id:1},{name:"Visits Management",id:2},{name:"Service Management",id:4},{name:"Invoice Management",id:8},{name:"Subscription Management",id:16},{name:"Companion Management",id:32},{name:"Representative Management",id:64},{name:"Time Shift Management",id:128},{name:"Analytics Management",id:256},{name:"Super Admin",id:511}]);function k(){x.getById(f.value).then(function(t){o.id=t.data.content.id,o.displayName=t.data.content.displayName,o.email=t.data.content.email,o.isActive=t.data.content.isActive,o.permissions=t.data.content.permissions}).catch(function(t){console.log(t)})}R(async()=>{k(),b()});function b(){x.permissions().then(function(t){y.value=t.data.content})}const E=(t,a)=>{for(let n=0;n<t.length;n++){const h=t[n];if((a&h)===h)return!0}return!1};z(i,()=>{i.value&&(l.value=[],$.value.forEach(t=>{(o.permissions&t.id)===t.id&&l.value.push(t)}))});const P=()=>{var a;r.value=!0;let t=0;l.value.some(n=>n.name==="Super Admin")?t=((a=l.value.find(n=>n.name==="Super Admin"))==null?void 0:a.id)||0:t=l.value.reduce((n,h)=>n+h.id,0),x.edit(f.value,t).then(function(n){p.add({severity:"success",summary:"رسالة نجاح",detail:n.data.msg,life:3e3}),i.value=!1,k()}).catch(function(n){console.log(n),p.add({severity:"error",summary:"رسالة فشل",detail:n.response.data.msg||"هنالك مشكلة في الوصول",life:3e3})}).finally(()=>{r.value=!1})};return(t,a)=>{const n=_("Button"),h=_("card"),L=_("MultiSelect"),U=_("Dialog"),j=H("tooltip");return m(),u(N,null,[(m(),J(Ae,{admin:o,key:o.id,onGetAdmins:w(A).getAdmins},null,8,["admin","onGetAdmins"])),s(h,{class:"shadow-2 p-3 mt-3 border-round-2xl"},{content:v(()=>[s(w(K),{class:"tabview-custom",ref:"tabview4"},{default:v(()=>[s(w(C),null,{header:v(()=>[Me,Ne]),default:v(()=>[e("div",xe,[Ve,Q(s(n,{onClick:a[0]||(a[0]=c=>i.value=!i.value),icon:" fa-solid fa-pen",label:"تعديل",text:"",rounded:"",class:"p-button-primary p-button-text"},null,512),[[j,{value:"تعديل صلاحيات المستخدم",fitContent:!0},void 0,{top:!0}]])]),e("div",Se,[e("table",Ce,[Be,e("tbody",null,[(m(!0),u(N,null,W(y.value,c=>(m(),u("tr",{key:c.id},[c.name!=="None"&&c.name!=="Super Admin"?(m(),u(N,{key:0},[e("td",null,X(c.name),1),E([c.id],o.permissions)?(m(),u("td",De,[e("button",{disabled:i.value,style:{background:"none",border:"none",cursor:"pointer"}},Ee,8,Ie)])):(m(),u("td",Pe,[e("button",{disabled:i.value,style:{background:"none",border:"none",cursor:"pointer"}},je,8,Le)]))],64)):V("",!0)]))),128)),qe])])])]),_:1}),s(w(C),null,{header:v(()=>[Ge,Oe]),_:1})]),_:1},512)]),_:1}),s(U,{modal:!0,visible:i.value,"onUpdate:visible":a[3]||(a[3]=c=>i.value=c),style:{width:"50rem"},header:"تعديل صلاحيات المستخدم",breakpoints:{"600px":"100vw"}},{default:v(()=>[e("div",null,[s(L,{modelValue:l.value,"onUpdate:modelValue":a[1]||(a[1]=c=>l.value=c),display:"chip",options:$.value,optionLabel:"name",placeholder:"اختر الصلاحيات"},null,8,["modelValue","options"])]),e("div",Fe,[s(n,{class:"p-button-primry",label:"تعديل",type:"submit",onClick:P,loading:r.value},null,8,["loading"]),s(n,{onClick:a[2]||(a[2]=c=>i.value=!1),label:"الغاء",class:"p-button-danger"})])]),_:1},8,["visible"])],64)}}});export{Ke as default};
