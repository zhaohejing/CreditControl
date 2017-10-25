using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Extensions;
using Abp.Runtime.Caching;
using Abp.WebApi.Controllers;
using Newtonsoft.Json;
using YT.WebApi.Models;

namespace YT.WebApi.Controllers
{
    /// <summary>
    /// 微信服务
    /// </summary>
    [AllowAnonymous]
    public class WeChatController : AbpApiController
    {
        private readonly ICacheManager _cacheManager;

        public WeChatController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }


        public async Task<string> GetWeChatAccessToken()
        {
            return await _cacheManager.GetCache(CacheName.WeChatToken)
               .GetAsync(CacheName.WeChatToken, () => GetAccessToken());
        }
        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task SendMessage(string content)
        {
            var token = await GetWeChatAccessToken();
            var userurl = $"https://api.weixin.qq.com/cgi-bin/user/get?access_token={token}&next_openid=";
            var request = HttpHelper.Get(userurl);
            if (request.IsNullOrWhiteSpace())
            {
                return;
            }
            var temp = JsonConvert.DeserializeObject<dynamic>(request);
            if (!temp || temp.errcode)
            {
                return;
            }

            var list = temp.data.openid as List<string>;
            var json = new { touser = list, msgtype = "text", text = new { content = content } };
            var url = $"https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={token}";
            HttpHelper.Post(url, JsonConvert.SerializeObject(json));

        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAccessToken()
        {
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx2a5d55f687f63dff&secret=cae60453b500c4ade00179fbbb941c07";
            return HttpHelper.Get(url);
        }


    }



}
