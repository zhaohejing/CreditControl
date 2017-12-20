// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from "vue"
import App from "./App"
import router from "./router"
import iView from "iview";
import "iview/dist/styles/iview.css";
import "babel-polyfill"
Vue.config.productionTip = false

Vue.use(iView);
Vue.prototype.$down = (type, token, name) => {
  const url =
    "http://47.93.2.82:9999/api/File/Download?fileType=" +
    type +
    "&fileToken=" +
    token +
    "&fileName=" +
    name;
  window.open(url);
}

/* eslint-disable no-new */
new Vue({
  el: "#app",
  router,
  template: "<App/>",
  components: {
    App
  }
})
