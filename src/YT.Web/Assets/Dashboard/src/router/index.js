import Vue from 'vue';
import Router from 'vue-router';
const _import = require('./_import_' + process.env.NODE_ENV);
import Full from '@/containers/Full';
Vue.use(Router);
export const constantRouterMap = [{
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
        children: [{
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

export const asyncRouterMap = [{
        path: '/',
        redirect: '/dashboard',
        name: '首页',
        component: Full,
        children: [{
                path: '/dashboard',
                name: '介绍',
                icon: 'speedometer',
                component: r => require(['views/Dashboard'], r)
            },
            {
                path: '/public',
                name: '公告管理',
                icon: 'speedometer',
                component: r => require(['views/adsence/Index'], r)
            },
            {
                path: '',
                name: '采购管理',
                icon: 'person-stalker',
                component: {
                    render(c) {
                        return c('router-view');
                    }
                },
                children: [{
                        path: '/products',
                        name: '商品管理',
                        icon: 'person',
                        hidden: false,
                        component: r => require(['views/procurement/index'], r)
                    },
                    {
                        path: '/pricing',
                        name: '商品定价',
                        icon: 'person-add',
                        component: r => require(['views/procurement/pricing'], r)
                    },
                    {
                        path: '/orders',
                        name: '订单管理',
                        icon: 'person-add',
                        component: r => require(['views/procurement/order'], r)
                    },
                    {
                        path: '/order',
                        name: '订单详情',
                        icon: 'person-add',
                        component: r => require(['views/procurement/orderdetail'], r)
                    }
                ]
            },
            {
                path: '/customers',
                name: '客户管理',
                icon: 'card',
                component: r => require(['views/customer/index'], r),

            },
            {
                path: '/customerpricing',
                name: '客户定价',
                component: r => require(['views/customer/customerpricing'], r)
            },
            {
                path: '/series',
                name: '系列管理',
                icon: 'person-stalker',
                component: r => require(['views/series/index'], r)
            },
            {
                path: '',
                name: '财务管理',
                icon: 'person-stalker',
                component: {
                    render(c) {
                        return c('router-view');
                    }
                },
                children: [{
                        path: '/chargerecord',
                        name: '充值记录',
                        icon: 'person',
                        hidden: false,
                        component: r => require(['views/finance/record'], r)
                    },
                    {
                        path: '/applyforcharge',
                        name: '充值申请记录',
                        icon: 'person-add',
                        component: r => require(['views/finance/apply'], r)
                    }, {
                        path: '/cost',
                        name: '消费记录',
                        icon: 'person-stalker',
                        component: r => require(['views/finance/cost'], r)
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
                children: [{
                        path: '/roles',
                        name: '角色管理',
                        icon: 'person',
                        component: r => require(['views/manager/Role'], r)
                    },
                    {
                        path: '/users',
                        name: '用户管理',
                        icon: 'person-add',
                        component: r => require(['views/manager/Account'], r)
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