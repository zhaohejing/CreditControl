                           
 
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YT.Dto;
using YT.Promoters.Dtos;

namespace YT.Promoters.Exporting
{
	/// <summary>
    /// 推广员管理的数据导出功能 
    /// </summary>
    public interface IPromoterListExcelExporter
    {
        
//## 可以将下面的这个实体类，作为filedto来进行接收 


    //public class FileDto
    //{
    //    [Required]
    //    public string FileName { get; set; }

    //    [Required]
    //    public string FileType { get; set; }

    //    [Required]
    //    public string FileToken { get; set; }

    //    public FileDto()
    //    {
            
    //    }

    //    public FileDto(string fileName, string fileType)
    //    {
    //        FileName = fileName;
    //        FileType = fileType;
    //        FileToken = Guid.NewGuid().ToString("N");
    //    }
    //}

        /// <summary>
        /// 导出推广员管理到EXCEL文件
        /// <param name="promoterListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportPromoterToFile(List<PromoterListDto> promoterListDtos);



    }
}
