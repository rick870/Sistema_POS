using AutoMapper;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ClientMappingsProfile : Profile
    {
        public ClientMappingsProfile()
        {
            // Mapeo de la Entidad Client al DTO ClientResponseDto
            CreateMap<Client, ClientResponseDto>()
                .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
                 .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.DocumentType.Abbreviation))
                .ForMember(x => x.StateClient, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            // Mapeo para la respuesta paginada (BaseEntityResponse)
            // Esto es necesario para que el Mapper pueda convertir la lista completa de entidades a DTOs
            CreateMap<BaseEntityResponse<Client>, BaseEntityResponse<ClientResponseDto>>()
                .ReverseMap();

            CreateMap<ClientRequestDto, Client>();
        }
    }
}