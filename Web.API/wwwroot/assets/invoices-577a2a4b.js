import{K as h,g as a,E as P,x as y,h as D}from"./index-5db4e683.js";import{i as S}from"./invoice-d4d2af83.js";const I=h("visit",()=>{const s=a([]),i=a(),u=a(!1),v=a(1),r=a(1),d=a(10),f=a(0),l=a(""),t=a(""),n=a(""),o=P();y(()=>o&&o.params&&o.params.id?o.params.id:null),D(async()=>{c()});function g(e,m,p){l.value=e,t.value=m,n.value=p,c()}function c(){u.value=!0,(t.value===void 0&&n.value===void 0||t.value===null&&n.value==null)&&(t.value="",n.value=""),S.get(r.value,d.value,l.value).then(function(e){console.log(e),s.value=e.data.content,i.value=e.data.content,v.value=e.data.totalPages,f.value=e.data.currentPage}).catch(function(e){console.log(e)}).finally(function(){u.value=!1})}return{invoices:s,name:l,startDate:t,endDate:n,getInvoices:c,loading:u,visits:i,totalPages:v,pageNumber:r,pageSize:d,currentPage:f,searchByDateAndName:g}});export{I as u};
