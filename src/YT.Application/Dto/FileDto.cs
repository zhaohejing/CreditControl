using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace YT.Dto
{/// <summary>
/// 
/// </summary>
    public class FileDto
    {/// <summary>
    /// 
    /// </summary>
        [Required]
        public string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string FileType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string FileToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileDto()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        public FileDto(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
        }
    }
}