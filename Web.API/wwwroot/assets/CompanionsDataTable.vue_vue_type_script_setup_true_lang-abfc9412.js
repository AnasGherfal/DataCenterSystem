import{d as b,g as c,r as d,i as y,o as _,n as h,w as l,b as t,k as C,t as v,q as T,j as x}from"./index-365e7f1b.js";const k=b({__name:"CompanionsDataTable",props:["compList","editable","compId"],setup(n){const i=n;c(!1),c();const r=o=>{switch(o){case 1:return"اثبات هويه";case 2:return"جواز سفر"}};function p(o){const a=i.compList.findIndex(e=>e.identityNo===o.data.identityNo);console.log(a),a!==-1&&i.compList.splice(a,1)}return(o,a)=>{const e=d("Column"),m=d("Button"),u=d("DataTable"),f=y("tooltip");return n.compList.length>0?(_(),h(u,{key:0,value:n.compList},{default:l(()=>[t(e,{field:"firstName",header:"اسم المرافق"}),t(e,{field:"lastName",header:"لقب المرافق"}),t(e,{field:"jobTitle",header:"صفة المرافق"}),t(e,{field:"identityType",header:"نوع الاثبات"},{body:l(({data:s})=>[C(v(r(s.identityType)),1)]),_:1}),t(e,{field:"identityNo",header:"رقم الاثبات"}),t(e,{header:"  الاجراءات "},{body:l(s=>[T(t(m,{class:"btn btn-danger",onClick:N=>p(s),icon:"fa-solid fa-trash",severity:"danger",text:"",rounded:"","aria-label":"Cancel",disabled:i.editable},null,8,["onClick","disabled"]),[[f,{value:"حذف",fitContent:!0},void 0,{top:!0}]])]),_:1})]),_:1},8,["value"])):x("",!0)}}});export{k as _};
