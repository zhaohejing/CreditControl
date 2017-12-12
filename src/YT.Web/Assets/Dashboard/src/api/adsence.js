import fetch from 'utils/fetch';
export function getAdsences(data) {
    return fetch({
        url: '/api/services/app/adsence/GetPagedAdsencesAsync',
        method: 'post',
        data
    });
}

export function getAdsenceEdit(data) {
    return fetch({
        url: '/api/services/app/adsence/GetAdsenceForEditAsync',
        method: 'post',
        data
    });
}
export function getAdsence(data) {
    return fetch({
        url: '/api/services/app/adsence/GetAdsenceByIdAsync',
        method: 'post',
        data
    });
}
export function modifyAdsence(data) {
    return fetch({
        url: '/api/services/app/adsence/CreateOrUpdateAdsenceAsync',
        method: 'post',
        data
    });
}
export function deleteAdsence(data) {
    return fetch({
        url: '/api/services/app/adsence/DeleteAdsenceAsync',
        method: 'post',
        data
    });
}
export function deleteAdsences(data) {
    return fetch({
        url: '/api/services/app/adsence/BatchDeleteAdsenceAsync',
        method: 'post',
        data
    });
}
export function publicAdsences(data) {
    return fetch({
        url: '/api/services/app/adsence/PublicAdsenceAsync',
        method: 'post',
        data
    });
}