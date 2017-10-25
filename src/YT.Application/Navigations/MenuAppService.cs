using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using YT.Authorization.Permissions.Dto;
using YT.Authorizations;
using YT.Caching;
using YT.Dto;
using YT.Managers.Roles;
using YT.Navigations.Dtos;

namespace YT.Navigations
{
    /// <summary>
    /// 菜单服务实现
    /// </summary>
    [AbpAuthorize]
    [DisableAuditing]
    public class MenuAppService : YtAppServiceBase, IMenuAppService
    {
        private readonly IRepository<Menu, int> _menuRepository;
        private readonly IRepository<YtPermission, int> _permissionRepository;
        private readonly ICachingAppService _cachingAppService;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly MenuManager _menuManager;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="menuRepository"></param>
        /// <param name="menuManager"></param>
        /// <param name="cachingAppService"></param>
        /// <param name="rolePermissionRepository"></param>
        /// <param name="permissionRepository"></param>
        public MenuAppService(IRepository<Menu, int> menuRepository,
            MenuManager menuManager,
        ICachingAppService cachingAppService,
        IRepository<RolePermissionSetting, long> rolePermissionRepository,
        IRepository<YtPermission, int> permissionRepository)
        {
            _menuRepository = menuRepository;
            _menuManager = menuManager;
            _cachingAppService = cachingAppService;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        #region 菜单管理

        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        public async Task<PagedResultDto<MenuListDto>> GetPagedMenusAsync(GetMenuInput input)
        {

            var query = _menuRepository.GetAll();
            query = query.WhereIf(!input.FilterText.IsNullOrWhiteSpace(), c => c.DisplayName.Contains(input.FilterText));
            var menuCount = await query.CountAsync();

            var menus = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var menuListDtos = menus.MapTo<List<MenuListDto>>();
            return new PagedResultDto<MenuListDto>(
            menuCount,
            menuListDtos
            );
        }


        /// <summary>
        /// 获取当前登录用户的菜单项
        /// </summary>
        public async Task<ListResultDto<MenuEditDto>> GetUserMenusAsync()
        {
            var u = AbpSession.GetUserId();
            return await _menuManager.GetCurrentUserMenu(u);
        }

        /// <summary>
        /// 通过Id获取菜单信息进行编辑或修改 
        /// </summary>
        public async Task<GetMenuForEditOutput> GetMenuForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetMenuForEditOutput();

            MenuEditDto menuEditDto;
            List<DictionaryDto> result = AppConsts.StaticPermissions;
            if (input.Id.HasValue)
            {
                var entity = await _menuRepository.GetAsync(input.Id.Value);
                menuEditDto = entity.MapTo<MenuEditDto>();
                if (entity.RequiresAuthentication&&!entity.RequiredPermissions.IsNullOrWhiteSpace()&&!entity.Url.IsNullOrWhiteSpace())
                {
                    var filterName = $"{entity.RequiredPermissions}";
                    var ps = await _permissionRepository.GetAllListAsync(c => c.Name.Contains(filterName));

                    result = ps.Select(c => new DictionaryDto()
                    {
                        DisplayName = c.DisplayName,
                        Name = c.Name.Replace(filterName,string.Empty),
                        IsCheck = true
                    }).ToList();
                    output.PermissionTypes = result;
                }
              
            }
            else
            {
                menuEditDto = new MenuEditDto();
                output.PermissionTypes = result;

            }

            output.Menu = menuEditDto;
            return output;
        }

        /// <summary>
        /// 通过指定id获取菜单ListDto信息
        /// </summary>
        public async Task<MenuListDto> GetMenuByIdAsync(EntityDto<int> input)
        {
            var entity = await _menuRepository.GetAsync(input.Id);
            return entity.MapTo<MenuListDto>();
        }

