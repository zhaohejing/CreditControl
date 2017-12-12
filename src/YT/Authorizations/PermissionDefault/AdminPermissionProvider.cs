using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using YT.Navigations;

namespace YT.Authorizations.PermissionDefault
{
    public abstract class BasePermissionProvider : PermissionProvider
    {

    }
    public class AdminPermissionProvider : BasePermissionProvider
    {
        public override IEnumerable<PermissionDefinition> GetPermissionDefinitions(PermissionDefinitionProviderContext context)
        {
            return new List<PermissionDefinition>()
            {

                new PermissionDefinition(StaticPermissionsName.Page, "页面", "鼻祖权限")
                {
                    Childs = new List<PermissionDefinition>()
                    {
                        new PermissionDefinition(StaticPermissionsName.Page_Dashboard, "控制台", "整体概述",
                            PermissionType.Control),
                        new PermissionDefinition(StaticPermissionsName.Page_Procurement, "采购商品", "采购商品",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_Procurement_Products, "商品管理", "商品管理",
                                    PermissionType.Control)
                                {
                                    Childs = new List<PermissionDefinition>()
                                    {
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Procurement_Products_Create, "创建商品", "",
                                            PermissionType.Control),
                                        new PermissionDefinition(StaticPermissionsName.Page_Procurement_Products_Edit,
                                            "编辑商品", "", PermissionType.Control),
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Procurement_Products_Delete, "删除商品", "",
                                            PermissionType.Control),
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Procurement_Products_Export, "导出商品", "",
                                            PermissionType.Control),
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Procurement_Products_Pricing, "商品定价", "",
                                            PermissionType.Control),
                                    }
                                },
                                new PermissionDefinition(StaticPermissionsName.Page_Procurement_Orders, "订单管理", "订单管理",
                                    PermissionType.Control)
                            }
                        },
                        new PermissionDefinition(StaticPermissionsName.Page_Customers, "用户管理", "用户管理",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_Customers_Delete, "删除客户", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Customers_Apply, "审核客户", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Customers_Charge, "客户重置", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Customers_Reset, "重置密码", "",
                                    PermissionType.Control),
                            }
                        },

                        new PermissionDefinition(StaticPermissionsName.Page_Public, "公告管理", "公告管理",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_Public_Delete, "删除公告", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Public_Create, "添加公告", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Public_Edit, "修改公告", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Public_Public, "发布公告", "",
                                    PermissionType.Control),
                            }
                        },

                        new PermissionDefinition(StaticPermissionsName.Page_Series, "系列管理", "系列管理",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_Series_Create, "添加系列", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Series_Edit, "编辑系列", "",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Series_Delete, "删除系列", "",
                                    PermissionType.Control),
                            }
                        },

                        new PermissionDefinition(StaticPermissionsName.Page_Finance, "财务管理", "财务管理",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_Finance_ApplyforCharge, "充值申请记录",
                                    "充值申请记录", PermissionType.Control)
                                {
                                    Childs = new List<PermissionDefinition>()
                                    {
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Finance_ApplyforCharge_Charge, "充值", "",
                                            PermissionType.Control),
                                        new PermissionDefinition(
                                            StaticPermissionsName.Page_Finance_ApplyforCharge_Delete, "删除", "",
                                            PermissionType.Control),
                                    }
                                },
                                new PermissionDefinition(StaticPermissionsName.Page_Finance_ChargeRecord, "充值记录", "充值记录",
                                    PermissionType.Control),
                                new PermissionDefinition(StaticPermissionsName.Page_Finance_Costs, "消费记录", "消费记录",
                                    PermissionType.Control)
                            }
                        },

                        new PermissionDefinition(StaticPermissionsName.Page_System, "权限管理", "权限管理",
                            PermissionType.Control)
                        {
                            Childs = new List<PermissionDefinition>()
                            {
                                new PermissionDefinition(StaticPermissionsName.Page_System_Role, "角色管理", "角色管理",
                                    PermissionType.Control)
                                {
                                    Childs = new List<PermissionDefinition>()
                                    {
                                        new PermissionDefinition(StaticPermissionsName.Page_System_Role_Create, "创建角色",
                                            "", PermissionType.Control),
                                        new PermissionDefinition(StaticPermissionsName.Page_System_Role_Edit, "编辑角色", "",
                                            PermissionType.Control),
                                        new PermissionDefinition(StaticPermissionsName.Page_System_Role_Delete, "删除角色",
                                            "", PermissionType.Control),
                                    }
                                },
                                new PermissionDefinition(StaticPermissionsName.Page_System_User, "账户管理", "账户管理",
                                    PermissionType.Control)
                                {
                                    Childs = new List<PermissionDefinition>()
                                    {
                                        new PermissionDefinition(StaticPermissionsName.Page_System_User_Create, "创建账户",
                                            "", PermissionType.Control),
                                        new PermissionDefinition(StaticPermissionsName.Page_System_User_Delete, "编辑账户",
                                            "", PermissionType.Control),
                                        new PermissionDefinition(StaticPermissionsName.Page_System_User_Edit, "删除账户", "",
                                            PermissionType.Control),
                                    }
                                }
                            }
                        }

                    }
                }
            };
        }
    }
    /// <summary>
    /// 静态权限名
    /// </summary>
    public class StaticPermissionsName
    {
        //默认首页权限
        public const string Page = "page";

        public const string Page_Dashboard = "page.dashboard";

        /// <summary>
        /// 采购管理
        /// </summary>
        public const string Page_Procurement = "page.procurement";

        public const string Page_Procurement_Products = "page.procurement.products";
        public const string Page_Procurement_Products_Create = "page.procurement.products.create";
        public const string Page_Procurement_Products_Edit = "page.procurement.products.edit";
        public const string Page_Procurement_Products_Delete = "page.procurement.products.delete";
        public const string Page_Procurement_Products_Export = "page.procurement.products.export";
        public const string Page_Procurement_Products_Pricing = "page.procurement.products.pricing";
        
        /// <summary>
        /// 订单权限
        /// </summary>
        public const string Page_Procurement_Orders = "page.procurement.orders";

        /// <summary>
        /// 客户管理
        /// </summary>
        public const string Page_Customers = "page.customers";
        public const string Page_Customers_Charge = "page.customers.charge";
        public const string Page_Customers_Apply = "page.customers.apply";
        public const string Page_Customers_Delete = "page.customers.delete";
        public const string Page_Customers_Reset = "page.customers.reset";

        /// <summary>
        /// 系列管理
        /// </summary>
        public const string Page_Series = "page.series";
        public const string Page_Series_Create = "page.series.create";
        public const string Page_Series_Edit = "page.series.edit";
        public const string Page_Series_Delete = "page.series.delete";

        public const string Page_Public = "page.public";
        public const string Page_Public_Create = "page.public.create";
        public const string Page_Public_Edit = "page.public.edit";
        public const string Page_Public_Delete = "page.public.delete";
        public const string Page_Public_Public = "page.public.public";

        /// <summary>
        /// 财务管理
        /// </summary>
        public const string Page_Finance = "page.finance";
        public const string Page_Finance_ChargeRecord = "page.finance.chargerecord";
        public const string Page_Finance_ApplyforCharge = "page.finance.applyforcharge";
        public const string Page_Finance_ApplyforCharge_Delete = "page.finance.applyforcharge.delete";
        public const string Page_Finance_ApplyforCharge_Charge = "page.finance.applyforcharge.charge";
        public const string Page_Finance_Costs = "page.finance.costs";


        /// <summary>
        /// xitong
        /// </summary>
        public const string Page_System = "page.system";
        public const string Page_System_Role = "page.system.role";
        public const string Page_System_Role_Create = "page.system.role.create";
        public const string Page_System_Role_Edit = "page.system.role.edit";
        public const string Page_System_Role_Delete = "page.system.role.delete";
        public const string Page_System_User = "page.system.user";
        public const string Page_System_User_Create = "page.system.user.create";
        public const string Page_System_User_Edit = "page.system.user.edit";
        public const string Page_System_User_Delete = "page.system.user.delete";

    }
}
