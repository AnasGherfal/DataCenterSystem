import{d as D,L as V,g as m,u as w,v as N,x as r,r as f,i as z,o as k,c as _,b as i,w as y,a as d,k as g,t as F,j as T,q as j,Y as q,F as E}from"./index-e53cc164.js";const H={class:"confirmation-content"},I=d("i",{class:"pi pi-exclamation-triangle mr-3",style:{"font-size":"2rem"}},null,-1),S={key:0},Y=D({__name:"LockButton",props:{id:{},name:{},status:{},typeLock:{}},setup(C){const t=C,v=V(),l=m(!1),a=m(!1),u=w(),o=N({value:t.status}),x=r(()=>o.value===2?"fa-solid fa-lock":"fa-solid fa-lock-open"),B=r(()=>o.value===2?"green":"red"),p=r(()=>o.value===2?"الغاء تقييد":"تقييد ");function $(){l.value=!0,v.put(`${t.typeLock}/${t.id}/lock`).then(s=>{u.add({severity:"success",summary:"رسالة تأكيد",detail:s.data.msg,life:3e3}),o.value=2,a.value=!1,l.value=!1})}function b(){l.value=!0,v.put(`${t.typeLock}/${t.id}/unlock`).then(s=>{u.add({severity:"success",summary:"رسالة تأكيد",detail:s.data.msg,life:3e3}),o.value=1}).catch(s=>{u.add({severity:"error",summary:"رسالة خطأ",detail:s.data.msg,life:3e3})}).finally(()=>{a.value=!1,l.value=!1})}return(s,e)=>{const c=f("Button"),L=f("Dialog"),h=z("tooltip");return k(),_(E,null,[i(L,{visible:a.value,"onUpdate:visible":e[2]||(e[2]=n=>a.value=n),style:{width:"450px"},header:"Confirm",modal:!0},{footer:y(()=>[i(c,{label:"نعم",icon:"pi pi-check",text:"",loading:l.value,onClick:e[0]||(e[0]=n=>o.value===2?b():$())},null,8,["loading"]),i(c,{label:"لا",icon:"pi pi-times",text:"",onClick:e[1]||(e[1]=n=>a.value=!1)})]),default:y(()=>[d("div",H,[I,t.id?(k(),_("span",S,[g("هل انت متأكد من "),d("b",null,F(`${p.value} ${t.name}`),1),g(" ؟")])):T("",!0)])]),_:1},8,["visible"]),j(i(c,{onClick:e[3]||(e[3]=n=>a.value=!0),icon:x.value,class:q(B.value),text:""},null,8,["icon","class"]),[[h,{value:p.value,fitContent:!0}]])],64)}}});export{Y as _};