        /// <summary>
        /// 新增或更改菜单
        /// </summary>
        public async Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input)
        {
            //权限配置
            var entity = input.MenuEditDto.MapTo<Menu>();
            var plist = await _cachingAppService.GetPermissionCache();
            var sort = plist.Items.Count + 1;
            var page = plist.Items.First(c => c.Name.Equals("page"));
            if (input.MenuEditDto.ParentId.HasValue)
            {
                var parent = await _menuRepository.FirstOrDefaultAsync(input.MenuEditDto.ParentId.Value);
                page = plist.Items.First(c => c.Name.Equals(parent.RequiredPermissions));
            }
            var temp = $"{page.Name}.{entity.Name}";
            input.MenuEditDto.RequiredPermissions = temp;

            await ModifyMenuPermissions(entity, temp, page, sort, input.PermissionTypes);

            //if(!input.MenuEditDto.RequiresAuthentication)
            //{
            //    if (await _permissionRepository.CountAsync(c => c.Name.Contains(temp)) > 0)
            //    {
            //    await DeleteMenuPermissions(temp);
            //    }
            //}

            //菜单配置
            if (input.MenuEditDto.Id.HasValue)
            {
                await UpdateMenuAsync(input.MenuEditDto);
            }
            else
            {
                await CreateMenuAsync(input.MenuEditDto);
            }
            await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.MenuCache));
        }
        /// <summary>
        /// 菜单移动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Move(MoveMenuInput input)
        {
            var menu = await _menuRepository.FirstOrDefaultAsync(input.MenuId);
            if (menu == null)
            {
                throw new AbpException("菜单信息不存在");
            }
            if (menu.ParentId == input.ParentId) return;
            menu.ParentId = input.ParentId;
            var code = GenderLevelCode();
            Menu parent = null;
            if (input.ParentId.HasValue)
            {
                parent = await _menuRepository.FirstOrDefaultAsync(input.ParentId.Value);
                code = $"{parent.LevelCode}.{code}";
            }
            await ChangeMenuPermissions(menu, parent);
            menu.LevelCode = code;
            await _menuRepository.UpdateAsync(menu);
            await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.MenuCache));
        }
        /// <summary>
        /// 移动权限
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private async Task ChangeMenuPermissions(Menu menu, Menu parent)
        {
            if (!menu.RequiresAuthentication) return;
            if (parent != null)
            {
                var pp = await _permissionRepository.FirstOrDefaultAsync(c => c.Name.Contains(parent.RequiredPermissions));
                var mp = await _permissionRepository.FirstOrDefaultAsync(c => c.Name.Equals(menu.RequiredPermissions));
                mp.ParentId = pp.Id;
                var code = $"{pp.LevelCode}.{GenderLevelCode()}";
                mp.LevelCode = code;
                var temp = menu.RequiredPermissions.Replace($".{menu.Name}", "");
                mp.Name = mp.Name.Replace(temp, parent.RequiredPermissions);
                if (mp.Children.Any())
                {
                    foreach (var child in mp.Children)
                    {
                        child.Name = child.Name.Replace(temp, parent.RequiredPermissions);
                        child.LevelCode = $"{code}.{GenderLevelCode()}";
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        private async Task CreateMenuAsync(MenuEditDto input)
        {
            var entity = input.MapTo<Menu>();
            var code = GenderLevelCode();
            if (!entity.ParentId.HasValue)
            {
                entity.LevelCode = code;
            }
            else
            {
                var parent = await _menuRepository.FirstOrDefaultAsync(c => c.Id == entity.ParentId.Value);
                entity.LevelCode = parent != null ? $"{parent.LevelCode}.{code}" : code;
            }
            if (!input.Id.HasValue)
            {
                await _menuRepository.InsertAsync(entity);
            }

        }

        //private async Task DeleteMenuPermissions(string permission)
        //{
        //    await _permissionRepository.DeleteAsync(c => c.Name.Contains(permission));
        //    await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.PermissionCache));
        //}


        private async Task ModifyMenuPermissions(Menu menu, string permission,
            FlatPermissionDto page, int sort, IReadOnlyCollection<DictionaryDto> types = null)
        {

            await _permissionRepository.DeleteAsync(c => c.Name.Contains(permission));
            var code = $"{page.LevelCode}.{Guid.NewGuid().ToString("D").Split('-')[0]}";
            var temp = new YtPermission()
            {
                DisplayName = $"{menu.DisplayName}权限",
                IsActive = true,
                IsStatic = false,
                Name = permission,
                ParentId = page.Id,
                PermissionType = PermissionType.Operation,
                LevelCode = code,
                Sort = sort
            };
            if (types != null && types.Any())
            {
                temp.Children = types.Where(c => c.IsCheck).Select((w, i) => new YtPermission()
                {
                    DisplayName = w.DisplayName,
                    IsActive = true,
                    LevelCode = $"{code}.{Guid.NewGuid().ToString("D").Split('-')[0]}",
                    Name = $"{permission}.{w.Name}",
                    PermissionType = PermissionType.Operation,
                    IsStatic = false,
                    Sort = i
                }).ToList();
            }
            await _permissionRepository.InsertAsync(temp);
            await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.PermissionCache));

        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        private async Task UpdateMenuAsync(MenuEditDto input)
        {
            if (input.Id != null)
            {
                var entity = await _menuRepository.GetAsync(input.Id.Value);
                input.MapTo(entity);
                await _menuRepository.UpdateAsync(entity);
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public async Task DeleteMenuAsync(EntityDto<int> input)
        {
            var model = _menuRepository.Get(input.Id);

            if (model.RequiresAuthentication)
            {
                var pers = model.RequiredPermissions;
                await _permissionRepository.DeleteAsync(c => c.Name.Contains(pers));
                await _rolePermissionRepository.DeleteAsync(c => c.Name.Contains(pers));
            }
            if (model.Childs.Any())
            {
                //  throw new AbpException("该菜单下有子菜单,不可删除");
                await _menuRepository.DeleteAsync(c => c.LevelCode.Contains(model.LevelCode));
            }
            else
            {
                await _menuRepository.DeleteAsync(input.Id);

            }
            await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.MenuCache));
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        public async Task BatchDeleteMenuAsync(List<int> input)
        {
            foreach (var i in input)
            {
                await DeleteMenuAsync(new EntityDto(i));
            }
            await _cachingAppService.ClearCache(new EntityDto<string>(CacheName.MenuCache));
        }
        #endregion
    }
}
