import fetch from "utils/fetch";
// 登陆
export function products(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetProducts",
    method: "post",
    data
  });
}
// 推出
export function detail(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetProductDetail",
    method: "post",
    data
  });
}
// 重置
export function info(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetInfo",
    method: "post",
    data
  });
}
// 注册
export function createOrder(data) {
  return fetch({
    url: "/api/services/app/dashboard/CreateOrder",
    method: "post",
    data
  });
}
// 所有订单
export function orders(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetHaveProducts",
    method: "post",
    data
  });
}
// 单个订单
export function order(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetHaveProduct",
    method: "post",
    data
  });
}
export function completeOrder(data) {
  return fetch({
    url: "/api/services/app/dashboard/CompleteOrder",
    method: "post",
    data
  });
}
export function getFormByOrder(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetFormByOrder",
    method: "post",
    data
  });
}
export function modifyForm(data) {
  return fetch({
    url: "/api/services/app/dashboard/ModifyForm",
    method: "post",
    data
  });
}
export function costs(data) {
  return fetch({
    url: "/api/services/app/dashboard/GetCustomerCosts",
    method: "post",
    data
  });
}
