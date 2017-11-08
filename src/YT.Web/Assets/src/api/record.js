import fetch from 'utils/fetch';
// 获取所有角色信息
export function records(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/GetPagedChargeRecordsAsync',
        method: 'post',
         data
    });
}
export function applys(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/GetPagedApplyChargesAsync',
        method: 'post',
         data
    });
}
export function chargeCustomer(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/ChargeCustomer',
        method: 'post',
         data
    });
}
export function apply(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/ChargeApplyCustomer',
        method: 'post',
         data
    });
}
export function clear(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/DeleteApplyChargeAsync',
        method: 'post',
         data
    });
}
export function costs(data) {
    return fetch({
        url: '/api/services/app/chargeRecord/GetCustomerCosts',
        method: 'post',
         data
    });
}