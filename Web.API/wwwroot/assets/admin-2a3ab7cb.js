import{K as g,g as t,s as r}from"./index-24cf2b03.js";const m=g("useAdminStore",()=>{const e=t(),n=t(!0),l=t(null),o=t(1),c=t(1),s=t(10),u=t(0);return{userData:e,getAdmins:()=>{r.get(c.value,s.value).then(function(a){console.log(a),e.value=a.data.content,o.value=a.data.totalPages,u.value=a.data.currentPage,n.value=!1}).catch(function(a){console.log(a)})},loading:n,data:l,totalPages:o,pageNumber:c,currentPage:u,pageSize:s}});export{m as u};
