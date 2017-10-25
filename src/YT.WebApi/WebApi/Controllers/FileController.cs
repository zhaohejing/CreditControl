using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Abp;
using Abp.Auditing;
using Abp.Web.Models;
using Abp.WebApi.Authorization;
using YT.Dto;

namespace YT.WebApi.Controllers
{
    public class FileController : YtApiControllerBase
    {
        private readonly IAppFolders _appFolders;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appFolders"></param>
        public FileController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        [AbpApiAuthorize]
        [DisableAuditing]
        public AjaxResponse DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFolder, file.FileToken);
            if (!File.Exists(filePath))
            {
                throw new AbpException("文件信息不存在");
            }
          //  var fileBytes = System.IO.File.ReadAllBytes(filePath);
          //  File.Delete(filePath);
            return new AjaxResponse(new { type = file.FileType, token = file.FileToken, name = file.FileName });
            // return File(fileBytes, file.FileType, file.FileName);
        }

        #region 上传文件
        /// <summary>
        /// 图片上传  
        /// </summary>
        /// <returns></returns>
        [AbpApiAuthorize]
        [DisableAuditing]
        [HttpPost]
        public async Task<AjaxResponse> ImageUpload()
        {
            var result = new List<object>();

            try
            {
                // 是否请求包含multipart/form-data。
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var provider = new MultipartFormDataStreamProvider(_appFolders.ImageFolder);
                // 阅读表格数据并返回一个异步任务.
                await Request.Content.ReadAsMultipartAsync(provider);
                // 如何上传文件到文件名.
                foreach (var file in provider.FileData)
                {
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小
                    const int maxSize = 1000000;
                    if (fileinfo.Length <= 0)
                    {
                        result.Add(new { code = -1, message = "请选择上传的文件" });
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        result.Add(new { code = -1, message = "上传文件大小超过限制" });
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        // 定义允许上传的文件扩展名
                        if (string.IsNullOrEmpty(fileExt) || Array.IndexOf("png,jpeg,jpg,gif,bmp".Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            result.Add(new { code = -1, message = "图片类型不正确" });
                        }
                        else
                        {
                            var ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            var pathName = $"{AbpSession.UserId}/{ymd}";
                            var temp = Path.Combine(_appFolders.ImageFolder, pathName);
                            if (!Directory.Exists(temp))
                            {
                                Directory.CreateDirectory(temp);
                            }
                            var fileName = Guid.NewGuid().ToString("N") + fileExt;
                            fileinfo.CopyTo(Path.Combine(temp, fileName), true);
                            var url = $"/Files/Images/{pathName}/{fileName}";
                            result.Add(new
                            {
                                code = 1,
                                url = url,
                                fileName=fileinfo.Name,
                                viewUrl=Request.RequestUri.Authority+ url,
                                size = fileinfo.Length,
                                suffix = fileExt
                            });
                        }
                    }
                    fileinfo.Delete();//删除原文件
                }
            }
            catch (Exception e)
            {
                result.Add( new { code = -1, message = e.Message });
            }
            return new AjaxResponse(result);
        }
        #endregion 上传文件

    }
}