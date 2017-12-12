import fetch from 'utils/fetch';
export function Authenticate(usernameOrEmailAddress, password) {
  const data = {
    usernameOrEmailAddress,
    password
  };
  return fetch({
    url: '/api/Account/Authenticate',
    method: 'post',
    data
  });
}

export function logout() {
  return fetch({
    url: '/login/logout',
    method: 'post'
  });
}

export function getInfo() {
  return fetch({
    url: '/api/services/app/session/GetCurrentLoginInformations',
    method: 'post'
  });
}

