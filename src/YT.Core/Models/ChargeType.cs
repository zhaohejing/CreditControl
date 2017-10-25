using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace YT.Models
{
    /// <summary>
    /// 充值类型
    /// </summary>
   public class ChargeType
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ChargeType() { }
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="id"></param>
       /// <param name="show"></param>
       /// <param name="truemoney"></param>
        public ChargeType(int id, int show, int truemoney)
       {
           Id = id;
            ShowMoney = show;
            TrueMoney = truemoney;

        }
        /// <summary>
        /// key
        /// </summary>
        public  int Id { get; set; }
        /// <summary>
        /// 显示金额
        /// </summary>
        public int ShowMoney { get; set; }
        /// <summary>
        /// 实际到账金额
        /// </summary>
        public int TrueMoney { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Content => $"充值{ShowMoney/100}元,实到{TrueMoney/100}元";
    }
}
