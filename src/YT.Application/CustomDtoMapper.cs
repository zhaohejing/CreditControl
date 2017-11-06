using AutoMapper;
using YT.Authorization.Users;
using YT.Authorization.Users.Dto;
using YT.Managers.Users;
using YT.Models;
using YT.Products.Dtos;

namespace YT
{
    /// <summary>
    /// dto”≥…‰≈‰÷√Œƒº˛
    /// </summary>
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();

        public static void CreateMappings(IMapperConfigurationExpression mapper)
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal(mapper);

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());

            mapper.CreateMap<Order, OrderListDto>()
                .ForMember(dto => dto.Mobile, options => options.MapFrom(c => c.Customer.Mobile))
                .ForMember(dto => dto.Contact, options => options.MapFrom(c => c.Customer.Contact))
                .ForMember(dto => dto.CompanyName, options => options.MapFrom(c => c.Customer.CompanyName));
        }
    }
}