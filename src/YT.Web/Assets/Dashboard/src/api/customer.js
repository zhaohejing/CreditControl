import fetch from 'utils/fetch';
// 获取所有角色信息
export function getCustomers(data) {
    return fetch({
        url: '/api/services/app/customer/GetPagedCustomersAsync',
        method: 'post',
        data
    });
}
// 获取角色信息用于编辑
export function getCustomerForEdit(data) {
    return fetch({
        url: '/api/services/app/customer/GetCustomerForEditAsync',
        method: 'post',
        data
    });
}
export function prices(data) {
    return fetch({
        url: '/api/services/app/customerPreferencePrice/GetCustomerPricesAsync',
        method: 'post',
        data
    });
}

// 获取角色信息用于编辑
export function getCustomer(data) {
    return fetch({
        url: '/api/services/app/customer/GetCustomerByIdAsync',
        method: 'post',
        data
    });
}

// 保存角色
export function saveCustomer(data) {
    return fetch({
        url: '/api/services/app/customer/CreateOrUpdateCustomerAsync',
        method: 'post',
        data
    });
}
// 删除角色
export function deleteCustomer(data) {
    return fetch({
        url: '/api/services/app/customer/DeleteCustomerAsync',
        method: 'post',
        data
    });
}
// 客户审核
export function auditCustomer(data) {
    return fetch({
        url: '/api/services/app/customer/AuditCustomer',
        method: 'post',
        data
    });
}
// 重置密码
export function resetCustomer(data) {
    return fetch({
        url: '/api/services/app/customer/ResetCustomer',
        method: 'post',
        data
    });
}

// 批量删除角色
export function deleteCustomers(data) {
    return fetch({
        url: '/api/services/app/customer/BatchDeleteCustomerAsync',
        method: 'post',
        data
    });
}