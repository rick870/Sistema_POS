using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        // El listado sigue la misma estructura de filtros
        Task<BaseEntityResponse<Sale>> ListSales(BaseFiltersRequest filters);

        // Nuevo método para obtener la venta con TODO su detalle
        Task<Sale?> GetSaleById(int saleId);
    }
}