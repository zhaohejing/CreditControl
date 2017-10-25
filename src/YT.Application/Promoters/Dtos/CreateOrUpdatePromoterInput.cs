                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.Promoters.Dtos
{
    /// <summary>
    /// 推广员管理新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdatePromoterInput  
    {
    /// <summary>
    /// 推广员管理编辑Dto
    /// </summary>
		public PromoterEditDto  PromoterEditDto {get;set;}
 
    }
}
