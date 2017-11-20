import fetch from 'utils/fetch';
// 获取所有角色信息
export function getProducts(data) {
    return fetch({
        url: '/api/services/app/product/GetPagedProductsAsync',
        method: 'post',
        data
    });
}
// 获取角色信息用于编辑
export function getProductForEdit(data) {
    return fetch({
        url: '/api/services/app/product/GetProductForEditAsync',
        method: 'post',
        data
    });
}
// 获取角色信息用于编辑
export function UpdateState(data) {
    return fetch({
        url: '/api/services/app/product/UpdateState',
        method: 'post',
        data
    });
}
export function getFormByOrder(data) {
    return fetch({
        url: '/api/services/app/dashboard/GetFormByOrder',
        method: 'post',
        data
    });
}
export function prices(data) {
    return fetch({
        url: '/api/services/app/customerPreferencePrice/GetProductPricesAsync',
        method: 'post',
        data
    });
}
export function pricing(data) {
    return fetch({
        url: '/api/services/app/customerPreferencePrice/ModifyPriceAsync',
        method: 'post',
        data
    });
}
export function exportData(data) {
    return fetch({
        url: '/api/File/DownLoadFile?orderId=' + data,
        method: 'post'
    });
}
export function exportOrder(data) {
    return fetch({
        url: 'api/services/app/product/ExportOrdersAsync',
        method: 'post',
        data
    });
}
// 获取角色信息用于编辑
export function getProduct(data) {
    return fetch({
        url: '/api/services/app/product/GetProductByIdAsync',
        method: 'post',
        data
    });
}

// 保存角色
export function saveProduct(data) {
    return fetch({
        url: '/api/services/app/product/CreateOrUpdateProductAsync',
        method: 'post',
        data
    });
}
// 删除角色
export function deleteProduct(data) {
    return fetch({
        url: '/api/services/app/product/DeleteProductAsync',
        method: 'post',
        data
    });
}
// 批量删除角色
export function deleteProducts(data) {
    return fetch({
        url: '/api/services/app/product/BatchDeleteProductAsync',
        method: 'post',
        data
    });
}
export function orders(data) {
    return fetch({
        url: '/api/services/app/product/GetPagedOrdersAsync',
        method: 'post',
        data
    });
}
export function order(data) {
    return fetch({
        url: '/api/services/app/product/GetOrderByIdAsync',
        method: 'post',
        data
    });
}
export function completeorder(data) {
    return fetch({
        url: '/api/services/app/product/CompleteOrder',
        method: 'post',
        data
    });
}