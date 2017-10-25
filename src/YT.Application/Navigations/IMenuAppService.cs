

// 项目展示地址:"http://www.ddxc.org/"
 // 如果你有什么好的建议或者觉得可以加什么功能，请加QQ群：104390185大家交流沟通
// 项目展示地址:"http://www.yoyocms.com/"
//博客地址：http://www.cnblogs.com/wer-ltm/
//代码生成器帮助文档：http://www.cnblogs.com/wer-ltm/p/5777190.html
// <Author-作者>角落的白板笔</Author-作者>
// Copyright © YoYoCms@中国.2017-08-09T16:04:16. All Rights Reserved.
//<生成时间>2017-08-09T16:04:16</生成时间>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Navigations.Dtos;

namespace YT.Navigations
{
	/// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuAppService : IApplicationService
    {
        #region 菜单管理

        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        Task<PagedResultDto<MenuListDto>> GetPagedMenusAsync(GetMenuInput input);

        /// <summary>
        /// 通过Id获取菜单信息进行编辑或修改 
        /// </summary>
        Task<GetMenuForEditOutput> GetMenuForEditAsync(NullableIdDto<int> input);

		  /// <summary>
        /// 通过指定id获取菜单ListDto信息
        /// </summary>
		Task<MenuListDto> GetMenuByIdAsync(EntityDto<int> input);

        /// <summary>
        /// 根据查询条件获取菜单分页列表
        /// </summary>
        Task<ListResultDto<MenuEditDto>> GetUserMenusAsync();

        /// <summary>
        /// 菜单移动
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Move(MoveMenuInput input);

        /// <summary>
        /// 新增或更改菜单
        /// </summary>
        Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input);





       

        /// <summary>
        /// 删除菜单
        /// </summary>
        Task DeleteMenuAsync(EntityDto<int> input);

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        Task BatchDeleteMenuAsync(List<int> input);

        #endregion




    }
}
