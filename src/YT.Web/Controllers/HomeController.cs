using System.Web.Mvc;
using Abp.Auditing;

namespace YT.Web.Controllers
{
    [DisableAuditing]
    public class HomeController : AbpZeroTemplateControllerBase
    {
        public JsonResult Index()
        {
            Response.Redirect("/swagger");

            return Json(new {});
        }
    }
}