import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/Home/HomeView.vue";
import LoginPage from "../views/LoginPage.vue";
// import { useAuthStore } from "../stores/auth";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import { ref } from "vue";

NProgress.configure({ showSpinner: false });
const loading = ref(false);

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/Login",
      name: "loginPage",
      component: LoginPage,
      meta: {
        guest: true,
      },
    },

    {
      path: "/",
      name: "home",
      component: HomeView,
      meta: {
        guest: false,
      },
    },
    {
      path: "/representativesRecord",
      name: "representativesRecord",
      component: () =>
        import("../views/Representatives/RecordRepresentatives.vue"),
      children: [
        {
          path: "DetailsRepresentatives/:id",
          component: () =>
            import("../views/Representatives/DetailsRepresentatives.vue"),
        },
        {
          path: "CreateRepresentative/",
          component: () =>
            import("../views/Representatives/CreateRepresentative.vue"),
        },
      ],
    },

    {
      path: "/visitsRecord",
      name: "visitsRecord",
      component: () => import("../views/Visits/RecordVisits.vue"),
      children:[
        {
          path: "requestVisit/",
          component: () =>
            import("../views/Visits/RequestVisit.vue"),
        },
        
      ]
    },
    {
      path: "/subscriptionsRecord",
      name: "subscriptionsRecord",
      component: () => import("../views/Subscriptions/RecordSubscriptions.vue"),
      children: [
        {
          path: "SubscriptionsDetails/:id",
          component: () =>
            import("../views/Subscriptions/SubscriptionsDetails.vue"),
        },
      ],
    },

    {
      path: "/:pathMatch(.*)*",
      name: "NotFound",
      component: () => import("../views/NotFound.vue"),
    },

    // {
    //   path: "/invoices",
    //   name: "Invoices",
    //   component: () => import("../views/Invoices/InvoicesRecord.vue"),
    //   children: [
    //     {
    //       path: "invoicesDetails/:id",
    //       props: true,
    //       component: () => import("../views/Invoices/InvoicesDetailsView.vue"),
    //     },

    //   ],
    // },
  ],
});

//beforeeach route login //-----------------------

// router.beforeEach(async (to, from, next) => {
//   // Check if the user is authorized/logged in (you can use your own logic here)
//   const authorized = useAuthStore();

//   // if the route is guest only then let the user continue
//   if (to.meta.guest) {
//     document.getElementById("InitScreenDOM")?.remove();
//     return next();
//   }

//   if (!authorized.userData) {
//     const res = await authorized.getProfile();
//     document.getElementById("InitScreenDOM")?.remove();

//     if (res) {
//       // the user is logged in and trying to access the login page then redirect to dashboard
//       if (to.meta.guest) {
//         return next("/dashboard");
//       }

//       // continue to the route
//       return next();
//     }

//     // if the user is not logged in and the route is not guest only then redirect to login
//     if (to.meta.guest) {
//       return next();
//     }

//     return next("/Login");
//   }

//   // otherwise continue to the route
//   document.getElementById("InitScreenDOM")?.remove();
//   next();

//   // Scroll page to top on every route change
//   setTimeout(() => {
//     window.scrollTo(0, 0);
//   }, 100);
// });
// router.beforeResolve((to, from, next) => {
//   // If this isn't an initial page load.
//   NProgress.start();
//   if (to.name) {
//     loading.value = true;
//     // Start the route progress bar.
//   }
//   next();
// });

// router.afterEach(() => {
//   loading.value = false;
//   // Complete the animation of the route progress bar.
//   NProgress.done();
// });

export default router;
