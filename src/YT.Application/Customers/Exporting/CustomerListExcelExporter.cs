using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using YT.Customers.Dtos;
using YT.DataExporting.Excel.EpPlus;
using YT.Dto;
using YT.Models;

    namespace YT.Customers.Exporting
{
    /// <summary>
    /// 客户信息的导出EXCEL功能的实现
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
        /// 导出客户信息到EXCEL文件
        /// <param name="customerListDtos">导出信息的DTO</param>
        /// </summary>
    public    FileDto ExportCustomerToFile(List<CustomerListDto> customerListDtos){


var file=CreateExcelPackage("customerList.xlsx",excelPackage=>{

var sheet=excelPackage.Workbook.Worksheets.Add(L("Customer"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                          L("CompanyName"),  
     L("Provence"),  
     L("City"),  
     L("Address"),  
     L("Contact"),  
     L("Mobile"),  
     L("Email"),  
     L("Account"),  
     L("Password"),  
     L("License"),  
     L("FaceIdentityCard"),  
     L("BackIdentityCard"),  
     L("Balance"),  
     L("State"),  
     L("IsActive"),  
     L("CreationTime")
                        );
         AddObjects(sheet,2,customerListDtos,
            
      _ => _.CompanyName,   
       
      _ => _.Provence,   
       
      _ => _.City,   
       
      _ => _.Address,   
       
      _ => _.Contact,   
       
      _ => _.Mobile,   
       
      _ => _.Email,   
       
      _ => _.Account,   
      _ => _.Balance,   
      _ => _.State,   
       
      _ => _.IsActive,   
       
 _ =>_timeZoneConverter.Convert( _.CreationTime,_abpSession.TenantId, _abpSession.GetUserId())
);
                    //写个时间转换的吧
          //var creationTimeColumn = sheet.Column(10);
                    //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

        for (var i = 1; i <= 16; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }       

});
   return file;

}


 

 
  

    }
    }
