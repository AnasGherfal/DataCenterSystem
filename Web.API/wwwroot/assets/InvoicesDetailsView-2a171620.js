import{_ as k}from"./BackButton.vue_vue_type_script_setup_true_lang-2010ef65.js";import{u as N}from"./invoices-0513eb21.js";import{d as S,g as T,D as I,v as M,s as C,h as j,r as l,n as v,w as r,o as n,b as s,l as h,c as m,a as t,t as d,U as P,F as B,m as z,k as V,M as Y}from"./index-e13382f1.js";import{i as F}from"./invoice-10c37e50.js";import{f as H}from"./formatTime-b1b2594d.js";const A={key:0},E={class:"grid p-fluid"},L={class:"field col-12 md:col-6 lg:col-4"},R={class:"p-float-label"},U={class:"field col-12 md:col-6 lg:col-4"},$={class:"p-float-label"},q={class:"field col-12 md:col-6 lg:col-4"},G={class:"p-float-label"},J={class:"field col-12 md:col-6 lg:col-4"},K={class:"p-float-label"},O={class:"field col-12 md:col-6 lg:col-4"},Q={class:"p-float-label"},W={class:"field col-12 md:col-6 lg:col-4"},X={key:1,class:"flex justify-content-between flex-wrap card-container"},Z={class:"align-items-center justify-content-center border-round m-3"},tt={class:"m-0"},et=t("h5",{class:"text-600 m-0"},"مركز ادارة الخدمات والبيانات",-1),it={class:"border-green-500 border-round-md text-center justify-content-center"},st={class:"align-items-center justify-content-center border-round m-3"},ot={style:{"font-size":"medium"}},at={style:{"font-size":"medium"}},nt=t("h1",{class:"text-center"},"الزيارات",-1),lt={key:0,class:"border-round border-1 surface-border p-4 surface-card"},dt={class:"grid p-fluid"},ct={class:"field col-12 md:col-6 lg:col-4"},rt={class:"p-float-label"},mt={class:"flex justify-content-between mt-3"},pt=S({__name:"InvoicesDetailsView",setup(_t){const u=N(),a=T(!1),c=I(),p=M(()=>c&&c.params&&c.params.id?String(c.params.id):""),e=C({id:0,status:0,date:"",description:"",invoiceNo:"",startDate:"",endDate:"",isPaid:!1,subscriptionId:0,visits:[{startTime:"",endTime:"",customerName:"",id:0,visitType:"",notes:"",timeShift:"",totalMin:"",price:0,invoiceId:0,representives:[],companions:[{firstName:"string",lastName:"string",identityNo:"string",identityType:0,jobTitle:"string"}]}]});j(()=>{F.getById(p.value).then(i=>{console.log(i),e.id=i.data.id,e.status=i.data.status,e.date=i.data.date,e.description=i.data.description,e.invoiceNo=i.data.invoiceNo,e.startDate=i.data.startDate,e.endDate=i.data.endDate,e.isPaid=i.data.isPaid,e.subscriptionId=i.data.subscriptionId,e.visits=i.data.visits})});const y=i=>i?"مدفوعه":"غير مدفوعه";function f(i){return Y(i).format("YYYY-MM-DD HH:mm")}return(i,b)=>{const o=l("Skeleton"),w=l("Divider"),_=l("Column"),D=l("DataTable"),x=l("Card");return n(),v(x,null,{title:r(()=>[s(k,{style:{float:"left"}}),h(u).loading?(n(),m("div",A,[t("div",E,[t("div",L,[t("span",R,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])]),t("div",U,[t("span",$,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])]),t("div",q,[t("span",G,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])]),t("div",J,[t("span",K,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])]),t("div",O,[t("span",Q,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])]),t("div",W,[s(o,{loading:a.value,width:"100%",height:"2rem"},null,8,["loading"])])])])):(n(),m("div",X,[t("div",Z,[t("h2",tt,"الفاتورة رقم "+d(e.invoiceNo),1),et,t("div",it,[t("p",{style:P({color:e.isPaid?"green":"red",fontSize:" 20px"}),class:"text-md"},d(y(e.isPaid)),5)])]),t("div",st,[t("p",ot," تاريخ الانشاء : "+d(f(e.startDate)),1),t("p",at," تاريخ الاستحقاق : "+d(f(e.endDate)),1)])]))]),content:r(()=>[s(w),nt,h(u).loading?(n(),m("div",lt,[t("div",dt,[(n(),m(B,null,z(9,g=>t("div",ct,[t("span",rt,[s(o,{width:"100%",height:"2rem"})])])),64))]),s(o,{width:"100%",height:"100px"}),t("div",mt,[s(o,{width:"100%",height:"2rem"})])])):(n(),v(D,{key:1,value:e.visits},{default:r(()=>[s(_,{style:{"min-width":"2rem"},field:"totalMin",header:" الوقت"},{body:r(({data:g})=>[V(d(h(H)(g.totalMin)),1)]),_:1}),s(_,{style:{"text-align":"right","min-width":"2rem"},field:"price",header:"السعر",tableStyle:"min-width: 2rem"}),s(_,{style:{"min-width":"2rem"},field:"notes",header:"ملحوظات"})]),_:1},8,["value"]))]),_:1})}}});export{pt as default};
