import{_ as K}from"./BackButton.vue_vue_type_script_setup_true_lang-750e6b6c.js";import{d as j,g as p,h as M,o as n,c,a as e,U as W,t as d,X as Y,_ as Z,u as ee,D as te,s as se,v as oe,V,M as z,r as b,i as ne,n as D,w as x,k as C,b as a,j as N,F as A,m as T,q as ae,f as ie}from"./index-98f53cd7.js";import{u as le}from"./subscriptions-35d9bdea.js";import{s as ce}from"./service-f0ecf9d7.js";const re={class:"progress"},de={class:"bar-overflow"},ue=j({__name:"CircularProgressBar",props:["percentage"],setup(O){const{percentage:m}=O,u=p(0),k=p(""),f=p("");M(()=>{h()});const h=()=>{const l=m,y=120;let s=0;const B=setInterval(()=>{if(s>=y)clearInterval(B);else{const S=_(s/y);s+=1;const R=Math.round(S*l);u.value=R;const I=45+R*.49;k.value=`transform: rotate(${I}deg);`,u.value<=30?f.value="border-bottom-color: red; border-right-color: red;":f.value="border-bottom-color: navy; border-right-color: navy;"}},25)};function _(l){return l<.5?2*l*l:-1+(4-2*l)*l}return(l,y)=>(n(),c("div",re,[e("div",de,[e("div",{class:"bar",style:W([k.value,f.value])},null,4)]),e("span",{class:Y(["percentage-text",{"red-text":u.value<=30}])},d(u.value),3)]))}});const _e=Z(ue,[["__scopeId","data-v-1c8c1848"]]),me={key:0},ve=e("div",{class:"warning-message"},[e("div",{class:"warning-message-icon"}),e("div",{class:"warning-message-text"}," هذا الاشتراك مقفل لا يمكن التجديد ")],-1),pe=[ve],fe={class:"grid p-fluid"},he={key:0},ye={class:"grid p-fluid"},ge={key:1,class:"flex-1",style:{"text-align":"center",width:"20rem"}},be={style:{display:"inline-block"}},xe=e("h2",null,"الأيام المتبقية",-1),ke={key:0},we=e("h3",{class:"text-red-600"},"هذا الاشتراك قارب على الانتهاء",-1),De={key:0},Ce=e("h5",{class:"text-red-600",style:{"margin-bottom":"5px"}}," اضغط على الأيقونة للتجديد ",-1),Re={key:1},Pe=e("h3",{class:"text-red-600"},"هذا الاشتراك انتهى",-1),Ne=e("h5",{class:"text-red-600",style:{"margin-bottom":"5px"}}," اضغط على الأيقونة للتجديد ",-1),Be=e("div",{class:"confirmation-content"},[e("i",{class:"fa-solid fa-repeat mr-3",style:{color:"green"}}),e("span",null,"هل انت متأكد من تجديد الاشتراك؟")],-1),Se={class:"grid p-fluid"},Ie={class:"field col-4 md:col-4 lg:col-6"},Fe={class:"file-input-label",for:"fileInput"},Ve={class:"file-input-content"},Oe=e("div",{class:"file-input-icon"},null,-1),$e={class:"file-input-text"},Ue=e("i",{class:"pi pi-upload"},null,-1),Le={class:"flex-1"},ze=e("h3",{style:{margin:"0"}},"اسم العميل",-1),Ae={key:1},Te={style:{margin:"0",display:"inline"}},je=e("h3",{style:{margin:"0"}},"اسم الباقة",-1),Me={key:3},Ee={style:{margin:"0",display:"inline"}},qe={style:{"height-min":"450px"}},Qe={class:"justify-content-between"},Xe={class:"block text-center text-3xl font-bold"},Ge={class:"text-center mb-3"},He={class:"text-center font-semibold text-4xl"},Je=e("span",{class:"text-xs mr-1 text-blue-800"},"د.ل",-1),Ke=e("p",{class:"font-bold"},"خواص هذه الباقة :",-1),We={style:{direction:"ltr"},class:"text-center font-bold text-sm"},Ye=e("i",{class:"text-green-600 fa-solid fa-circle-check mr-2"},null,-1),Ze=e("span",{class:"font-medium"},null,-1),et={class:"text-center font-semibold text-sm"},tt=e("i",{class:"text-green-600 fa-solid fa-circle-check mr-2"},null,-1),st=e("span",{class:"font-medium"},null,-1),ot={style:{direction:"ltr"},class:"text-center font-bold text-sm"},nt=e("i",{class:"text-green-600 fa-solid fa-circle-check mr-1"},null,-1),at=e("span",{class:"text-green-500 font-medium"},null,-1),it=e("h4",{style:{margin:"0"}},"عدد الزيارات المتبقية بالساعة",-1),lt=e("h4",{style:{margin:"0","margin-bottom":"2rem"}},"عقد الاشتراك :",-1),ct={class:"field col-4 md:col-4 lg:col-6"},rt={class:""},dt=e("label",{for:"secondaryPhone"},"اسم الملف",-1),ut={class:"file-actions field col-4 md:col-4 lg:col-6"},_t=e("div",{class:"card"},null,-1),ht=j({__name:"SubscriptionsDetails",setup(O){const m=p(!1),u=p(!1),k=p(!1),f=p(),h=p(!1),_=p();le();const l=ee(),y=te(),s=se({id:"",status:0,serviceName:"",customerName:"",startDate:"",endDate:"",subscriptionFileId:null,daysRemaining:0,monthlyVisits:0,visits:[],file:[{createdOn:"",fileType:0,isActive:!0,id:""}],totalPrice:0,createdOn:""}),B=oe(()=>y&&y.params&&y.params.id?String(y.params.id):"");M(async()=>{S()});function S(){m.value=!0,V.getById(B.value).then(function(r){const t=r.data.content;s.id=t.id,s.status=t.status,s.customerName=t.customerName,s.endDate=t.endDate,s.startDate=t.startDate,s.serviceName=t.serviceName,s.file=t.files,s.createdOn=t.createdOn,s.totalPrice=t.totalPrice;const i=z(t.endDate).diff(z(),"days");s.daysRemaining=Math.max(0,i)}).then(function(){ce.get().then(function(r){_.value=r.data.content.find(t=>t.name===s.serviceName)})}).finally(()=>{m.value=!1})}async function R(r){const t=r.target.files[0];f.value=t}const I=()=>{u.value=!0;const r=new FormData;r.append("file",f.value),V.renew(s.id,r).then(t=>{u.value=!1,l.add({severity:"success",summary:"تم التجديد",detail:t,life:3e3}),h.value=!1,ie.go(-1)}).catch(function(t){l.add({severity:"error",summary:t.response.data.msg||"حدثة مشكلة",detail:t,life:3e3}),u.value=!1}).finally(()=>{u.value=!1})};function E(){k.value=!k.value}const q=async(r,t)=>{try{const i=await V.getFile(r,t);if(i){const w=new Blob([i.data.content],{type:"application/octet-stream"}),v=URL.createObjectURL(w),g=document.createElement("a");g.href=v,g.download="downloaded_file.png",document.body.appendChild(g),g.click(),document.body.removeChild(g),URL.revokeObjectURL(v)}else console.error("Invalid file response")}catch(i){console.error("Error downloading file:",i),l.add({severity:"error",summary:"خطأ",detail:"حدث خطأ أثناء تحميل الملف",life:3e3})}};return(r,t)=>{const i=b("Divider"),w=b("Skeleton"),v=b("Button"),g=b("Dialog"),Q=b("RouterLink"),X=b("ProgressBar"),G=b("InputText"),H=b("card"),J=ne("tooltip");return n(),D(H,null,{title:x(()=>[C(" بيانات الاشتراك "),a(K,{style:{float:"left"}}),a(i)]),content:x(()=>[s.status==2?(n(),c("div",me,pe)):N("",!0),e("div",fe,[m.value?(n(),c("div",he,[e("div",ye,[(n(),c(A,null,T(4,o=>e("div",{key:o,class:"ml-3 mb-2"},[e("span",null,[a(w,{width:"15rem",height:"20rem"})])])),64))])])):(n(),c("div",ge,[e("div",be,[a(_e,{percentage:s.daysRemaining},null,8,["percentage"])]),e("div",null,[xe,s.daysRemaining<30&&s.daysRemaining!==0&&s.daysRemaining>0?(n(),c("div",ke,[we,s.status!==2?(n(),c("div",De,[Ce,a(v,{icon:"fa-solid fa-repeat",severity:"danger",text:"",rounded:"","aria-label":"Cancel",onClick:t[0]||(t[0]=o=>h.value=!0)})])):N("",!0)])):s.daysRemaining<=0&&s.status!==2?(n(),c("div",Re,[Pe,Ne,a(v,{icon:"fa-solid fa-repeat",severity:"danger",text:"",rounded:"","aria-label":"Cancel",onClick:t[1]||(t[1]=o=>h.value=!0)})])):N("",!0)]),a(g,{visible:h.value,"onUpdate:visible":t[4]||(t[4]=o=>h.value=o),style:{width:"450px",height:"auto",overflow:"hidden"},header:"تجديد الاشتراك",modal:!0},{footer:x(()=>[a(v,{label:"نعم",icon:"pi pi-check",loading:u.value,text:"",onClick:I},null,8,["loading"]),a(v,{label:"لا",icon:"pi pi-times",text:"",onClick:t[3]||(t[3]=o=>h.value=!1)})]),default:x(()=>[Be,e("div",Se,[e("div",Ie,[e("label",Fe,[e("div",Ve,[Oe,e("div",$e,[Ue,C(" "+d(f.value?f.value.name:"ارفق ملف 1"),1)])]),e("input",{id:"fileInput",style:{display:"none"},type:"file",onChange:t[2]||(t[2]=o=>R(o)),accept:"*"},null,32)])])])]),_:1},8,["visible"])])),a(i,{class:"p-divider-solid",layout:"vertical"}),e("div",Le,[ze,m.value?(n(),D(w,{key:0,width:"100%",height:"1rem"})):(n(),c("span",Ae,[e("p",Te,d(s.customerName),1),(n(),D(Q,{key:s.customerName,to:"/subscriptionsRecord/SubscriptionsDetaView/1",style:{"text-decoration":"none"}}))])),a(i,{class:"p-divider-solid",layout:"horizontal"}),je,m.value?(n(),D(w,{key:2,width:"50%",height:"1rem"})):(n(),c("span",Me,[e("p",Ee,d(s.serviceName),1),ae(a(v,{onClick:E,icon:"fa-solid fa-circle-info",severity:"info",text:"",rounded:"",style:{display:"flex",float:"left",width:"2rem",height:"1rem"}},null,512),[[J,{value:"التفاصيل",fitContent:!0}]]),a(g,{visible:k.value,"onUpdate:visible":t[5]||(t[5]=o=>k.value=o),modal:!0},{default:x(()=>{var o,P,F,$,U,L;return[e("div",qe,[e("div",Qe,[e("div",null,[e("span",Xe,d((o=_.value)==null?void 0:o.name),1),e("div",Ge," عدد الزيارات المتاحة في هذه الباقة في الشهر : "+d((P=_.value)==null?void 0:P.monthlyVisits),1),e("div",He,[C(d((F=_.value)==null?void 0:F.price),1),Je])]),a(i)]),Ke,e("div",We,[Ye,e("span",null,"(Acp Port): "+d(($=_.value)==null?void 0:$.acpPort),1),Ze]),e("div",et,[e("span",null,"DNS : "+d((U=_.value)==null?void 0:U.dns),1),tt,st]),e("div",ot,[nt,e("span",null," (Amount Of Power) : "+d((L=_.value)==null?void 0:L.amountOfPower),1),at])])]}),_:1},8,["visible"])])),a(i,{class:"p-divider-solid",layout:"horizontal"}),it,m.value?(n(),D(w,{key:4,width:"50%",height:"1rem"})):(n(),D(X,{key:5,class:"mt-2",value:20},{default:x(()=>{var o;return[C(d((o=_.value)==null?void 0:o.monthlyVisits),1)]}),_:1})),a(i,{class:"p-divider-solid",layout:"horizontal"}),lt,m.value?(n(),D(w,{key:6,width:"50%",height:"1rem"})):N("",!0),(n(!0),c(A,null,T(s.file,(o,P)=>(n(),c("div",{key:P,class:"grid p-fluid align-items-center"},[e("div",ct,[e("span",rt,[dt,a(G,{class:"p-inputtext p-component",value:o.id,disabled:!0},null,8,["value"])])]),e("div",ut,[a(v,{onClick:F=>q(s.id,o.id),icon:"fa-solid fa-download",class:"p-button-text p-button-info"},{default:x(()=>[C(" تحميل ")]),_:2},1032,["onClick"])])]))),128))])])]),footer:x(()=>[_t]),_:1})}}});export{ht as default};
