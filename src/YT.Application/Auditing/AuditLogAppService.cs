using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using YT.Auditing.Dto;
using YT.Auditing.Exporting;
using YT.Authorization;
using YT.Authorization.Users;
using YT.Dto;
using YT.Managers.Users;

namespace YT.Auditing
{/// <summary>
 /// 
 /// </summary>
    [DisableAuditing]
    public class AuditLogAppService : YtAppServiceBase, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IAuditLogListExcelExporter _auditLogListExcelExporter;
        private readonly INamespaceStripper _namespaceStripper;

        private static List<ServiceMapperDto> NameList => new List<ServiceMapperDto>()
        {
            new ServiceMapperDto("YT.WebApi.Controllers.AccountController","�˻�ģ��",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("Authenticate","��ȡtoken��½"),
                new ServiceMapperDto("Register","ע��"),
                new ServiceMapperDto("Authenticate","��ȡtoken��½"),
            }),
            new ServiceMapperDto("YT.Cards.CardAppService","��ֵ��ģ��",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedCardsAsync","��ȡ��ҳ����"),
                new ServiceMapperDto("GetCardForEditAsync","��ȡ��ֵ������༭"),
                new ServiceMapperDto("GetCardByIdAsync","��ȡ��ֵ������"),
                new ServiceMapperDto("CreateOrUpdateCardAsync","��ӻ�༭��ֵ��"),
                new ServiceMapperDto("DeleteCardAsync","ɾ����ֵ��"),
                new ServiceMapperDto("BatchDeleteCardAsync","����ɾ����ֵ��"),
                new ServiceMapperDto("GetCardToExcel","������ֵ����Ϣ"),
            }),
            new ServiceMapperDto("YT.Customers.CustomerAppService","�ͻ���Ϣģ��",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedCustomersAsync","��ȡ�ͻ���ҳ��Ϣ"),
                new ServiceMapperDto("GetCustomerForEditAsync","��ȡ�ͻ��������ڱ༭"),
                new ServiceMapperDto("GetCustomerByIdAsync","��ȡ�ͻ�����"),
                new ServiceMapperDto("CreateOrUpdateCustomerAsync","������༭�ͻ���Ϣ"),
                new ServiceMapperDto("CustomerCharge","�ͻ�����ֵ"),
                new ServiceMapperDto("DeleteCustomerAsync","ɾ���ͻ���Ϣ"),
                new ServiceMapperDto("BatchDeleteCustomerAsync","����ɾ���ͻ���Ϣ"),
                new ServiceMapperDto("GetCustomerToExcel","�����ͻ���Ϣ"),
            }),
            new ServiceMapperDto("YT.Promoters.PromoterAppService","�ƹ�Աģ��",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedPromotersAsync","��ȡ�ƹ�Ա��ҳ��Ϣ"),
                new ServiceMapperDto("GetPromoterForEditAsync","��ȡ�ƹ�Ա��Ϣ�༭"),
                new ServiceMapperDto("GetPromoterByIdAsync","��ȡ�ƹ�Ա����"),
                new ServiceMapperDto("CreateOrUpdatePromoterAsync","������༭�ƹ�Ա"),
                new ServiceMapperDto("DeletePromoterAsync","ɾ���ƹ�Ա"),
                new ServiceMapperDto("BatchDeletePromoterAsync","����ɾ���ƹ�Ա"),
                new ServiceMapperDto("GetPromoterToExcel","�����ƹ�Ա��Ϣ"),
            }),
            new ServiceMapperDto("YT.Authorization.Roles.RoleAppService","��ɫ����ģ��",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetRoles","��ȡ��ɫ��Ϣȫ��"),
                new ServiceMapperDto("GetRoleForEdit","��ȡ��ɫ����༭"),
                new ServiceMapperDto("CreateOrUpdateRole","������༭��ɫ��Ϣ"),
                new ServiceMapperDto("DeleteRole","ɾ����ɫ"),
                new ServiceMapperDto("GetRolesAsync","��ȡ��ɫ��ҳ��Ϣ"),
            }),
            new ServiceMapperDto("YT.Authorization.Users.UserAppService","�û�����ģ��",new List<ServiceMapperDto>()
            {
               
                 new ServiceMapperDto("GetUsers","��ȡ�û���Ϣ��ҳ"),
                new ServiceMapperDto("GetUsersToExcel","�����û���Ϣ��ҳ"),
                new ServiceMapperDto("GetUserForEdit","��ȡ�û�����༭"),
                new ServiceMapperDto("CreateOrUpdateUser","������༭�û�"),
                new ServiceMapperDto("DeleteUser","ɾ���û���Ϣ"),
            })
        };

        /// <summary>
        /// 
        /// </summary>
        public AuditLogAppService(
            IRepository<AuditLog, long> auditLogRepository, 
            IRepository<User, long> userRepository, 
            IAuditLogListExcelExporter auditLogListExcelExporter, 
            INamespaceStripper namespaceStripper)
        {
            _auditLogRepository = auditLogRepository;
            _userRepository = userRepository;
            _auditLogListExcelExporter = auditLogListExcelExporter;
            _namespaceStripper = namespaceStripper;
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input)
        {
            var query = CreateAuditLogAndUsersQuery(input);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var auditLogListDtos = ConvertToAuditLogListDtos(results);

            return new PagedResultDto<AuditLogListDto>(resultCount, auditLogListDtos);
        }
        /// <summary>
        /// 
        /// </summary>
        public async Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input)
        {
            var auditLogs = await CreateAuditLogAndUsersQuery(input)
                        .AsNoTracking()
                        .OrderByDescending(al => al.AuditLog.ExecutionTime)
                        .ToListAsync();

            var auditLogListDtos = ConvertToAuditLogListDtos(auditLogs);

            return _auditLogListExcelExporter.ExportToFile(auditLogListDtos);
        }
        /// <summary>
        /// 
        /// </summary>
        private List<AuditLogListDto> ConvertToAuditLogListDtos(List<AuditLogAndUser> results)
        {
            var list=new List<AuditLogListDto>();
            foreach (var user in results)
            {
                var model = NameList.FirstOrDefault(c => user.AuditLog.ServiceName.Contains(c.Name));
                var temp = user.AuditLog.MapTo<AuditLogListDto>();
                temp.UserName = user.User == null ? null : user.User.UserName;
                temp.Name = user.User == null ? null : user.User.Name;
                if (model!=null)
                {
                    temp.ServiceName = model.Show;
                    var t = model.Child.FirstOrDefault(c => temp.MethodName.Contains(c.Name));
                    temp.MethodName = t?.Show;
                    //todo ����ת�� 
                }
                else
                {
                    temp.ServiceName = _namespaceStripper.StripNameSpace(temp.ServiceName);
                }
                list.Add(temp);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        private IQueryable<AuditLogAndUser> CreateAuditLogAndUsersQuery(GetAuditLogsInput input)
        {
            var query = from auditLog in _auditLogRepository.GetAll()
                join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
                from joinedUser in userJoin.DefaultIfEmpty()
                where auditLog.ExecutionTime >= input.StartDate && auditLog.ExecutionTime <= input.EndDate
                select new AuditLogAndUser {AuditLog = auditLog, User = joinedUser};

            query = query
                .WhereIf(!input.UserName.IsNullOrWhiteSpace(), item => item.User.UserName.Contains(input.UserName))
                .WhereIf(!input.ServiceName.IsNullOrWhiteSpace(),
                    item => item.AuditLog.ServiceName.Contains(input.ServiceName));
            return query;
        }
    }
}
