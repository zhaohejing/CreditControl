import fetch from 'utils/fetch';
export function getAudits(data) {
  return fetch({
    url: '/api/services/app/auditLog/GetAuditLogs',
    method: 'post',
    data
  });
}
export function getAuditsToExcel(data) {
  return fetch({
    url: '/api/services/app/auditLog/GetAuditLogsToExcel',
    method: 'post',
    data
  });
}
