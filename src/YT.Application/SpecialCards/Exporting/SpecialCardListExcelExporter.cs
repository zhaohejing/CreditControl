                                                                                                   

         
	using System;
    using System.Collections.Generic;
	using Abp.Runtime.Session;
	using Abp.Timing.Timezone;
	using YT.DataExporting.Excel.EpPlus;
	using YT.Dto;
	using YT.SpecialCards.Dtos;

namespace YT.SpecialCards.Exporting
{
    /// <summary>
    /// 唯鲜卡的导出EXCEL功能的实现
    /// </summary>
    public class SpecialCardListExcelExporter : EpPlusExcelExporterBase, ISpecialCardListExcelExporter
    {
     
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public SpecialCardListExcelExporter(ITimeZoneConverter timeZoneConverter,     IAbpSession abpSession)
        {
                       _timeZoneConverter = timeZoneConverter;    
                     _abpSession = abpSession;
        }

    

         /// <summary>
        /// 导出唯鲜卡到EXCEL文件
        /// <param name="specialCardListDtos">导出信息的DTO</param>
        /// </summary>
    public    FileDto ExportSpecialCardToFile(List<SpecialCardListDto> specialCardListDtos){


var file=CreateExcelPackage("specialCardList.xlsx",excelPackage=>{

var sheet=excelPackage.Workbook.Worksheets.Add(L("SpecialCard"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                          L("CardCode"),  
     L("Password"),  
     L("IsActive"),  
     L("IsUsed"),  
     L("CreationTime")
                        );
         AddObjects(sheet,2,specialCardListDtos,
            
      _ => _.CardCode,   
       
      _ => _.Password,   
       
      _ => _.IsActive,   
       
      _ => _.IsUsed,   
       
 _ =>_timeZoneConverter.Convert( _.CreationTime,_abpSession.TenantId, _abpSession.GetUserId())
);
                    //写个时间转换的吧
          //var creationTimeColumn = sheet.Column(10);
                    //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

        for (var i = 1; i <= 5; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }       

});
   return file;

}


 

 
  

    }
    }
