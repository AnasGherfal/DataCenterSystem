import{L as y,K as v,g as o,h as w}from"./index-bc1988a6.js";const n=y(),l={get:async function(t,s,a,i){return await n.get("/Visits",{params:{CustomerId:a,SubscriptionId:i}})},getPages:async function(t,s){return await n.get("/Visits",{params:{PageNumber:t,PageSize:s}})},getTypes:async function(){return await n.get("/VisitTypes")},getById:async function(t){return await n.get(`/Visits/${t}`)},create:async function(t){return await n.post("/Visits",t)},remove:async function(t){return await n.delete(`/Visits/${t}`)},start:async function(t,s){return await n.put(`/Visits/${t}/start`,{startTime:s})},stop:async function(t,s){return await n.put(`/Visits/${t}/stop`,{endTime:s})},sign:async function(t,s){return await n.put(`/Visits/${t}/sign`,s)}},d=v("visit",()=>{const t=o(),s=o([{}]),a=o(),i=o(!0),r=o(1),c=o(1),u=o(10),p=o(0);w(async()=>{await g(),await f()});async function g(){await l.getPages(c.value,u.value).then(function(e){t.value=e.data.content,r.value=e.data.totalPages,p.value=e.data.currentPage}).catch(e=>{console.log(e)}).finally(()=>{i.value=!1})}async function f(){await l.getTypes().then(function(e){a.value=e.data.content}).catch(function(e){console.log(e)})}return{visits:t,getVisits:g,representatives:s,loading:i,totalPages:r,pageNumber:c,currentPage:p,pageSize:u,visitReasons:a}});export{d as u,l as v};
