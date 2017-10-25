import Vue from 'vue';
import Router from 'vue-router';
const _import = require('./_import_' + process.env.NODE_ENV);
import Full from '@/containers/Full';
Vue.use(Router);
export const constantRouterMap = [
  {
    path: '/login',
    component: _import('login/index'),
    hidden: true
  },
  {
    path: '/pages',
    redirect: '/pages/p404',
    name: 'Pages',
    component: {
      render(c) {
        return c('router-view');
      }
      // Full,
    },
    children: [
      {
        path: '404',
        name: 'Page404',
        component: _import('errorPages/Page404')
      },
      {
        path: '500',
        name: 'Page500',
        component: _import('errorPages/Page500')
      }
    ]
  }
];

export const asyncRouterMap = [
  {
    path: '/',
    redirect: '/dashboard',
    name: '首页',
    component: Full,
    children: [
      {
        path: '/dashboard',
        name: '介绍',
        icon: 'speedometer',
        component: r => require(['views/dashboard'], r)
      },
      {
        path: '',
        name: '客户管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'customer/client',
            name: '用户',
            icon: 'person',
            hidden: false,
            component: r => require(['views/customer/client'], r)
          },
          {
            path: 'customer/charge',
            name: '账户',
            icon: 'person-add',
            component: r => require(['views/customer/charge'], r)
          }
        ]
      },
      {
        path: '',
        name: '充值卡管理',
        icon: 'card',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'card/charge',
            name: '卡片管理',
            icon: '',
            hidden: false,
            component: r => require(['views/card/card'], r)
          }, {
            path: 'card/only',
            name: '唯鲜卡管理',
            icon: '',
            hidden: false,
            component: r => require(['views/card/onlymilk'], r)
          }
        ]
      },
      {
        path: '',
        name: '推广管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'generalize/promoters',
            name: '推广员',
            icon: 'person',
            hidden: false,
            component: r => require(['views/generalize/promoters'], r)
          },
          {
            path: 'generalize/wechat',
            name: '群发',
            icon: 'person-add',
            component: r => require(['views/generalize/wechat'], r)
          }
        ]
      },
      {
        path: '',
        name: '权限管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'system/role',
            name: '角色管理',
            icon: 'person',
            component: r => require(['views/manager/role'], r)
          },
          {
            path: 'system/account',
            name: '用户管理',
            icon: 'person-add',
            component: r => require(['views/manager/account'], r)
          },
          {
            path: 'system/menu',
            name: '菜单管理',
            icon: 'person-add',
            component: r => require(['views/manager/menu'], r)
          }
        ]
      },
      {
        path: '',
        name: '操作记录',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'log/audit',
            name: '日志',
            icon: 'person',
            hidden: false,
            component: r => require(['views/operation/audit'], r)
          }
        ]
      },
      {
        path: '',
        name: '报表管理',
        icon: 'person-stalker',
        component: {
          render(c) {
            return c('router-view');
          }
        },
        children: [
          {
            path: 'statics/a',
            name: '销售明细表',
            icon: 'person',
            component: r => require(['views/statics/a'], r)
          },
          {
            path: 'statics/b',
            name: '销售汇总',
            icon: 'person',
            component: r => require(['views/statics/b'], r)
          },
          {
            path: 'statics/c',
            name: '顾客取货报表',
            icon: 'person',
            component: r => require(['views/statics/c'], r)
          },
          {
            path: 'statics/d',
            name: '顾客消费',
            icon: 'person',
            component: r => require(['views/statics/d'], r)
          },
          {
            path: 'statics/e',
            name: '商品销售数量',
            icon: 'person',
            component: r => require(['views/statics/e'], r)
          },
          {
            path: 'statics/f',
            name: '充值记录',
            icon: 'person',
            component: r => require(['views/statics/f'], r)
          },
          {
            path: 'statics/g',
            name: '订单管理',
            icon: 'person',
            component: r => require(['views/statics/g'], r)
          },
          {
            path: 'statics/h',
            name: '待补货记录',
            icon: 'person',
            component: r => require(['views/statics/h'], r)
          }
        ]
      }
    ]
  },
  {
    path: '*',
    redirect: '/pages/404',
    hidden: true
  }
];

const temp = constantRouterMap.concat(asyncRouterMap);
export default new Router({
  mode: 'hash',
  linkActiveClass: 'open active',
  scrollBehavior: () => ({
    y: 0
  }),
  routes: temp
});
