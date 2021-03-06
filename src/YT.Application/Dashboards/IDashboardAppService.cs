﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.ChargeRecords.Dtos;
using YT.Customers.Dtos;
using YT.Dashboards.Dtos;
using YT.Dto;
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
        /// 获取客户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerListDto> GetInfo(EntityDto<int> input);

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        Task PayBackOrder(EntityDto<int> inputDto);
        /// <summary>
        /// 导出订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FileDto> ExportOrders(GetHaveProductInput input);
        /// <summary>
        /// 获取用户消费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CustomerCostListDto>> GetCustomerCosts(GetCustomerCostsInput input);

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrderProductDetail> GetHaveProduct(EntityDto<int> input);
        /// <summary>
        /// 提交充值申请
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task ApplyCharge(ApplyChargeInput input);

        /// <summary>
        /// 获取订单扩展信息
        /// </summary>
        /// <returns></returns>
          Task<CustomerFormEditDto> GetFormByOrder(EntityDto input);

        /// <summary>
        /// 创建订单扩展信息
        /// </summary>
        /// <returns></returns>
          Task ModifyForm(CustomerFormEditDto input);

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<OrderProductDetail>> GetHaveProducts(GetHaveProductInput input);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        Task<OrderListDto> CreateOrder(CreateOrderInput input);
        /// <summary>
        /// 重置密码链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetPassword(ResetPasswordInput input);

       
      //  Task CompleteOrder(CompleteOrderInput input);
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
        /// 用户modify
        /// </summary>
        /// <returns></returns>
        Task Modify(CustomerEditDto input);
        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProductDetail>> GetProducts(GetProductByFilter input);

        /// <summary>
        /// 获取所有产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
          Task<ProductDetail> GetProductDetail(GetProductByFilter input);
    }
}
