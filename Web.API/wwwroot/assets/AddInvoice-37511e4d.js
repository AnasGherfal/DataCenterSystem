import{d as E,g as l,v as W,x as G,y as X,u as j,N as v,h as J,G as K,r as u,c,b as n,w,o as d,k as O,a as o,F,m as T,t as M,l as N,z as P,A as h,B as $,X as Q,f as R,W as Z}from"./index-434e95c2.js";import{u as ee}from"./customers-d53ce529.js";import{u as te}from"./invoices-37325176.js";import{i as oe}from"./invoice-082b1acf.js";import{_ as se}from"./BackButton.vue_vue_type_script_setup_true_lang-2c968fd6.js";import{c as ae}from"./customers-f4702f24.js";const le=["onSubmit"],ne={class:"grid p-fluid"},ie={class:"field col-12 md:col-6 lg:col-4"},ue={class:"p-float-label"},re=o("label",{for:"customerName"},"العملاء",-1),ce={class:"field col-12 md:col-6 lg:col-4"},de={class:"p-float-label"},me=o("label",{for:"startDate"},"تاريخ بداية ",-1),pe={style:{height:"2px"}},fe={class:"field col-12 md:col-6 lg:col-4"},_e={class:"p-float-label"},ge=o("label",{for:"endtDate"},"تاريخ انتهاء ",-1),ve={style:{height:"2px"}},he={class:"field col-12 md:col-6 lg:col-4"},Ve={class:"p-float-label"},ye=o("label",{for:"notes"}," التفاصيل",-1),Me=E({__name:"AddInvoice",setup(Ie){ee(),te();const r=l(),V=l(!1);l(!1);const m=l([]),t=W({Notes:"",IncludeVisitsFrom:"",IncludeVisitsTo:"",CustomerId:null}),B=G(()=>({IncludeVisitsFrom:{required:h.withMessage("  الحقل مطلوب",$)},IncludeVisitsTo:{required:h.withMessage(" الحقل مطلوب",$),minValue:h.withMessage("تاريخ انتهاء الزياره يجب ان يكون بعد تاريخ البدايه",Q(t.IncludeVisitsFrom))}})),p=X(B,t),y=j(),f=l(new Date),I=l(new Date),k=new Date(v(t.IncludeVisitsFrom).format("hh:mm a"));l(k);const x=()=>{f.value>I.value&&(I.value=f.value)};l();const Y=l(),_=l();J(()=>{S()}),K(r,s=>{s&&q(s.id)});const b=async()=>{if(await p.value.$validate()){const e=new FormData;e.append("CustomerId",r.value.id),e.append("IncludeVisitsFrom",v(t.IncludeVisitsFrom).format("YYYY-MM-DD HH:mm:ss")),e.append("IncludeVisitsTo",v(t.IncludeVisitsTo).format("YYYY-MM-DD HH:mm:ss")),e.append("Notes",t.Notes);for(const[i,g]of e.entries())console.log(`${i}: ${g}`);oe.create(e).then(function(i){y.add({severity:"success",summary:"تمت الاضافه",detail:i.data.msg,life:3e3}),R.go(-1)}).catch(function(i){y.add({severity:"error",summary:"رسالة فشل",detail:i.response.data.msg||"هنالك مشكلة في الوصول",life:3e3})})}else console.log("not valid")},A=()=>{r.value="",t.Notes="",t.IncludeVisitsFrom="",t.IncludeVisitsTo="",t.CustomerId=null};async function S(){await ae.get(1,1e3,"").then(function(s){m.value=s.data.content}).catch(function(s){console.log(s)}).finally(()=>{V.value=!1})}const q=s=>{Z.get(1,50,s).then(function(e){Y.value=e.data.content}).catch(function(e){console.log(e)})},z=s=>{setTimeout(()=>{s.query.trim().length?_.value=m.value.filter(e=>e.name.toLowerCase().startsWith(s.query.toLowerCase())):_.value=[...m.value]},250)};return(s,e)=>{const i=u("Divider"),g=u("AutoComplete"),C=u("Calendar"),H=u("Textarea"),D=u("Button"),L=u("Toast"),U=u("Card");return d(),c("div",null,[n(U,null,{title:w(()=>[O(" إضافة فاتورة "),n(se,{style:{float:"left"}}),n(i,{layout:"horizontal"})]),content:w(()=>[o("form",{onSubmit:P(b,["prevent"])},[o("div",ne,[o("div",ie,[o("span",ue,[n(g,{modelValue:r.value,"onUpdate:modelValue":e[0]||(e[0]=a=>r.value=a),optionLabel:"name",suggestions:_.value,onComplete:z},null,8,["modelValue","suggestions"]),re])]),o("div",ce,[o("span",de,[n(C,{inputId:"startDate",modelValue:t.IncludeVisitsFrom,"onUpdate:modelValue":e[1]||(e[1]=a=>t.IncludeVisitsFrom=a),dateFormat:"yy/mm/dd",selectionMode:"single",showButtonBar:!0,manualInput:!1,onChange:x},null,8,["modelValue"]),me,o("div",pe,[(d(!0),c(F,null,T(N(p).IncludeVisitsFrom.$errors,a=>(d(),c("span",{key:a.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},M(a.$message),1))),128))])])]),o("div",fe,[o("span",_e,[n(C,{inputId:"endDate",modelValue:t.IncludeVisitsTo,"onUpdate:modelValue":e[2]||(e[2]=a=>t.IncludeVisitsTo=a),dateFormat:"yy/mm/dd",selectionMode:"single",minDate:f.value,showButtonBar:!0,manualInput:!1},null,8,["modelValue","minDate"]),ge,o("div",ve,[(d(!0),c(F,null,T(N(p).IncludeVisitsTo.$errors,a=>(d(),c("span",{key:a.$uid,style:{color:"red","font-weight":"bold","font-size":"small"}},M(a.$message),1))),128))])])]),o("div",he,[o("span",Ve,[n(H,{modelValue:t.Notes,"onUpdate:modelValue":e[3]||(e[3]=a=>t.Notes=a),rows:"5",cols:"77"},null,8,["modelValue"]),ye])])]),n(D,{onClick:b,class:"p-button-primry",icon:"fa-solid fa-plus",label:"إضافة",type:"submit",loading:V.value},null,8,["loading"]),n(D,{onClick:A,icon:"fa-solid fa-delete-left",label:"مسح",class:"p-button-danger",style:{"margin-right":"0.5em"}}),n(L,{position:"bottom-right"})],40,le)]),_:1})])}}});export{Me as default};
