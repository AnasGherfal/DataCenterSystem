import{L as t}from"./index-d19732ab.js";const s=t(),c={get:async function(){return await s.get("/Services")},getById:async function(e){return await s.get(`/Services/${e}`)},create:async function(e){return await s.post("/Services",e)},remove:async function(e){return await s.delete(`/Services/${e}`)},edit:async function(e,n){return await s.put(`/Services/${e}`,n)},block:async function(e){return await s.put(`/Services/${e}/lock`)},unblock:async function(e){return await s.put(`/Services/${e}/unlock`)}};export{c as s};
