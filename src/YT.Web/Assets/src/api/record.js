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
