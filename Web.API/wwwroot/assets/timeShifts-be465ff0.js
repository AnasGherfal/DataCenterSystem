import{L as n}from"./index-102baab4.js";const t=n(),r={get:async function(){return await t.get("/TimeShifts")},getById:async function(e){return await t.get(`/TimeShifts/${e}`)},create:async function(e){return await t.post("/TimeShifts",e)},edit:async function(e,s){return await t.put(`/TimeShifts/${e}`,s)},remove:async function(e){return await t.delete(`/TimeShifts/${e}`)}};export{r as t};
