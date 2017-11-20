using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using YT.Authorization.Users.Dto;
using YT.Authorization.Users.Exporting;
using YT.DataExporting.Excel.EpPlus;
using YT.Dto;
using YT.Products.Dtos;

namespace YT.Products
{
 
    /// <summary>
    /// 
    /// </summary>
    public class OrderExcelExporter : EpPlusExcelExporterBase, IOrderExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="timeZoneConverter"></param>
        /// <param name="abpSession"></param>
        public OrderExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }
        /// <summary>
        /// 导出用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public FileDto ExportToFile(List<OrderExportDto> list)
        {
            return CreateExcelPackage(
                "订单信息.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add("订单信息");
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "订单号",
                        "总价",
                        "单价",
                        "数量",
                        "代理商名",
                        "代理商手机",
                        "联系人",
                        "状态",
                        "产品名",
                        "取消订单原因",
                        "客户名",
                        "行业",
                        "品牌",
                        "品牌负责人",
                        "品牌负责人手机",
                         "法人",
                        "法人手机",
                        "地址",
                        "订单时间"
                        );

                    AddObjects(
                        sheet, 2, list,
                        _ => _.OrderNum,
                        _ => _.TotalPrice,
                        _ => _.Price,
                        _ => _.Count,
                        _ => _.CompanyName,
                        _ => _.Mobile,
                        _ => _.Contact,
                        _ => _.State.HasValue?(_.State.Value?"已完成":"已取消"): "未完成",
                        _ => _.ProductName,
                        _ => _.CancelReason,

                        _ => _.FormName,
                        _ => _.Industry,
                        _ => _.Brands,
                        _ => _.BrandsPerson,
                        _ => _.BrandsMobile,
                        _ => _.LegalPerson,
                        _ => _.LegalMobile,
                        _ => _.Address,
                        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
                        );

                    //Formatting cells

                    var lastLoginTimeColumn = sheet.Column(19);
                    lastLoginTimeColumn.Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                    for (var i = 1; i <= 19; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
    /// <summary>
    /// 到处接口
    /// </summary>
    public interface IOrderExcelExporter
    {
        /// <summary>
        /// 到处用户连接信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        FileDto ExportToFile(List<OrderExportDto> list);
    }
}
