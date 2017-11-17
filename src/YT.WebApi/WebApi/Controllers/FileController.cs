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
                throw new AbpException("�ļ���Ϣ������");
            }
            return new AjaxResponse(new {type = file.FileType, token = file.FileToken, name = file.FileName});
        }

        #region �ϴ��ļ�

        /// <summary>
        /// ͼƬ�ϴ�  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> ImageUpload()
        {
            var result = new List<object>();
            try
            {
                // �Ƿ��������multipart/form-data��
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var provider = new MultipartFormDataStreamProvider(_appFolders.ImageFolder);
                // �Ķ�������ݲ�����һ���첽����.
                await Request.Content.ReadAsMultipartAsync(provider);
                // ����ϴ��ļ����ļ���.
                foreach (var file in provider.FileData)
                {
                    var orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    var fileinfo = new FileInfo(file.LocalFileName);
                    //����ļ���С
                    const int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        throw new UserFriendlyException("��ѡ���ϴ����ļ�");
                    }
                    if (fileinfo.Length > maxSize)
                    {
                        throw new UserFriendlyException("�ϴ��ļ���С��������");
                    }
                    string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                    // ���������ϴ����ļ���չ��
                    if (string.IsNullOrEmpty(fileExt) ||
                        Array.IndexOf("png,jpeg,jpg,gif,bmp".Split(','), fileExt.Substring(1).ToLower()) == -1)
                    {
                        throw new UserFriendlyException("ͼƬ���Ͳ���ȷ");
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
                    //�����ļ�
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
                    fileinfo.Delete(); //ɾ��ԭ�ļ�
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new AjaxResponse(result);
        }

        /// <summary>
        /// ͼƬ�ϴ�  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResponse> FileUpload()
        {
            var result = new List<object>();
            try
            {
                // �Ƿ��������multipart/form-data��
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var provider = new MultipartFormDataStreamProvider(_appFolders.ImageFolder);
                // �Ķ�������ݲ�����һ���첽����.
                await Request.Content.ReadAsMultipartAsync(provider);
                // ����ϴ��ļ����ļ���.
                foreach (var file in provider.FileData)
                {
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //����ļ���С
                    const int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        throw new UserFriendlyException("��ѡ���ϴ����ļ�");
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        throw new UserFriendlyException("�ϴ��ļ���С��������");
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
                        //�����ļ�
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
                    fileinfo.Delete(); //ɾ��ԭ�ļ�
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new AjaxResponse(result);
        }

        /// <summary>
        /// ɾ��ͼƬ  
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

        #endregion �ϴ��ļ�

        [HttpGet]
        public async Task<HttpResponseMessage> DownLoadFile(int orderId)
        {
            var order = await _ordeRepository.FirstOrDefaultAsync(orderId);
            if (order == null) throw new UserFriendlyException("����������");
            var form = order.Form;
            var product = await _productRepository.FirstOrDefaultAsync(order.ProductId);
            if (product == null) throw new UserFriendlyException("��Ʒ������");
            StringBuilder sb = new StringBuilder();
            sb.Append("��˾����,������ҵ,Ʒ������,���˴���,������ϵ��ʽ,Ʒ�Ƹ�����,Ʒ�Ƹ�������ϵ��ʽ,��ϵ��ַ,�ʱ�,��������\r\n");
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
                result.Content.Headers.ContentDisposition.FileName = "���ݵ���.csv";

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

    
}
}