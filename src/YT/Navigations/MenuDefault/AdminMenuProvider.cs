using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using YT.Authorizations.PermissionDefault;

namespace YT.Navigations.MenuDefault
{
    public abstract class BaseMenuProvider : MenuProvider
    {

    }

    public class AdminMenuProvider : BaseMenuProvider
    {
        public override IEnumerable<MenuDefinition> GetMenuDefinitions(MenuDefinitionProviderContext context)
        {
            return new List<MenuDefinition>()
           {
                new MenuDefinition("控制台","/dashboard","speedometer",true,StaticPermissionsName.Page_Dashboard),
                new MenuDefinition("客户管理","","person-stalker",true,StaticPermissionsName.Page_Customer) {
                    Childs=new List<MenuDefinition>()
                    {
                        new MenuDefinition("用户信息","/customer/client","",true,StaticPermissionsName.Page_Customer_Client),
                        new MenuDefinition( "用户代充","/customer/charge","",true,StaticPermissionsName.Page_Customer_ForCharge),
                    }
                }
                ,
                   new MenuDefinition("充值卡管理","","card",true,StaticPermissionsName.Page_Card) {
                    Childs=   new List<MenuDefinition>()
                {
                    new MenuDefinition( "卡片管理","/card/charge","",true,StaticPermissionsName.Page_Card_Charge),
                    new MenuDefinition( "唯鲜卡管理","/card/only","",true,StaticPermissionsName.Page_Card_Only),
                }
                   },
                new MenuDefinition("推广管理","","person-stalker",true,StaticPermissionsName.Page_Generalize) {
                    Childs=new List<MenuDefinition>()
                {
                    new MenuDefinition( "推广员","/generalize/promoters","",true,StaticPermissionsName.Page_Generalize_Promoters),
                    new MenuDefinition( "消息群发","/generalize/wechat","",true,StaticPermissionsName.Page_Generalize_Wechat)
                }
                },
                 new MenuDefinition("权限管理","","person-stalker",true,StaticPermissionsName.Page_System) {
                     Childs=new List<MenuDefinition>()
                {
                    new MenuDefinition( "角色管理","/system/role","",true,StaticPermissionsName.Page_System_Role),
                    new MenuDefinition( "用户管理","/system/account","",true,StaticPermissionsName.Page_System_User),
                    new MenuDefinition( "菜单管理","/system/menu","",true,StaticPermissionsName.Page_System_Menu)
                }
                 },
                    new MenuDefinition("操作记录","","person-stalker",true,StaticPermissionsName.Page_Log) {
                        Childs=new List<MenuDefinition>()
                {
                    new MenuDefinition( "日志查看","/log/audit","",true,StaticPermissionsName.Page_Log_Audit)
                }
                    },
                   new MenuDefinition("报表管理","","person-stalker",true,StaticPermissionsName.Page_Statistics) {
                       Childs=new List<MenuDefinition>()
                {
                    new MenuDefinition( "销售明细表","/statics/a","",true,StaticPermissionsName.Page_Statistics_A),
                    new MenuDefinition( "销售汇总","/statics/b","",true,StaticPermissionsName.Page_Statistics_B),
                    new MenuDefinition( "顾客取货报表","/statics/c","",true,StaticPermissionsName.Page_Statistics_C),
                    new MenuDefinition( "顾客消费","/statics/d","",true,StaticPermissionsName.Page_Statistics_D),
                    new MenuDefinition( "商品销售数量","/statics/e","",true,StaticPermissionsName.Page_Statistics_E),
                    new MenuDefinition( "充值记录","/statics/f","",true,StaticPermissionsName.Page_Statistics_F),
                    new MenuDefinition( "订单管理","/statics/g","",true,StaticPermissionsName.Page_Statistics_G),
                    new MenuDefinition( "待补货记录","/statics/h","",true,StaticPermissionsName.Page_Statistics_H),
                }
                   }
           };
        }
    }
}
