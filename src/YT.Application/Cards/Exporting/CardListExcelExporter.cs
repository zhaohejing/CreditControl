using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using YT.Cards.Dtos;
using YT.DataExporting.Excel.EpPlus;
using YT.Dto;

namespace YT.Cards.Exporting
{
    /// <summary>
    /// 充值卡的导出EXCEL功能的实现
    /// </summary>
    public class CardListExcelExporter : EpPlusExcelExporterBase, ICardListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public CardListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出充值卡到EXCEL文件
        /// <param name="cardListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportCardToFile(List<CardListDto> cardListDtos)
        {


            var file = CreateExcelPackage("cardList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("Card"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                      L("CardCode"),
 L("Money"),
 L("IsUsed"),
 L("CreationTime")

                    );
                AddObjects(sheet, 2, cardListDtos,

             _ => _.CardCode,

             _ => _.Money,

             _ => _.IsUsed,

        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
       );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 4; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }







    }
}
