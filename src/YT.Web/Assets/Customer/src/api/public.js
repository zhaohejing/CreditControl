import fetch from "utils/fetch";
export function infos(data) {
  return fetch({
    url: "/api/services/app/adsence/GetPagedAdsencesAsync",
    method: "post",
    data
  });
}
export function info(data) {
  return fetch({
    url: "/api/services/app/adsence/GetAdsenceByIdAsync",
    method: "post",
    data
  });
}
