import{d as J,g as r,v as K,h as N,x as O,u as Q,y as Z,r as c,c as a,k as D,t as u,a as e,b as i,w as Y,F as p,o as n,z as R,m as f,l as _,j as ee,A as v,B as b,X as te,N as C,W as oe,f as se}from"./index-9e7537ee.js";import{_ as le}from"./BackButton.vue_vue_type_script_setup_true_lang-f010849f.js";import{u as ae}from"./subscriptions-bd40b9f0.js";import{u as ne}from"./customers-f91d4833.js";import{s as ie}from"./service-e003fa54.js";import{c as re}from"./customers-28ddb044.js";const de=["onSubmit"],ue={class:"grid p-fluid"},ce={class:"field col-12 md:col-6 lg:col-4"},me={class:"p-float-label"},pe=e("label",{for:"customerName"},"العملاء",-1),fe={style:{height:"2px"}},_e={class:"field col-12 md:col-6 lg:col-4"},ge={class:"p-float-label"},ve=e("label",{for:"startDate"},"تاريخ بداية الاشتراك",-1),he={style:{height:"2px"}},ye={class:"field col-12 md:col-6 lg:col-4"},De={class:"p-float-label"},be=e("label",{for:"endtDate"},"تاريخ انتهاء الاشتراك",-1),Ie={style:{height:"2px"}},we={class:"field col-12 md:col-6 lg:col-4"},Ve={class:"p-float-label"},$e=e("label",{for:"subscriptionType"},"الباقة",-1),Ce={style:{height:"2px"}},Me={class:"field col-12 md:col-6 lg:col-4"},xe={class:"p-float-label"},ke=e("label",{for:"number"},"رقم العقد",-1),Be={style:{height:"2px"}},Ne={class:"field col-12 md:col-6 lg:col-4"},Ye={class:"p-float-label"},Fe=e("label",{for:"date"},"تاريخ العقد",-1),ze={style:{height:"2px"}},Se={class:"field col-12 md:col-6 lg:col-4"},Te={class:"file-input-label",for:"fileInput1"},Ae={class:"file-input-content"},qe=e("div",{class:"file-input-icon"},null,-1),Ue={class:"file-input-text"},Le=e("i",{class:"pi pi-upload"},null,-1),Pe={key:0,style:{color:"red","font-weight":"bold","font-size":"small"}},Ke=J({__name:"Addsubscription",setup(Ee){ae(),ne();const g=r(!1),M=r(),h=r(null),F=r(1),z=r(1),S=r(10),T=r(0),y=r(""),I=r(),t=K({serviceId:null,customerId:null,startDate:"",endDate:"",contractNumber:"",contractDate:"",file:null});N(async()=>{ie.get().then(function(l){M.value=l.data.content}).catch(function(l){console.log(l)})}),N(async()=>{A()});async function A(){(y.value===void 0||y.value===null)&&(y.value=""),await re.get(z.value,S.value,y.value).then(function(l){I.value=l.data.content,F.value=l.data.totalPages,T.value=l.data.currentPage}).catch(function(l){console.log(l)}).finally(()=>{g.value=!1})}const q=O(()=>({customerId:{required:v.withMessage("الحقل مطلوب",b)},startDate:{required:v.withMessage("الحقل مطلوب",b)},endDate:{required:v.withMessage(" الحقل مطلوب",b),minValue:v.withMessage("تاريخ انتهاء الاشتراك يجب ان يكون بعد تاريخ البدايه",te(t.startDate))},serviceId:{required:v.withMessage("الحقل مطلوب",b)}})),x=Q(),m=Z(q,t);async function U(l,s){const d=l.target.files[0];d&&(t.file=d)}const L=async()=>{const l=await m.value.$validate();if(!t.file)return h.value="الحقل مطلوب";const s=new FormData;if(h.value="",!!l){if(g.value=!0,t.file===null){console.log("File is null");return}s.append("serviceId",String(t.serviceId)),t.customerId!==null&&t.customerId!==void 0?s.append("customerId",String(t.customerId.id)):s.append("customerId","0"),s.append("startDate",C(t.startDate).format("YYYY/MM/DD")),s.append("endDate",C(t.endDate).format("YYYY/MM/DD")),s.append("file",t.file),s.append("contractNumber",t.contractNumber),s.append("contractDate",C(t.contractDate).format("YYYY/MM/DD")),oe.create(s).then(d=>{x.add({severity:"success",summary:"تمت اضافة اشتراك",detail:d.data.msg,life:3e3}),console.log(d),g.value=!1,se.go(-1)}).catch(function(d){console.log(d),x.add({severity:"error",summary:"هناك مشكلة",detail:d.response.data.msg||"هنالك مشكلة في الوصول",life:3e3})}),g.value=!1}},P=()=>{t.serviceId=null,t.customerId=null,t.startDate="",t.endDate="",t.file=null},w=r(new Date),V=r(),E=l=>{setTimeout(()=>{l.query.trim().length?V.value=I.value.filter(s=>s.name.toLowerCase().startsWith(l.query.toLowerCase())):V.value=[...I.value]},250)};return(l,s)=>{const d=c("Divider"),W=c("AutoComplete"),$=c("Calendar"),j=c("Dropdown"),X=c("InputText"),k=c("Button"),G=c("Toast"),H=c("Card");return n(),a(p,null,[D(u(t.customerId)+" ",1),e("div",null,[i(H,null,{title:Y(()=>[D(" إضافة اشتراك "),i(le,{style:{float:"left"}}),i(d,{layout:"horizontal"})]),content:Y(()=>{var B;return[e("form",{onSubmit:R(L,["prevent"])},[e("div",ue,[e("div",ce,[e("span",me,[D(u()+" ",1),i(W,{modelValue:t.customerId,"onUpdate:modelValue":s[0]||(s[0]=o=>t.customerId=o),optionLabel:"name",suggestions:V.value,onComplete:E},null,8,["modelValue","suggestions"]),pe,e("div",fe,[(n(!0),a(p,null,f(_(m).customerId.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",_e,[e("span",ge,[i($,{inputId:"startDate",modelValue:t.startDate,"onUpdate:modelValue":s[1]||(s[1]=o=>t.startDate=o),minDate:w.value,dateFormat:"yy/mm/dd",selectionMode:"single",showButtonBar:!0,manualInput:!1},null,8,["modelValue","minDate"]),ve,e("div",he,[(n(!0),a(p,null,f(_(m).startDate.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",ye,[e("span",De,[i($,{inputId:"endDate",modelValue:t.endDate,"onUpdate:modelValue":s[2]||(s[2]=o=>t.endDate=o),dateFormat:"yy/mm/dd",selectionMode:"single",minDate:w.value,showButtonBar:!0,manualInput:!1},null,8,["modelValue","minDate"]),be,e("div",Ie,[(n(!0),a(p,null,f(_(m).endDate.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",we,[e("span",Ve,[i(j,{id:"subscriptionType",options:M.value,"option-value":"id",optionLabel:"name",modelValue:t.serviceId,"onUpdate:modelValue":s[3]||(s[3]=o=>t.serviceId=o),placeholder:"اختر الباقه",emptyMessage:"لايوجد باقات"},null,8,["options","modelValue"]),$e,e("div",Ce,[(n(!0),a(p,null,f(_(m).serviceId.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",Me,[e("span",xe,[i(X,{id:"number",modelValue:t.contractNumber,"onUpdate:modelValue":s[4]||(s[4]=o=>t.contractNumber=o)},null,8,["modelValue"]),ke,e("div",Be,[(n(!0),a(p,null,f(_(m).serviceId.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",Ne,[e("span",Ye,[i($,{inputId:"endDate",modelValue:t.contractDate,"onUpdate:modelValue":s[5]||(s[5]=o=>t.contractDate=o),dateFormat:"yy/mm/dd",selectionMode:"single",minDate:w.value,showButtonBar:!0,manualInput:!1},null,8,["modelValue","minDate"]),Fe,e("div",ze,[(n(!0),a(p,null,f(_(m).endDate.$errors,o=>(n(),a("span",{style:{color:"red","font-weight":"bold","font-size":"small"},key:o.$uid,class:"p-error"},u(o.$message),1))),128))])])]),e("div",Se,[e("label",Te,[e("div",Ae,[qe,e("div",Ue,[Le,D(" "+u(((B=t.file)==null?void 0:B.name)||"ارفق ملف  1 "),1)])]),e("input",{id:"fileInput1",style:{display:"none"},type:"file",onChange:s[6]||(s[6]=o=>U(o,0)),accept:"*"},null,32)]),h.value?(n(),a("div",Pe,u(h.value),1)):ee("",!0)])]),i(k,{class:"p-button-primry",icon:"fa-solid fa-plus",label:"إضافة",type:"submit",loading:g.value},null,8,["loading"]),i(k,{onClick:P,icon:"fa-solid fa-delete-left",label:"مسح",class:"p-button-danger",style:{"margin-right":"0.5em"}}),i(G,{position:"bottom-right"})],40,de)]}),_:1})])],64)}}});export{Ke as default};