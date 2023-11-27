import{d as E,g as x,v as R,Q as j,x as Q,y as G,u as H,r as y,c as l,b as d,w as T,o as n,k as V,a as e,z as J,l as c,R as C,F as h,m as v,t as m,j as D,A as u,B as g,C as K,D as O,f as W,p as X,e as Y,_ as Z}from"./index-9e7537ee.js";import{_ as ee}from"./BackButton.vue_vue_type_script_setup_true_lang-f010849f.js";import{c as te}from"./customers-28ddb044.js";import{v as se,i as oe}from"./validations-45222b88.js";import{u as le}from"./customers-f91d4833.js";const r=b=>(X("data-v-aca5f4e1"),b=b(),Y(),b),ne={class:"grid p-fluid"},ae={class:"field col-12 md:col-6 lg:col-4"},ie={class:"p-float-label"},de={style:{height:"2px"}},re=r(()=>e("label",{for:"name"},"الاسم ",-1)),ce={class:"field col-12 md:col-6 lg:col-4"},me={class:"p-float-label"},ue={style:{height:"2px"}},pe=r(()=>e("label",{for:"email"},"البريد الإلكتروني",-1)),_e={class:"field col-12 md:col-6 lg:col-4"},fe={class:"p-float-label"},ye={style:{height:"2px"}},he=r(()=>e("label",{for:"city"},"المدينة",-1)),ve={class:"field col-12 md:col-6 lg:col-4"},ge={class:"p-float-label"},be={style:{height:"2px"}},Ce=r(()=>e("label",{for:"address"},"العنوان",-1)),De={class:"field col-12 md:col-6 lg:col-4"},xe={class:"p-float-label"},Ve={style:{height:"2px"}},we=r(()=>e("label",{for:"primaryPhone"},"رقم هاتف ",-1)),$e={class:"field col-12 md:col-6 lg:col-4"},Ie={class:"p-float-label"},Pe=r(()=>e("label",{for:"secondaryPhone"},"رقم هاتف 2",-1)),ke=r(()=>e("div",{for:"files",style:{"margin-bottom":"0.5rem"}},"الملفات",-1)),Me={class:"grid p-fluid"},Se={class:"field col-12 md:col-6 lg:col-4"},Be=r(()=>e("div",{class:"file-input-label-text"},"تعريف شخصي",-1)),Ne={class:"file-input-label",for:"fileInput1"},Te={class:"file-input-content"},Ue={key:0,class:"file-input-icon"},qe={class:"file-input-text"},ze=r(()=>e("i",{class:"pi pi-upload"},null,-1)),Ae={key:0,class:"error-message"},Le={class:"field col-12 md:col-6 lg:col-4"},Fe=r(()=>e("div",{class:"file-input-label-text"},"تخويل من الشركة",-1)),Ee={class:"file-input-label",for:"fileInput2"},Re={class:"file-input-content"},je={key:0,class:"file-input-icon"},Qe={class:"file-input-text"},Ge=r(()=>e("i",{class:"pi pi-upload"},null,-1)),He={key:0,class:"error-message"},Je=E({__name:"AddCustomer",emits:["getCustomers"],setup(b,{emit:U}){le();const q=x(!1),p=x(null),_=x(null),w=U,s=R({name:"",email:"",primaryPhone:"",secondaryPhone:"",city:"",address:"",CompanyDocuments:null,IdentityDocuments:null});j((a,o,i)=>{i()});const z=Q(()=>({name:{required:u.withMessage("الحقل مطلوب",g),validateText:u.withMessage(", حروف فقط",se),minLength:u.withMessage("يجب أن يحتوي على الأقل 3 أحرف",K(3))},email:{required:u.withMessage("الحقل مطلوب",g),email:u.withMessage(" ليس عنوان بريد إلكتروني صالح",O)},city:{required:u.withMessage("الحقل مطلوب",g)},address:{required:u.withMessage("الحقل مطلوب",g)},primaryPhone:{required:u.withMessage("الحقل مطلوب",g),isLibyanPhoneNumber:u.withMessage(" , ليس رقم ليبي صالح",oe)}})),f=G(z,s),$=async()=>{const a=new FormData,o=await f.value.$validate();if(!s.IdentityDocuments||!s.CompanyDocuments)s.IdentityDocuments?p.value="":p.value="الحقل مطلوب",s.CompanyDocuments?_.value="":_.value="الحقل مطلوب";else{if(p.value="",_.value="",!o)return;a.append("Name",s.name),a.append("City",s.city),a.append("Address",s.address),a.append("Email",s.email),a.append("PrimaryPhone",s.primaryPhone),a.append("SecondaryPhone",s.secondaryPhone),a.append("IdentityDocument",s.IdentityDocuments),a.append("CompanyDocuments",s.CompanyDocuments),te.create(a).then(i=>{w("getCustomers"),I.add({severity:"success",summary:"رسالة نجاح",detail:i.data.msg,life:2e3}),setTimeout(()=>{W.go(-1)},1)}).catch(i=>{I.add({severity:"error",summary:"خطأ",detail:i.response.data.msg,life:3e3})}).finally(()=>{q.value=!1,w("getCustomers")})}},I=H();async function P(a,o){const i=a.target.files[0];i&&(o===0?s.IdentityDocuments=i:s.CompanyDocuments=i,o===0?p.value="":_.value="")}return(a,o)=>{const i=y("Divider"),k=y("InputMask"),A=y("Button"),L=y("Toast"),F=y("Card");return n(),l("div",null,[d(F,null,{title:T(()=>[V(" إضافة عميل "),d(ee,{style:{float:"left"}}),d(i)]),content:T(()=>{var M,S,B,N;return[e("div",null,[e("form",{onSubmit:o[9]||(o[9]=J(t=>$(),["prevent"]))},[e("div",ne,[e("div",ae,[e("span",ie,[d(c(C),{id:"name",type:"text",modelValue:s.name,"onUpdate:modelValue":o[0]||(o[0]=t=>s.name=t)},null,8,["modelValue"]),e("div",de,[(n(!0),l(h,null,v(c(f).name.$errors,t=>(n(),l("span",{key:t.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},m(t.$message),1))),128))]),re])]),e("div",ce,[e("span",me,[d(c(C),{id:"email",type:"text",modelValue:s.email,"onUpdate:modelValue":o[1]||(o[1]=t=>s.email=t)},null,8,["modelValue"]),e("div",ue,[(n(!0),l(h,null,v(c(f).email.$errors,t=>(n(),l("span",{key:t.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},m(t.$message),1))),128))]),pe])]),e("div",_e,[e("span",fe,[d(c(C),{id:"city",name:"city",type:"text",modelValue:s.city,"onUpdate:modelValue":o[2]||(o[2]=t=>s.city=t)},null,8,["modelValue"]),e("div",ye,[(n(!0),l(h,null,v(c(f).city.$errors,t=>(n(),l("span",{key:t.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},m(t.$message),1))),128))]),he])]),e("div",ve,[e("span",ge,[d(c(C),{id:"address",name:"address",type:"text",modelValue:s.address,"onUpdate:modelValue":o[3]||(o[3]=t=>s.address=t)},null,8,["modelValue"]),e("div",be,[(n(!0),l(h,null,v(c(f).address.$errors,t=>(n(),l("span",{key:t.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},m(t.$message),1))),128))]),Ce])]),e("div",De,[e("span",xe,[d(k,{id:"primaryPhone",modelValue:s.primaryPhone,"onUpdate:modelValue":o[4]||(o[4]=t=>s.primaryPhone=t),mask:"+218999999999",style:{direction:"ltr","text-align":"end"}},null,8,["modelValue"]),e("div",Ve,[(n(!0),l(h,null,v(c(f).primaryPhone.$errors,t=>(n(),l("span",{key:t.$uid,style:{color:"red",direction:"ltr","font-weight":"bold","font-size":"small"}},m(t.$message),1))),128))]),we])]),e("div",$e,[e("span",Ie,[d(k,{id:"secondaryPhone",modelValue:s.secondaryPhone,"onUpdate:modelValue":o[5]||(o[5]=t=>s.secondaryPhone=t),mask:"+218999999999",style:{direction:"ltr","text-align":"end"}},null,8,["modelValue"]),Pe])])]),ke,e("div",Me,[e("div",Se,[Be,e("label",Ne,[e("div",Te,[(M=s.IdentityDocuments)!=null&&M.name?D("",!0):(n(),l("div",Ue)),e("div",qe,[ze,V(" "+m(((S=s.IdentityDocuments)==null?void 0:S.name)||"ارفق ملف"),1)])]),e("input",{id:"fileInput1",style:{display:"none"},type:"file",onChange:o[6]||(o[6]=t=>P(t,0)),accept:"*"},null,32)]),p.value?(n(),l("div",Ae,m(p.value),1)):D("",!0)]),e("div",Le,[Fe,e("label",Ee,[e("div",Re,[(B=s.CompanyDocuments)!=null&&B.name?D("",!0):(n(),l("div",je)),e("div",Qe,[Ge,V(" "+m(((N=s.CompanyDocuments)==null?void 0:N.name)||"ارفق ملف"),1)])]),e("input",{id:"fileInput2",style:{display:"none"},type:"file",onChange:o[7]||(o[7]=t=>P(t,1)),accept:"*"},null,32)]),_.value?(n(),l("div",He,m(_.value),1)):D("",!0)])]),d(A,{onClick:o[8]||(o[8]=t=>$()),icon:"fa-solid fa-plus",label:"إضافة"}),d(L,{position:"bottom-left"})],32)])]}),_:1})])}}});const Ze=Z(Je,[["__scopeId","data-v-aca5f4e1"]]);export{Ze as default};
