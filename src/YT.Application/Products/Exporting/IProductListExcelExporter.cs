using System.Collections.Generic;
using YT.Dto;
using YT.Products.Dtos;

namespace YT.Products.Exporting
{
	/// <summary>
    /// 产品的数据导出功能 
    /// </summary>
    public interface IProductListExcelExporter
    {
        /// <summary>
        /// 导出产品到EXCEL文件
        /// <param name="productListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportProductToFile(List<ProductListDto> productListDtos);

        /// <summary>
        /// 导出产品到EXCEL文件
        /// <param name="orders">导出信息的DTO</param>
        /// </summary>
        FileDto ExportOrderToFile(List<OrderProductDetail> orders);

    }
}
