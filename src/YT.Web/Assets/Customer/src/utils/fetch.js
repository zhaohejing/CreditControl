import axios from "axios";
// import vue from "vue";
// import router from "../router";

// 创建axios实例
const service = axios.create({
    baseURL: process.env.BASE_API, // api的base_url
    timeout: 50000 // 请求超时时间
});

// request拦截器
service.interceptors.request.use(
    config => {
        // Do something before request is sent
        if (localStorage.getItem("CreditToken")) {
            config.headers.Authorization =
                "Bearer " + localStorage.getItem("CreditToken");
            // 让每个请求携带token--["X-Token"]为自定义key 请根据实际情况自行修改
        }
        return config;
    },
    error => {
        // Do something with request error
        console.log(error); // for debug
        Promise.reject(error);
    }
);
// respone拦截器
service.interceptors.response.use(
    response => response,
    error => {
        if (error && error.response) {
            error.message = error.response.data.error.message;
        }
        return Promise.reject(error);
    }
);

export default service;