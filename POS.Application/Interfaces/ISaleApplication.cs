using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface ISaleApplication
    {
        Task<BaseResponse<BaseEntityResponse<SaleResponseDto>>> ListSales(BaseFiltersRequest filters);

        Task<BaseResponse<SaleByIdResponseDto>> SaleById(int saleId);

        Task<BaseResponse<bool>> RegisterSale(SaleRequestDto requestDto);

        Task<BaseResponse<bool>> CancelSale(int saleId);
    }
}
