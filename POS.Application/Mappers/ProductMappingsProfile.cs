using AutoMapper;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                // Mapeamos el ID heredado de BaseEntity al ProductId del DTO
                .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))

                // Mapeamos los nombres de las relaciones
                .ForMember(x => x.CategoryName, x => x.MapFrom(y => y.Category.Name))
                .ForMember(x => x.ProviderName, x => x.MapFrom(y => y.Provider.Name))

                // Lógica para el estado (Activo/Inactivo)
                .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            // Mapeo para la respuesta paginada/listado base
            CreateMap<BaseEntityResponse<Product>, BaseEntityResponse<ProductResponseDto>>()
                .ReverseMap();

            // Mapeo para la creación/edición
            CreateMap<ProductRequestDto, Product>();
        }
    }
}