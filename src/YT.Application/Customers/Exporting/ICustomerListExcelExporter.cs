using System.Collections.Generic;
using YT.Customers.Dtos;
using YT.Dto;

namespace YT.Customers.Exporting
{
	/// <summary>
    /// 客户信息的数据导出功能 
    /// </summary>
    public interface ICustomerListExcelExporter
    {
        

        /// <summary>
        /// 导出客户信息到EXCEL文件
        /// <param name="customerListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportCustomerToFile(List<CustomerListDto> customerListDtos);



    }
}
