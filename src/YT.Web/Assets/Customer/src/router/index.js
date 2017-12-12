import Vue from "vue";
import Router from "vue-router";
Vue.use(Router);
export const loginRoutes = [{
  path: "/",
  name: "container",
  component: r => require(["@/components/Container"], r),
  redirect: "/login",
  children: [{
      path: "/login",
      name: "login",
      component: r => require(["views/first/login"], r)
    },
    {
      path: "/register",
      name: "register",
      component: r => require(["views/first/register"], r)
    },
    {
      path: "/reset",
      name: "reset",
      component: r => require(["views/first/reset"], r)
    },
    {
      path: "/message",
      name: "message",
      component: r => require(["views/first/message"], r)
    },
    {
      path: "/resetpassword",
      name: "resetpassword",
      component: r => require(["views/first/resetpassword"], r)
    }
  ]
}];

let dashboard = [];

dashboard = [{
    path: "/control",
    name: "dashboard",
    component: r => require(["@/components/Dashboard"], r),
    redirect: "/infos",
    children: [{
        path: "/dashboard",
        name: "dashboard",
        component: r => require(["views/dashboard/index"], r)
      },
      // 产品详情
      {
        path: "/detail",
        name: "detail",
        component: r => require(["views/dashboard/detail"], r)
      },
      // 客户服务详情
      {
        path: "/adsence",
        name: "adsence",
        component: r => require(["views/dashboard/adsence"], r)
      },
      {
        path: "/infos",
        name: "infos",
        component: r => require(["views/dashboard/infos"], r)
      },
      {
        path: "/info/:id",
        name: "info",
        component: r => require(["views/dashboard/adsenceinfo"], r)
      }

    ]
  },
  // 支持
  {
    path: "/support",
    name: "support",
    component: r => require(["views/dashboard/support"], r)
  },
  // 订单
  {
    path: "/order",
    name: "order",
    component: r => require(["views/dashboard/order"], r)
  },
  // 取消订单
  {
    path: "/cancelorder",
    name: "cancelorder",
    component: r => require(["views/dashboard/cancelorder"], r)
  },
  // 取消订单审核
  {
    path: "/auditing",
    name: "auditing",
    component: r => require(["views/dashboard/auditing"], r)
  },
  // 客户资料信息
  {
    path: "/info",
    name: "info",
    component: r => require(["views/dashboard/info"], r)
  },
  // 用户设置
  {
    path: "/setting",
    name: "setting",
    component: r => require(["views/dashboard/setting"], r)
  },
  // 充值信息
  {
    path: "/charge",
    name: "charge",
    component: r => require(["views/dashboard/charge"], r)
  },
  // 结算明细
  {
    path: "/statistical",
    name: "statistical",
    component: r => require(["views/dashboard/statistical"], r)
  }
];
const temp = loginRoutes.concat(dashboard);
export default new Router({
  routes: temp
});
