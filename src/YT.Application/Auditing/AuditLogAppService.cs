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
            new ServiceMapperDto("YT.WebApi.Controllers.AccountController","账户模块",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("Authenticate","获取token登陆"),
                new ServiceMapperDto("Register","注册"),
                new ServiceMapperDto("Authenticate","获取token登陆"),
            }),
            new ServiceMapperDto("YT.Cards.CardAppService","充值卡模块",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedCardsAsync","获取分页数据"),
                new ServiceMapperDto("GetCardForEditAsync","获取充值卡详情编辑"),
                new ServiceMapperDto("GetCardByIdAsync","获取充值卡详情"),
                new ServiceMapperDto("CreateOrUpdateCardAsync","添加或编辑充值卡"),
                new ServiceMapperDto("DeleteCardAsync","删除充值卡"),
                new ServiceMapperDto("BatchDeleteCardAsync","批量删除充值卡"),
                new ServiceMapperDto("GetCardToExcel","导出充值卡信息"),
            }),
            new ServiceMapperDto("YT.Customers.CustomerAppService","客户信息模块",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedCustomersAsync","获取客户分页信息"),
                new ServiceMapperDto("GetCustomerForEditAsync","获取客户详情用于编辑"),
                new ServiceMapperDto("GetCustomerByIdAsync","获取客户详情"),
                new ServiceMapperDto("CreateOrUpdateCustomerAsync","新增或编辑客户信息"),
                new ServiceMapperDto("CustomerCharge","客户代充值"),
                new ServiceMapperDto("DeleteCustomerAsync","删除客户信息"),
                new ServiceMapperDto("BatchDeleteCustomerAsync","批量删除客户信息"),
                new ServiceMapperDto("GetCustomerToExcel","导出客户信息"),
            }),
            new ServiceMapperDto("YT.Promoters.PromoterAppService","推广员模块",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetPagedPromotersAsync","获取推广员分页信息"),
                new ServiceMapperDto("GetPromoterForEditAsync","获取推广员信息编辑"),
                new ServiceMapperDto("GetPromoterByIdAsync","获取推广员详情"),
                new ServiceMapperDto("CreateOrUpdatePromoterAsync","新增或编辑推广员"),
                new ServiceMapperDto("DeletePromoterAsync","删除推广员"),
                new ServiceMapperDto("BatchDeletePromoterAsync","批量删除推广员"),
                new ServiceMapperDto("GetPromoterToExcel","导出推广员信息"),
            }),
            new ServiceMapperDto("YT.Authorization.Roles.RoleAppService","角色管理模块",new List<ServiceMapperDto>()
            {
                new ServiceMapperDto("GetRoles","获取角色信息全部"),
                new ServiceMapperDto("GetRoleForEdit","获取角色详情编辑"),
                new ServiceMapperDto("CreateOrUpdateRole","新增或编辑角色信息"),
                new ServiceMapperDto("DeleteRole","删除角色"),
                new ServiceMapperDto("GetRolesAsync","获取角色分页信息"),
            }),
            new ServiceMapperDto("YT.Authorization.Users.UserAppService","用户管理模块",new List<ServiceMapperDto>()
            {
               
                 new ServiceMapperDto("GetUsers","获取用户信息分页"),
                new ServiceMapperDto("GetUsersToExcel","到处用户信息分页"),
                new ServiceMapperDto("GetUserForEdit","获取用户详情编辑"),
                new ServiceMapperDto("CreateOrUpdateUser","新增或编辑用户"),
                new ServiceMapperDto("DeleteUser","删除用户信息"),
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
                    //todo 参数转化 
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
