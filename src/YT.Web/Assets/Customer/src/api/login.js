import fetch from "utils/fetch";
// 登陆
export function Authenticate(account, password) {
    const data = {
        account,
        password
    };
    return fetch({
        url: "/api/services/app/dashboard/Authenticate",
        method: "post",
        data
    });
}
// 获取用户信息
export function info(data) {
    return fetch({
        url: "/api/services/app/dashboard/GetInfo",
        method: "post",
        data
    });
}

export function apply(data) {
    return fetch({
        url: "/api/services/app/dashboard/ApplyCharge",
        method: "post",
        data
    })
}
// 获取用户信息
export function modify(data) {
    return fetch({
        url: "/api/services/app/dashboard/Modify",
        method: "post",
        data
    });
}
// 重置
export function reset(data) {
    return fetch({
        url: "/api/services/app/dashboard/ResetPassword",
        method: "post",
        data
    });
}
// 重置
export function password(data) {
    return fetch({
        url: "/api/services/app/dashboard/ChangePassword",
        method: "post",
        data
    });
}
// 注册
export function register(data) {
    return fetch({
        url: "/api/services/app/dashboard/Register",
        method: "post",
        data
    });
}
// 注册
export function menus(data) {
    return fetch({
        url: "/api/services/app/dashboard/GetCustomerMenus",
        method: "post",
        data
    });
}