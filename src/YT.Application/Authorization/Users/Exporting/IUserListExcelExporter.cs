using System.Collections.Generic;
using YT.Authorization.Users.Dto;
using YT.Dto;

namespace YT.Authorization.Users.Exporting
{/// <summary>
/// 
/// </summary>
    public interface IUserListExcelExporter
    {/// <summary>
    /// 到处用户连接信息
    /// </summary>
    /// <param name="userListDtos"></param>
    /// <returns></returns>
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}