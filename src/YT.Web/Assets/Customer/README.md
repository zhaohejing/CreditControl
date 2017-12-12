# credit

> A Vue.js project


********
## 目录结构
```shell
CREDIT 
  ├─build   //webpack项目生成文件路径
  ├─config  //项目打包配置文件夹
  ├─node_modules  //npm 包管理
  ├─src  //项目根路径
  │  ├─api //接口定义文件夹
  │  ├─assets  //logo文件存放
  │  ├─components //通用以及系统组件存放
  │  │    ├─Container.vue //登录页容器
  │  │    └─Dashboard.vue //控制台容器
  │  ├─router //路由配置
  │  ├─utils //通用帮助类
  │  └─views  //视图存放
  │      ├─dashboard  //中控视图
  │      │    ├─cancelorder.vue
  │      │    ├─charge.vue
  │      │    ├─index.vue
  │      │    ├─info.vue
  │      │    ├─order.vue
  │      │    ├─statistical.vue  
  │      │    ├─support.vue
  │      └─first  //登录页视图
  │           ├─login.vue  //登陆页
  │           ├─message.vue //消息提示页
  │           ├─register.vue //注册页
  │           ├─reset.vue  //重置密码页
  └─static //静态文件存放



## Build Setup
``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev

# build for production with minification
npm run build

# build for production and view the bundle analyzer report
npm run build --report
```

For a detailed explanation on how things work, check out the [guide](http://vuejs-templates.github.io/webpack/) and [docs for vue-loader](http://vuejs.github.io/vue-loader).
