import fetch from 'utils/fetch';
export function getPageCates() {
  return fetch({
    url: '/api/services/app/category/GetPagedCategorysAsync',
    method: 'post'
  });
}
export function getCates(data) {
    return fetch({
      url: '/api/services/app/category/GetCategories',
      method: 'post',
      data
    });
  }
export function getCateForEdit(data) {
  return fetch({
    url: '/api/services/app/category/GetCategoryForEditAsync',
    method: 'post',
    data
  });
}
export function getCate(data) {
  return fetch({
    url: '/api/services/app/category/GetCategoryByIdAsync',
    method: 'post',
    data
  });
}
export function modifyCate(data) {
  return fetch({
    url: '/api/services/app/category/CreateOrUpdateCategoryAsync',
    method: 'post',
    data
  });
}
export function deleteCate(data) {
  return fetch({
    url: '/api/services/app/category/DeleteCategoryAsync',
    method: 'post',
    data
  });
}
export function deleteCates(data) {
  return fetch({
    url: '/api/services/app/category/BatchDeleteCategoryAsync',
    method: 'post',
    data
  });
}
