                           
 
using System.Collections.Generic;
using YT.Dto;
using YT.SpecialCards.Dtos;

namespace YT.SpecialCards.Exporting
{
	/// <summary>
    /// 唯鲜卡的数据导出功能 
    /// </summary>
    public interface ISpecialCardListExcelExporter
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
        /// 导出唯鲜卡到EXCEL文件
        /// <param name="specialCardListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportSpecialCardToFile(List<SpecialCardListDto> specialCardListDtos);



    }
}
