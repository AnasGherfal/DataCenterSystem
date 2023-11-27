import{d as P,v as R,x as O,u as W,y as G,g as d,r as c,o as u,c as m,b as a,w as U,a as e,z as X,l as b,U as le,F as x,h as ae,G as ie,k as ne,m as C,t as M,A as V,B as I,X as de,W as re,f as ce}from"./index-5db4e683.js";import{u as ue,v as me}from"./visits-0e816652.js";import{u as pe}from"./subscriptions-f02ed228.js";import{_ as fe}from"./CompanionsDataTable.vue_vue_type_script_setup_true_lang-dfc31ec6.js";import{_ as _e}from"./BackButton.vue_vue_type_script_setup_true_lang-26549c53.js";import{u as ve}from"./customers-865950fc.js";import{r as ye}from"./representatives-6d631fa8.js";import{c as be}from"./customers-9f2c437f.js";const ge=["onSubmit"],he={class:"grid p-fluid"},Te={class:"field col-12 md:col-4"},Ve={class:"p-float-label"},xe=e("label",{for:"firstName"},"الاسم ",-1),we={class:"field col-12 md:col-4"},$e={class:"p-float-label"},Ne=e("label",{for:"lastName"},"اللقب ",-1),Se={class:"field col-12 md:col-4"},Ce={class:"p-float-label"},Me=e("label",{for:"jobTitle"},"صفة المرافق",-1),Ie={class:"field col-12 md:col-4"},Le={class:"p-float-label"},De=e("label",{for:"identityType"},"نوع الاثبات",-1),ke={class:"field col-12 md:col-4"},Ue={class:"p-float-label"},Be=e("label",{for:"identityNo"},"رقم الاثبات",-1),Fe=P({__name:"addCompanion",props:["compList","disableValidation"],setup(E){const v=E,t=R({firstName:"",lastName:"",identityNo:"",identityType:null,jobTitle:""}),B=O(()=>{}),p=W(),w=G(B,t),L=async()=>{const h=/^[^\d]+$/;if(!t.firstName||!t.lastName||!t.identityType||!t.identityNo||!t.jobTitle){p.add({severity:"warn",summary:"فشل",detail:"يرجى تعبئة جميع الحقول المطلوبة",life:3e3});return}else if(!h.test(t.firstName)||!h.test(t.lastName)){p.add({severity:"warn",summary:"فشل",detail:"الاسم يجب أن يحتوي على أحرف فقط",life:3e3});return}let i=null;if(v.disableValidation||(i=await w.value.$validate(),console.log(v.disableValidation,i)),i){p.add({severity:"success",summary:"Success Message",detail:"تمت إضافة المرافق",life:3e3});const f={firstName:t.firstName,lastName:t.lastName,identityNo:t.identityNo,identityType:t.identityType,jobTitle:t.jobTitle};v.compList.push(f),D(),$()}else p.add({severity:"warn",summary:"فشل",detail:"لم يتم اضافة المرافق",life:3e3})},D=()=>{t.firstName="",t.lastName="",t.identityType=null,t.identityNo="",t.jobTitle=""},g=d(!1),F=()=>{g.value=!0},$=()=>{g.value=!1},N=[{value:1,text:"اثبات هويه"},{value:2,text:"جواز سفر"}];return(h,i)=>{const f=c("InputText"),o=c("Dropdown"),_=c("Button"),k=c("Toast");return u(),m(x,null,[a(b(le),{header:"اضافة مُرافق",contentStyle:"height: 200px; padding: 20px;",visible:g.value,"onUpdate:visible":i[5]||(i[5]=r=>g.value=r),breakpoints:{"960px":"75vw","640px":"90vw"},style:{width:"60vw"},modal:!0},{footer:U(()=>[a(_,{onClick:L,class:"p-button-primry",icon:"fa-solid fa-plus",label:"إضافة",type:"submit"}),a(_,{onClick:D,icon:"fa-solid fa-delete-left",label:"مسح",class:"p-button-danger",style:{"margin-right":"0.5rem"}}),a(k,{position:"bottom-left"})]),default:U(()=>[e("form",{onSubmit:X(L,["prevent"])},[e("div",he,[e("div",Te,[e("span",Ve,[a(f,{id:"firstName",type:"text",modelValue:t.firstName,"onUpdate:modelValue":i[0]||(i[0]=r=>t.firstName=r)},null,8,["modelValue"]),xe])]),e("div",we,[e("span",$e,[a(f,{id:"lastName",type:"text",modelValue:t.lastName,"onUpdate:modelValue":i[1]||(i[1]=r=>t.lastName=r)},null,8,["modelValue"]),Ne])]),e("div",Se,[e("span",Ce,[a(f,{id:"jobTitle",type:"text",modelValue:t.jobTitle,"onUpdate:modelValue":i[2]||(i[2]=r=>t.jobTitle=r)},null,8,["modelValue"]),Me])]),e("div",Ie,[e("span",Le,[a(o,{id:"identityType",modelValue:t.identityType,"onUpdate:modelValue":i[3]||(i[3]=r=>t.identityType=r),placeholder:"اختر نوع الاثبات",emptyMessage:"لاتوجد انواع",options:N,optionValue:"value",optionLabel:"text"},null,8,["modelValue"]),De])]),e("div",ke,[e("span",Ue,[a(f,{id:"identityNo",type:"text",modelValue:t.identityNo,"onUpdate:modelValue":i[4]||(i[4]=r=>t.identityNo=r)},null,8,["modelValue"]),Be])])])],40,ge)]),_:1},8,["visible"]),a(_,{onClick:F,class:"p-button-primary p-button-sm p-button-rounded",style:{display:"flex","justify-content":"center","align-items":"center",width:"6rem"},icon:"fa-solid fa-plus",label:" مُرافق"}),a(fe,{compList:v.compList},null,8,["compList"])],64)}}}),Ee=["onSubmit"],je={class:"grid p-fluid"},qe={class:"field col-12 md:col-6 lg:col-4"},ze={class:"p-float-label"},Ae=e("label",{for:"customerName"},"العملاء",-1),Pe={class:"field col-12 md:col-6 lg:col-4"},Re={class:"p-float-label"},Oe=e("label",{for:"customerName"},"اشتراكات",-1),We={style:{height:"2px"}},Ge={class:"field col-12 md:col-6 lg:col-4"},Xe={class:"p-float-label"},He=e("label",{for:"representatives"},"المخولين",-1),Je={style:{height:"2px"}},Ke={class:"field col-12 md:col-6 lg:col-4"},Qe={class:"p-float-label"},Ye=e("label",{for:"visitType"},"سبب الزيارة ",-1),Ze={style:{height:"2px"}},et={class:"field col-12 md:col-6 lg:col-4"},tt={class:"p-float-label"},ot=e("label",{for:"startTime"},"تاريخ بداية الزيارة ",-1),st={style:{height:"2px"}},lt={class:"field col-12 md:col-6 lg:col-4"},at={class:"p-float-label"},it=e("label",{for:"endTime"},"تاريخ انتهاء الزيارة ",-1),nt={style:{height:"2px"}},dt={class:"field col-12 md:col-8 lg:col-8"},rt={class:"p-float-label"},ct=e("label",{for:"notes"}," الملاحظات",-1),ut=e("br",null,null,-1),mt=e("br",null,null,-1),Tt=P({__name:"VisitForm",setup(E){const v=ue();pe(),ve();const t=d(!1),B=d(!1),p=d(""),w=d(),L=d(1),D=d(1),g=d(10),F=d(0),$=d(),N=d(),h=d(),i=d();d(),d(!1);const f=W(),o=R({expectedStartTime:"",expectedEndTime:"",visitType:1,notes:"",subscriptionId:null,representatives:[],companions:[]}),_=d(new Date),k=d(new Date),r=()=>{_.value>k.value&&(k.value=_.value)},H=O(()=>({subscriptionId:{required:V.withMessage(" الحقل مطلوب",I)},visitType:{required:V.withMessage("الحقل مطلوب",I)},expectedStartTime:{required:V.withMessage("  الحقل مطلوب",I)},expectedEndTime:{required:V.withMessage(" الحقل مطلوب",I),minValue:V.withMessage("تاريخ انتهاء الزياره يجب ان يكون بعد تاريخ البدايه",de(o.expectedStartTime))},representatives:{required:V.withMessage("  الحقل مطلوب",I)}})),T=G(H,o);ae(async()=>{J()});async function J(){(p.value===void 0||p.value===null)&&(p.value=""),await be.get(D.value,g.value,p.value).then(function(n){w.value=n.data.content,L.value=n.data.totalPages,F.value=n.data.currentPage}).catch(function(n){console.log(n)}).finally(()=>{t.value=!1})}ie(N,async n=>{const s=n.id;if(n&&s!==void 0)try{t.value=!0,console.log(s),await K(s),await Q(s),t.value=!1}catch(S){console.error("Error fetching representatives:",S)}});const K=n=>{ye.get(n).then(function(s){console.log(s),h.value=s.data.content}).catch(function(s){console.log(s)})},Q=n=>{re.get(1,50,n).then(function(s){i.value=s.data.content}).catch(function(s){console.log(s)})},j=async()=>{if(await T.value.$validate()){const s=o.representatives.map(y=>y.id),S={subscriptionId:o.subscriptionId?o.subscriptionId[0].id:"",visitType:+o.visitType,representatives:s,expectedStartTime:o.expectedStartTime,expectedEndTime:o.expectedEndTime,notes:o.notes,companions:o.companions};t.value=!0,me.create(S).then(function(y){f.add({severity:"success",summary:"تمت الاضافه",detail:y.data.msg,life:3e3}),v.getVisits(),ce.go(-1)}).catch(function(y){f.add({severity:"error",summary:"فشلت الاضافه",detail:y.response.data.msg||"حدث خطأ ما",life:3e3})})}else console.log("not valid")},Y=()=>{o.expectedStartTime="",o.expectedEndTime="",o.visitType=0,o.notes="",o.subscriptionId=null,o.representatives=[],o.companions=[]},Z=n=>{setTimeout(()=>{n.query.trim().length?$.value=w.value.filter(s=>s.name.toLowerCase().startsWith(n.query.toLowerCase())):$.value=[...w.value]},250)};return(n,s)=>{const S=c("Divider"),y=c("AutoComplete"),q=c("MultiSelect"),ee=c("Dropdown"),z=c("Calendar"),te=c("Textarea"),A=c("Button"),oe=c("Toast"),se=c("Card");return u(),m("div",null,[a(se,null,{title:U(()=>[ne(" إنشاء زيارة "),a(_e,{style:{float:"left"}}),a(S)]),content:U(()=>[e("form",{onSubmit:X(j,["prevent"])},[e("div",je,[e("div",qe,[e("span",ze,[a(y,{modelValue:N.value,"onUpdate:modelValue":s[0]||(s[0]=l=>N.value=l),optionLabel:"name",suggestions:$.value,onComplete:Z},null,8,["modelValue","suggestions"]),Ae])]),e("div",Pe,[e("span",Re,[a(q,{modelValue:o.subscriptionId,"onUpdate:modelValue":s[1]||(s[1]=l=>o.subscriptionId=l),options:i.value,optionLabel:"serviceName",emptyMessage:"هاذا العميل ليس لديه اشتراكات",placeholder:" اختر اشتراك",selectionLimit:1,loading:t.value},null,8,["modelValue","options","loading"]),Oe,e("div",We,[(u(!0),m(x,null,C(b(T).subscriptionId.$errors,l=>(u(),m("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:l.$uid,class:"p-error"},M(l.$message),1))),128))])])]),e("div",Ge,[e("span",Xe,[a(q,{modelValue:o.representatives,"onUpdate:modelValue":s[2]||(s[2]=l=>o.representatives=l),options:h.value,optionLabel:"firstName",placeholder:"اختر",emptySelectionMessage:"ll",selectionLimit:2,emptyMessage:"هاذا العميل ليس لديه مخوليين"},null,8,["modelValue","options"]),He,e("div",Je,[(u(!0),m(x,null,C(b(T).representatives.$errors,l=>(u(),m("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:l.$uid,class:"p-error"},M(l.$message),1))),128))])])]),e("div",Ke,[e("span",Qe,[a(ee,{id:"visitType",modelValue:o.visitType,"onUpdate:modelValue":s[3]||(s[3]=l=>o.visitType=l),options:b(v).visitReasons,optionValue:"id",optionLabel:"name"},null,8,["modelValue","options"]),Ye,e("div",Ze,[(u(!0),m(x,null,C(b(T).visitType.$errors,l=>(u(),m("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:l.$uid,class:"p-error"},M(l.$message),1))),128))])])]),e("div",et,[e("span",tt,[a(z,{inputId:"startTime",modelValue:o.expectedStartTime,"onUpdate:modelValue":s[4]||(s[4]=l=>o.expectedStartTime=l),dateFormat:"yy/mm/dd",showTime:!0,selectionMode:"single",minDate:_.value,showButtonBar:!0,manualInput:!0,stepMinute:5,hourFormat:"12",onOnChange:r},null,8,["modelValue","minDate"]),ot,e("div",st,[(u(!0),m(x,null,C(b(T).expectedStartTime.$errors,l=>(u(),m("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:l.$uid,class:"p-error"},M(l.$message),1))),128))])])]),e("div",lt,[e("span",at,[a(z,{inputId:"endTime",modelValue:o.expectedEndTime,"onUpdate:modelValue":s[5]||(s[5]=l=>o.expectedEndTime=l),dateFormat:"yy/mm/dd",showTime:!0,selectionMode:"single",minDate:_.value,showButtonBar:!0,manualInput:!0,stepMinute:5,hourFormat:"12"},null,8,["modelValue","minDate"]),it,e("div",nt,[(u(!0),m(x,null,C(b(T).expectedEndTime.$errors,l=>(u(),m("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:l.$uid,class:"p-error"},M(l.$message),1))),128))])])]),e("div",dt,[e("span",rt,[a(te,{modelValue:o.notes,"onUpdate:modelValue":s[6]||(s[6]=l=>o.notes=l),rows:"5",cols:"77"},null,8,["modelValue"]),ct])])]),a(Fe,{compList:o.companions,"disable-validation":B.value},null,8,["compList","disable-validation"]),ut,mt,a(A,{onClick:j,icon:"fa-solid fa-plus",label:"إنشاء"}),a(A,{onClick:Y,icon:"fa-solid fa-delete-left",label:"مسح",class:"p-button-danger",style:{"margin-right":"0.5rem"}}),a(oe,{position:"bottom-left"})],40,Ee)]),_:1})])}}});export{Tt as default};
