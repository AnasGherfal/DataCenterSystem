import{K as l,g as u}from"./index-1dcc6fbd.js";const i=l("Status",()=>{const r=u([{value:1,label:"نشط"},{value:2,label:"مقيد"},{value:3,label:"طلب"},{value:4,label:"مرفوض"},{value:5,label:"ملغي"}]),s=e=>{switch(a(e)){case"نشط":return"success";case"مقيد":return"danger";case"طلب":return"warning";case"مرفوض":return"secondary";case"ملغي":return"help"}},a=e=>{if(e=="1")return"نشط";if(e=="2")return"مقيد";if(e=="3")return"طلب";if(e=="4")return"مرفوض";if(e=="5")return"ملغي"};return{getSeverity:s,getSelectedStatusLabel:e=>{const t=r.value.find(n=>n.value===e);return t?t.label:""}}});export{i as u};
