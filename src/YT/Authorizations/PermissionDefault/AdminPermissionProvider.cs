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

               new PermissionDefinition(StaticPermissionsName.Page,"页面","鼻祖权限")
               {
                   Childs = new List<PermissionDefinition>()
                   {
                       new PermissionDefinition(StaticPermissionsName.Page_Dashboard,"控制台","整体概述",PermissionType.Control),
                       new PermissionDefinition(StaticPermissionsName.Page_Customer,"客户管理","客户管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Customer_Client,"客户信息","客户信息管理",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Customer_Client_Create,"创建客户","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Customer_Client_Edit,"编辑客户","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Customer_Client_Delete,"删除客户","",PermissionType.Control),
                                   }
                               },
                               new PermissionDefinition(StaticPermissionsName.Page_Customer_ForCharge,"客户代充","客户代充",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Customer_ForCharge_Charge,"客户代充值","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Customer_ForCharge_Clear,"清空余额","",PermissionType.Control),
                                   }
                               },
                           }
                       },
                          new PermissionDefinition(StaticPermissionsName.Page_Card,"卡片管理","卡片管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Card_Charge,"卡片管理","卡片管理",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Charge_Create,"创建卡片","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Charge_Edit,"编辑卡片","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Charge_Delete,"删除卡片","",PermissionType.Control),
                                   }
                               },
                               new PermissionDefinition(StaticPermissionsName.Page_Card_Only,"唯鲜卡管理","唯鲜卡管理",PermissionType.Control)
                               {
                                      Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Only_Create,"创建唯鲜卡","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Only_Delete,"编辑唯鲜卡","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Card_Only_Edit,"删除唯鲜卡","",PermissionType.Control),
                                   }
                               },
                           }
                       },
                            new PermissionDefinition(StaticPermissionsName.Page_Generalize,"推广管理","推广管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Generalize_Promoters,"推广员管理","推广员管理",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Generalize_Promoters_Create,"添加推广员","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Generalize_Promoters_Edit,"编辑推广员","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_Generalize_Promoters_Delete,"删除推广员","",PermissionType.Control),
                                   }
                               },
                               new PermissionDefinition(StaticPermissionsName.Page_Generalize_Wechat,"群发管理","群发管理",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Generalize_Wechat_Send,"发送消息","",PermissionType.Control)
                                   }
                               },
                           }
                       },
                              new PermissionDefinition(StaticPermissionsName.Page_System,"权限管理","权限管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_System_Role,"角色管理","角色管理",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_System_Role_Create,"创建角色","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_System_Role_Edit,"编辑角色","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_System_Role_Delete,"删除角色","",PermissionType.Control),
                                   }
                               },
                               new PermissionDefinition(StaticPermissionsName.Page_System_User,"账户管理","账户管理",PermissionType.Control)
                               {
                                     Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_System_User_Create,"创建账户","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_System_User_Delete,"编辑账户","",PermissionType.Control),
                                       new PermissionDefinition(StaticPermissionsName.Page_System_User_Edit,"删除账户","",PermissionType.Control),
                                   }
                               },
                               new PermissionDefinition(StaticPermissionsName.Page_System_Menu,"菜单管理","菜单管理",PermissionType.Control),
                           }
                       },
                       new PermissionDefinition(StaticPermissionsName.Page_Log,"日志管理","日志管理",PermissionType.Control)
                               
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Log_Audit,"日志查看","日志查看",PermissionType.Control)
                               {
                                   Childs = new List<PermissionDefinition>()
                                   {
                                       new PermissionDefinition(StaticPermissionsName.Page_Log_Audit_Export,"日志导出","",PermissionType.Control)
                                   }
                               },
                           }
                       },
                         new PermissionDefinition(StaticPermissionsName.Page_Statistics,"报表管理","报表管理",PermissionType.Control)
                       {
                           Childs = new List<PermissionDefinition>()
                           {
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_A,"Statistics_A","Statistics_A",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_B,"Statistics_B","Statistics_B",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_C,"Statistics_C","Statistics_C",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_D,"Statistics_D","Statistics_D",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_E,"Statistics_E","Statistics_E",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_F,"Statistics_F","Statistics_F",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_G,"Statistics_G","Statistics_G",PermissionType.Control),
                               new PermissionDefinition(StaticPermissionsName.Page_Statistics_H,"Statistics_H","Statistics_H",PermissionType.Control),
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

        public const string Page_Customer = "page.customer";
        public const string Page_Customer_Client = "page.customer.client";
        public const string Page_Customer_Client_Create = "page.customer.client.create";
        public const string Page_Customer_Client_Edit = "page.customer.client.edit";
        public const string Page_Customer_Client_Delete = "page.customer.delete";

        public const string Page_Customer_ForCharge = "page.customer.forcharge";
        public const string Page_Customer_ForCharge_Charge = "page.customer.forcharge.charge";
        public const string Page_Customer_ForCharge_Clear = "page.customer.forcharge.clear";

        public const string Page_Card = "page.card";
        public const string Page_Card_Charge = "page.card.charge";
        public const string Page_Card_Charge_Create = "page.card.charge.create";
        public const string Page_Card_Charge_Edit = "page.card.charge.edit";
        public const string Page_Card_Charge_Delete = "page.card.charge.delete";

        public const string Page_Card_Only = "page.card.only";
        public const string Page_Card_Only_Create = "page.card.only.create";
        public const string Page_Card_Only_Edit = "page.card.only.edit";
        public const string Page_Card_Only_Delete = "page.card.only.delete";

        public const string Page_Generalize = "page.generalize";
        public const string Page_Generalize_Promoters = "page.generalize.promoters";
        public const string Page_Generalize_Promoters_Create = "page.generalize.promoters.create";
        public const string Page_Generalize_Promoters_Edit = "page.generalize.promoters.edit";
        public const string Page_Generalize_Promoters_Delete = "page.generalize.promoters.delete";
        public const string Page_Generalize_Wechat = "page.generalize.wechat";
        public const string Page_Generalize_Wechat_Send = "page.generalize.wechat.send";

        public const string Page_System = "page.system";
        public const string Page_System_Role = "page.system.role";
        public const string Page_System_Role_Create = "page.system.role.create";
        public const string Page_System_Role_Edit = "page.system.role.edit";
        public const string Page_System_Role_Delete = "page.system.role.delete";
        public const string Page_System_User = "page.system.user";
        public const string Page_System_User_Create = "page.system.user.create";
        public const string Page_System_User_Edit = "page.system.user.edit";
        public const string Page_System_User_Delete = "page.system.user.delete";
        public const string Page_System_Menu = "page.system.menu";
        public const string Page_System_Menu_Create = "page.system.menu.create";
        public const string Page_System_Menu_Edit = "page.system.menu.edit";
        public const string Page_System_Menu_Delete = "page.system.menu.delete";

        public const string Page_Log = "page.log";
        public const string Page_Log_Audit = "page.log.audit";
        public const string Page_Log_Audit_Export = "page.log.audit.export";

        public const string Page_Statistics = "page.statistics";
        public const string Page_Statistics_A = "page.statistics.a";
        public const string Page_Statistics_B = "page.statistics.b";
        public const string Page_Statistics_C = "page.statistics.c";
        public const string Page_Statistics_D = "page.statistics.d";
        public const string Page_Statistics_E = "page.statistics.e";
        public const string Page_Statistics_F = "page.statistics.f";
        public const string Page_Statistics_G = "page.statistics.g";
        public const string Page_Statistics_H = "page.statistics.h";



    }
}
