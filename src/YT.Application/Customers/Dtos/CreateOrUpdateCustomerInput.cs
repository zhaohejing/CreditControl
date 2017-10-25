                            
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Abp.Extensions;
using YT.Models;
namespace YT.Customers.Dtos
{
    /// <summary>
    /// 客户表新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateCustomerInput  
    {
    /// <summary>
    /// 客户表编辑Dto
    /// </summary>
		public CustomerEditDto  CustomerEditDto {get;set;}
 
    }
}
