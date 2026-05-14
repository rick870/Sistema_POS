using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ISaleDetailRepository : IGenericRepository<SaleDetail>
    {
        Task<BaseEntityResponse<SaleDetail>> GetSaleDetailBySaleId(int saleId);
    }
}
