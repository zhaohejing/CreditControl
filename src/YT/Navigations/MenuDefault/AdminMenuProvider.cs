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
                new MenuDefinition("采购商品","","speedometer",true,StaticPermissionsName.Page_Procurement)
                {
                    Childs = new List<MenuDefinition>()
                    {
                        new MenuDefinition("商品管理","/products","",true,StaticPermissionsName.Page_Procurement_Products),
                        new MenuDefinition("订单管理","/orders","",true,StaticPermissionsName.Page_Procurement_Orders)
                    }
                },
               new MenuDefinition("用户管理","/customers","person-stalker",true,StaticPermissionsName.Page_Customers),
            
               new MenuDefinition("系列管理","/series","calendar",true,StaticPermissionsName.Page_Series),
               new MenuDefinition("公告管理","/public","calendar",true,StaticPermissionsName.Page_Public),

                new MenuDefinition("财务管理","","ios-calculator",true,StaticPermissionsName.Page_Finance)
                {
                    Childs = new List<MenuDefinition>()
                    {
                        new MenuDefinition("充值记录","/chargerecord","",true,StaticPermissionsName.Page_Finance_ChargeRecord),
                        new MenuDefinition("充值申请记录","/applyforcharge","",true,StaticPermissionsName.Page_Finance_ApplyforCharge),
                        new MenuDefinition("消费记录","/cost","",true,StaticPermissionsName.Page_Finance_Costs),
                    }
                },

                  new MenuDefinition("权限管理","","settings",true,StaticPermissionsName.Page_System)
                {
                    Childs = new List<MenuDefinition>()
                    {
                        new MenuDefinition("用户管理","/users","",true,StaticPermissionsName.Page_System_User),
                        new MenuDefinition("角色管理","/roles","",true,StaticPermissionsName.Page_System_Role),
                    }
                },
           };
        }
    }
}

