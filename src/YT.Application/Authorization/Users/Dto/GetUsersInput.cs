using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// ��ȡ��ɫinput
/// </summary>
    public class GetUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ȩ�޹���
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public int? Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }
}