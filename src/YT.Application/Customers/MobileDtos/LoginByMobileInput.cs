using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Customers.MobileDtos
{
    /// <summary>
    /// 客户手机登录验证
    /// </summary>
  public  class LoginByMobileInput
    {
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
    }
}
