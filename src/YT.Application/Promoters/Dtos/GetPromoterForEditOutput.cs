                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using YT.Models;
namespace YT.Promoters.Dtos
{
	/// <summary>
    /// 用于添加或编辑 推广员管理时使用的DTO
    /// </summary>
  
    public class GetPromoterForEditOutput 
    {
 

	      /// <summary>
         /// Promoter编辑状态的DTO
        /// </summary>
    public PromoterEditDto Promoter{get;set;}


    }
}
