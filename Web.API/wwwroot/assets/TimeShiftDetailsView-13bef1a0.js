import{u as w}from"./visits-5bdb4a37.js";import{d as H,g as v,v as k,u as B,r as u,i as I,c as r,b as o,w as S,o as d,k as N,q as M,l as D,a as e,j as x,z as U,E as j,x as q,h as E,F as y,m as O,n as z}from"./index-bc1988a6.js";import{_ as A}from"./BackButton.vue_vue_type_script_setup_true_lang-fee06f4c.js";import{t as C}from"./timeShifts-7c065e7f.js";import{b as V}from"./formatTime-b5685e4a.js";const L={key:0},G={class:"grid p-fluid"},J={class:"field col-12 md:col-6 lg:col-4"},K={class:"p-float-label"},P={class:"field col-12 md:col-6 lg:col-4"},Q={class:"p-float-label"},W={class:"field col-12 md:col-6 lg:col-4"},X={class:"p-float-label"},Y={class:"field col-12 md:col-6 lg:col-4"},Z={class:"p-float-label"},ee={class:"field col-12 md:col-6 lg:col-4"},te={class:"p-float-label"},oe={class:"field col-12 md:col-6 lg:col-4"},ie={key:1},se=["onSubmit","disabled"],le={class:"grid p-fluid my-5"},ae={class:"field col-12 md:col-6 lg:col-4"},ne={class:""},de=e("label",{for:"startTime"},"بداية التوقيت ",-1),re={class:"field col-12 md:col-6 lg:col-4"},ce={class:""},me=e("label",{for:"stopDate"}," نهاية التوقيت ",-1),ue={class:"grid p-fluid"},_e={class:"field col-12 md:col-6 lg:col-4"},pe={class:""},fe=e("label",{for:"customerName"},"السعر مقابل الساعة الاولى",-1),he={class:"field col-12 md:col-6 lg:col-4"},ge={class:""},ve=e("label",{for:"customerName"},"السعر مقابل باقي الساعات ",-1),be={key:0},Te=H({__name:"TimeShiftDetails",props:{timeShiftData:{}},setup(b){const _=w(),i=v(!0),n=v(!0),l=b,t=k({id:l.timeShiftData.id,startTime:l.timeShiftData.startTime,endTime:l.timeShiftData.endTime,priceForFirstHour:l.timeShiftData.priceForFirstHour,priceForRemainingHours:l.timeShiftData.priceForRemainingHours}),p={...t},c=B(),h=async()=>{const f={startTime:p.startTime,endTime:p.endTime,priceForFirstHour:t.priceForFirstHour,priceForRemainingHours:t.priceForRemainingHours};p.startTime!==t.startTime&&(f.startTime=V(t.startTime)),p.endTime!==t.endTime&&(f.endTime=V(t.endTime)),console.log(t),C.edit(t.id,f).then(s=>{c.add({severity:"success",summary:"رسالة تأكيد",detail:s.data.msg,life:3e3}),i.value=!0}).catch(s=>{c.add({severity:"error",summary:"خطأ",detail:s.response.data.msg,life:3e3})})};return(f,s)=>{const g=u("Button"),m=u("Skeleton"),T=u("Calendar"),F=u("InputNumber"),R=u("Card"),$=I("tooltip");return d(),r("div",null,[o(R,null,{title:S(()=>[N(" تفاصيل التوقيت "),o(A,{style:{float:"left"}}),M(o(g,{onClick:s[0]||(s[0]=a=>i.value=!i.value),icon:" fa-solid fa-pen",text:"",rounded:"",class:"p-button-primary p-button-text"},null,512),[[$,{value:"تعديل بيانات التوقيت",fitContent:!0},void 0,{top:!0}]])]),content:S(()=>[D(_).loading?(d(),r("div",L,[e("div",G,[e("div",J,[e("span",K,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",P,[e("span",Q,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",W,[e("span",X,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",Y,[e("span",Z,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",ee,[e("span",te,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])]),e("div",oe,[o(m,{loading:n.value,width:"100%",height:"2rem"},null,8,["loading"])])])])):(d(),r("div",ie,[e("form",{onSubmit:U(h,["prevent"]),disabled:i.value},[e("div",le,[e("div",ae,[e("span",ne,[de,o(T,{inputId:"startTime",modelValue:t.startTime,"onUpdate:modelValue":s[1]||(s[1]=a=>t.startTime=a),timeOnly:!0,disabled:i.value,selectionMode:"single",manualInput:!0,stepMinute:15,"show-seconds":!0,"step-second":60},null,8,["modelValue","disabled"])])]),e("div",re,[e("span",ce,[me,o(T,{inputId:"stopDate",modelValue:t.endTime,"onUpdate:modelValue":s[2]||(s[2]=a=>t.endTime=a),timeOnly:!0,disabled:i.value,selectionMode:"single",manualInput:!0,stepMinute:15,"show-seconds":!0,"step-second":60},null,8,["modelValue","disabled"])])])]),e("div",ue,[e("div",_e,[e("span",pe,[fe,o(F,{modelValue:t.priceForFirstHour,"onUpdate:modelValue":s[3]||(s[3]=a=>t.priceForFirstHour=a),disabled:i.value,suffix:"د.ل"},null,8,["modelValue","disabled"])])]),e("div",he,[e("span",ge,[ve,o(F,{modelValue:t.priceForRemainingHours,"onUpdate:modelValue":s[4]||(s[4]=a=>t.priceForRemainingHours=a),disabled:i.value,suffix:"د.ل"},null,8,["modelValue","disabled"])])])]),i.value?x("",!0):(d(),r("div",be,[o(g,{onClick:h,icon:"fa-solid fa-check",label:"تعديل"}),o(g,{onClick:s[5]||(s[5]=a=>i.value=!i.value),icon:"fa-solid fa-ban",label:"إلغاء التعديل",class:"p-button-danger",style:{"margin-right":"0.5em"}})]))],40,se)]))]),_:1})])}}});const Fe={key:0,class:"border-round border-1 surface-border p-4 surface-card"},Se={class:"grid p-fluid"},ye={class:"field col-12 md:col-6 lg:col-4"},Ve={class:"p-float-label"},we={class:"flex justify-content-between mt-3"},Re=H({__name:"TimeShiftDetailsView",setup(b){const _=w(),i=j(),n=q(()=>i&&i.params&&i.params.id?i.params.id:null),l=k({id:"",startTime:"",endTime:"",priceForFirstHour:"",priceForRemainingHours:""});return v(),E(async()=>{_.loading=!0,C.getById(n.value).then(t=>{l.id=t.data.content.id,l.startTime=t.data.content.startTime,l.endTime=t.data.content.endTime,l.priceForFirstHour=t.data.content.priceForFirstHour,l.priceForRemainingHours=t.data.content.priceForRemainingHours}).catch(t=>{console.log(t)}).finally(()=>{_.loading=!1})}),(t,p)=>{const c=u("Skeleton");return d(),r(y,null,[D(_).loading?(d(),r("div",Fe,[e("div",Se,[(d(),r(y,null,O(9,h=>e("div",ye,[e("span",Ve,[o(c,{width:"100%",height:"2rem"})])])),64))]),o(c,{width:"100%",height:"100px"}),e("div",we,[o(c,{width:"100%",height:"2rem"})])])):x("",!0),(d(),z(Te,{timeShiftData:l,key:l.id},null,8,["timeShiftData"]))],64)}}});export{Re as default};
