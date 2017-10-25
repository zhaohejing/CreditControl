using System.Threading.Tasks;
using YT.Sessions.Dto;

namespace YT.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
