import fetch from 'utils/fetch';
export function userMenus() {
  return fetch({
    url: '/api/services/app/menu/GetUserMenusAsync',
    method: 'post'
  });
}

export function getMenus(data) {
  return fetch({
    url: '/api/services/app/menu/GetPagedMenusAsync',
    method: 'post',
    data
  });
}
export function detailMenu(data) {
  return fetch({
    url: '/api/services/app/menu/GetPagedMenusAsync',
    method: 'post',
    data
  });
}
export function modifyMenus(data) {
  return fetch({
    url: '/api/services/app/menu/CreateOrUpdateMenuAsync',
    method: 'post',
    data
  });
}
export function deleteMenu(data) {
  return fetch({
    url: '/api/services/app/menu/DeleteMenuAsync',
    method: 'post',
    data
  });
}
export function deleteMenus(data) {
  return fetch({
    url: '/api/services/app/menu/BatchDeleteMenuAsync',
    method: 'post',
    data
  });
}

export function allPermissions() {
  return fetch({
    url: '/api/services/app/permission/GetAllPermissions',
    method: 'post'
  });
}