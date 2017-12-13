using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using YT.DataExporting.Excel.EpPlus;
using YT.Dto;
using YT.Products.Dtos;

namespace YT.Products.Exporting
{
    /// <summary>
    /// 产品的导出EXCEL功能的实现
    /// </summary>
    public class ProductListExcelExporter : EpPlusExcelExporterBase, IProductListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        /// <summary>
        /// 导出产品到EXCEL文件
        /// <param name="orders">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportOrderToFile(List<OrderProductDetail> orders)
        {
            var file = CreateExcelPackage("订单详情.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add("订单");
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                    "商品名称",
                    "单价",
                    "数量",
                    "总价",
                    "订单号",
                    "下单时间",
                    "客户名称"
                );
                AddObjects(sheet, 2, orders,
                    _ => _.ProductName,
                    _ => _.Price,
                    _ => _.Count,
                    _ => _.TotalPrice,
                    _ => _.OrderNum,
                    _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),
                    _ => _.CustomerName

                );
                ////写个时间转换的吧
                //var creationTimeColumn = sheet.Column(6);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";
                for (var i = 1; i <= 7; i++)
                {
                    sheet.Column(i).AutoFit();
                }
            });
            return file;

        }

        /// <summary>
        /// 导出产品到EXCEL文件
        /// <param name="productListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportProductToFile(List<ProductListDto> productListDtos)
        {


            var file = CreateExcelPackage("productList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("Product"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                    L("Price"),
                    L("LevelTwo"),
                    L("Description"),
                    L("Content"),
                    L("IsActive"),
                    L("Profile"),
                    L("CreationTime")
                );
                AddObjects(sheet, 2, productListDtos,
                    _ => _.LevelOneName,

                    _ => _.Description,

                    _ => _.Content,

                    _ => _.IsActive,

                    _ => _.Profile,

                    _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
                );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 7; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }
    }
}
