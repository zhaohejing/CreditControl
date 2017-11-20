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

            mapper.CreateMap<Order, OrderExportDto>()
                .ForMember(dto => dto.Mobile, options => options.MapFrom(c => c.Customer.Mobile))
                .ForMember(dto => dto.Contact, options => options.MapFrom(c => c.Customer.Contact))
                .ForMember(dto => dto.CompanyName, options => options.MapFrom(c => c.Customer.CompanyName))

                .ForMember(dto => dto.Address, options => options.MapFrom(c => c.Form.Address))
                .ForMember(dto => dto.BrandsPerson, options => options.MapFrom(c => c.Form.BrandsPerson))
                .ForMember(dto => dto.Brands, options => options.MapFrom(c => c.Form.Brands))
                .ForMember(dto => dto.BrandsMobile, options => options.MapFrom(c => c.Form.BrandsMobile))
                .ForMember(dto => dto.FormName, options => options.MapFrom(c => c.Form.CompanyName))
                .ForMember(dto => dto.Industry, options => options.MapFrom(c => c.Form.Industry))
                .ForMember(dto => dto.LegalMobile, options => options.MapFrom(c => c.Form.LegalMobile))
                .ForMember(dto => dto.LegalPerson, options => options.MapFrom(c => c.Form.LegalPerson));
        }
    }
}