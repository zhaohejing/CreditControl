using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using YT.Navigations;

namespace YT.Tests.Authorization.Users
{
   public class Menu_Tests:AppTestBase
   {
       private readonly MenuManager _menuAppService;

       public Menu_Tests()
       {
           _menuAppService = Resolve<MenuManager>();
       }
        [Fact]
        public async Task Should_Get_UserMenus()
        {
         LoginAsDefaultTenantAdmin();
          var res=await  _menuAppService.GetCurrentUserMenu(1);
            res.Items.ShouldNotBeEmpty();
         
        }

    }
}
