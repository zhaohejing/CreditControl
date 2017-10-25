import fetch from 'utils/fetch';
export function getSpecialCards(data) {
  return fetch({
    url: '/api/services/app/specialCard/GetPagedSpecialCardsAsync',
    method: 'post',
    data
  });
}

export function updateSpecialCard(data) {
  return fetch({
    url: '/api/services/app/specialCard/CreateOrUpdateSpecialCardAsync',
    method: 'post',
    data
  });
}

export function getSpecialCardForEdit(data) {
  return fetch({
    url: '/api/services/app/specialCard/GetSpecialCardForEditAsync',
    method: 'post',
    data
  });
}
export function deleteSpecialCard(data) {
  return fetch({
    url: '/api/services/app/specialCard/DeleteSpecialCardAsync',
    method: 'post',
    data
  });
}
export function deleteSpecialCards(data) {
  return fetch({
    url: '/api/services/app/specialCard/BatchDeleteSpecialCardAsync',
    method: 'post',
    data
  });
}
export function exportSpecialCards(data) {
  return fetch({
    url: '/api/services/app/specialCard/GetSpecialCardToExcel',
    method: 'post',
    data
  });
}