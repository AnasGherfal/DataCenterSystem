import{L as r}from"./index-91db0b6a.js";const s=r(),a={get:async function(e){return await s.get("/Representatives",{params:{CustomerId:e}})},getById:async function(e){return await s.get(`/Representatives/${e}`)},getFile:async function(e,t){return await s.get(`/Representatives/${e}/document/${t}`,{responseType:"arraybuffer"})},create:async function(e){return await s.post("/Representatives",e,{headers:{"Content-Type":"multipart/form-data"}})},edit:async function(e,t){return await s.put(`/Representatives/${e}`,t)},remove:async function(e){return await s.delete(`/Representatives/${e}`)},block:async function(e){return await s.put(`/Representatives/${e}/lock`)},unblock:async function(e){return await s.put(`/Representatives/${e}/unlock`)},approve:async function(e){return await s.put(`/Representatives/${e}/Approve`)},reject:async function(e){return await s.put(`/Representatives/${e}/Reject`)}};export{a as r};
