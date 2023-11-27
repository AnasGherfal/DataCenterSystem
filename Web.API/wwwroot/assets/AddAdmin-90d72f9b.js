import{d as D,g as f,s as E,v as z,u as P,x as j,r as o,c as r,b as l,w as u,o as n,k as g,a as e,F as V,m as M,n as I,t as v,l as x,j as R,y as G,z as c,A as N,B as H,C as J,f as K}from"./index-98f53cd7.js";import{_ as O}from"./BackButton.vue_vue_type_script_setup_true_lang-750e6b6c.js";import{u as Q,a as W}from"./admin-2b50c468.js";const X=["onSubmit"],Y={class:"grid p-fluid"},Z={class:"field col-12 md:col-6 lg:col-4"},ee={class:"p-float-label"},se={style:{height:"2px"}},te=e("label",{for:"emil"}," اسم الموظف",-1),ae={class:"field col-12 md:col-6 lg:col-4"},le={class:"p-float-label"},oe={style:{height:"2px"}},ne=e("label",{for:"emil"}," البريد الالكتروني",-1),ie={class:"field col-12 md:col-6 lg:col-4"},de={class:"p-float-label"},me={style:{height:"2px"}},re=e("label",{for:"empId"},"الرقم الوظيفي",-1),ue={class:"field col-12 md:col-6 lg:col-4"},ce={class:"p-float-label"},pe=e("label",{for:"empId"},"الصلاحيات",-1),_e={key:0,class:"error-message"},ye=D({__name:"AddAdmin",setup(fe){const h=f(!1),k=Q(),t=E({fullName:"",email:"",empId:null,permissions:0}),p=f(),d=f([]),C=f([{name:"Customer Management",id:1},{name:"Visits Management",id:2},{name:"Service Management",id:4},{name:"Invoice Management",id:8},{name:"Subscription Management",id:16},{name:"Companion Management",id:32},{name:"Representative Management",id:64},{name:"Time Shift Management",id:128},{name:"Analytics Management",id:256},{name:"Super Admin",id:511}]),w=z(()=>({fullName:{required:c.withMessage("الحقل مطلوب",N)},empId:{required:c.withMessage("الحقل مطلوب",N),minLength:c.withMessage("يجب أن يحتوي 4 ارقام",H(4))},email:{required:c.withMessage("الحقل مطلوب",N),email:c.withMessage(" ليس عنوان بريد إلكتروني صالح",J)}})),y=P(),_=j(w,t),A=()=>{let m=0;return d.value&&d.value.length>0&&(m=d.value.reduce((a,i)=>a+parseInt(i),0)),m},B=async()=>{const m=await _.value.$validate();if(d?p.value="":p.value="الحقل مطلوب",m){const a=A();t.permissions=a,W.create(t).then(function(i){k.getAdmins(),y.add({severity:"success",summary:"رسالة نجاح",detail:i.data.msg,life:3e3}),h.value=!1,K.go(-1)}).catch(function(i){y.add({severity:"error",summary:"رسالة فشل",detail:i.response.data.msg||"هنالك مشكلة في الوصول",life:3e3})})}else y.add({severity:"error",summary:"رسالة فشل",detail:"قم بتعبئة الحقول",life:3e3});h.value=!1},T=()=>{t.empId=null,t.fullName="",t.email="",t.permissions=0};return(m,a)=>{const i=o("Divider"),$=o("InputText"),b=o("error"),F=o("InputNumber"),L=o("MultiSelect"),S=o("Button"),q=o("Toast"),U=o("Card");return n(),r("div",null,[l(U,null,{title:u(()=>[g(" إضافة مستخدم "),l(O,{style:{float:"left"}}),l(i,{layout:"horizontal"})]),content:u(()=>[e("form",{onSubmit:G(B,["prevent"])},[e("div",Y,[e("div",Z,[e("span",ee,[l($,{id:"FullName",modelValue:t.fullName,"onUpdate:modelValue":a[0]||(a[0]=s=>t.fullName=s),type:"text",placeholder:"أدخل اسم الموظف"},null,8,["modelValue"]),e("div",se,[(n(!0),r(V,null,M(x(_).fullName.$errors,s=>(n(),I(b,{key:s.$uid,class:"p-error"},{default:u(()=>[g(v(s.$message),1)]),_:2},1024))),128))]),te])]),e("div",ae,[e("span",le,[l($,{id:"Email",type:"text",modelValue:t.email,"onUpdate:modelValue":a[1]||(a[1]=s=>t.email=s)},null,8,["modelValue"]),e("div",oe,[(n(!0),r(V,null,M(x(_).email.$errors,s=>(n(),I(b,{key:s.$uid,class:"p-error"},{default:u(()=>[g(v(s.$message),1)]),_:2},1024))),128))]),ne])]),e("div",ie,[e("span",de,[l(F,{id:"EmpId",mask:"99999",modelValue:t.empId,"onUpdate:modelValue":a[2]||(a[2]=s=>t.empId=s)},null,8,["modelValue"]),e("div",me,[(n(!0),r(V,null,M(x(_).empId.$errors,s=>(n(),I(b,{key:s.$uid,class:"p-error"},{default:u(()=>[g(v(s.$message),1)]),_:2},1024))),128))]),re])]),e("div",ue,[e("span",ce,[l(L,{modelValue:d.value,"onUpdate:modelValue":a[3]||(a[3]=s=>d.value=s),display:"chip",optionValue:"id",options:C.value,optionLabel:"name",maxSelectedLabels:3,placeholder:"اختر الصلاحيات"},null,8,["modelValue","options"]),pe,p.value?(n(),r("div",_e,v(p.value),1)):R("",!0)])])]),l(S,{class:"p-button-primry",icon:"fa-solid fa-plus",label:"إضافة",type:"submit",loading:h.value},null,8,["loading"]),l(S,{onClick:T,icon:"fa-solid fa-delete-left",label:"مسح",class:"p-button-danger",style:{"margin-right":"0.5em"}}),l(q,{position:"bottom-left"})],40,X)]),_:1})])}}});export{ye as default};
