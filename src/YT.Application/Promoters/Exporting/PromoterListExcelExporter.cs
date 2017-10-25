                                                                                                   

         
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
	using YT.DataExporting.Excel.EpPlus;
	using YT.Dto;
	using YT.Promoters.Dtos;

namespace YT.Promoters.Exporting
{
    /// <summary>
    /// 推广员管理的导出EXCEL功能的实现
    /// </summary>
    public class PromoterListExcelExporter : EpPlusExcelExporterBase, IPromoterListExcelExporter
    {
     
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public PromoterListExcelExporter(ITimeZoneConverter timeZoneConverter,     IAbpSession abpSession)
        {
                       _timeZoneConverter = timeZoneConverter;    
                     _abpSession = abpSession;
        }

    

         /// <summary>
        /// 导出推广员管理到EXCEL文件
        /// <param name="promoterListDtos">导出信息的DTO</param>
        /// </summary>
    public    FileDto ExportPromoterToFile(List<PromoterListDto> promoterListDtos){


var file=CreateExcelPackage("promoterList.xlsx",excelPackage=>{

var sheet=excelPackage.Workbook.Worksheets.Add(L("Promoter"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                          L("PromoterName"),  
     L("Mobile"),  
     L("OnlyCode"),  
     L("ShareUrl"),  
     L("IsActive"),  
     L("CreationTime") 
                        );
         AddObjects(sheet,2,promoterListDtos,
            
      _ => _.PromoterName,   
       
      _ => _.Mobile,   
       
      _ => _.OnlyCode,   
       
      _ => _.ShareUrl,   
       
      _ => _.IsActive,   
       
 _ =>_timeZoneConverter.Convert( _.CreationTime,_abpSession.TenantId, _abpSession.GetUserId()) 
);
                    //写个时间转换的吧
          //var creationTimeColumn = sheet.Column(10);
                    //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

        for (var i = 1; i <= 6; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }       

});
   return file;

}


 

 
  

    }
    }
