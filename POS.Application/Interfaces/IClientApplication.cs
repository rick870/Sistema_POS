using POS.Application.Commons.Bases;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<BaseResponse<BaseEntityResponse<ClientResponseDto>>> ListClients(BaseFiltersRequest filters);

        Task<BaseResponse<ClientResponseDto>> ClientById(int clientId);

        Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto);

        Task<BaseResponse<bool>> EditClient(int clienttId, ClientRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveClient(int clientId);

    }
}
