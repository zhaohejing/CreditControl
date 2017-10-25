import fetch from 'utils/fetch';
export function getPromoters(data) {
  return fetch({
    url: '/api/services/app/promoter/GetPagedPromotersAsync',
    method: 'post',
    data
  });
}

export function updatePromoter(data) {
  return fetch({
    url: '/api/services/app/promoter/CreateOrUpdatePromoterAsync',
    method: 'post',
    data
  });
}

export function getPromoterForEdit(data) {
  return fetch({
    url: '/api/services/app/promoter/GetPromoterForEditAsync',
    method: 'post',
    data
  });
}
export function deletePromoter(data) {
  return fetch({
    url: '/api/services/app/promoter/DeletePromoterAsync',
    method: 'post',
    data
  });
}
export function deletePromoters(data) {
  return fetch({
    url: '/api/services/app/promoter/BatchDeletePromoterAsync',
    method: 'post',
    data
  });
}