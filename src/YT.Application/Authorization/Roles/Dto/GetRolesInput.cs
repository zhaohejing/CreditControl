using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// ��ȡ��ɫinput
/// </summary>
    public class GetRolesInput : PagedAndSortedInputDto, IShouldNormalize
    {/// <summary>
     /// Ȩ�޹�������
     /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
