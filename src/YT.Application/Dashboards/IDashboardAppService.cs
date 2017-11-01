using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Customers.Dtos;
using YT.Dashboards.Dtos;
using YT.Products.Dtos;

namespace YT.Dashboards
{
    /// <summary>
    /// 前段接口
    /// </summary>
   public interface IDashboardAppService:IApplicationService
    {
        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task<CustomerListDto> Authenticate(CustomerLoginDto input);

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        Task CreateOrder(CreateOrderInput input);
        /// <summary>
        /// 重置密码链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetPassword(ResetPasswordInput input);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <returns></returns>
        Task CompleteOrder(CompleteOrderInput input);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangePassword(ChangeCustomerPasswordInput input);
        /// <summary>
        /// 获取用户菜单集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<TreeModel>> GetCustomerMenus(EntityDto<int> input);
        /// <summary>
        /// 客户注册
        /// </summary>
        /// <returns></returns>
        Task Register(CustomerEditDto input);

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task<List<ProductDetail>> GetProducts(NullableIdDto<int> input);

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task<ProductDetail> GetProductDetail(EntityDto<int> input);
    }
}
