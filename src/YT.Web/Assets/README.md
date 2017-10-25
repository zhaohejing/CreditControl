# vue-milk #


[![TeamCity CodeBetter](https://img.shields.io/teamcity/codebetter/bt428.svg)]() [![npm](https://img.shields.io/npm/dw/localeval.svg)]() [![npm](https://img.shields.io/npm/v/npm.svg)]() [![Chrome Web Store](https://img.shields.io/chrome-web-store/stars/nimelepbpejjlbmoobocpfnjhihnpked.svg)]()
**本项目是后台管理框架，集成了权限管理、登录功能、UI组件、七牛上传等功能，建议直接使用。**
> **注意**：便于前端调试，所以数据请求都是用了mockjs模拟。**在需要请求外部api时请移除mock文件**。
## 功能
- `工业化UI组件`（上手即用，无需自己造轮子）
- `自适应布局`（完美适配大中小屏）
- `登录/注销`
- `权限验证`
- `Tinymce 编辑器`
- `Markdown 编辑器`
- 侧边栏
- 面包屑
- JSON展示组件
- vue-chartjs图表
- 401，401错误页面
- `表格数据直接导出cvs`
- `多环境发布`
- dashboard
- 二次登录
- `动态侧边栏`（支持多级路由）
- mock数据
- 炫酷hover特效

## 开发
```bash
    # 克隆项目
    git clone https://github.com/herozhou/vue-framework-wz.git

    # 安装依赖
    npm install
    //or # 建议不要用cnpm  安装有各种诡异的bug 可以通过如下操作解决npm速度慢的问题
    npm install --registry=https://registry.npm.taobao.org

    # 本地开发 开启服务
    npm run dev
```
浏览器访问 http://localhost:9001

## 发布
```bash
    # 发布测试环境 带webpack ananalyzer
    npm run build:sit-preview

    # 构建生成环境
    npm run build:prod
```

## 目录结构
```shell

├── build                      // 构建相关  
├── config                     // 配置相关
├── src                        // 源代码
│   ├── api                    // 所有请求
│   ├── components             // 全局UI组件
│   ├── directives              // 全局指令
│   ├── mock                   // mock数据
│   ├── router                 // 路由
│   ├── store                  // 全局store管理
│   ├── utils                  // 全局公用方法
│   ├── containers              // 自适应布局组合
│   ├── vendor                  // UI组件依赖js
│   ├── view                   // view界面
│   │    ├── charts             //图表组件
│   │    ├── components         //首页组件
│   │    ├── login              //登录界面
│   │    ├── pages              //错误界面
│   │    └── permission        //权限测试界面
│   ├── App.vue                // 入口页面
│   └── main.js                // 入口 加载组件 初始化等
├── static                     // 静态资源
│   ├── bower_components        //七牛SDK
│   ├── css                     //css
│   ├── js                      //js
│   └── Jquery.min.js           // jq
├── .babelrc                   // babel-loader 配置
├── eslintrc.js                // eslint 配置项
├── .gitignore                 // git 忽略项
├── favicon.ico                // favicon图标
├── index.html                 // html模板
└── package.json               // package.json

## License

MIT
