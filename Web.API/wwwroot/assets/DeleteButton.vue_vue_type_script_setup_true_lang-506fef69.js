import{d as b,g as s,r,i as C,o as d,c as u,q as k,b as n,w as p,a,k as c,t as x,j as y,F as B}from"./index-d19732ab.js";const D={class:"confirmation-content"},w=a("i",{class:"pi pi-exclamation-triangle mr-3",style:{"font-size":"2rem",color:"red"}},null,-1),N={key:0},$=b({__name:"DeleteButton",props:{name:{},id:{},type:{}},emits:["submit"],setup(m){const v=s(!1),t=s(!1),l=m;return(f,e)=>{const i=r("Button"),g=r("Dialog"),_=C("tooltip");return d(),u(B,null,[k(n(i,{onClick:e[0]||(e[0]=o=>t.value=!0),icon:"fa-solid fa-trash",severity:"danger",text:"",rounded:"","aria-label":"Cancel"},null,512),[[_,{value:"حذف",fitContent:!0},void 0,{top:!0}]]),n(g,{visible:t.value,"onUpdate:visible":e[3]||(e[3]=o=>t.value=o),style:{width:"450px"},header:"تأكيد",modal:!0},{footer:p(()=>[n(i,{label:"نعم",icon:"pi pi-check",text:"",onClick:e[1]||(e[1]=o=>{f.$emit("submit"),t.value=!1}),loading:v.value},null,8,["loading"]),n(i,{label:"لا",icon:"pi pi-times",text:"",onClick:e[2]||(e[2]=o=>t.value=!1)})]),default:p(()=>[a("div",D,[w,l.name?(d(),u("span",N,[c("هل انت متأكد من حذف "),a("b",null,x(l.name),1),c(" ؟")])):y("",!0)])]),_:1},8,["visible"])],64)}}});export{$ as _};