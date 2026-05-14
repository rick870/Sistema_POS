using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ClientApplication : IClientApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseResponse<BaseEntityResponse<ClientResponseDto>>> ListClients(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ClientResponseDto>>();
            var clients = await _unitOfWork.Client.ListClients(filters);

            if (clients is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<ClientResponseDto>>(clients);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }


        public async Task<BaseResponse<ClientResponseDto>> ClientById(int clientId)
        {
            var response = new BaseResponse<ClientResponseDto>();

            // Usamos GetEntityQuery para incluir DocumentType sin modificar el repositorio genérico
            var client = await _unitOfWork.Client
                .GetEntityQuery(x => x.Id == clientId && x.AuditDeleteDate == null)
                .AsNoTracking()
                .Include(x => x.DocumentType) // <--- Carga la navegación del tipo de documento
                .FirstOrDefaultAsync();

            if (client is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ClientResponseDto>(client);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var client = _mapper.Map<Client>(requestDto);

            response.Data = await _unitOfWork.Client.RegisterAsync(client);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditClient(int clientId, ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var clientById = await ClientById(clientId);

            if (clientById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var client = _mapper.Map<Client>(requestDto);
            client.Id = clientId;
            response.Data = await _unitOfWork.Client.EditAsync(client);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveClient(int clientId)
        {
            var response = new BaseResponse<bool>();
            var clientById = await ClientById(clientId);

            if (clientById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.Data = await _unitOfWork.Client.RemoveAsync(clientId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
