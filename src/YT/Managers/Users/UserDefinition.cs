using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;

namespace YT.Managers.Users
{
    public class UserDefinition
    {
        public UserDefinition() { }

        public UserDefinition(string userName, string name, string passWord)
        {
            UserName = userName;
            Name = name;
            Password = passWord;
        }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
