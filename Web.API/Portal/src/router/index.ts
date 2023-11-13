import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/Home/HomeView.vue";
import LoginPage from "../views/LoginPage.vue";
// import { useAuthStore } from "../stores/auth";
import NProgress from "nprogress";
import "nprogress/nprogress.css";

NProgress.configure({ showSpinner: false });

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

router.beforeEach(async (to, from, next) => {
  const token = localStorage.getItem("token");

  // If there is no token and the route is not the login page, redirect to the login page
  if (!token && to.name !== "loginPage") {
    return next("/Login");
  }

  // If there is a token and the route is the login page, redirect to the home page
  if (token && to.name === "loginPage") {
    return next("/");
  }

  // Continue to the requested route
  return next();
});

export default router;
