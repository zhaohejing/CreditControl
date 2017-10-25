import fetch from 'utils/fetch';
export function send(data) {
  return fetch({
    url: '/api/services/app/card/GetPagedCardsAsync',
    method: 'post',
    data
  });
}
