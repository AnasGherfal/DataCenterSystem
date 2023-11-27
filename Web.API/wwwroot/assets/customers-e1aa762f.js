import{L as r}from"./index-5abe41cf.js";const t=r(),u={create:async function(e){return await t.post("/Customers",e,{headers:{"Content-Type":"multipart/form-data"}})},get:async function(e,s,n){return await t.get("/Customers",{params:{PageNumber:e,PageSize:s,CustomerName:n}})},getById:async function(e){return await t.get(`/Customers/${e}`)},edit:async function(e,s){return await t.put(`/customers/${e}`,s)},remove:async function(e){return await t.delete(`/Customers/${e}`)},getDocument:async function(e,s){return await t.get(`/Customers/${e}/document/${s}`,{responseType:"arraybuffer"})},editDocument:async function(e,s,n){return await t.put(`/Customers/${e}/document/${s}`,n)},createDocument:async function(e,s){return await t.post(`/Customers/${e}/document`,s,{headers:{"Content-Type":"multipart/form-data"}})},block:async function(e){return await t.put(`/Customers/${e}/lock`)},unblock:async function(e){return await t.put(`/Customers/${e}/unlock`)}};export{u as c};
