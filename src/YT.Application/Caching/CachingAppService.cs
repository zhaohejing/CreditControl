using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using YT.Authorization;
using YT.Authorization.Permissions.Dto;
using YT.Authorizations;
using YT.Caching.Dto;
using YT.Navigations;
using YT.Navigations.Dtos;

namespace YT.Caching
{/// <summary>
 /// 缓存集中管理
 /// </summary>
    [DisableAuditing]

    public class CachingAppService : YtAppServiceBase, ICachingAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Menu, int> _menuRepository;
        private readonly IRepository<YtPermission> _permissionRepository;

      /// <summary>
      /// ctor
      /// </summary>
      /// <param name="cacheManager"></param>
      /// <param name="menuRepository"></param>
      /// <param name="permissionRepository"></param>
        public CachingAppService(
            ICacheManager cacheManager,
            IRepository<Menu, int> menuRepository,
            IRepository<YtPermission> permissionRepository)
        {
            _cacheManager = cacheManager;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
        }
        /// <summary>
        /// 获取所有的缓存
        /// </summary>
        public ListResultDto<CacheDto> GetAllCaches()
        {
            var caches = _cacheManager.GetAllCaches()
                                        .Select(cache => new CacheDto
                                        {
                                            Name = cache.Name
                                        })
                                        .ToList();

            return new ListResultDto<CacheDto>(caches);
        }
        /// <summary>
        /// 清除指定缓存
        /// </summary>
        public async Task ClearCache(EntityDto<string> input)
        {
            var cache = _cacheManager.GetCache(input.Id);
            await cache.ClearAsync();
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public async Task ClearAllCaches()
        {
            var caches = _cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                await cache.ClearAsync();
            }
        }
        /// <summary>
        /// 获取菜单缓存
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuEditDto>> GetMenuCache()
        {
            return await _cacheManager.GetCache(CacheName.MenuCache)
            .GetAsync(CacheName.MenuCache, () => GetMenusFromDb());
        }
        /// <summary>
        /// 获取权限缓存
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<FlatPermissionWithLevelDto>> GetPermissionCache()
        {
            return await _cacheManager.GetCache(CacheName.PermissionCache)
               .GetAsync(CacheName.PermissionCache,()=> GetPermissionsFromDb());
        }

        private async Task<ListResultDto<FlatPermissionWithLevelDto>> GetPermissionsFromDb()
        {
            var permissions = await _permissionRepository.GetAllListAsync();

            var rootPermissions = permissions.Where(p => p.Parent == null);
            var result = new List<FlatPermissionWithLevelDto>();
            foreach (var rootPermission in rootPermissions)
            {
                var level = 0;
                AddPermission(rootPermission, result, level);
            }
            return new ListResultDto<FlatPermissionWithLevelDto>
            {
                Items = result
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="result"></param>
        /// <param name="level"></param>
        private void AddPermission(YtPermission permission,
            List<FlatPermissionWithLevelDto> result, int level)
        {
            var flatPermission = permission.MapTo<FlatPermissionWithLevelDto>();
            flatPermission.Level = level;
            flatPermission.ParentName = permission.Parent?.DisplayName;
            result.Add(flatPermission);

            if (permission.Children == null)
            {
                return;
            }

            //  var children = allPermissions.Where(p => p.Parent != null && p.Parent.Name == permission.Name).ToList();

            foreach (var childPermission in permission.Children)
            {
                AddPermission(childPermission, result, level + 1);
            }
        }
        private async Task<List<MenuEditDto>> GetMenusFromDb()
        {
            var list= await _menuRepository.GetAllListAsync();
            return list.MapTo<List<MenuEditDto>>();
        }
    }
}