import{J as o}from"./index-d7c2489a.js";const t=o(),i={get:async function(e,n,s){return await t.get("/Invoices",{params:{PageNumber:e,PageSize:n,CustomerId:s}})},getById:async function(e){return await t.get(`/Invoices/${e}`)},create:async function(e){return await t.post("/Invoices",e,{headers:{"Content-Type":"multipart/form-data"}})},paid:async function(e,n){return await t.put(`/Invoices/${e}/paid`,n)}};export{i};
