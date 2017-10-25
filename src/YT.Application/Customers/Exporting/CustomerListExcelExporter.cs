                                                                                                   

         
	using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using System.Threading.Tasks;
    using Abp;
    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.AutoMapper;
    using Abp.Configuration;
    using Abp.Domain.Repositories;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
	using Abp.Runtime.Session;
	using Abp.Timing.Timezone;
	using YT.Customers.Dtos;
	using YT.DataExporting.Excel.EpPlus;
	using YT.Dto;
    namespace YT.Customers.Exporting
{
    /// <summary>
    /// 客户表的导出EXCEL功能的实现
    /// </summary>
    public class CustomerListExcelExporter : EpPlusExcelExporterBase, ICustomerListExcelExporter
    {
     
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerListExcelExporter(ITimeZoneConverter timeZoneConverter,     IAbpSession abpSession)
        {
                       _timeZoneConverter = timeZoneConverter;    
                     _abpSession = abpSession;
        }

    

         /// <summary>
        /// 导出客户表到EXCEL文件
        /// <param name="customerListDtos">导出信息的DTO</param>
        /// </summary>
    public    FileDto ExportCustomerToFile(List<CustomerListDto> customerListDtos){


var file=CreateExcelPackage("customerList.xlsx",excelPackage=>{

var sheet=excelPackage.Workbook.Worksheets.Add(L("Customer"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                          L("Account"),  
     L("CustomerName"),  
     L("Mobile"),  
     L("BirthDay"),  
     L("Gender"),  
     L("IsActive"),  
     L("ProveId"),  
     L("CityId"),  
     L("AreaId"),  
     L("CreationTime")
                        );
         AddObjects(sheet,2,customerListDtos,
            
      _ => _.Account,   
       
      _ => _.CustomerName,   
       
      _ => _.Mobile,   
       
 _ =>_timeZoneConverter.Convert( _.BirthDay,_abpSession.TenantId, _abpSession.GetUserId()),          
      _ => _.Gender,   
      _ => _.IsActive,   
 _ =>_timeZoneConverter.Convert( _.CreationTime,_abpSession.TenantId, _abpSession.GetUserId())
);
                    //写个时间转换的吧
          //var creationTimeColumn = sheet.Column(10);
                    //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

        for (var i = 1; i <= 10; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }       

});
   return file;

}


 

 
  

    }
    }
