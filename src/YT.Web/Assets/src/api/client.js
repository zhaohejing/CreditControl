import fetch from 'utils/fetch';
export function getClients(data) {
  return fetch({
    url: '/api/services/app/customer/GetPagedCustomersAsync',
    method: 'post',
    data
  });
}

export function updateClient(data) {
  return fetch({
    url: '/api/services/app/customer/CreateOrUpdateCustomerAsync',
    method: 'post',
    data
  });
}

export function getClientForEdit(data) {
  return fetch({
    url: '/api/services/app/customer/GetCustomerForEditAsync',
    method: 'post',
    data
  });
}

export function charge(data) {
  return fetch({
    url: '/api/services/app/customer/CustomerCharge',
    method: 'post',
    data
  });
}

export function deleteClient(data) {
  return fetch({
    url: '/api/services/app/customer/DeleteCustomerAsync',
    method: 'post',
    data
  });
}
export function updateStraw(data) {
  return fetch({
    url: '/api/services/app/mobile/UpdateCustomerStrawState',
    method: 'post',
    data
  });
}
