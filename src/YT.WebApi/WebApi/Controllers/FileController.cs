using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Authorization;
using Swashbuckle.Swagger;
using YT.Dto;
using YT.Models;
using YT.Storage;

namespace YT.WebApi.Controllers
{
    public class FileController : YtApiControllerBase
    {
        private readonly IAppFolders _appFolders;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _ordeRepository;
        public static string Host => ConfigurationManager.AppSettings.Get("WebSiteRootAddress");
        private readonly IBinaryObjectManager _binaryObjectManager;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="appFolders"></param>
        /// <param name="binaryObjectManager"></param>
        /// <param name="productRepository"></param>
        /// <param name="ordeRepository"></param>
        public FileController(IAppFolders appFolders, IBinaryObjectManager binaryObjectManager,
            IRepository<Order> ordeRepository, IRepository<Product> productRepository)
        {
            _appFolders = appFolders;
            _binaryObjectManager = binaryObjectManager;
            _ordeRepository = ordeRepository;
            _productRepository = productRepository;
        }

        public AjaxResponse DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFolder, file.FileToken);
            if (!File.Exists(filePath))
            {
                throw new AbpException("文件信息不存在");
            }
            return new AjaxResponse(new {type = file.FileType, token = file.FileToken, name = file.FileName});
        }

        #region 上传文件

        /// <summary>
        /// 图片上传  
        /// </summary>
        /// <returns></returns>
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
                    var orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    var fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小
                    const int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        throw new UserFriendlyException("请选择上传的文件");
                    }
                    if (fileinfo.Length > maxSize)
                    {
                        throw new UserFriendlyException("上传文件大小超过限制");
                    }
                    string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                    // 定义允许上传的文件扩展名
                    if (string.IsNullOrEmpty(fileExt) ||
                        Array.IndexOf("png,jpeg,jpg,gif,bmp".Split(','), fileExt.Substring(1).ToLower()) == -1)
                    {
                        throw new UserFriendlyException("图片类型不正确");
                    }
                    var ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    var p = AbpSession.UserId?.ToString() ?? "total";
                    var pathName = $"{p}/{ymd}";
                    var temp = Path.Combine(_appFolders.ImageFolder, pathName);
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    var guid = Guid.NewGuid();
                    var fileName = guid + fileExt;
                    fileinfo.CopyTo(Path.Combine(temp, fileName), true);
                    var url = $"/Files/Images/{pathName}/{fileName}";
                    //保存文件
                    await _binaryObjectManager.SaveAsync(new BinaryObject()
                    {
                        Id = guid,
                        Url = url,
                        Size = fileinfo.Length,
                        Suffix = fileExt,
                        Name = orfilename
                    });
                    result.Add(new
                    {
                        code = 1,
                        guid = guid,
                        viewUrl = Host + url,
                        size = fileinfo.Length,
                        suffix = fileExt,
                        name = orfilename
                    });
                    fileinfo.Delete(); //删除原文件
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new AjaxResponse(result);
        }

        /// <summary>
        /// 图片上传  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> FileUpload()
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
                    const int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        throw new UserFriendlyException("请选择上传的文件");
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        throw new UserFriendlyException("上传文件大小超过限制");
                    }
                    else
                    {
                        //ext
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //rename
                        var ymd = DateTime.Now.ToString("yyyyMMdd",
                            System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        var p = AbpSession.UserId?.ToString() ?? "total";
                        var pathName = $"{p}/{ymd}";
                        var temp = Path.Combine(_appFolders.ImageFolder, pathName);
                        if (!Directory.Exists(temp))
                        {
                            Directory.CreateDirectory(temp);
                        }
                        var guid = Guid.NewGuid();
                        var fileName = guid + fileExt;
                        fileinfo.CopyTo(Path.Combine(temp, fileName), true);
                        var url = $"/Files/Images/{pathName}/{fileName}";
                        //保存文件
                        await _binaryObjectManager.SaveAsync(new BinaryObject()
                        {
                            Id = guid,
                            Url = url,
                            Size = fileinfo.Length,
                            Suffix = fileExt,
                            Name = orfilename
                        });
                        //
                        result.Add(new
                        {
                            code = 1,
                            guid = guid,
                            viewUrl = Host + url,
                            size = fileinfo.Length,
                            suffix = fileExt,
                            name = orfilename
                        });
                    }
                    fileinfo.Delete(); //删除原文件
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new AjaxResponse(result);
        }

        /// <summary>
        /// 删除图片  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task DeleteFile(Guid guid)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(guid);
            if (file != null)
            {
                var filePath = System.Web.Hosting.HostingEnvironment.MapPath(file.Url);
                if (File.Exists(filePath))
                    if (filePath != null)
                    {
                        File.Delete(filePath);
                        await _binaryObjectManager.DeleteAsync(guid);
                    }

            }
        }

        #endregion 上传文件

        [HttpGet]
        public async Task<HttpResponseMessage> DownLoadFile(int orderId)
        {
            var order = await _ordeRepository.FirstOrDefaultAsync(orderId);
            if (order == null) throw new UserFriendlyException("订单不存在");
            var form = order.Form;
            var product = await _productRepository.FirstOrDefaultAsync(order.ProductId);
            if (product == null) throw new UserFriendlyException("商品不存在");
            StringBuilder sb = new StringBuilder();
            sb.Append("公司名称,所属行业,品牌名称,法人代表,法人联系方式,品牌负责人,品牌负责人联系方式,联系地址,邮编,订单类型\r\n");
            sb.Append(form.CompanyName+",");
            sb.Append(form.Industry + ",");
            sb.Append(form.Brands + ",");
            sb.Append(form.LegalPerson + ",");
            sb.Append(form.LegalMobile + ",");
            sb.Append(form.BrandsPerson + ",");
            sb.Append(form.BrandsMobile + ",");
            sb.Append(form.Address + ",");
            sb.Append(form.PostNum + ",");
            sb.Append(product.LevelOne?.Name + product.LevelTwo?.Name);
            try
            {

                string file = sb.ToString();

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(file,Encoding.Default);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "数据导出.csv";

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

    
}
}