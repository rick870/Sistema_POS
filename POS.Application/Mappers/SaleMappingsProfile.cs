using AutoMapper;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Mappers
{
    public class SaleMappingsProfile : Profile
    {
        public SaleMappingsProfile()
        {
            CreateMap<Sale, SaleResponseDto>()
                .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
                // Si el tipo de documento es nulo, devuelve string vacío en lugar de explotar
                .ForMember(x => x.VoucherDescription, x => x.MapFrom(y => y.VoucherDocumentType != null ? y.VoucherDocumentType.Description : "N/A"))
                // Si el cliente es nulo, devuelve "Sin Cliente"
                .ForMember(x => x.Client, x => x.MapFrom(y => y.Client != null ? y.Client.Name : "Sin Cliente"))
                .ForMember(x => x.DateOfSale, x => x.MapFrom(y => y.AuditCreateDate))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Sale>, BaseEntityResponse<SaleResponseDto>>()
                .ReverseMap();

            CreateMap<Sale, SaleByIdResponseDto>()
                .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.DateOfSale, x => x.MapFrom(y => y.AuditCreateDate))
                .ReverseMap();

            CreateMap<SaleDetail, SaleDetailByIdResponseDto>()
                 .ForMember(x => x.TotalAmount, x => x.MapFrom(y => y.Total))
                 // Acceso seguro: si Product es nulo, no rompe el código
                 .ForMember(x => x.Name, x => x.MapFrom(y => y.Product != null ? y.Product.Name : ""))
                 .ForMember(x => x.Code, x => x.MapFrom(y => y.Product != null ? y.Product.Code : ""))
                 .ForMember(x => x.Image, x => x.MapFrom(y => y.Product != null ? y.Product.Image : ""))
                 .ReverseMap();

            CreateMap<SaleRequestDto, Sale>();

            CreateMap<SaleDetailRequestDto, SaleDetail>();
        }
    }
}