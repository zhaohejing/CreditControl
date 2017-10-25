// 项目展示地址:"http://www.ddxc.org/"
// 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-08-09T16:04:18. All Rights Reserved.
//<生成时间>2017-08-09T16:04:18</生成时间>
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Runtime.Caching;
using YT.Caching;
using YT.Managers.Roles;
using YT.Navigations.Dtos;

namespace YT.Navigations
{
    /// <summary>
    /// 菜单业务管理
    /// </summary>
    public class MenuManager : IDomainService
    {
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly ICachingAppService _cachingAppService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="cachingAppService"></param>
        public MenuManager(
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            ICachingAppService cachingAppService)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _cachingAppService = cachingAppService;
        }
        /// <summary>
        /// 获取当前用户已授权menu
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ListResultDto<MenuEditDto>> GetCurrentUserMenu(long userId)
        {
            var userRoles = from c in await _userRoleRepository.GetAllListAsync(c => c.UserId == userId)
                            join d in await _roleRepository.GetAllListAsync(c => c.IsActive) on c.RoleId equals d.Id
                            select d;
            var menus =
              await _cachingAppService.GetMenuCache();
          //  var permissions = await _cachingAppService.GetPermissionCache();
            var ps = userRoles.SelectMany(c=>c.Permissions.Where(ww=>ww.IsGranted).Select(w =>w.Name)).Distinct();
            var list = menus.Where( c =>  ps.Any(e => e.Equals(c.RequiredPermissions))).ToList();
            foreach (var dto in list)
            {
                if (dto.RequiresAuthentication&&!dto.Url.IsNullOrWhiteSpace())
                {
                    dto.GrantedPermissionNames = ps.Where(c =>  c.Contains($"{dto.RequiredPermissions}.")).ToList();
                }  
            }
            return new ListResultDto<MenuEditDto>(list);
        }

    }



}
