using AutoMapper;
using YT.Authorization.Users;
using YT.Authorization.Users.Dto;
using YT.Customers.Dtos;
using YT.Managers.Users;
using YT.Mobiles.Dtos;
using YT.Models;

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
            mapper.CreateMap<Customer, CustomerListDto>()
                .ForMember(dto => dto.PromoterName, options => options.MapFrom(c => c.Promoter.PromoterName));
            mapper.CreateMap<Order, OrderDto>()
              .ForMember(dto => dto.CustomerName, options => options.MapFrom(c => c.Customer.CustomerName));

        }
    }
}