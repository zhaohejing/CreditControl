using System.Collections.Generic;
using YT.Authorization.Users.Dto;
using YT.Dto;

namespace YT.Authorization.Users.Exporting
{/// <summary>
/// 
/// </summary>
    public interface IUserListExcelExporter
    {/// <summary>
    /// �����û�������Ϣ
    /// </summary>
    /// <param name="userListDtos"></param>
    /// <returns></returns>
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}