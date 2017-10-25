                           
 
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Cards.Dtos;
using YT.Dto;
namespace YT.Cards.Exporting
{
	/// <summary>
    /// 充值卡的数据导出功能 
    /// </summary>
    public interface ICardListExcelExporter
    {
        
        /// <summary>
        /// 导出充值卡到EXCEL文件
        /// <param name="cardListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportCardToFile(List<CardListDto> cardListDtos);



    }
}
