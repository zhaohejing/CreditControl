import fetch from 'utils/fetch';
export function getCards(data) {
  return fetch({
    url: '/api/services/app/card/GetPagedCardsAsync',
    method: 'post',
    data
  });
}

export function updateCard(data) {
  return fetch({
    url: '/api/services/app/card/CreateOrUpdateCardAsync',
    method: 'post',
    data
  });
}

export function getCardForEdit(data) {
  return fetch({
    url: '/api/services/app/card/GetCardForEditAsync',
    method: 'post',
    data
  });
}
export function deleteCard(data) {
  return fetch({
    url: '/api/services/app/card/DeleteCardAsync',
    method: 'post',
    data
  });
}
export function deleteCards(data) {
  return fetch({
    url: '/api/services/app/card/BatchDeleteCardAsync',
    method: 'post',
    data
  });
}