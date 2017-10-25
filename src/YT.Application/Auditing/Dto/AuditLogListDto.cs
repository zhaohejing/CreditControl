using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;
using Castle.Components.DictionaryAdapter;

namespace YT.Auditing.Dto
{
    /// <summary>
    /// ��־dto
    /// </summary>
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogListDto : EntityDto<long>
    {
        /// <summary>
        /// �û�id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? ImpersonatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomData { get; set; }
    }
    /// <summary>
    /// ��־����ת��
    /// </summary>
    public class ServiceMapperDto
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ServiceMapperDto() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="show"></param>
        public ServiceMapperDto(string name, string show)
        {
            Name = name;
            Show = show;
            Child=new EditableList<ServiceMapperDto>();
        }
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="name"></param>
       /// <param name="show"></param>
       /// <param name="child"></param>
        public ServiceMapperDto(string name, string show,List<ServiceMapperDto> child )
        {
            Name = name;
            Show = show;
            Child = child;
        }
        /// <summary>
        /// 
        /// </summary>
        public    List<ServiceMapperDto> Child { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        public string Show { get; set; }
    }
}