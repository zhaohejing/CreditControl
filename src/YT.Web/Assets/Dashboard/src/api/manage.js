import fetch from 'utils/fetch';
// 获取所有角色信息
export function getRoles() {
  return fetch({
    url: '/api/services/app/role/GetRoles',
    method: 'post'
  });
}
// 获取角色分页信息
export function getRolesByPage(data) {
  return fetch({
    url: '/api/services/app/role/GetRolesAsync',
    method: 'post',
    data
  });
}
// 获取角色信息用于编辑
export function getRoleForEdit(data) {
  return fetch({
    url: '/api/services/app/role/GetRoleForEdit',
    method: 'post',
    data
  });
}
// 保存角色
export function saveRole(data) {
  return fetch({
    url: '/api/services/app/role/CreateOrUpdateRole',
    method: 'post',
    data
  });
}
// 删除角色
export function deleteRole(data) {
  return fetch({
    url: '/api/services/app/role/DeleteRole',
    method: 'post',
    data
  });
}
// 批量删除角色
export function deleteRoles(data) {
  return fetch({
    url: '/api/services/app/role/GetRolesAsync',
    method: 'post',
    data
  });
}









export function getUsers(data) {
  return fetch({
    url: '/api/services/app/user/GetUsers',
    method: 'post',
    data
  });
}
export function getUserForEdit(data) {
  return fetch({
    url: '/api/services/app/user/GetUserForEdit',
    method: 'post',
    data
  });
}
export function deleteUser(data) {
  return fetch({
    url: '/api/services/app/user/DeleteUser',
    method: 'post',
    data
  });
}
export function modifyUser(data) {
  return fetch({
    url: '/api/services/app/user/CreateOrUpdateUser',
    method: 'post',
    data
  });
}
export function getInfo() {
  return fetch({
    url: '/api/services/app/session/GetCurrentLoginInformations',
    method: 'post'
  });
}

